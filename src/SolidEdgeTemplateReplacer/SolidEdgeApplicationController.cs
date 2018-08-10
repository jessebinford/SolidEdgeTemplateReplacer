using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SolidEdgeFramework;
using SolidEdgeCommunity;

namespace SolidEdgeTemplateReplacer
{
    class SolidEdgeApplicationController : IDisposable
    {
        private Application _application;

        internal Application SEApplication => _application;

        internal SolidEdgeApplicationController()
        {

        }

        internal void LoadSolidEdgeApplication(bool OpenNewAppInstance)
        {
            try
            {
                CustomEvents.OnProgressChanged("Launching Solid Edge");
                _application = SolidEdgeUtils.Connect(OpenNewAppInstance, true);
            }
            catch
            {

            }
        }

        internal void ProcessDocuments(SEDocument TargetDoc, SEDocument TemplateDoc)
        {
            if (string.IsNullOrWhiteSpace(TargetDoc.FullFilePath))
            {
                CustomEvents.OnProgressChanged("No target file defined, launching the file lookup");
                TargetDoc = SolidEdgeDocumentController.LoadOpenDocument(SEApplication);
                if (TargetDoc == null)
                {
                    throw new Exception("No target document selected!");
                }
            }
            else
            {
                CustomEvents.OnProgressChanged("Target file is defined, opening up the document");
                SolidEdgeDocumentController.OpenDocument(SEApplication, TargetDoc, false);
                if (TargetDoc.SEDocumentInstance == null)
                {
                    throw new Exception("Target document failed to load!");
                }
            }


            CustomEvents.OnProgressChanged("Loading up the template document");
            SolidEdgeDocumentController.OpenDocument(SEApplication, TemplateDoc, true);
            if (TargetDoc.SEDocumentInstance == null)
            {
                throw new Exception("Template document failed to load!");
            }


            CustomEvents.OnProgressChanged("Loading the sheet pointer");
            frmSheetReplacerPicker _sr = new frmSheetReplacerPicker(TargetDoc, TemplateDoc);
            _sr.ShowDialog();
            var _slist = _sr.SheetReplacerList;

            if (_slist.Count > 0)
            {
                CustomEvents.OnProgressChanged($"Performing the sheet replacer on {_slist.Count} sheets");
                SolidEdgeSheetController.BackgroundReplacer(_slist, TargetDoc, TemplateDoc, SEApplication);
            }
            else
            {
                throw new Exception("No sheets were selected to be replaced by anything!");
            }
        }

        public void Dispose()
        {
            if (_application != null)
            {
                Marshal.ReleaseComObject(_application);
            }
        }

        ~SolidEdgeApplicationController()
        {
            Dispose();
        }
    }
}
