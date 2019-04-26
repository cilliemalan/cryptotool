using Microsoft.WindowsAPICodePack.Dialogs;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.EC;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoTool
{
    public partial class MainForm : Form
    {
        FileSystemWatcher watcher;
        bool loaded;

        private string CurrentWorkingDirectory
        {
            get { return txtWorkingDir.Text; }
            set { txtWorkingDir.Text = value; }
        }

        private IniFile IniFile { get; } = new IniFile(PathUtils.GetSettingsFilePath());

        private CertificateFile SelectedItem => lstFiles.SelectedItems.Cast<CertificateFile>().FirstOrDefault();

        private static Dictionary<int, string> keyUsageDictionary = new Dictionary<int, string>
        {
            [KeyUsage.CrlSign] = "CRL Signing",
            [KeyUsage.DataEncipherment] = "Data Encipherment",
            [KeyUsage.DecipherOnly] = "Decipherment Only",
            [KeyUsage.DigitalSignature] = "Digital Signature",
            [KeyUsage.EncipherOnly] = "Encipherment Only",
            [KeyUsage.KeyAgreement] = "Key Agreement",
            [KeyUsage.KeyCertSign] = "Key Certificate Signature",
            [KeyUsage.KeyEncipherment] = "Key Encipherment",
            [KeyUsage.NonRepudiation] = "Non Repudiation",
        };
        string[] defaultKeyUsage = new string[] { "Digital Signature", "Non Repudiation", "Key Encipherment" };

        private static Dictionary<KeyPurposeID, string> extKeyUsageDictionary = new Dictionary<KeyPurposeID, string>
        {
            [KeyPurposeID.AnyExtendedKeyUsage] = "Any Extended Key Usage",
            [KeyPurposeID.IdKPClientAuth] = "Client Authentication",
            [KeyPurposeID.IdKPCodeSigning] = "Code Signing",
            [KeyPurposeID.IdKPEmailProtection] = "Email Protection",
            [KeyPurposeID.IdKPIpsecEndSystem] = "IPSec End System",
            [KeyPurposeID.IdKPIpsecTunnel] = "IPSec Tunnel",
            [KeyPurposeID.IdKPIpsecUser] = "IPSec User",
            [KeyPurposeID.IdKPMacAddress] = "Mac Address",
            [KeyPurposeID.IdKPOcspSigning] = "OCSP Signing",
            [KeyPurposeID.IdKPServerAuth] = "Server Authentication",
            [KeyPurposeID.IdKPSmartCardLogon] = "SmartCard Logon",
            [KeyPurposeID.IdKPTimeStamping] = "Time Stamping",
        };
        string[] defaultExtKeyUsage = new string[] { "Server Authentication" };

        public MainForm()
        {
            InitializeComponent();
            lstFiles.Groups.Add("Unknown", "Unknown");
            lstFiles.Groups.Add("Certificate", "Certificate");
            lstFiles.Groups.Add("Csr", "Certificate Signing Request");
            lstFiles.Groups.Add("PublicKey", "Public Key");
            lstFiles.Groups.Add("PrivateKey", "Private Key");
            lstFiles.Groups.Add("Multiple", "Multiple");

            rdCsrKeyDsa_CheckedChanged(this, EventArgs.Empty);

            cbCsrUsage.Items.Clear();
            cbCsrUsage.Items.AddRange(keyUsageDictionary.Values.ToArray());
            foreach (var ku in defaultKeyUsage) cbCsrUsage.SetItemChecked(cbCsrUsage.Items.IndexOf(ku), true);
            cbCsrExtendedUsage.Items.Clear();
            cbCsrExtendedUsage.Items.AddRange(extKeyUsageDictionary.Values.ToArray());
            foreach (var ku in defaultExtKeyUsage) cbCsrExtendedUsage.SetItemChecked(cbCsrExtendedUsage.Items.IndexOf(ku), true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var workingdir = IniFile.Read("WorkingDir");
            if (string.IsNullOrEmpty(workingdir))
            {
                workingdir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.DoNotVerify), "CertTool");
                IniFile.Write("WorkingDir", workingdir);
            }
            CurrentWorkingDirectory = workingdir;
            if (!Directory.Exists(CurrentWorkingDirectory)) Directory.CreateDirectory(CurrentWorkingDirectory);
            SetupWatcher();

            txtCsrSubjO.Text = IniFile.Read("Organization", "Subject");
            txtCsrSubjOU.Text = IniFile.Read("OrganizationUnit", "Subject");
            txtCsrSubjC.Text = IniFile.Read("Country", "Subject");
            txtCsrSubjST.Text = IniFile.Read("State", "Subject");
            txtCsrSubjL.Text = IniFile.Read("Locality", "Subject");
            txtCsrSubjEmail.Text = IniFile.Read("Email", "Subject");

            loaded = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fd = new CommonOpenFileDialog())
            {
                fd.IsFolderPicker = true;
                if (!string.IsNullOrEmpty(CurrentWorkingDirectory) && Directory.Exists(CurrentWorkingDirectory))
                {
                    fd.InitialDirectory = CurrentWorkingDirectory;
                }
                else
                {
                    fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }

                if (fd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    CurrentWorkingDirectory = fd.FileName;
                }
            }
        }

        private void Subject_Changed(object sender, EventArgs e)
        {
            if (loaded)
            {
                IniFile.Write("Organization", txtCsrSubjO.Text, "Subject");
                IniFile.Write("OrganizationUnit", txtCsrSubjOU.Text, "Subject");
                IniFile.Write("Country", txtCsrSubjC.Text, "Subject");
                IniFile.Write("State", txtCsrSubjST.Text, "Subject");
                IniFile.Write("Locality", txtCsrSubjL.Text, "Subject");
                IniFile.Write("Email", txtCsrSubjEmail.Text, "Subject");
            }
        }

        private void SetupWatcher()
        {
            var fullPath = CurrentWorkingDirectory == null ? null : PathUtils.GetFullPath(CurrentWorkingDirectory);

            if (fullPath == null || watcher == null || !string.Equals(watcher.Path, fullPath))
            {
                watcher?.Dispose();

                if (fullPath != null && Directory.Exists(fullPath))
                {
                    watcher = new FileSystemWatcher(fullPath);
                    watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                    watcher.Filter = "*.*";
                    watcher.Changed += File_Changed;
                    watcher.Created += File_Changed;
                    watcher.Deleted += File_Changed;
                    watcher.Renamed += File_Changed;

                    lstFiles.Items.Clear();
                    foreach (var item in new DirectoryInfo(fullPath)
                        .GetFiles("*.*", SearchOption.AllDirectories)
                        .Select(x => CertificateFile.FromFile(fullPath, x.FullName)))
                    {
                        AddItem(item);
                    }

                    UpdateCaList();
                    UpdateCsrToSignList();
                    watcher.EnableRaisingEvents = true;
                }
            }
        }

        private void File_Changed(object sender, FileSystemEventArgs e)
        {
            Invoke(new Action(() =>
            {
                bool mutated = false;
                if ((e.ChangeType & WatcherChangeTypes.Created) != 0)
                {
                    if (File.Exists(e.FullPath))
                    {
                        AddItem(CertificateFile.FromFile(PathUtils.GetFullPath(CurrentWorkingDirectory), e.FullPath));
                        mutated = true;
                    }
                }

                if ((e.ChangeType & WatcherChangeTypes.Deleted) != 0)
                {
                    var files = lstFiles.Items.Cast<CertificateFile>()
                        .Where(x => string.Equals(x.FullPath, e.FullPath, StringComparison.OrdinalIgnoreCase))
                        .ToArray();
                    foreach (var f in files)
                    {
                        RemoveItem(f);
                        mutated = true;
                    }
                }

                if ((e.ChangeType & WatcherChangeTypes.Renamed) != 0)
                {
                    // huh?
                }

                if ((e.ChangeType & WatcherChangeTypes.Changed) != 0)
                {
                    var files = lstFiles.Items.Cast<CertificateFile>()
                        .Where(x => string.Equals(x.FullPath, e.FullPath, StringComparison.OrdinalIgnoreCase))
                        .ToArray();
                    foreach (var f in files)
                    {
                        ReloadItem(f);
                        mutated = true;
                    }
                }

                if (mutated)
                {
                    UpdateCaList();
                    UpdateCsrToSignList();
                }
            }));
        }

        private void AddItem(CertificateFile f)
        {
            lstFiles.Items.Add(f);
            AssignToGroup(f);
        }

        private void RemoveItem(CertificateFile f)
        {
            lstFiles.Items.Remove(f);
        }

        private void ReloadItem(CertificateFile f)
        {
            f.Reload();
            lstFiles.Items.Remove(f);
            AssignToGroup(f);
            lstFiles.Items.Add(f);
        }

        private void AssignToGroup(CertificateFile f)
        {
            f.Group = lstFiles.Groups[f.Type.ToString()];
        }

        private void UpdateCaList()
        {
            var files = lstFiles.Items.Cast<CertificateFile>();
            var newItems = new HashSet<CertificateFile>(files
                .Where(x => x.IsCertificateAuthority)
                .Where(x => files.Any(f => f.Type == CertificateFileType.PrivateKey && f.PublicKey.Equals(x.PublicKey))));

            var currentItems = new HashSet<CertificateFile>(cbCaCertificates.Items.Cast<CertificateFile>());
            var toAdd = newItems.Except(currentItems);
            var toRemove = currentItems.Except(newItems);
            cbCaCertificates.Items.AddRange(toAdd.ToArray());
            foreach (var r in toRemove) cbCaCertificates.Items.Remove(r);
        }

        private void UpdateCsrToSignList()
        {
            bool IsClientCert(Pkcs10CertificationRequest csr) => GetCsrExtensions(csr).Any(x => x.value is ExtendedKeyUsage ku && ku.HasKeyPurposeId(KeyPurposeID.IdKPClientAuth));
            bool IsServerCert(Pkcs10CertificationRequest csr) => GetCsrExtensions(csr).Any(x => x.value is ExtendedKeyUsage ku && ku.HasKeyPurposeId(KeyPurposeID.IdKPServerAuth));

            bool includeClient = cbSignClientCerts.Checked;
            bool includeServer = cbSignServerCerts.Checked;
            bool includeSigned = cbSignAlreadySigned.Checked;

            var files = lstFiles.Items.Cast<CertificateFile>()
                .Where(x => x.Type == CertificateFileType.Csr)
                .Select(x => new
                {
                    csr = x,
                    client = IsClientCert((Pkcs10CertificationRequest)x.PemObject),
                    server = IsServerCert((Pkcs10CertificationRequest)x.PemObject),
                    signed = lstFiles.Items.Cast<CertificateFile>().Any(f => f.Type == CertificateFileType.Certificate && f.PublicKey.Equals(x.PublicKey))
                });

            var allfiles = files.Where(x =>
                (x.client == includeClient || x.server == includeServer) && includeSigned ? true : !x.signed).Select(x => x.csr);

            var newItems = new HashSet<CertificateFile>(allfiles);
            var currentItems = new HashSet<CertificateFile>(cblSignCsrs.Items.Cast<CertificateFile>());
            var toAdd = newItems.Except(currentItems);
            var toRemove = currentItems.Except(newItems);
            cblSignCsrs.Items.AddRange(toAdd.ToArray());
            foreach (var r in toRemove) cblSignCsrs.Items.Remove(r);
        }

        private void txtWorkingDir_TextChanged(object sender, EventArgs e)
        {
            SetupWatcher();
            IniFile.Write("WorkingDir", CurrentWorkingDirectory);
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHighlights();
            UpdateSelectedThingInfoText();
        }

        private void UpdateHighlights()
        {
            var pk = SelectedItem?.PublicKey;
            if (pk == null)
            {
                foreach (CertificateFile item in lstFiles.Items)
                {
                    item.BackColor = SystemColors.Window;
                }
            }
            else
            {
                foreach (CertificateFile item in lstFiles.Items)
                {
                    if (item != SelectedItem && item.PublicKey != null && item.PublicKey.Equals(pk))
                    {
                        item.BackColor = SystemColors.ButtonFace;
                    }
                }
            }
        }

        private void UpdateSelectedThingInfoText()
        {
            if (SelectedItem != null)
            {
                txtSelectedThingInfo.Text = SelectedItem.GetDetail();
            }
            else
            {
                txtSelectedThingInfo.Text = "";
            }
        }

        private void rdCsrKeyDsa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCsrKeyEcdsa.Checked)
            {
                if (gbKeyBits.Text == "Key Size")
                {
                    var newItems = CustomNamedCurves.Names
                        .Cast<string>()
                        .Where(x => CustomNamedCurves.GetOid(x) != null)
                        .Select(x => x == "secp256r1" ? $"{x} (recommended)" : x)
                        .ToArray();
                    var recommended = newItems.Single(x => x.EndsWith("(recommended)"));
                    gbKeyBits.Text = "Curve";
                    cbCsKeyBits.Items.Clear();
                    cbCsKeyBits.Items.AddRange(newItems);
                    cbCsKeyBits.SelectedItem = recommended;
                }
            }
            else
            {
                if (gbKeyBits.Text != "Key Size")
                {
                    var newItems = new[] { "1024 bits", "2048 bits (recommended)", "4096 bits" };
                    gbKeyBits.Text = "Key Size";
                    cbCsKeyBits.Items.Clear();
                    cbCsKeyBits.Items.AddRange(newItems);
                    cbCsKeyBits.SelectedItem = newItems[1];
                }
            }
        }

        private void cbCsrExtendedUsage_Click(object sender, EventArgs e)
        {
            var cb = (CheckedListBox)sender;
            var si = cb.SelectedIndex;
            if (si >= 0)
            {
                cb.SetItemChecked(si, !cb.GetItemChecked(si));
            }
        }

        private async void btnCsrProcess_Click(object sender, EventArgs _)
        {
            Enabled = false;
            try
            {
                await ProcessCsrGeneration(rdProcessCreateCSR.Checked ? ThingToGenerate.Csr : ThingToGenerate.SelfSignedCert);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SetStatusAndProgress("OK");
            SetupWatcher();
            Enabled = true;
        }

        private async Task ProcessCsrGeneration(ThingToGenerate toGenerate)
        {
            SetStatusAndProgress("Generating...");
            var (keyGenerator, signerFactoryFactory) = await CsrGetKeyGeneratorAsync();

            var lines = txtCsrDomains.Text
                .Split('\r')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split(';', ',').Select(y => y.Trim()).Where(y => !string.IsNullOrEmpty(y)).ToArray())
                .Where(x => x.Length >= 1)
                .ToArray();

            string dnO = txtCsrSubjO.Text.Trim();
            string dnOU = txtCsrSubjOU.Text.Trim();
            string dnC = txtCsrSubjC.Text.Trim();
            string dnST = txtCsrSubjST.Text.Trim();
            string dnL = txtCsrSubjL.Text.Trim();
            string dnEmail = txtCsrSubjEmail.Text.Trim();

            var dnOrdering = new List<DerObjectIdentifier>();
            var dnValues = new List<string>();
            dnOrdering.Add(X509Name.CN);
            dnValues.Add("<placeholder>");
            if (!string.IsNullOrEmpty(dnO)) { dnOrdering.Add(X509Name.O); dnValues.Add(dnO); }
            if (!string.IsNullOrEmpty(dnOU)) { dnOrdering.Add(X509Name.OU); dnValues.Add(dnOU); }
            if (!string.IsNullOrEmpty(dnC)) { dnOrdering.Add(X509Name.C); dnValues.Add(dnC); }
            if (!string.IsNullOrEmpty(dnST)) { dnOrdering.Add(X509Name.ST); dnValues.Add(dnST); }
            if (!string.IsNullOrEmpty(dnL)) { dnOrdering.Add(X509Name.L); dnValues.Add(dnL); }
            if (!string.IsNullOrEmpty(dnEmail)) { dnOrdering.Add(X509Name.EmailAddress); dnValues.Add(dnEmail); }

            int cnt = 0;
            foreach (var line in lines)
            {
                SetStatusAndProgress("Generating...", (double)cnt++ / lines.Length);

                string cn = line[0];
                string[] altnames = line;

                var dnValuesThese = dnValues.ToArray();
                dnValuesThese[0] = cn;

                var keyPath = Path.Combine(CurrentWorkingDirectory, FixFilename($"{cn}.key"));
                AsymmetricCipherKeyPair key = null;
                if (rdCsrUseExistingKey.Checked && File.Exists(keyPath))
                {
                    try
                    {
                        PemReader pr = new PemReader(File.OpenText(keyPath));
                        key = pr.ReadObject() as AsymmetricCipherKeyPair;
                    }
                    catch { key = null; }
                }

                if (key == null)
                {
                    key = await Task.Factory.StartNew(() => keyGenerator.GenerateKeyPair());
                    WriteToPemFile(keyPath, key);
                }

                X509Name subject = new X509Name(dnOrdering, dnValuesThese);


                if ((toGenerate | ThingToGenerate.Csr) == ThingToGenerate.Csr)
                {
                    X509ExtensionsGenerator extGenerator = GetExtensionsGenerator(altnames);

                    var exts = extGenerator.Generate();
                    Asn1Set attrs = new DerSet(new DerSequence(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(exts)));

                    var req = new Pkcs10CertificationRequest(
                        signerFactoryFactory(key.Private),
                        subject,
                        key.Public,
                        attrs,
                        key.Private);

                    if (!req.Verify()) throw new InvalidOperationException("Generated an invalid CSR.");
                    WriteToPemFile(Path.Combine(CurrentWorkingDirectory, FixFilename($"{cn}.csr")), req);
                }
                if ((toGenerate | ThingToGenerate.SelfSignedCert) == ThingToGenerate.SelfSignedCert)
                {
                    SecureRandom sr = new SecureRandom();
                    X509V3CertificateGenerator certgen = new X509V3CertificateGenerator();

                    certgen.SetSerialNumber(new BigInteger(128, sr));
                    certgen.SetIssuerDN(subject);
                    certgen.SetSubjectDN(subject);
                    certgen.SetNotBefore(DateTime.Today.Subtract(new TimeSpan(1, 0, 0, 0)));
                    certgen.SetNotAfter(DateTime.Today.AddYears(30));
                    certgen.SetPublicKey(key.Public);

                    foreach (var ext in GetExtensions(altnames))
                    {
                        certgen.AddExtension(ext.oid, ext.isCritical, ext.section);
                    }

                    ISignatureFactory signer;
                    if (key.Private is ECPrivateKeyParameters)
                    {
                        signer = new Asn1SignatureFactory("SHA256WITHECDSA", key.Private, sr);
                    }
                    else
                    {
                        signer = new Asn1SignatureFactory("SHA256WITHRSA", key.Private, sr);
                    }

                    var cert = certgen.Generate(signer);
                    cert.CheckValidity();
                    cert.Verify(key.Public);
                    WriteToPemFile(Path.Combine(CurrentWorkingDirectory, FixFilename($"{cn}.cer")), cert);
                }
            }

            SetStatusAndProgress("Generating CSRs", 1);
        }

        private string FixFilename(string cn) => cn.Replace('*', '_');

        private void WriteToPemFile(string filename, object obj)
        {
            using (var sw = new StreamWriter(File.OpenWrite(filename)))
            {
                PemWriter writer = new PemWriter(sw);
                writer.WriteObject(obj);
            }
        }

        private IEnumerable<(DerObjectIdentifier oid, bool isCritical, Asn1Encodable section)> GetExtensions(string[] altnames)
        {
            var (basicConstrainsCritical, basicConstraintsExt) = GetBasicConstraints();
            var (keyUsageCritical, keyUsageExt) = GetKeyUsage();
            var (extKeyUsageCritical, extKeyUsageExt) = GetExtendedUsage();
            var (altNamesCritical, altNamesExt) = GetSubjectAltName(altnames);

            if (basicConstraintsExt != null) yield return (X509Extensions.BasicConstraints, basicConstrainsCritical, basicConstraintsExt);
            if (keyUsageExt != null) yield return (X509Extensions.KeyUsage, keyUsageCritical, keyUsageExt);
            if (extKeyUsageExt != null) yield return (X509Extensions.ExtendedKeyUsage, extKeyUsageCritical, extKeyUsageExt);
            if (altNamesExt != null) yield return (X509Extensions.SubjectAlternativeName, altNamesCritical, altNamesExt);
        }

        private X509ExtensionsGenerator GetExtensionsGenerator(string[] altnames)
        {
            X509ExtensionsGenerator extGenerator = new X509ExtensionsGenerator();

            var (basicConstrainsCritical, basicConstraintsExt) = GetBasicConstraints();
            var (keyUsageCritical, keyUsageExt) = GetKeyUsage();
            var (extKeyUsageCritical, extKeyUsageExt) = GetExtendedUsage();
            var (altNamesCritical, altNamesExt) = GetSubjectAltName(altnames);

            if (basicConstraintsExt != null) extGenerator.AddExtension(X509Extensions.BasicConstraints, basicConstrainsCritical, basicConstraintsExt);
            if (keyUsageExt != null) extGenerator.AddExtension(X509Extensions.KeyUsage, keyUsageCritical, keyUsageExt);
            if (extKeyUsageExt != null) extGenerator.AddExtension(X509Extensions.ExtendedKeyUsage, extKeyUsageCritical, extKeyUsageExt);
            if (altNamesExt != null) extGenerator.AddExtension(X509Extensions.SubjectAlternativeName, altNamesCritical, altNamesExt);

            return extGenerator;
        }

        private (bool, KeyUsage) GetKeyUsage()
        {
            int keyUsage = 0;
            foreach (string item in cbCsrUsage.CheckedItems)
            {
                keyUsage |= keyUsageDictionary.Where(x => x.Value == item).Select(x => x.Key).Single();
            }

            bool keyUsageCritical = cbCsrCertUsageCritical.Checked;
            KeyUsage keyUsageExt = keyUsage != 0 || keyUsageCritical ? new KeyUsage(keyUsage) : null;
            return (keyUsageCritical, keyUsageExt);
        }

        private (bool, ExtendedKeyUsage) GetExtendedUsage()
        {
            var purposes = cbCsrExtendedUsage.CheckedItems.Cast<string>()
                .Select(item => extKeyUsageDictionary.Where(x => x.Value == item).Select(x => x.Key).Single())
                .ToArray();
            bool extKeyUsageCritical = cbCsrExtendedUsageCritical.Checked;
            ExtendedKeyUsage extendedKeyUsageExt = purposes.Length != 0 || extKeyUsageCritical ? new ExtendedKeyUsage(purposes) : null;
            return (extKeyUsageCritical, extendedKeyUsageExt);
        }

        private (bool, BasicConstraints) GetBasicConstraints()
        {
            BasicConstraints bc = null;
            if (rbCsrBasicConstraintCA.Checked)
            {
                var pathLenStr = txtCsrBasicConstraintPathLength.Text;
                if (!string.IsNullOrEmpty(pathLenStr))
                {
                    var pathLengthValue = int.TryParse(pathLenStr, out var plv)
                        ? plv
                        : throw new InvalidOperationException("Path length must be a valid integer");
                    bc = new BasicConstraints(pathLengthValue);
                }
                else
                {
                    bc = new BasicConstraints(true);
                }
            }
            else if (rbCsrBasicConstraintEndEntity.Checked)
            {
                bc = new BasicConstraints(false);
            }

            return (cbCsrBasicConstraintCritical.Checked, bc);
        }

        private (bool, GeneralNames) GetSubjectAltName(string[] altnames)
        {
            var altNamesCritical = cbCsrAltNamesCritical.Checked;
            GeneralNames altNamesExt = null;
            if (cbCsrAltNamesInclude.Checked)
            {
                GeneralName[] gennames = null;
                if (altnames != null && altnames.Length > 0)
                {
                    gennames = altnames.Select((x, i) => new GeneralName(GeneralName.DnsName, new DerIA5String(x))).ToArray();
                }
                altNamesExt = new GeneralNames(gennames ?? new GeneralName[0]);
            }
            return (altNamesCritical, altNamesExt);
        }

        private async Task<(IAsymmetricCipherKeyPairGenerator, Func<AsymmetricKeyParameter, ISignatureFactory>)> CsrGetKeyGeneratorAsync() =>
            await Task.Factory.StartNew<(IAsymmetricCipherKeyPairGenerator, Func<AsymmetricKeyParameter, ISignatureFactory>)>(() =>
            {
                var (rsaChecked, dsaChecked, ecdsaChecked) = ((bool, bool, bool))Invoke((Func<(bool, bool, bool)>)(
                    () => (rdCsrKeyRsa.Checked, rdCsrKeyDsa.Checked, rdCsrKeyEcdsa.Checked)));

                var random = new SecureRandom();
                var keyBitsString = (string)Invoke((Func<string>)(() => cbCsKeyBits.Text.Replace(" (recommended)", "").Replace(" bits", "")));
                if (rsaChecked)
                {
                    int bits = int.Parse(keyBitsString);
                    var gen = new RsaKeyPairGenerator();
                    gen.Init(new KeyGenerationParameters(random, bits));
                    return (gen, k => new Asn1SignatureFactory("SHA256WITHRSA", k));
                }
                else if (rdCsrKeyDsa.Checked)
                {
                    int L = int.Parse(keyBitsString);
                    int S;
                    switch (L)
                    {
                        case 1024: S = 160; break;
                        case 2048: S = 224; break;
                        case 3072: S = 256; break;
                        default: throw new NotSupportedException("invalid DSA bits");
                    }
                    var gen = new DsaKeyPairGenerator();
                    var dsaParmGen = new DsaParametersGenerator(new Sha256Digest());
                    dsaParmGen.Init(new DsaParameterGenerationParameters(L, S, 80, random));
                    var dsaParms = dsaParmGen.GenerateParameters();
                    gen.Init(new DsaKeyGenerationParameters(random, dsaParms));
                    return (gen, k => new Asn1SignatureFactory("SHA256WITHDSA", k));
                }
                else
                {
                    var gen = new ECKeyPairGenerator();
                    gen.Init(new ECKeyGenerationParameters(CustomNamedCurves.GetOid(keyBitsString), random));
                    return (gen, k => new Asn1SignatureFactory("SHA256WITHECDSA", k));
                }
            });

        private void rbCsrBasicConstraint_Click(object sender, EventArgs e)
        {
            if (rbCsrBasicConstraintCA.Checked)
            {
                txtCsrBasicConstraintPathLength.Enabled = true;
                lblCsrBasicConstraintPathLength.Enabled = true;
                cbCsrBasicConstraintCritical.Enabled = true;
            }
            else if (rbCsrBasicConstraintEndEntity.Checked)
            {
                txtCsrBasicConstraintPathLength.Enabled = false;
                txtCsrBasicConstraintPathLength.Text = "";
                lblCsrBasicConstraintPathLength.Enabled = false;
                cbCsrBasicConstraintCritical.Enabled = true;
            }
            else
            {
                txtCsrBasicConstraintPathLength.Enabled = false;
                txtCsrBasicConstraintPathLength.Text = "";
                lblCsrBasicConstraintPathLength.Enabled = false;
                cbCsrBasicConstraintCritical.Enabled = false;
                cbCsrBasicConstraintCritical.Checked = false;
            }
        }

        private void SetStatusAndProgress(string status, double? progress = null)
        {
            tsslStatus.Text = status;
            if (progress.HasValue)
            {
                tspbProgress.Visible = true;
                tspbProgress.Value = (int)(progress * tspbProgress.Maximum);
            }
            else
            {
                tspbProgress.Visible = false;
            }
        }

        public enum ThingToGenerate
        {
            Csr = 1,
            SelfSignedCert = 2
        }
        private void CbSigning_CheckedChanged(object sender, EventArgs e) => UpdateCsrToSignList();

        private void BtnSignCertificates_Click(object sender, EventArgs e)
        {
            var rng = new SecureRandom();
            try
            {
                if (!int.TryParse(cbSignCertDuration.Text, out var certDuration)) throw new Exception("Please specify a numeric number of years");
                if (certDuration < 1) throw new Exception("Please specify a number of years greater than zero");

                var ca = (CertificateFile)cbCaCertificates.SelectedItem;
                var cacert = (X509Certificate)ca.PemObject;
                var cakey = (AsymmetricCipherKeyPair)lstFiles.Items.Cast<CertificateFile>().First(x => x.Type == CertificateFileType.PrivateKey && x.PublicKey.Equals(ca.PublicKey)).PemObject;
                var csrs = cblSignCsrs.CheckedItems.Cast<CertificateFile>().ToArray();
                var notBefore = DateTime.Today;
                var notAfter = DateTime.Today.AddYears(certDuration);

                if (ca == null) throw new Exception("Please select a CA certificate");
                if (csrs.Length == 0) throw new Exception("Please select at least one CSR to sign");

                foreach (var csr in csrs)
                {
                    string outfile = PathUtils.GetFullPath(Path.Combine(CurrentWorkingDirectory, Path.ChangeExtension(csr.Filename, ".cer")));
                    if (File.Exists(outfile))
                    {
                        var r = MessageBox.Show(this, $"Replace {outfile}?", "File already exists", MessageBoxButtons.YesNoCancel);
                        if (r == DialogResult.No) continue;
                        if (r == DialogResult.Cancel) return;
                    }

                    // stuff for the cert
                    var pkc = (Pkcs10CertificationRequest)csr.PemObject;
                    var pkcinfo = pkc.GetCertificationRequestInfo();
                    var pkcpub = pkc.GetPublicKey();
                    byte[] bserial = new byte[16];
                    rng.NextBytes(bserial);
                    bserial[0] &= 0b0111_1111;

                    // extensions
                    var extensions = GetCsrExtensions(pkc);

                    // generate the cert
                    var gen = new X509V3CertificateGenerator();
                    gen.SetIssuerDN(cacert.SubjectDN);
                    gen.SetSerialNumber(new BigInteger(bserial));
                    gen.SetNotBefore(notBefore);
                    gen.SetNotAfter(notAfter);
                    gen.SetSubjectDN(pkcinfo.Subject);
                    gen.SetPublicKey(pkcpub);
                    foreach (var ext in extensions) gen.AddExtension(ext.oid, ext.critical, ext.value);
                    // extensions

                    // sign the cert
                    ISignatureFactory signer;
                    if (cakey.Private is ECPrivateKeyParameters)
                    {
                        signer = new Asn1SignatureFactory("SHA256WITHECDSA", cakey.Private, rng);
                    }
                    else
                    {
                        signer = new Asn1SignatureFactory("SHA256WITHRSA", cakey.Private, rng);
                    }

                    var cert = gen.Generate(signer);
                    cert.CheckValidity();
                    cert.Verify(cakey.Public);
                    WriteToPemFile(outfile, cert);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private (DerObjectIdentifier oid, bool critical, Asn1Encodable value)[] GetCsrExtensions(Pkcs10CertificationRequest csr)
        {
            return csr.GetCertificationRequestInfo()
                .Attributes.Cast<Asn1Encodable>()
                .Where(x => x is DerSequence)
                .Cast<DerSequence>()
                .Where(x => x.Count == 2 && x[0] is DerObjectIdentifier)
                .Where(x => ((DerObjectIdentifier)x[0]).Equals(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest))
                .Select(x => x[1] as DerSet)
                .Where(x => x != null)
                .Select(x => x[0] as DerSequence)
                .SelectMany(x => x.Cast<Asn1Encodable>().Where(y => y is DerSequence).Cast<DerSequence>())
                .Where(x => x.Count == 3 || x.Count == 2)
                .Select(x => TryParseExtension(x))
                .Where(x => x.oid != null)
                .ToArray();
        }

        private (DerObjectIdentifier oid, bool critical, Asn1Encodable value) TryParseExtension(DerSequence x)
        {
            if (x.Count < 2) return default;
            var oid = x[0] as DerObjectIdentifier;
            var critical = x.Count >= 3 ? ((x[1] as DerBoolean)?.IsTrue ?? false) : false;
            var value = x[x.Count - 1] as DerOctetString;

            try
            {
                var v = Asn1Object.FromByteArray(value.GetOctets());
                if (oid.Id == X509Extensions.BasicConstraints.Id) return (oid, critical, BasicConstraints.GetInstance(v));
                if (oid.Id == X509Extensions.SubjectAlternativeName.Id) return (oid, critical, GeneralNames.GetInstance(v));
                if (oid.Id == X509Extensions.ExtendedKeyUsage.Id) return (oid, critical, ExtendedKeyUsage.GetInstance(v));
                if (oid.Id == X509Extensions.KeyUsage.Id) return (oid, critical, KeyUsage.GetInstance(v));
            }
            catch
            {
            }
            return default;
        }
    }
}
