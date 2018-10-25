using Atlas.Efes.Manager.Menu;
using BigCinch;
using Infragistics.Windows.Ribbon;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Atlas.Efes.Manager.Base
{
    public class TViewModelBase<TEntity> : ViewModelBase
    {
        public XamRibbon RibbonContainer { get; set; }

        private TEntity entity;

        public TEntity Entity
        {
            get { return entity; }
            set
            {
                entity = value;
                NotifyPropertyChanged("Entity");
            }
        }


        public IViewAwareStatus ViewAwareStatus { get; set; }

        public Guid ScreenTransactionId { get; set; }

        public ScreenResolver<TEntity> ScreenEventResolver { get; set; }

        private MenuContextualTabGroup contextualTabGroup = new MenuContextualTabGroup();
        public MenuContextualTabGroup ContextualTabGroup
        {
            get { return contextualTabGroup; }
            set
            {
                contextualTabGroup = value;
                NotifyPropertyChanged("ContextualTabGroup");
            }
        }

        private ObservableCollection<MenuTabItem> processTabs = new ObservableCollection<MenuTabItem>();
        public ObservableCollection<MenuTabItem> ProcessTabs
        {
            get { return processTabs; }
            set
            {
                processTabs = value;
                NotifyPropertyChanged("ProcessTabs");
            }
        }


        public TViewModelBase(IViewAwareStatus status)
        {
            ViewAwareStatus = status;
            status.ViewLoaded += OnViewLoaded;
            status.ViewUnloaded += OnViewUnLoaded;
            ScreenTransactionId = Guid.NewGuid();

        }

        void ViewModelBase_TabChangedEventHandler(object sender, EventArgs e)
        {

        }

        public virtual void CreateContextualTabGroup()
        {

        }


        public void SetSelectedTab()
        {
            if (ScreenEventResolver == null)
            {
                return;
            }

            ScreenManager.SetSelectedTab(ScreenEventResolver.RibbonContainer, ScreenTransactionId, true);
        }



        public virtual void OnViewLoaded()
        {
            IWorkSpaceAware workspaceData = (IWorkSpaceAware)ViewAwareStatus.View;
            ScreenEventResolver = (ScreenResolver<TEntity>)workspaceData.WorkSpaceContextualData.DataValue;

            CreateContextualTabGroup();

            if (ScreenEventResolver == null)
            {
                throw new NotImplementedException("View has not an event parameter");
            }


            ScreenManager.AddOperationItemToTab(ScreenEventResolver.RibbonContainer, ContextualTabGroup);

            if (ScreenEventResolver.Entity != null)
            {
                Entity = ScreenEventResolver.Entity;
            }

            if (ScreenEventResolver.ActiveViews != null && ScreenEventResolver.ActiveViews.Count != 0)
            {
                Views = ScreenEventResolver.ActiveViews;
            }

            SetSelectedTab();
        }



        public virtual void ExecuteScreen<TParameter>(ScreenResolver<TParameter> screenParameter, string viewLookupKey = null, string displayText = null, string title = null, string imagePath = null) where TParameter : class
        {
            int count = Views.Where(f => f.ViewLookupKey == viewLookupKey).Count();

            if (count != 0)
            {
                displayText = string.Format(displayText + "(" + "{0}" + ")", count);
            }

            WorkspaceData workspaceItem = null;

            if (ScreenEventResolver == null)
            {
                workspaceItem = new WorkspaceData(imagePath, viewLookupKey, screenParameter, displayText, ScreenTransactionId, true, screenParameter.RibbonContainer, string.Empty);
            }
            else
            {
                workspaceItem = new WorkspaceData(imagePath, viewLookupKey, screenParameter, displayText, ScreenTransactionId, true, ScreenEventResolver.RibbonContainer, string.Empty);
            }




            screenParameter.Workspace = workspaceItem;
            Views.Add(workspaceItem);
            SetActiveWorkspace(workspaceItem);
        }



        public virtual void CloseView(bool param = true)
        {
            if (param)
            {
                ScreenEventResolver.ActiveViews.Remove(ScreenEventResolver.Workspace);
            }
        }


        void OnViewUnLoaded()
        {
            ScreenManager.RemoveOperationTab(ScreenTransactionId, ScreenEventResolver.RibbonContainer);
        }
    }


}
