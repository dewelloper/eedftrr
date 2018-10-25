using BigCinch;
using Infragistics.Windows.Ribbon;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Atlas.Efes.Manager
{
    [ViewnameToViewLookupKeyMetadata("MasterWindow", typeof(MasterWindow))]
    public partial class MasterWindow : Window, IWorkSpaceAware
    {
        public MasterWindow()
        {
            InitializeComponent();

            RibbonContainer = this.ribbonMenu;

        }

        [Import]
        public XamRibbon RibbonContainer { get; set; }

        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
           DependencyProperty.Register("WorkSpaceContextualData", typeof(object), typeof(MasterWindow),
               new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }

    }
}
