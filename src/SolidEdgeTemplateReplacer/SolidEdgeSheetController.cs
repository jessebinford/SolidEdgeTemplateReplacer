using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidEdgeDraft;

namespace SolidEdgeTemplateReplacer
{
    class SolidEdgeSheetController
    {
        internal static void BackgroundReplacer(List<SheetReplacement> sheetReplacement, SEDocument TargetDocument,
            SEDocument TemplateDocument, SolidEdgeFramework.Application SEAppObj)
        {
            foreach (var item in sheetReplacement)
            {
                CustomEvents.OnProgressChanged($"Replacing background for target sheet: {item.TargetSheet.Name} with background sheet: {item.TemplateSheet.Name}");
                TargetDocument.SEDocumentInstance.Activate();
                SEAppObj.DoIdle();
                
                item.TargetSheet.Activate();
                SEAppObj.DoIdle();
                
                TargetDocument.SEDocumentInstance.SelectSet.AddAll();
                TargetDocument.SEDocumentInstance.SelectSet.Delete();
                TargetDocument.SEDocumentInstance.SelectSet.RemoveAll();
                
                TemplateDocument.SEDocumentInstance.Activate();
                SEAppObj.DoIdle();
                
                item.TemplateSheet.Activate();
                SEAppObj.DoIdle();
                
                TemplateDocument.SEDocumentInstance.SelectSet.AddAll();
                TemplateDocument.SEDocumentInstance.SelectSet.Copy();
                TemplateDocument.SEDocumentInstance.SelectSet.RemoveAll();

                
                TargetDocument.SEDocumentInstance.Activate();
                (SEAppObj.ActiveWindow as dynamic).Paste();
            }

            CustomEvents.OnProgressChanged("Replacement done!");
        }

        /*
        public bool ProcessSheetReplacement(DraftDocument TargetDocument, SolidEdgeFramework.Application objSEApp, SolidEdgeDraft.DraftDocument TemplateDocument)
        {
            FoundMatchingBGSheet = new Hashtable();
            FoundMatchingBGSheet.Clear();
            bool functionReturnValue = false;

            double dblTol = 0;
            bool blnOKToCopyFromClipboard = false;


            functionReturnValue = false;
            // dblTol = 0.00001
            try
            {
                foreach (SolidEdgeDraft.Sheet objBGSheet in objdoc.Sections.BackgroundSection.Sheets)
                {
                    blnOKToCopyFromClipboard = false;
                    objdoc.Activate();
                    objSEApp.ActiveWindow.fit();
                    objSEApp.DoIdle();
                    string strBGSheetNameFromDocBeingProcessed = objBGSheet.Name;
                    //MessageBox.Show(objBGSheet.Name);
                    objBGSheet.Activate();
                    objSEApp.ActiveWindow.fit();
                    objSEApp.DoIdle();
                    objdoc.SelectSet.AddAll();
                    objdoc.SelectSet.Delete();
                    objdoc.SelectSet.RemoveAll();
                    objTemplate.Activate();
                    // switch to the new template file
                    objSEApp.ActiveWindow.fit();
                    objSEApp.DoIdle();
                    foreach (SolidEdgeDraft.Sheet objTemplateBGSheet in objTemplate.Sections.BackgroundSection.Sheets)
                    {
                        functionReturnValue = false;
                        MessageBox.Show(objTemplateBGSheet.Name + " " + objBGSheet.Name);
                        if (objTemplateBGSheet.Name == strBGSheetNameFromDocBeingProcessed)
                        {
                            objTemplateBGSheet.Activate();
                            objSEApp.ActiveWindow.fit();
                            objSEApp.DoIdle();
                            objTemplate.SelectSet.AddAll();
                            if (objTemplate.SelectSet.Count != 0)
                            {
                                blnOKToCopyFromClipboard = true;
                            }
                            objTemplate.SelectSet.Copy();
                            objTemplate.SelectSet.RemoveAll();
                            oReleaseObject(objTemplateBGSheet);
                            functionReturnValue = true;
                            FoundMatchingBGSheet.Add(strBGSheetNameFromDocBeingProcessed, "Found");
                            break; // TODO: might not be correct. Was : Exit For
                        }

                    }

                    if (functionReturnValue == false)
                    {
                        FoundMatchingBGSheet.Add(strBGSheetNameFromDocBeingProcessed, "Not Found");
                        goto skiptoHere;
                    }

                    objdoc.Activate();
                    objSEApp.ActiveWindow.fit();
                    objSEApp.DoIdle();
                    if (blnOKToCopyFromClipboard == true)
                    {
                        objSEApp.ActiveWindow.Paste();
                    }
                    skiptoHere:

                    //at this point clipboard still contains the stuff that was pasted.  really need to remove it from the clipboard.
                    //My.Computer.Clipboard.Clear();
                    oReleaseObject(objBGSheet);
                    functionReturnValue = false;
                }

                objdoc.Sections.WorkingSection.Sheets.Item(1).Activate();
                objSEApp.ActiveWindow.fit();
                objSEApp.DoIdle();
                objSEApp.ActiveWindow.displaybackgroundsheettabs = false;


                objTemplate.Close(false);
                oReleaseObject(objTemplate);
                oReleaseObject(objdoc);
                oReleaseObject(objSEApp);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
                functionReturnValue = false;
                objTemplate.Close(false);
                oReleaseObject(objTemplate);
            }

            return functionReturnValue;


        }
        */
    }
}
