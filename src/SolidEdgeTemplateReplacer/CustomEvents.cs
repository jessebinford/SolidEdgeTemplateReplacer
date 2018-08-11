using System;

namespace SolidEdgeTemplateReplacer
{
    class CustomEvents
    {
        internal delegate void ProgressChangedHandler(Object sender, string StatusText);

        internal static event ProgressChangedHandler ProgressChanged;

        internal static void OnProgressChanged(string ProgressMessage)
        {
            ProgressChanged?.Invoke(new object(), ProgressMessage);
        }
    }
}
