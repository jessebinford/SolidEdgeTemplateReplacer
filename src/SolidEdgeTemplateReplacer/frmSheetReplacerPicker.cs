using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SolidEdgeTemplateReplacer
{
    internal partial class frmSheetReplacerPicker : Form
    {
        SEDocument _targetDocument;
        SEDocument _templateDocument;
        private List<ComboBox> _templatePickers = new List<ComboBox>();
        private List<string> _comboBoxSelections = new List<string>();

        internal List<SheetReplacement> SheetReplacerList = new List<SheetReplacement>();

        internal frmSheetReplacerPicker(SEDocument TargetDoc, SEDocument TemplateDoc)
        {
            _targetDocument = TargetDoc;
            _templateDocument = TemplateDoc;
            InitializeComponent();
        }

        private void frmSheetReplacerPicker_Load(object sender, EventArgs e)
        {
            _comboBoxSelections.Add("DONT REPLACE");
            foreach (var item in _templateDocument.BackgroundDocumentSheets)
            {
                _comboBoxSelections.Add(item.Name);
            }

            buildPickers();
        }

        private void buildPickers()
        {
            var _labelPoint = new Point(13, 11);
            var _comboBoxPoint = new Point(119, 13);
            int _count = 0;

            foreach (var item in _targetDocument.BackgroundDocumentSheets)
            {
                _labelPoint.Y += 27;
                _comboBoxPoint.Y += 27;

                Label _nl = new Label();
                _nl.Text = item.Name;
                _nl.AutoSize = false;
                _nl.TextAlign = ContentAlignment.MiddleRight;
                _nl.Size = new Size(100, 23);
                _nl.Location = _labelPoint;
                panel1.Controls.Add(_nl);

                ComboBox _cb = new ComboBox();
                _cb.Size = new Size(121, 21);
                _cb.Location = _comboBoxPoint;
                _cb.DropDownStyle = ComboBoxStyle.DropDownList;
                _cb.Tag = item;
                loadComboBox(_cb, _count);
                panel1.Controls.Add(_cb);
                _templatePickers.Add(_cb);

                _count++;
            }
        }

        private void loadComboBox(ComboBox cbox, int count)
        {
            foreach (var item in _comboBoxSelections)
            {
                cbox.Items.Add(item);
            }

            if (cbox.Items.Count >= count + 2)
            {
                cbox.SelectedIndex = count + 1;
            }
            else
            {
                cbox.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loopOverEachControlToFindSheets();
            if (SheetReplacerList.Count == 0)
            {
                MessageBox.Show("You have nothing to replace...");
                return;
            }
            this.Close();
        }

        private void loopOverEachControlToFindSheets()
        {
            foreach (var item in _templatePickers)
            {
                if (item.SelectedIndex != 0)
                {
                    int _i = item.SelectedIndex - 1;

                    SheetReplacerList.Add(new SheetReplacement
                    {
                        TargetSheet = (SolidEdgeDraft.Sheet)item.Tag,
                        TemplateSheet = _templateDocument.BackgroundDocumentSheets[_i]
                    });
                }
            }
        }
    }
}
