﻿#pragma checksum "..\..\NewUser.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F4B56AD6378374B81D9671EEF9322BBB1390B167"
//------------------------------------------------------------------------------
// <auto-generated>
//     Bu kod araç tarafından oluşturuldu.
//     Çalışma Zamanı Sürümü:4.0.30319.42000
//
//     Bu dosyada yapılacak değişiklikler yanlış davranışa neden olabilir ve
//     kod yeniden oluşturulursa kaybolur.
// </auto-generated>
//------------------------------------------------------------------------------

using ProductEvalation;
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


namespace ProductEvalation {
    
    
    /// <summary>
    /// NewUser
    /// </summary>
    public partial class NewUser : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCon;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtName;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTelNo;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPr;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDis;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPostaKod;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAdress;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMail;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtPass1;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\NewUser.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtPass2;
        
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
            System.Uri resourceLocater = new System.Uri("/ProductEvalation;component/newuser.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewUser.xaml"
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
            this.btnCon = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\NewUser.xaml"
            this.btnCon.Click += new System.Windows.RoutedEventHandler(this.BtnCon_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtTelNo = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\NewUser.xaml"
            this.txtTelNo.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TxtTelNo_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtPr = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtDis = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtPostaKod = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\NewUser.xaml"
            this.txtPostaKod.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TxtTelNo_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.txtAdress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtMail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.txtPass1 = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 10:
            this.txtPass2 = ((System.Windows.Controls.PasswordBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
