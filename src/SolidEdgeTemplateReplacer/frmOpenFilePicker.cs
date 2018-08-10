using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidEdgeTemplateReplacer
{
    internal partial class frmOpenFilePicker : Form
    {
        private List<SEDocument> _documents;
        internal SEDocument SelectedDocument = null;

        internal frmOpenFilePicker(List<SEDocument> Documents)
        {
            _documents = Documents;
            InitializeComponent();
        }

        private void frmOpenFilePicker_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = _documents;
            listBox1.DisplayMember = "CombinedListingName";
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please make a selection!");
            }

            SelectedDocument = (SEDocument)listBox1.SelectedItem;
            this.Close();
        }
    }
}
