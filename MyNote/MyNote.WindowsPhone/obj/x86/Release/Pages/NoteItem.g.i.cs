﻿

#pragma checksum "D:\My Soft\WorkingSpace\Apps\My Note\MyNote\MyNote\MyNote.WindowsPhone\Pages\NoteItem.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "75BF9153CC54F8C25481A4E045761223"
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
    partial class NoteItem : global::Windows.UI.Xaml.Controls.Page
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.CommandBar appBar; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton appbarNew; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton appbarSave; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton appbarDelete; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton appbarHome; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.AppBarButton appbarExit; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Primitives.Popup StandardPopup; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button btnLocation; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Button btnLoadImage; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Image imgPhoto; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox txtLatitude; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox txtLongitude; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Maps.MapControl mapLocation; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox txtNote; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TextBox txtSubject; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.TimePicker tpkTime; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.DatePicker dpkDate; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///Pages/NoteItem.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            appBar = (global::Windows.UI.Xaml.Controls.CommandBar)this.FindName("appBar");
            appbarNew = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("appbarNew");
            appbarSave = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("appbarSave");
            appbarDelete = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("appbarDelete");
            appbarHome = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("appbarHome");
            appbarExit = (global::Windows.UI.Xaml.Controls.AppBarButton)this.FindName("appbarExit");
            StandardPopup = (global::Windows.UI.Xaml.Controls.Primitives.Popup)this.FindName("StandardPopup");
            btnLocation = (global::Windows.UI.Xaml.Controls.Button)this.FindName("btnLocation");
            btnLoadImage = (global::Windows.UI.Xaml.Controls.Button)this.FindName("btnLoadImage");
            imgPhoto = (global::Windows.UI.Xaml.Controls.Image)this.FindName("imgPhoto");
            txtLatitude = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("txtLatitude");
            txtLongitude = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("txtLongitude");
            mapLocation = (global::Windows.UI.Xaml.Controls.Maps.MapControl)this.FindName("mapLocation");
            txtNote = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("txtNote");
            txtSubject = (global::Windows.UI.Xaml.Controls.TextBox)this.FindName("txtSubject");
            tpkTime = (global::Windows.UI.Xaml.Controls.TimePicker)this.FindName("tpkTime");
            dpkDate = (global::Windows.UI.Xaml.Controls.DatePicker)this.FindName("dpkDate");
        }
    }
}


