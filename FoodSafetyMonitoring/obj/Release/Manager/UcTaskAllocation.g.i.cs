﻿#pragma checksum "..\..\..\Manager\UcTaskAllocation.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FE0C78529A7228CB4553DB7F48547B90"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using FoodSafetyMonitoring.Manager.UserControls;
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
    /// UcTaskAllocation
    /// </summary>
    public partial class UcTaskAllocation : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Manager\UcTaskAllocation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl _tabControl;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Manager\UcTaskAllocation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock _dept_name;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Manager\UcTaskAllocation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox _detect_station;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Manager\UcTaskAllocation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid _grid_detail;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Manager\UcTaskAllocation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtMsg;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Manager\UcTaskAllocation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Manager\UcTaskAllocation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FoodSafetyMonitoring.Manager.UserControls.UcTableOperableView _tableview;
        
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
            System.Uri resourceLocater = new System.Uri("/ZRDFSSystem;component/manager/uctaskallocation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Manager\UcTaskAllocation.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this._tabControl = ((System.Windows.Controls.TabControl)(target));
            
            #line 21 "..\..\..\Manager\UcTaskAllocation.xaml"
            this._tabControl.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this._tabControl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this._dept_name = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this._detect_station = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this._grid_detail = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.txtMsg = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\Manager\UcTaskAllocation.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this._tableview = ((FoodSafetyMonitoring.Manager.UserControls.UcTableOperableView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

