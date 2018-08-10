using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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

        public string FullFilePath { get; set; }
        public string FileName { get; set; }

        public string CombinedListingName
        {
            get { return $"File: {FileName}   |   File Path: {FullFilePath}"; }
        }
        internal SolidEdgeDocument SEDocumentInstance { get; set; }
        internal List<Sheet> BackgroundDocumentSheets { get; set; }

        private bool _closeOnDispose;
        private bool _saveDocOnClose;

        public void Dispose()
        {
            if (SEDocumentInstance != null)
            {
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

                if (_closeOnDispose)
                {
                    SEDocumentInstance.Close(SaveChanges: _saveDocOnClose);
                }

                Marshal.ReleaseComObject(SEDocumentInstance);
            }
            BackgroundDocumentSheets.Clear();
            
        }
    }
}
