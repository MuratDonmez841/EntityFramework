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
namespace deneme.Views
{
    /// <summary>
    /// UCPasswaord.xaml etkileşim mantığı
    /// </summary>
    public partial class UCPasswaord : UserControl
    {
        Services.Services services = new Services.Services();
        User_Com _Com = new User_Com();
        int? id;
        public UCPasswaord(int? ID)
        {
            InitializeComponent();
            id = ID;
        }

        private void BtnPassUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (passBoxNewFirst.Password.ToString()!=""&&passBoxNewSecond.Password.ToString()!="" && passBoxOld.Password.ToString()!="")
            {
                _Com.Password = passBoxNewFirst.Password.ToString();

                var control = services.UpdatePassword(_Com, id, passBoxOld.Password.ToString());

                if (control == true)
                {

                    MessageBox.Show("Şifre Değiştirildi");
                }
                else
                {
                    MessageBox.Show("Şifre Değiştirilemedi");
                } 
            }
            else
            {
                MessageBox.Show("Bütün alanları doldurunuz!");
            }
        }

        private void PassBoxNewFirst_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passBoxNewSecond.Password.ToString()!=passBoxNewFirst.Password.ToString())
            {
                lblpass.Foreground =new SolidColorBrush(Colors.Crimson);
                lblpass.Content = "Şifreler Eşleşmedi";
            }
            else
            {
                lblpass.Content = "";
            }
        }
    }
}
