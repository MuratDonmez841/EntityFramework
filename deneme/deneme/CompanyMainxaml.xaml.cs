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
using System.Windows.Shapes;
using deneme.Views;
namespace deneme
{
    /// <summary>
    /// CompanyMainxaml.xaml etkileşim mantığı
    /// </summary>
    public partial class CompanyMainxaml : Window
    {
        int? id;
        public CompanyMainxaml(int? ID)
        {
            InitializeComponent();

            id = ID;
        }

        private void BtnCompanyProfil_Click(object sender, RoutedEventArgs e)
        {
            gridCompanyMain.Children.Clear();
            UCcompany uCcompany = new UCcompany(id);
            gridCompanyMain.Children.Add(uCcompany);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            gridCompanyMain.Children.Clear();
            UCNewProduct uCNewProduct = new UCNewProduct(id);
            gridCompanyMain.Children.Add(uCNewProduct);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            gridCompanyMain.Children.Clear();
            UCUpdateProduct uCUpdateProduct = new UCUpdateProduct(id);
            gridCompanyMain.Children.Add(uCUpdateProduct);

        }

        private async void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run( () =>
            {
                 Dispatcher.Invoke( () =>
                {
                    gridCompanyMain.Children.Clear();
                    UCRemoveProduct uCRemoveProduct = new UCRemoveProduct(id);
                    gridCompanyMain.Children.Add(uCRemoveProduct);
                });
            });
        }

        private void BtnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            gridCompanyMain.Children.Clear();
            UCcategory uCcategory = new UCcategory();
            gridCompanyMain.Children.Add(uCcategory);
        }

        private void BbtnFeed_Click(object sender, RoutedEventArgs e)
        {
            gridCompanyMain.Children.Clear();
            UCFeedBacksCompany uCFeedBacksCompany = new UCFeedBacksCompany(id);
            gridCompanyMain.Children.Add(uCFeedBacksCompany);
        }
    }
}
