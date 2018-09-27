using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoTool
{
    public partial class MainForm : Form
    {
        FileSystemWatcher watcher;

        private string CurrentWorkingDirectory
        {
            get { return txtWorkingDir.Text; }
            set { txtWorkingDir.Text = value; }
        }

        private CertificateFile SelectedItem => lstFiles.SelectedItems.Cast<CertificateFile>().FirstOrDefault();

        public MainForm()
        {
            InitializeComponent();
            lstFiles.Groups.Add("Unknown", "Unknown");
            lstFiles.Groups.Add("Certificate", "Certificate");
            lstFiles.Groups.Add("Csr", "Certificate Signing Request");
            lstFiles.Groups.Add("PublicKey", "Public Key");
            lstFiles.Groups.Add("PrivateKey", "Private Key");
            lstFiles.Groups.Add("Multiple", "Multiple");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupWatcher();
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

                    watcher.EnableRaisingEvents = true;
                }
            }
        }

        private void File_Changed(object sender, FileSystemEventArgs e)
        {
            Invoke(new Action(() =>
            {
                if ((e.ChangeType & WatcherChangeTypes.Created) != 0)
                {
                    if (File.Exists(e.FullPath))
                    {
                        AddItem(CertificateFile.FromFile(PathUtils.GetFullPath(CurrentWorkingDirectory), e.FullPath));
                    }
                }

                if ((e.ChangeType & WatcherChangeTypes.Deleted) != 0)
                {
                    var files = lstFiles.Items.Cast<CertificateFile>()
                        .Where(x => string.Equals(x.FullPath, e.FullPath, StringComparison.OrdinalIgnoreCase))
                        .ToArray();
                    foreach (var f in files) RemoveItem(f);
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
                    foreach (var f in files) ReloadItem(f);
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

        private void txtWorkingDir_TextChanged(object sender, EventArgs e)
        {
            SetupWatcher();
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateHighlights();
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
    }
}
