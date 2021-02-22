using Caliburn.Micro;
using Klient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Klient.ViewModels
{
    class ModifyDataViewModel : Screen
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        private string _producer;
        public string Producer
        {
            get { return _producer; }
            set { 
                _producer = value;
                NotifyOfPropertyChange(() => Producer);
                }
        }
        private long _ean;
        public long Ean
        {
            get { return _ean; }
            set { 
                _ean = value;
                NotifyOfPropertyChange(() => Ean);
            }
        }
        private string _type;
        public string Type
        {
            get { return _type; }
            set { 
                _type = value;
                NotifyOfPropertyChange(() => Type);
            }
        }
        private float _price;
        public float Price
        {
            get { return _price; }
            set {
                _price = value;
                NotifyOfPropertyChange(() => Price);
                }
        }
        private int _vat;
        public int Vat
        {
            get { return _vat; }
            set { _vat = value;
                if (value < 0 && value > 100)
                    value = 23;
                NotifyOfPropertyChange(() => Vat);
            }
        }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value;
                NotifyOfPropertyChange(() => Quantity);
            }
        }
        private long _eanToDelete;
        public long EanToDelete
        {
            get { return _eanToDelete; }
            set { _eanToDelete = value;
                NotifyOfPropertyChange(() => EanToDelete);
                }
        }
        private int _quantityToDelete;
        public int QuantityToDelete
        {
            get { return _quantityToDelete; }
            set {
                _quantityToDelete = value;
                NotifyOfPropertyChange(() => QuantityToDelete);
            }
        }
        public ModifyDataViewModel()
        {
            ApiConnectModel.InitializeClient();
        }
        public void AddToBase()
        { 
            List<string> types = new List<string>();
            types.Add(Type);
            ProductsModel pm = new ProductsModel(Name, Ean, Producer, types, Quantity, Price, Vat);
            ApiConnectModel.insertProducts(pm);
            MessageBox.Show("Zrobione!");
        }
        public void DeleteFromBase()
        {
            ApiConnectModel.DeleteProduct(EanToDelete);
        }

    }
}
