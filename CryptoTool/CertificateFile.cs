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
            string ness = key.IsPrivate ? "Private" : "Public";
            if(key is DsaKeyParameters dsa)
            {
                return $"{dsa.Parameters.P.BitLength} bit DSA {ness} key";
            }
            else if (key is ECKeyParameters ec)
            {
                var curve = Org.BouncyCastle.Crypto.EC.CustomNamedCurves.GetName(ec.PublicKeyParamSet);
                return $"{curve} ECDSA {ness} key";
            }
            else if (key is RsaKeyParameters rsa)
            {
                return $"{rsa.Modulus.BitLength} bit RSA {ness} key";
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
    }
}