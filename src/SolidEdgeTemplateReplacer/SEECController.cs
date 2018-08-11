using System;
using System.Runtime.InteropServices;
using SolidEdgeFramework;

namespace SolidEdgeTemplateReplacer
{
    class SEECController : IDisposable
    {
        private SolidEdgeTCE _seec;

        internal SolidEdgeTCE SEEC => _seec;

        public SEECController(SolidEdgeFramework.Application SeAppObj)
        {
            _seec = SeAppObj.SolidEdgeTCE;
        }

        /// <summary>
        /// Returns a bool to tell you if we are in SEEC mode in SE.
        /// </summary>
        /// <returns></returns>
        internal bool IsInSEECMode()
        {
            bool _inTCMode;
            _seec.GetTeamCenterMode(out _inTCMode);
            return _inTCMode;
        }

        /// <summary>
        /// Performs a SEEC query to Teamcenter to determine if there are any draft files found for the ItemID / Rev.
        /// Document.FullFileName will not be empty if it found one draft file to open.
        /// </summary>
        /// <param name="Document"></param>
        internal void FetchTeamcenterFilesForItem(SEDocument Document)
        {
            object _listOfFilenamesInTC = null;
            int _totalFilesFoundInTC = 0;
            int _totalDraftsFound = 0;

            try
            {
                CustomEvents.OnProgressChanged($"Searching TC for {Document.ItemID};{Document.RevID}!");
                SEEC.GetListOfFilesFromTeamcenterServer(Document.ItemID, Document.RevID, out _listOfFilenamesInTC, out _totalFilesFoundInTC);

                foreach (object file in (object[])_listOfFilenamesInTC)
                {
                    if (file.ToString().EndsWith(".dft", StringComparison.CurrentCultureIgnoreCase))
                    {
                        _totalDraftsFound++;
                        Document.FullFilePath = file.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomEvents.OnProgressChanged($"SEEC Error: {ex.Message}");
            }

            CustomEvents.OnProgressChanged($"Total drafts found from TC: {_totalDraftsFound}");
            if (_totalDraftsFound > 1)
            {
                // Clearing out the file path since we had too many draft results
                // this will cause it to fail out on return...
                Document.FullFilePath = string.Empty;
            }
        }

        /// <summary>
        /// Performs the download of the Teamcenter file into the local cache.
        /// After download has finished, it will adjust the Document.FullFilePath to have the extended file path to the file in the cache.
        /// </summary>
        /// <param name="Document"></param>
        internal void DownloadTCFileToCache(SEDocument Document)
        {
            string _pdmCachePath = string.Empty;
            // get the SEEC managed path
            SEEC.GetPDMCachePath(out _pdmCachePath);

            try
            {
                CustomEvents.OnProgressChanged($"Downloading template document {Document.FullFilePath} from TC to local SEEC cache");
                SEEC.CheckOutDocumentsFromTeamCenterServer(Document.ItemID, Document.RevID, true, Document.FullFilePath, DocumentDownloadLevel.SEECDownloadFirstLevel);
            }
            catch (Exception ex)
            {
                CustomEvents.OnProgressChanged($"SEEC Error: {ex.Message}");
                Document.FullFilePath = string.Empty;
                return;
            }

            Document.FullFilePath = System.IO.Path.Combine(_pdmCachePath, Document.FullFilePath);
        }

        public void Dispose()
        {
            if (_seec != null)
            {
                Marshal.ReleaseComObject(_seec);
            }
        }

        ~SEECController()
        {
            Dispose();
        }
    }
}
