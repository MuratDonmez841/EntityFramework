using deneme.Views;
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

namespace deneme
{
    /// <summary>
    /// UI.xaml etkileşim mantığı
    /// </summary>
    public partial class UI : Window
    {
        int id;
        Services.Services services = new Services.Services();
   
        public UI(int ID)
        {
            InitializeComponent();
            id = ID;


            var companyName = services.getCompanyName();

            foreach (var name in companyName)
            {

                var button = new Button {
                    Name = "btn" + name + "",
                    Content = "" + name.ToUpperInvariant() + "",
                    Background = new SolidColorBrush(Colors.Transparent),
                    BorderBrush = new SolidColorBrush(Colors.Transparent),
                    Width = 142,
                     };
                button.Click += (sender, e) => {
                    string name1 = button.Content.ToString();

                    if (name1 != null)
                    {
                        var companyID = services.GetCompanyID(name1);

                        gridMain.Children.Clear();
                        UCProducts uCProducts = new UCProducts(companyID);
                        gridMain.Children.Add(uCProducts);
                    }

                };
                treeViewItemMagzalar.Items.Add(button);
        
         
            }

        }

        private void Btndeneme_Click(object sender, RoutedEventArgs e)
        {
            gridMain.Children.Clear();
            UCFeedBack feedBack = new UCFeedBack(id);
            gridMain.Children.Add(feedBack);
        }

        private void BtnProfil_Click(object sender, RoutedEventArgs e)
        {
            gridMain.Children.Clear();
            UCProfil uCProfil = new UCProfil(id);
            gridMain.Children.Add(uCProfil);
        }

        private void BtnPassword_Click(object sender, RoutedEventArgs e)
        {
            gridMain.Children.Clear();
            UCPasswaord uCPasswaord = new UCPasswaord(id);
            gridMain.Children.Add(uCPasswaord);
        }

        private void BtnGecmis_Click(object sender, RoutedEventArgs e)
        {
            gridMain.Children.Clear();
            UCUserOldFeedBacks uCUserOldFeedBacks = new UCUserOldFeedBacks(id);
            gridMain.Children.Add(uCUserOldFeedBacks);
            
        }

 
    }
}
