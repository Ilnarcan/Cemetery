﻿#pragma checksum "..\..\..\EasterEgg.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "735BD9C41D2B44F88D324EC8DF33DBE0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18052
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Cemetery {
    
    
    /// <summary>
    /// EasterEgg
    /// </summary>
    public partial class EasterEgg : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\EasterEgg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image1;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\EasterEgg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image2;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\EasterEgg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image3;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Cemetery;component/easteregg.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\EasterEgg.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Image1 = ((System.Windows.Controls.Image)(target));
            
            #line 11 "..\..\..\EasterEgg.xaml"
            this.Image1.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.image1_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Image2 = ((System.Windows.Controls.Image)(target));
            
            #line 12 "..\..\..\EasterEgg.xaml"
            this.Image2.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.image2_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Image3 = ((System.Windows.Controls.Image)(target));
            
            #line 13 "..\..\..\EasterEgg.xaml"
            this.Image3.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.image3_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
