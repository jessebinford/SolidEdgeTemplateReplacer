using System;
using System.Collections.Generic;
using SolidEdgeFramework;
using SolidEdgeDraft;

namespace SolidEdgeTemplateReplacer
{
    class SolidEdgeDocumentController
    {
        /// <summary>
        /// Returns a list of all open documents currently in the SE application.
        /// </summary>
        /// <param name="SEAppObj"></param>
        /// <returns></returns>
        internal static List<SEDocument> GetOpenDocuments(Application SEAppObj)
        {
            List<SEDocument> _returnList = new List<SEDocument>();

            foreach (SolidEdgeDocument item in SEAppObj.Documents)
            {
                // Confirm the document is a draft
                if (item.Type == DocumentTypeConstants.igDraftDocument)
                {
                    _returnList.Add(new SEDocument(item.FullName, false, false) { FileName = item.Name, SEDocumentInstance = item });
                }
            }

            return _returnList;
        }

        /// <summary>
        /// Open a document in Solid edge.
        /// </summary>
        /// <param name="SEAppObj"></param>
        /// <param name="DocumentObj"></param>
        /// <param name="OpenDraftReadOnly"></param>
        internal static void OpenDocument(Application SEAppObj, SEDocument DocumentObj, bool OpenDraftReadOnly)
        {
            // Turn display alerts off so any extra SE prompts dont show up on/after open.
            SEAppObj.DisplayAlerts = false;

            object _originalOpenDraftReadOnlyValue = null;
            object _fileReadOnlySetting = OpenDraftReadOnly;

            try
            {
                // Fetch the user's readonly attribute for draft files to restore it after we process our open.
                SEAppObj.GetGlobalParameter(ApplicationGlobalConstants.seApplicationGlobalOpenAsReadOnlyDftFile,
                   _originalOpenDraftReadOnlyValue);

                // Set the readonly attribute per our bool on the input
                SEAppObj.SetGlobalParameter(ApplicationGlobalConstants.seApplicationGlobalOpenAsReadOnlyDftFile,
                    _fileReadOnlySetting);

                CustomEvents.OnProgressChanged($"Opening document: {DocumentObj.FullFilePath}");

                DocumentObj.SEDocumentInstance = (SolidEdgeDocument) SEAppObj.Documents.Open(DocumentObj.FullFilePath);
                DocumentObj.FileName = DocumentObj.SEDocumentInstance.Name;
                LoadBackgroundSheets(DocumentObj);
            }
            catch
            {

            }
            finally
            {
                SEAppObj.DisplayAlerts = true;

                // Reset the readonly attribute back to the users defined value
                SEAppObj.SetGlobalParameter(ApplicationGlobalConstants.seApplicationGlobalOpenAsReadOnlyDftFile,
                    _originalOpenDraftReadOnlyValue);
            }

        }

        /// <summary>
        /// Load the Open Document picker and force the user to pick an open file.
        /// Returns null if the user does not select anything.
        /// </summary>
        /// <param name="SEAppObj"></param>
        /// <returns></returns>
        internal static SEDocument LoadOpenDocument(Application SEAppObj)
        {
            SEDocument _returnValue = null;

            var _objects = GetOpenDocuments(SEAppObj);

            if (_objects.Count > 0)
            {
                frmOpenFilePicker _ofp = new frmOpenFilePicker(_objects);
                _ofp.ShowDialog();

                if (_ofp.SelectedDocument != null)
                {
                    foreach (var item in _objects)
                    {
                        if (item != _ofp.SelectedDocument)
                            item.Dispose();
                    }

                    LoadBackgroundSheets(_ofp.SelectedDocument);
                }

                _returnValue = _ofp.SelectedDocument;
            }
            else
            {
                throw new Exception("No draft files are open in Solid Edge!");
            }


            return _returnValue;
        }

        /// <summary>
        /// Load all of the background sheets for a particular document.
        /// </summary>
        /// <param name="DocumentObj"></param>
        internal static void LoadBackgroundSheets(SEDocument DocumentObj)
        {
            CustomEvents.OnProgressChanged($"Loading sheets for: {DocumentObj.CombinedListingName}");
            foreach (Sheet _sheet in (DocumentObj.SEDocumentInstance as DraftDocument).Sections.BackgroundSection.Sheets)
            {
                DocumentObj.BackgroundDocumentSheets.Add(_sheet);
            }
            CustomEvents.OnProgressChanged($"{DocumentObj.BackgroundDocumentSheets.Count} total sheets loaded");
        }

    }
}
