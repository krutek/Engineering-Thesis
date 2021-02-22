using Caliburn.Micro;
using Klient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klient.ViewModels
{
    public class ActualBaseViewModel : Screen
    {
        BindableCollection<ProductsModel> _bindable = new BindableCollection<ProductsModel>();
        BindableCollection<string> _bindableTypes = new BindableCollection<string>();
        List<string> SelectedTypeByUser = new List<string>();      
        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set {
                _selectedType = value;
                NotifyOfPropertyChange(() => SelectedType);
                }
        }
        private ProductsModel _selectedProduct;
        public ProductsModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                }
        }
        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set {
                _quantity = value;
                NotifyOfPropertyChange(() => Quantity);
                }
        }
        public ActualBaseViewModel()
        {
            ApiConnectModel.InitializeClient();
            BindableTypes = returnTypes();
            toBindable();
        }
        public List<ProductsModel> returnAllProducts()
        {
            return ApiConnectModel.getAllProducts().Result;
        }
        public BindableCollection<string> returnTypes()
        {
            BindableCollection<string> toReturn = new BindableCollection<string>();
            foreach (var types in returnAllProducts())
            {
                foreach (var item in types.type)
                {
                    toReturn.Add(item);
                }
            }
            toReturn = new BindableCollection<string>(toReturn.Distinct());
            return toReturn;
        }
        public BindableCollection<ProductsModel> toBindable()
        {
            foreach (var item in returnAllProducts())
            {
                Bindable.Add(item);
            }

            return Bindable;
        }
        public BindableCollection<ProductsModel> Bindable
        {
            get { return _bindable; }
            set { _bindable = value; }
        }
        public BindableCollection<string> BindableTypes
        {
            get { return _bindableTypes; }
            set { _bindableTypes = value; }
        }
        public BindableCollection<ProductsModel> FindAndDisplayProductFindedByType()
        {
            BindableCollection<ProductsModel> toReturn = new BindableCollection<ProductsModel>();
            foreach (var product in ApiConnectModel.FindByOneType(SelectedType).Result)
            {
                toReturn.Add(product);
            }
            return toReturn;
        }
        public void SearchFromType()
        {
            BindableCollection<ProductsModel> toReturn = new BindableCollection<ProductsModel>();
            foreach (var product in ApiConnectModel.FindByOneType(SelectedType).Result)
            {
                toReturn.Add(product);
            }

            Bindable = toReturn;
            Refresh();
        }
        public void SellProduct()
        {
            ApiConnectModel.SoldProduct(SelectedProduct, Quantity);
        }

    }
}
