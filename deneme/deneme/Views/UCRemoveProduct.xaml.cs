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
using deneme.Services;
using System.Threading;

namespace deneme.Views
{

    public partial class UCRemoveProduct : UserControl
    {
        Services.Services services = new Services.Services();
        int? id;
        string wait;
        public UCRemoveProduct(int? ID)
        {
            InitializeComponent();

            id = ID;
            getlists();
        }
        public async Task getlists() {
            await Dispatcher.Invoke(async () =>
            {
                var lists = await Task.Run(() => services.getView(id));
                dataGridDelProduct.ItemsSource = lists;
            });
        }
        private async void DataGridDelProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = (vProducts)dataGridDelProduct.SelectedItem;
            if (dataGridDelProduct.SelectedItem!=null)
            {
                if (MessageBox.Show("" + row.Name + " Adlı Öğeyi Silmek İstiyor Musunuz?", "Uyarı!", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                {
                    var progress = new Progress<int>();
                    await Task.Run(async () =>
                    {
                        await Dispatcher.Invoke(async () =>
                        {
                            wait =await Progress();

                        if (wait == "100")
                        {      
                           Processbar.Visibility = Visibility.Hidden; 
                           await services.DelProduct(row.ID);
                           var lists = await services.getView(id);
                           dataGridDelProduct.ItemsSource = lists;
                           MessageBox.Show("Öğe Silindi");
                        }
                         });
                   });
                }
                else
                {

                } 
            }
          
        }

        private void DataGridDelProduct_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="CompanyName")
            {
                e.Cancel = true;
            }
        }

        public async Task<string> Progress() {

            try
            {
              await  Dispatcher.Invoke(async () =>
                {
                for (int i = 0; i < 101; i++)
                {
                        
                    await Task.Run(()=> {
                         Dispatcher.Invoke(() => { Processbar.Visibility = Visibility.Visible; lblS.Content = ""+i+""; });
                    });

                        await Dispatcher.Invoke(async ()=> { await Task.Delay(100);});
                        
                    }
                });
                return lblS.Content.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
