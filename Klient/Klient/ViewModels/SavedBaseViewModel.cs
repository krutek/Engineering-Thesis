using Klient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Globalization;

namespace Klient.ViewModels
{
    public class SavedBaseViewModel : Screen
    {
        BindableCollection<ProductsModel> _bindable = new BindableCollection<ProductsModel>();
        private float _raportMoneyField;
        public float RaportMoneyField
        {
            get { return _raportMoneyField; }
            set { 
                _raportMoneyField = value;
                NotifyOfPropertyChange(() => RaportMoneyField);
            }
        }
        private int _raportCountTransactionField;
        public int RaportCountTransactionField
        {
            get { return _raportCountTransactionField; }
            set { _raportCountTransactionField = value;
                NotifyOfPropertyChange(() => RaportCountTransactionField);
            }
        }
        private int _raportAllSold;
        public int RaportAllSold
        {
            get { return _raportAllSold; }
            set { _raportAllSold = value;
                NotifyOfPropertyChange(() => RaportAllSold);
            } 
        }
        public BindableCollection<ProductsModel> Bindable
        {
            get { return _bindable; }
            set { _bindable = value; }
        }
        public SavedBaseViewModel()
        {
            ApiConnectModel.InitializeClient();
            toBindable();
        }
        public BindableCollection<ProductsModel> toBindable()
        {
            foreach (var item in ApiConnectModel.GetAllSoldProducts().Result)
            {
                Bindable.Add(item);
            }
            return Bindable;
        }
        public void generateRaportThisMonth()
        {
            string s = ApiConnectModel.returnSumOfPricesNow().Result;
            float f = (float)double.Parse(s, System.Globalization.CultureInfo.InvariantCulture);

            float truncated = (float)(Math.Truncate((double)f * 100.0) / 100.0);

            float rounded = (float)(Math.Round((double)f, 2));
            RaportMoneyField = rounded;
            RaportCountTransactionField = ApiConnectModel.ReturnSoldCountAll().Result;
            RaportAllSold = ApiConnectModel.ReturnSoldAllTransaction().Result;
        }


    }
}
