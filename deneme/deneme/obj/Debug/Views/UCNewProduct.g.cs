﻿#pragma checksum "..\..\..\Views\UCNewProduct.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "854A3E4D03D9E0BE33DE9F2D7AE4F196417253F7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using deneme.Views;


namespace deneme.Views {
    
    
    /// <summary>
    /// UCNewProduct
    /// </summary>
    public partial class UCNewProduct : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\Views\UCNewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnadd;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\UCNewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCompanyName;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\UCNewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtProductName;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\UCNewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSellPrice;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Views\UCNewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSubCategory;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Views\UCNewProduct.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtgridProduct;
        
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
            System.Uri resourceLocater = new System.Uri("/deneme;component/views/ucnewproduct.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\UCNewProduct.xaml"
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
            this.btnadd = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Views\UCNewProduct.xaml"
            this.btnadd.Click += new System.Windows.RoutedEventHandler(this.Btnadd_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtCompanyName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtProductName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtSellPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.cbSubCategory = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.dtgridProduct = ((System.Windows.Controls.DataGrid)(target));
            
            #line 34 "..\..\..\Views\UCNewProduct.xaml"
            this.dtgridProduct.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.DtgridProduct_AutoGeneratingColumn);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

