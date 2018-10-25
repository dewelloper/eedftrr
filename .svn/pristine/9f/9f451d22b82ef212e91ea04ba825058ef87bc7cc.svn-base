using BigCinch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Atlas.Efes.Manager.Views
{
    [ViewnameToViewLookupKeyMetadata("ModifyInvoiceInfoView", typeof(ModifyInvoiceInfoView))]
    public partial class ModifyInvoiceInfoView : UserControl, IWorkSpaceAware
    {
        public ModifyInvoiceInfoView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty WorkSpaceContextualDataProperty =
           DependencyProperty.Register("WorkSpaceContextualData", typeof(object), typeof(ModifyInvoiceInfoView),
               new FrameworkPropertyMetadata((WorkspaceData)null));

        public WorkspaceData WorkSpaceContextualData
        {
            get { return (WorkspaceData)GetValue(WorkSpaceContextualDataProperty); }
            set { SetValue(WorkSpaceContextualDataProperty, value); }
        }
    }
}
