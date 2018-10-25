using Atlas.Efes.Common.GIB;
using Atlas.Efes.Manager.Base;
using BigCinch;
using MEFedMVVM.ViewModelLocator;
using System.ComponentModel.Composition;
using System.Linq;

namespace Atlas.Efes.Manager.ViewModels
{

    [ExportViewModel("ModifyInvoiceInfoViewModel")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ModifyInvoiceInfoViewModel : ModifyViewModelBase<InvoiceInfo>
    {

        private decimal exchangeRateInfo;

        public decimal ExchangeRateInfo
        {
            get { return exchangeRateInfo; }
            set
            {
                exchangeRateInfo = value;
                NotifyPropertyChanged("ExchangeRateInfo");
            }
        }

        public override void OnSave()
        {
            base.OnSave();
        }

        [ImportingConstructor]
        public ModifyInvoiceInfoViewModel(IViewAwareStatus status)
            : base(status)
        {
            
        }


        public override void CreateContextualTabGroup()
        {
            base.CreateContextualTabGroup();
            var contextualTab = ContextualTabGroup.TabItems.Where(f => f.ViewKey == ScreenTransactionId).FirstOrDefault();
        }
    }
}
