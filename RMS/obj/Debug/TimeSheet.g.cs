﻿

#pragma checksum "C:\Users\Joshua\Documents\GitHub\RMS\RMS\TimeSheet.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "03E436617D5ABE3F1DD82B5415A1026D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS
{
    partial class TimeSheet : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 59 "..\..\TimeSheet.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.ProjectComboBox_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 32 "..\..\TimeSheet.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.ButtonSubmit_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


