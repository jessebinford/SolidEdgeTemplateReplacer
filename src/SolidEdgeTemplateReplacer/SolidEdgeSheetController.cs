using System.Collections.Generic;

namespace SolidEdgeTemplateReplacer
{
    class SolidEdgeSheetController
    {
        /// <summary>
        /// Perform the actual background sheet replacement.
        /// </summary>
        /// <param name="sheetReplacement"></param>
        /// <param name="TargetDocument"></param>
        /// <param name="TemplateDocument"></param>
        /// <param name="SEAppObj"></param>
        internal static void BackgroundReplacer(List<SheetReplacement> sheetReplacement, SEDocument TargetDocument,
            SEDocument TemplateDocument, SolidEdgeFramework.Application SEAppObj)
        {
            foreach (var item in sheetReplacement)
            {
                CustomEvents.OnProgressChanged($"Replacing background for target sheet: {item.TargetSheet.Name} with background sheet: {item.TemplateSheet.Name}");
                // Activate the target document
                TargetDocument.SEDocumentInstance.Activate();
                SEAppObj.DoIdle();
                
                // Active the target sheet where we want to paste our new template and clear it out
                item.TargetSheet.Activate();
                SEAppObj.DoIdle();
                
                TargetDocument.SEDocumentInstance.SelectSet.AddAll();
                TargetDocument.SEDocumentInstance.SelectSet.Delete();
                TargetDocument.SEDocumentInstance.SelectSet.RemoveAll();


                // Fetch the template background
                TemplateDocument.SEDocumentInstance.Activate();
                SEAppObj.DoIdle();

                item.TemplateSheet.Activate();
                SEAppObj.DoIdle();
                
                TemplateDocument.SEDocumentInstance.SelectSet.AddAll();
                TemplateDocument.SEDocumentInstance.SelectSet.Copy();
                TemplateDocument.SEDocumentInstance.SelectSet.RemoveAll();

                

                // Paste our copied background into the target doc/sheet
                TargetDocument.SEDocumentInstance.Activate();
                (SEAppObj.ActiveWindow as dynamic).Paste();

                if (ReplacementSettings.ResetTargetDraftToPageOneOnClose)
                {
                    // Reset the target document to sheet 1
                    try
                    {
                        (TargetDocument.SEDocumentInstance as SolidEdgeDraft.DraftDocument).Sections.WorkingSection.Sheets.Item(1).Activate();
                    }
                    catch
                    { }
                }
            }

            CustomEvents.OnProgressChanged("Replacement done!");
        }
    }
}
