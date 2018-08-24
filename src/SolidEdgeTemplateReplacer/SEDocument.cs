using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SolidEdgeDraft;
using SolidEdgeFramework;

namespace SolidEdgeTemplateReplacer
{
    class SEDocument : IDisposable
    {
        public SEDocument(string FullPath, bool CloseOnDispose, bool SaveDocOnClose)
        {
            FullFilePath = FullPath;
            BackgroundDocumentSheets = new List<Sheet>();
            _closeOnDispose = CloseOnDispose;
            _saveDocOnClose = SaveDocOnClose;
        }

        // Full file path to the underlining file
        public string FullFilePath { get; set; }

        // File name
        public string FileName { get; set; }

        // Used for SEEC Mode
        internal string ItemID { get; set; }

        // Used for SEEC Mode
        internal string RevID { get; set; }
        
        internal SolidEdgeDocument SEDocumentInstance { get; set; }

        internal List<Sheet> BackgroundDocumentSheets { get; set; }

        private bool _closeOnDispose;
        private bool _saveDocOnClose;


        // Used for display purposes in a couple screens
        public string CombinedListingName
        {
            get { return $"File: {FileName}   |   File Path: {FullFilePath}"; }
        }

        public void Dispose()
        {
            if (SEDocumentInstance != null)
            {

                // Dispose Sheets
                if (BackgroundDocumentSheets.Count > 0)
                {
                    BackgroundDocumentSheets.ForEach((Sheet _sheet) =>
                    {
                        if (_sheet != null)
                        {
                            Marshal.ReleaseComObject(_sheet);
                        }
                    });
                }
                BackgroundDocumentSheets.Clear();

                // Close Doc?
                if (_closeOnDispose)
                {
                    SEDocumentInstance.Close(SaveChanges: _saveDocOnClose);
                }

                Marshal.ReleaseComObject(SEDocumentInstance);
            }
            
        }
    }
}
