using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidEdgeFramework;
using SolidEdgeCommunity;
using SolidEdgeDraft;

namespace SolidEdgeTemplateReplacer
{
    class SolidEdgeDocumentController
    {

        internal static bool HasOpenDocuments(Application SEAppObj)
        {
            return SEAppObj.Documents.Count > 0;
        }

        internal static List<SEDocument> GetOpenDocuments(Application SEAppObj)
        {
            List<SEDocument> _returnList = new List<SEDocument>();

            foreach (SolidEdgeDocument item in SEAppObj.Documents)
            {
                if (item.Type == DocumentTypeConstants.igDraftDocument)
                {
                    _returnList.Add(new SEDocument(item.FullName, false, false) { FileName = item.Name, SEDocumentInstance = item });
                }
            }

            return _returnList;
        }

        internal static void OpenDocument(Application SEAppObj, SEDocument DocumentObj, bool OpenDraftReadOnly)
        {
            SEAppObj.DisplayAlerts = false;
            object _originalOpenDraftReadOnlyValue = null;
            object _fileReadOnlySetting = OpenDraftReadOnly;
            try
            {
                SEAppObj.GetGlobalParameter(ApplicationGlobalConstants.seApplicationGlobalOpenAsReadOnlyDftFile,
                   _originalOpenDraftReadOnlyValue);

                SEAppObj.SetGlobalParameter(ApplicationGlobalConstants.seApplicationGlobalOpenAsReadOnlyDftFile,
                    _fileReadOnlySetting);

                CustomEvents.OnProgressChanged($"Opening document: {DocumentObj.FullFilePath}");

                DocumentObj.SEDocumentInstance = (SolidEdgeDocument) SEAppObj.Documents.Open(DocumentObj.FullFilePath);
                DocumentObj.FileName = DocumentObj.SEDocumentInstance.Name;
                LoadSheets(DocumentObj);
            }
            catch
            {

            }
            finally
            {
                SEAppObj.DisplayAlerts = true;
                SEAppObj.SetGlobalParameter(ApplicationGlobalConstants.seApplicationGlobalOpenAsReadOnlyDftFile,
                    _originalOpenDraftReadOnlyValue);
            }

        }

        internal static SEDocument LoadOpenDocument(Application SEAppObj)
        {
            bool _returnValue = false;

            var _objects = GetOpenDocuments(SEAppObj);

            frmOpenFilePicker _ofp = new frmOpenFilePicker(_objects);
            _ofp.ShowDialog();

            if (_ofp.SelectedDocument != null)
            {
                foreach (var item in _objects)
                {
                    if (item != _ofp.SelectedDocument)
                        item.Dispose();
                }

                LoadSheets(_ofp.SelectedDocument);
            }


            return _ofp.SelectedDocument;
        }

        internal static void LoadSheets(SEDocument DocumentObj)
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
