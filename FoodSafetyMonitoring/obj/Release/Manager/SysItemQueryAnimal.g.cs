﻿#pragma checksum "..\..\..\Manager\SysItemQueryAnimal.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3FBA2500EFECA609C215C2068B501F18"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using FoodSafetyMonitoring.Manager.UserControls;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace FoodSafetyMonitoring.Manager {
    
    
    /// <summary>
    /// SysItemQueryAnimal
    /// </summary>
    public partial class SysItemQueryAnimal : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 73 "..\..\..\Manager\SysItemQueryAnimal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox _item_name;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\Manager\SysItemQueryAnimal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _query;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\Manager\SysItemQueryAnimal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock _hj;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\Manager\SysItemQueryAnimal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock _title;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\Manager\SysItemQueryAnimal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock _sj;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\Manager\SysItemQueryAnimal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button _new;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\Manager\SysItemQueryAnimal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_info;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\Manager\SysItemQueryAnimal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvlist;
        
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
            System.Uri resourceLocater = new System.Uri("/ZRDFSSystem;component/manager/sysitemqueryanimal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Manager\SysItemQueryAnimal.xaml"
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
            this._item_name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this._query = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\..\Manager\SysItemQueryAnimal.xaml"
            this._query.Click += new System.Windows.RoutedEventHandler(this._query_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this._hj = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this._title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this._sj = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this._new = ((System.Windows.Controls.Button)(target));
            
            #line 105 "..\..\..\Manager\SysItemQueryAnimal.xaml"
            this._new.Click += new System.Windows.RoutedEventHandler(this._new_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.grid_info = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.lvlist = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 9:
            
            #line 119 "..\..\..\Manager\SysItemQueryAnimal.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this._btn_modify_Click);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 126 "..\..\..\Manager\SysItemQueryAnimal.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this._btn_delete_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

