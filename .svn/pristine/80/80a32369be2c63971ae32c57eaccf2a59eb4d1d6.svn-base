using Atlas.Efes.Common.GIB;
using Atlas.Efes.Common.Model;
using Atlas.Efes.Core;
using Atlas.Efes.Manager.Base;
using BigCinch;
using MEFedMVVM.ViewModelLocator;
using System.ComponentModel.Composition;
using System.Reflection;

namespace Atlas.Efes.Manager.ViewModels
{
    [ExportViewModel("MasterWindowViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MasterWindowViewModel : TViewModelBase<UserInfo>
    {

        public SimpleCommand<object, object> CreateInvoiceCommand { get; set; }

        private string appVersion;

        public string AppVersion
        {
            get { return appVersion; }
            set
            {
                appVersion = value;
                NotifyPropertyChanged("AppVersion");
            }
        }



        [ImportingConstructor]
        public MasterWindowViewModel(IViewAwareStatus viewAwareStatus)
            : base(viewAwareStatus)
        {
            AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            viewAwareStatus.ViewUnloaded += () =>
            {

            };
            CreateInvoiceCommand = new SimpleCommand<object, object>((param) =>
            {
                ScreenResolver<InvoiceInfo> screenResolver = new ScreenResolver<InvoiceInfo>
                {
                    Entity = new InvoiceInfo()
                    {
                        UUID = AppHelper.GetGuid(),
                        AccountingCustomerPartyInfo = new AccountingCustomerPartyInfo()
                        {
                            Party = new PartyInfo()
                            {
                                PartyIdentifications =
                                {
                                    new PartyIdentificationInfo()
                                    {
                                        ID=new IDContainerInfo()
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    },
                    RibbonContainer = this.RibbonContainer,
                    ActiveViews = Views,
                };

                ExecuteScreen<InvoiceInfo>(screenResolver, "ModifyInvoiceInfoView", "Create Invoice");
            });
        }

        public override void OnViewLoaded()
        {
            dynamic view = ViewAwareStatus.View;
            RibbonContainer = view.RibbonContainer;
        }
    }
}
