﻿#pragma checksum "..\..\RegisterPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B921A95272427509FBDF570B010AC994ACD3E868E924145CBD714E14FF3C3DFD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Matchmaker;
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


namespace Matchmaker {
    
    
    /// <summary>
    /// RegisterPage
    /// </summary>
    public partial class RegisterPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BackToLogin;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameField;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailField;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordField;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordAgainField;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DayOfBirth;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YearOfBirth;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox MonthOfBirth;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ToSCheckBox;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ErrorGrid;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ErrorMessageImage;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorMessage;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\RegisterPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateAccount;
        
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
            System.Uri resourceLocater = new System.Uri("/Matchmaker;component/registerpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RegisterPage.xaml"
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
            this.BackToLogin = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\RegisterPage.xaml"
            this.BackToLogin.Click += new System.Windows.RoutedEventHandler(this.BackToLogin_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NameField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.EmailField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.PasswordField = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.PasswordAgainField = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            this.DayOfBirth = ((System.Windows.Controls.TextBox)(target));
            
            #line 44 "..\..\RegisterPage.xaml"
            this.DayOfBirth.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.DayOfBirth_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.YearOfBirth = ((System.Windows.Controls.TextBox)(target));
            
            #line 45 "..\..\RegisterPage.xaml"
            this.YearOfBirth.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.DayOfBirth_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.MonthOfBirth = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.ToSCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 10:
            this.ErrorGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 11:
            this.ErrorMessageImage = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            this.ErrorMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.CreateAccount = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\RegisterPage.xaml"
            this.CreateAccount.Click += new System.Windows.RoutedEventHandler(this.CreateAccount_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
