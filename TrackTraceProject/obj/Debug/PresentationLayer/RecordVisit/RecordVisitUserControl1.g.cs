﻿#pragma checksum "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CCD9760652618955EC04D8F2FF1E4906C2F6B01EB7CEAA47BBB9EE7FB6F6AB73"
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
using TrackTraceProject.PresentationLayer.RecordVisit;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Converters;
using Xceed.Wpf.Toolkit.Core;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Mag.Converters;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace TrackTraceProject.PresentationLayer.RecordVisit {
    
    
    /// <summary>
    /// RecordVisitUserControl1
    /// </summary>
    public partial class RecordVisitUserControl1 : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Lbl_Individual;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBox_Individual;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Lbl_Location;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBox_Location;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Lbl_DateOfContact;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.DateTimePicker DateTimePicker_DateTime;
        
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
            System.Uri resourceLocater = new System.Uri("/TrackTraceProject;component/presentationlayer/recordvisit/recordvisitusercontrol" +
                    "1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml"
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
            this.Lbl_Individual = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.ListBox_Individual = ((System.Windows.Controls.ListBox)(target));
            
            #line 12 "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml"
            this.ListBox_Individual.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBox_Individual_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Lbl_Location = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.ListBox_Location = ((System.Windows.Controls.ListBox)(target));
            
            #line 15 "..\..\..\..\PresentationLayer\RecordVisit\RecordVisitUserControl1.xaml"
            this.ListBox_Location.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBox_Location_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Lbl_DateOfContact = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.DateTimePicker_DateTime = ((Xceed.Wpf.Toolkit.DateTimePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

