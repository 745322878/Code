﻿

#pragma checksum "F:\Microsoft Visual Studio 12.0\Myproject\windows phone 8.1\TouchText\TouchText\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0B1EE786CC9132E91F648AE611074B0E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TouchText
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 46 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerMoved += this.image_PointerMoved;
                 #line default
                 #line hidden
                #line 48 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).ManipulationDelta += this.Grid_ManipulationDelta;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


