using BigCinch;
using Infragistics.Windows.Ribbon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Manager.Base
{ 
    public class ScreenResolver<TEntity> : ScreenResolver
    {
        public XamRibbon RibbonContainer { get; set; }

        public TEntity Entity { get; set; }

        public WorkspaceData Workspace { get; set; }

        public ObservableCollection<WorkspaceData> ActiveViews { get; set; }
    }

    public class ScreenResolver
    {
        public Guid ScreenId { get; set; }
    }
}
