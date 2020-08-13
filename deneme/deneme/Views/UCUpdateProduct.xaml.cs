using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using deneme.Services;
using deneme.Models;

namespace deneme.Views
{
    /// <summary>
    /// UCUpdateProduct.xaml etkileşim mantığı
    /// </summary>
    public partial class UCUpdateProduct : UserControl
    {
        Services.Services services = new Services.Services();
        Product product = new Product();
        decimal price = 0;
        int? ID;
        public UCUpdateProduct(int? id)
        {
            InitializeComponent();
            ID = id;
            GetLists();
        }

        public async void GetLists() {

            var lists =await Task.Run(()=>services.getView(ID));
            dtgridProduct.ItemsSource = lists;

            var subs = services.GetSub();
            cbSubCategory.ItemsSource = subs;

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            decimal.TryParse(txtSellPrice.Text,out price);
            if (txtProductName.Text!=""&& price!=0 && cbSubCategory.Text!=null && dtgridProduct.SelectedItem!=null)
            {
                product.Name = txtProductName.Text;
                product.SellPrice = price;
                product.SubCategoryID = services.SubID(cbSubCategory.Text);

                services.updateProduct(product,product.ID);
                GetLists();
            }
            else
            {
                MessageBox.Show("Bütün Bilgileri Doğru Giriniz");
            }
        }

        private void DtgridProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgridProduct.SelectedItem!=null)
            {
                var row = (vProducts)dtgridProduct.SelectedItem;
                txtProductName.Text = row.Name;
                txtSellPrice.Text = row.SellPrice.ToString();
                cbSubCategory.SelectedItem = row.SubName;
            }
            else
            {

            }
        }

        private void DtgridProduct_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="CompanyName")
            {
                e.Cancel = true;
            }
        }
    }
}
