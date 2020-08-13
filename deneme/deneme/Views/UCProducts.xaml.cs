using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using deneme.Services;

namespace deneme.Views
{
    /// <summary>
    /// UCProducts.xaml etkileşim mantığı
    /// </summary>
    public partial class UCProducts : UserControl
    {
        int? ID;
        Services.Services services = new Services.Services();
        public UCProducts(int? id)
        {
            InitializeComponent();

            ID = id;

            getlist();

        }

        public void getlist() {

            var list = services.getCompanyProduct(ID,null);
            dtGridProduct.ItemsSource=list;
        }


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Visibility = Visibility.Hidden;
            btnSearch.Visibility = Visibility.Hidden;
        }

        private void DtGridProduct_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="Company")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "CompanyID")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "SubCategory")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() =="FeedBack_Info")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "SubCategoryID")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString()=="ID")
            {
                e.Column.Header ="No";
            }
            if (e.Column.Header.ToString()=="SellPrice")
            {
                e.Column.Header = "Satış Fiyatı";
            }
            if (e.Column.Header.ToString()=="Name")
            {
                e.Column.Header = "Ürün Adı";
            }
        }

        //private void Grid_KeyUp(object sender, KeyEventArgs e)
        //{
           
        //}

        private void DtGridProduct_KeyUp(object sender, KeyEventArgs e)
        {
                if (e.Key == Key.LeftCtrl)
            {
                txtSearch.Visibility = System.Windows.Visibility.Visible;
                btnSearch.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = services.getCompanyProduct(ID,txtSearch.Text);
            dtGridProduct.ItemsSource = search;

        }
    }
}
