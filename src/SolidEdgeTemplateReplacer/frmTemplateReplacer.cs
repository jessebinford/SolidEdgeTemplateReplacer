using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolidEdgeCommunity;

namespace SolidEdgeTemplateReplacer
{
    public partial class frmTemplateReplacer : Form
    {
        public frmTemplateReplacer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CustomEvents.ProgressChanged += CustomEvents_ProgressChanged;
            seVersionLabel.Text += SolidEdgeUtils.GetVersion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validateFilePaths())
            {
                return;
            }

            if (clearLogChkbox.Checked)
                richTextBox1.Text = "";


            using (SolidEdgeApplicationController _ac = new SolidEdgeApplicationController())
            {
                _ac.LoadSolidEdgeApplication(!fileAlreadyOpenChkBox.Checked);

                if (_ac.SEApplication == null)
                {
                    MessageBox.Show("Solid Edge is not currently running. You cannot already have the target file open.");
                    return;
                }

                using (SEDocument _targetDoc = new SEDocument(targetFileTextbox.Text, false, false))
                using (SEDocument _templateDoc = new SEDocument(templateFileTextBox.Text, !leaveTempOpenChkBox.Checked, false))
                {
                    try
                    {
                        _ac.ProcessDocuments(_targetDoc, _templateDoc);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error!\r\n\r\n{ex.Message}");
                    }
                }

                CustomEvents.OnProgressChanged("Finished!");

            }

            MessageBox.Show("Done");
        }

        private bool validateFilePaths()
        {
            if (!isFileValid(targetFileTextbox.Text) && !fileAlreadyOpenChkBox.Checked)
            {
                MessageBox.Show("Invalid Target file path!");
                return false;
            }

            if (!isFileValid(templateFileTextBox.Text))
            {
                MessageBox.Show("Invalid Template file path!");
                return false;
            }

            return true;
        }

        private void CustomEvents_ProgressChanged(object sender, string StatusText)
        {
            richTextBox1.Text += $"{StatusText}\r\n";

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void targetFileBtn_Click(object sender, EventArgs e)
        {
            string _file = fileBrowser();
            if (!string.IsNullOrWhiteSpace(_file))
            {
                targetFileTextbox.Text = _file;
            }
        }

        private void templateFileBtn_Click(object sender, EventArgs e)
        {
            string _file = fileBrowser();
            if (!string.IsNullOrWhiteSpace(_file))
            {
                templateFileTextBox.Text = _file;
            }
        }

        private bool isFileValid(string FilePath)
        {
            bool _rv = false;

            _rv = System.IO.File.Exists(FilePath);

            if (_rv)
            {
                System.IO.FileInfo _fileInfo = new System.IO.FileInfo(FilePath);

                _rv = _fileInfo.Extension.Equals(".dft", StringComparison.CurrentCultureIgnoreCase);
            }

            return _rv;
        }

        private string fileBrowser()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "SE Draft (*.dft)|*.dft";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        private void fileAlreadyOpenChkBox_CheckedChanged(object sender, EventArgs e)
        {
            targetFileTextbox.Enabled = !fileAlreadyOpenChkBox.Checked;
            targetFileTextbox.Text = "";
            targetFileBtn.Enabled = !fileAlreadyOpenChkBox.Checked;
        }
    }
}
