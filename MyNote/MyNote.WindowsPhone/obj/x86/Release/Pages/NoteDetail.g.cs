﻿

#pragma checksum "D:\My Soft\WorkingSpace\Apps\My Note\MyNote\MyNote\MyNote.WindowsPhone\Pages\NoteDetail.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "549B0932655BEADB99A58218EACAD770"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyNote.Pages
{
    partial class NoteDetail : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 14 "..\..\..\Pages\NoteDetail.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.appbarNotes_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 15 "..\..\..\Pages\NoteDetail.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.appbarEdit_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 18 "..\..\..\Pages\NoteDetail.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.appbarHelp_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 19 "..\..\..\Pages\NoteDetail.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.appbarExit_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


