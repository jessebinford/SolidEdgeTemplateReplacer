using System;
using System.Runtime.InteropServices;
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

        /// <summary>
        /// Loads the Solid Edge Instance
        /// </summary>
        /// <param name="OpenNewAppInstance"></param>
        internal void LoadSolidEdgeApplication(bool OpenNewAppInstance)
        {
            try
            {
                CustomEvents.OnProgressChanged("Launching Solid Edge");
                OleMessageFilter.Register();
                _application = SolidEdgeUtils.Connect(OpenNewAppInstance, true);
            }
            catch
            {

            }
        }

        /// <summary>
        /// This method is only used if Solid Edge is in SEEC mode.
        /// This will perform the actual processing of the background sheet replacements
        /// </summary>
        /// <param name="TargetDoc"></param>
        /// <param name="TemplateDoc"></param>
        internal void ProcessSeecDocuments(SEDocument TargetDoc, SEDocument TemplateDoc)
        {
            // Initialize the SEEC Controller
            using (SEECController _sc = new SEECController(SEApplication))
            {
                // Confirm we are in SEEC mode
                if (!_sc.IsInSEECMode())
                {
                    CustomEvents.OnProgressChanged("Solid Edge is not in SEEC mode!");
                    throw new Exception("Solid Edge is not in SEEC mode!");
                }


                // Launch the file lookup window to select an already open file from SE. This is the target document for the replacements.
                CustomEvents.OnProgressChanged("Launching the file lookup");
                TargetDoc = SolidEdgeDocumentController.LoadOpenDocument(SEApplication);
                if (TargetDoc == null)
                {
                    throw new Exception("No target document selected!");
                }

                // Call the SEEC method to search for the item and rev provided by the user for the template draft doc.
                _sc.FetchTeamcenterFilesForItem(TemplateDoc);
                if (string.IsNullOrWhiteSpace(TemplateDoc.FullFilePath))
                {
                    CustomEvents.OnProgressChanged("Unable to find a file to open from TC!");
                    throw new Exception("Unable to find a file to open from TC!");
                }

                // Turn display alerts off and attempt to load the found template file into the cache
                SEApplication.DisplayAlerts = false;
                _sc.DownloadTCFileToCache(TemplateDoc);
                if (string.IsNullOrWhiteSpace(TemplateDoc.FullFilePath))
                {
                    throw new Exception("Failed to download file from TC!");
                }
                SEApplication.DisplayAlerts = true;


                // Attempt to open the template document from the local SEEC cache
                CustomEvents.OnProgressChanged("Loading up the template document");
                SolidEdgeDocumentController.OpenDocument(SEApplication, TemplateDoc, true);
                if (TargetDoc.SEDocumentInstance == null)
                {
                    throw new Exception("Template document failed to load!");
                }

                performSheetReplacement(TargetDoc, TemplateDoc);

            }
        }

        /// <summary>
        /// This method is only used if Solid Edge is in managed mode.
        /// This will perform the actual processing of the background sheet replacements
        /// </summary>
        /// <param name="TargetDoc"></param>
        /// <param name="TemplateDoc"></param>
        internal void ProcessUnmanagedDocuments(SEDocument TargetDoc, SEDocument TemplateDoc)
        {
            if (isApplicationInSEECMode())
            {
                CustomEvents.OnProgressChanged("Solid Edge is in SEEC mode!");
                throw new Exception("Solid Edge is in SEEC mode, but you have selected Unmanaged!");
            }

            // Either load the file lookup window to select an already open file, or open a file via the FullFilePath location.
            if (string.IsNullOrWhiteSpace(TargetDoc.FullFilePath))
            {
                CustomEvents.OnProgressChanged("No target file defined, launching the open file lookup");
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

            // Attempt to open the Template document
            CustomEvents.OnProgressChanged("Loading up the template document");
            SolidEdgeDocumentController.OpenDocument(SEApplication, TemplateDoc, true);
            if (TargetDoc.SEDocumentInstance == null)
            {
                throw new Exception("Template document failed to load!");
            }

            performSheetReplacement(TargetDoc, TemplateDoc);
        }

        /// <summary>
        /// This method will launch the sheet replacer prompt and perform the sheet replacements.
        /// </summary>
        /// <param name="TargetDoc"></param>
        /// <param name="TemplateDoc"></param>
        private void performSheetReplacement(SEDocument TargetDoc, SEDocument TemplateDoc)
        {
            CustomEvents.OnProgressChanged("Loading the sheet pointer");
            frmSheetReplacerPicker _sr = new frmSheetReplacerPicker(TargetDoc, TemplateDoc);
            _sr.ShowDialog();

            // Get the listing of sheets from the dialog. This will be empty if they choose not to replace any sheets.
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

        /// <summary>
        /// Confirms if we are in SEEC mode or not.
        /// </summary>
        /// <returns></returns>
        private bool isApplicationInSEECMode()
        {
            try
            {
                using (SEECController _sc = new SEECController(SEApplication))
                {
                    return _sc.IsInSEECMode();
                }
            }
            catch
            {

            }

            return false;
        }

        public void Dispose()
        {
            OleMessageFilter.Unregister();
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
