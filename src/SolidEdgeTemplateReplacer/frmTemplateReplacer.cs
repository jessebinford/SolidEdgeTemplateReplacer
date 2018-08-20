using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            
            try
            {
                seVersionLabel.Text += SolidEdgeUtils.GetVersion();
            }
            catch
            {
                seVersionLabel.Text += "ERROR";
            }
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

                if (seecRadio.Checked)
                {
                    using (SEDocument _targetDoc = new SEDocument(string.Empty, false, false) { ResetToSheetOneOnFinish = resetSheet1Chkbox.Checked })
                    using (SEDocument _templateDoc = new SEDocument(string.Empty, !leaveTempOpenChkBox.Checked, false) { ItemID = itemIDTxtbox.Text, RevID = revTxtbox.Text})
                    {
                        try
                        {
                            _ac.ProcessSeecDocuments(_targetDoc, _templateDoc);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error!\r\n\r\n{ex.Message}");
                        }
                    }
                }

                if (unmanagedRadio.Checked)
                {
                    using (SEDocument _targetDoc = new SEDocument(targetFileTextbox.Text, false, false) { ResetToSheetOneOnFinish = resetSheet1Chkbox.Checked })
                    using (SEDocument _templateDoc = new SEDocument(templateFileTextBox.Text, !leaveTempOpenChkBox.Checked, false))
                    {
                        try
                        {
                            _ac.ProcessUnmanagedDocuments(_targetDoc, _templateDoc);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error!\r\n\r\n{ex.Message}");
                        }
                    }
                }

                CustomEvents.OnProgressChanged("Finished!");

                _ac.SEApplication.DisplayAlerts = true;
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

            if (!isFileValid(templateFileTextBox.Text) && unmanagedRadio.Checked)
            {
                MessageBox.Show("Invalid Template file path!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(itemIDTxtbox.Text) && seecRadio.Checked)
            {
                MessageBox.Show("You must provide an ItemID for the template!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(revTxtbox.Text) && seecRadio.Checked)
            {
                MessageBox.Show("You must provide a Rev for the template!");
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

        private void seecRadio_CheckedChanged(object sender, EventArgs e)
        {
            templateFileTextBox.Visible = !seecRadio.Checked;
            templateFileBtn.Visible = !seecRadio.Checked;
            itemIDLbl.Visible = seecRadio.Checked;
            itemIDTxtbox.Visible = seecRadio.Checked;
            revLbl.Visible = seecRadio.Checked;
            revTxtbox.Visible = seecRadio.Checked;
            fileAlreadyOpenChkBox.Enabled = !seecRadio.Checked;
            if (seecRadio.Checked)
            {
                fileAlreadyOpenChkBox.Checked = true;
            }
        }
    }
}
