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
using deneme.Models;
namespace deneme.Views
{
    /// <summary>
    /// UCNewProduct.xaml etkileşim mantığı
    /// </summary>
    public partial class UCNewProduct : UserControl
    {
        Services.Services services = new Services.Services();
        Product product = new Product();
        int? id;
        decimal Price=0;
        public UCNewProduct(int? ID)
        {
            InitializeComponent();
            id = ID;
            getlist();
        }

        public async void getlist(){

            txtCompanyName.Text = services.GetSoloName(id);

            var lists =await Task.Run(()=> services.getView(id));   
            dtgridProduct.ItemsSource = lists;

            var subs = services.GetSub();
            cbSubCategory.ItemsSource = subs;
        }

        private void Btnadd_Click(object sender, RoutedEventArgs e)
        {
            decimal.TryParse(txtSellPrice.Text, out Price);
            if (txtProductName.Text!=""&&txtSellPrice.Text!=""&&cbSubCategory.SelectedItem!=null&&Price!=0)
            {
                product.Name = txtProductName.Text;
                product.SellPrice =Price;
                product.CompanyID = id;
                product.SubCategoryID = services.SubID(cbSubCategory.Text);


                 services.NewProduct(product);

                 getlist();
                 MessageBox.Show("Başarıla Eklendi"); 
       
            }
            else
            {
                MessageBox.Show("Bilgileri Doğru Giriniz!");
            }
        }

        private void DtgridProduct_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="CompanyName")
            {
                e.Cancel=true;
            }
        }
    }
}
