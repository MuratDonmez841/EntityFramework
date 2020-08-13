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
    /// UCcategory.xaml etkileşim mantığı
    /// </summary>
    public partial class UCcategory : UserControl
    {
        Services.Services services = new Services.Services();
        Category category = new Category();
        SubCategory subCategory = new SubCategory();
        public UCcategory()
        {
            InitializeComponent();

            getList();
        }

        public async void getList() {
            var list =await Task.Run(()=>services.categroyList());
            dtGridCategori.ItemsSource = list;
        }

        private void DtGridCategori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtGridCategori.SelectedItem!=null)
            {
                var row = (vCategory)dtGridCategori.SelectedItem;
                txtcategory.Text = row.CategoryName;
                txtSubCategory.Text = row.SubName;
            }
            else
            {

            }
        }
        private void BtnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (txtSubCategory.Text!="" && txtcategory.Text!="")
            {
                category.CategoryName = txtcategory.Text;
                subCategory.SubName = txtSubCategory.Text;
                services.NewCategory(category,subCategory);
          
                getList();
                MessageBox.Show("Kategori Başarıyla Eklendi!"); 
         
            }
            else
            {
                MessageBox.Show("Bütün Alanları Doldurunuz!");
            }
        }

        private void BtnUpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (dtGridCategori.SelectedItem!=null && txtSubCategory.Text!="" &&  txtcategory.Text!="")
            {
                var row = (vCategory)dtGridCategori.SelectedItem;

                category.CategoryName = txtcategory.Text;
                subCategory.SubName = txtSubCategory.Text;

                services.UpdateCategory(category,subCategory,row.CategoryID,row.SubName);
                getList();
                MessageBox.Show("Başarıyla düzenlendi");
            }
            else
            {
                MessageBox.Show("Öğe seçilmemiş!");
            }
        }

        private void DtGridCategori_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="")
            {
                e.Cancel=true;
            }

            if (e.Column.Header.ToString()=="CategoryID")
            {
                e.Cancel = true;
            }
        }


    }
}
