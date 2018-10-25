using Atlas.Efes.Manager.Helper;
using Atlas.Efes.Manager.Menu;
using BigCinch;
using Microsoft.Practices.Prism.Commands;

namespace Atlas.Efes.Manager.Base
{
    public class ModifyViewModelBase<TEntity> : TViewModelBase<TEntity>
    {
        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand CloseViewCommand { get; set; }

        public ModifyViewModelBase(IViewAwareStatus viewAware)
            : base(viewAware)
        {
            SaveCommand = new DelegateCommand(OnSave);

            CloseViewCommand = new DelegateCommand(() =>
            {
                this.CloseView();
            });

        }



        public virtual void OnSave()
        {
            
        }

        public override void CreateContextualTabGroup()
        {
            
            MenuTabItem operationTabItem = new MenuTabItem
            {
                ViewKey = ScreenTransactionId,
                Text = "User Operation",
            };

            MenuTabGroup operationTabGroup = new MenuTabGroup()
            {
                Text = "User Operation",
            };

            operationTabGroup.Buttons.Add(new MenuButton
            {
                Text = "Save",
                Icon = ResourceHelper.GetResource(@"Images\save.png"),
                Command = SaveCommand,
            });

            operationTabItem.Groups.Add(operationTabGroup);
            //Setting ContextualTabGroup properties
            ContextualTabGroup.TabKey = ScreenTransactionId;
            ContextualTabGroup.Text = string.Format("Transaction Tab\n{0}", ScreenEventResolver.Workspace.DisplayText);
            ContextualTabGroup.TabItems.Add(operationTabItem);
            
        }
    }
}
