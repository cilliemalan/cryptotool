using System;
using System.IO;
using System.Windows.Forms;
using Org.BouncyCastle;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using System.Linq;
using System.Text;

namespace CryptoTool
{
    public class CertificateFile : ListViewItem
    {
        public CertificateFileType Type { get; private set; }

        public string Filename
        {
            get
            {
                return Text;
            }
            private set
            {
                Name = value;
                Text = value;
            }
        }

        public string FullPath { get; private set; }
        public object PemObject { get; private set; }
        public string Info
        {
            get { return SubItems["Info"].Text; }
            private set { SubItems["Info"].Text = value; }
        }
        public AsymmetricKeyParameter PublicKey { get; private set; }

        private CertificateFile()
        {
            SubItems.Add(new ListViewSubItem() { Name = "Info" });
        }

        public void Reload()
        {
            try
            {
                Info = null;
                PemObject = null;
                Type = CertificateFileType.Unknown;

                using (var file = File.OpenText(FullPath))
                {
                    var rdr = new PemReader(file);
                    PemObject = rdr.ReadObject();

                    UpdateMetadata();
                }
            }
            catch { Type = CertificateFileType.Unknown; }
        }

        private string GetInfo(AsymmetricKeyParameter key)
        {
            string ness = key.IsPrivate ? "private key" : "public key";
            if (key is DsaKeyParameters dsa)
            {
                return $"{dsa.Parameters.P.BitLength} bit DSA {ness}";
            }
            else if (key is ECKeyParameters ec)
            {
                var curve = Org.BouncyCastle.Crypto.EC.CustomNamedCurves.GetName(ec.PublicKeyParamSet);
                return $"{curve} ECDSA {ness}";
            }
            else if (key is RsaKeyParameters rsa)
            {
                return $"{rsa.Modulus.BitLength} bit RSA {ness}";
            }

            return null;
        }

        private void UpdateMetadata()
        {
            if (PemObject != null)
            {
                if (PemObject is AsymmetricKeyParameter key)
                {
                    Info = GetInfo(key);
                    if (key.IsPrivate)
                    {
                        Type = CertificateFileType.PublicKey;
                    }
                    else
                    {
                        Type = CertificateFileType.PublicKey;
                        PublicKey = key;
                    }
                }
                else if (PemObject is AsymmetricCipherKeyPair kp)
                {
                    Type = CertificateFileType.PrivateKey;
                    PublicKey = kp.Public;
                    Info = GetInfo(kp.Private);
                }
                else if (PemObject is X509Certificate cer)
                {
                    Type = CertificateFileType.Certificate;
                    Info = cer.SubjectDN.GetValueList(X509ObjectIdentifiers.CommonName)?.Cast<string>().FirstOrDefault();
                    PublicKey = cer.GetPublicKey();
                }
                else if (PemObject is Pkcs10CertificationRequest csr)
                {
                    Type = CertificateFileType.Csr;
                    var csrinfo = csr.GetCertificationRequestInfo();
                    Info = csrinfo.Subject.GetValueList(X509ObjectIdentifiers.CommonName)?.Cast<string>().FirstOrDefault();
                    PublicKey = csr.GetPublicKey();
                }
            }
        }

        public static CertificateFile FromFile(string folderPath, string filePath)
        {
            var certFile = new CertificateFile
            {
                Filename = filePath.Substring(folderPath.Length + 1),
                FullPath = filePath
            };
            certFile.Reload();

            return certFile;
        }

        public string GetDetail()
        {
            if (PemObject == null)
            {
                return "Unsupported file";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                GetDetailInternal(PemObject, sb);

                return sb.ToString();
            }
        }

        private void GetDetailInternal(object obj, StringBuilder sb, string prefix = null)
        {
            Action<string> appendLine = prefix == null
                ? new Action<string>(s => sb.AppendLine(s))
                : (s) => sb.AppendLine($"{prefix}{s}");

            if (obj is AsymmetricKeyParameter key)
            {
                appendLine($"Type: {GetInfo(key)}");
                if (key is ECKeyParameters ecpk)
                {
                    appendLine($"  AlgorithmName: {ecpk.AlgorithmName}");
                    if (key is ECPrivateKeyParameters ecp)
                    {
                        appendLine($"  D: {ecp.D}");
                    }
                    if (key is ECPublicKeyParameters ecu)
                    {
                        appendLine($"  Q: {ecu.Q}");
                    }
                    appendLine($"  Parameters:");
                    appendLine($"    G: {ecpk.Parameters.G}");
                    appendLine($"    H: {ecpk.Parameters.H}");
                    appendLine($"    N: {ecpk.Parameters.N}");
                    appendLine($"    Curve:");
                    appendLine($"      A: {ecpk.Parameters.Curve.A}");
                    appendLine($"      B: {ecpk.Parameters.Curve.B}");
                    appendLine($"      Co factor: {ecpk.Parameters.Curve.Cofactor}");
                    appendLine($"      Coordinate System: {ecpk.Parameters.Curve.CoordinateSystem}");
                    appendLine($"      Inf: {ecpk.Parameters.Curve.Infinity}");
                    appendLine($"      Order: {ecpk.Parameters.Curve.Order}");
                    appendLine($"      Field:");
                    appendLine($"        Characteristic: {ecpk.Parameters.Curve.Field.Characteristic}");
                    appendLine($"        Dim: {ecpk.Parameters.Curve.Field.Dimension}");
                    appendLine($"        Size: {ecpk.Parameters.Curve.FieldSize}");
                }
                if (key is RsaKeyParameters rsakp)
                {
                    appendLine($"  Modulus: {rsakp.Modulus}");
                    appendLine($"  Exponent: {rsakp.Exponent}");
                }
                if(key is DsaKeyParameters dsakp)
                {
                    if (key is DsaPrivateKeyParameters dsap)
                    {
                        appendLine($"  X: {dsap.X}");
                    }
                    if (key is DsaPublicKeyParameters dsau)
                    {
                        appendLine($"  Y: {dsau.Y}");
                    }
                    appendLine($"  Parameters:");
                    appendLine($"    G: {dsakp.Parameters.G}");
                    appendLine($"    P: {dsakp.Parameters.P}");
                    appendLine($"    Q: {dsakp.Parameters.Q}");
                }
            }
            else if (obj is AsymmetricCipherKeyPair kp)
            {
                appendLine($"Type: Key pair");
                appendLine("  Public key:");
                GetDetailInternal(kp.Public, sb, "    ");
                appendLine("  Private key:");
                GetDetailInternal(kp.Private, sb, "    ");
            }
            else if (obj is Pkcs10CertificationRequest csr)
            {
                appendLine($"Type: Certificate Signing Request");
                appendLine($"  Signature Algorithm: {csr.SignatureAlgorithm.Algorithm}");
                appendLine($"  Signature: {csr.Signature}");
            }
            //else if (PemObject is X509Certificate cer)
            //{
            //    Type = CertificateFileType.Certificate;
            //    Info = cer.SubjectDN.GetValueList(X509ObjectIdentifiers.CommonName)?.Cast<string>().FirstOrDefault();
            //    PublicKey = cer.GetPublicKey();
            //}
            //else if (PemObject is Pkcs10CertificationRequest csr)
            //{
            //    Type = CertificateFileType.Csr;
            //    var csrinfo = csr.GetCertificationRequestInfo();
            //    Info = csrinfo.Subject.GetValueList(X509ObjectIdentifiers.CommonName)?.Cast<string>().FirstOrDefault();
            //    PublicKey = csr.GetPublicKey();
            //}
            else
            {
                appendLine("Unsupported object");
                appendLine(obj.ToString());
            }
        }
    }
}