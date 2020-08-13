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
using System.Windows.Shapes;
using deneme.Models;
using deneme.Services;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace deneme
{
    /// <summary>
    /// Register.xaml etkileşim mantığı
    /// </summary>
    public partial class Register : Window
    {
        User_PI _PI = new User_PI();
        User_Com _Com = new User_Com();
        FBEntities _contex = new FBEntities();
        //IGenericRepsitory<User_PI> UserPI = new IRepository<User_PI>();
        //IGenericRepsitory<User_Pass> USerPass = new IRepository<User_Pass>();
        //IGenericRepsitory<User_IMG> UserIMG = new IRepository<User_IMG>();
        //IGenericRepsitory<User_Com> UserCom = new IRepository<User_Com>();
        Services.Services services = new Services.Services();
        bool matchControl=false;
        bool PasswordControl = false;
        string file;
        public Register()
        {
            InitializeComponent();
            comboBoxCinsiyet.Items.Add("Erkek");
            comboBoxCinsiyet.Items.Add("Kadın");
        }

        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
            var match = Regex.Match(txtMail.Text,"@hotmail.com");
            var match2 = Regex.Match(txtMail.Text,"@gmail.com");
            if (txtMail.Text!="" && txtUserName.Text!="" &&txtTelNu.Text!="" && passBoxRegister1.Password.ToString()!="" && passBoxREgister2.Password.ToString()!="")
            {

                if (match.Success||match2.Success)
                {
                    _Com.Mail = txtMail.Text;
                    matchControl = true;
                }


                if (passBoxRegister1.Password.ToString()==passBoxREgister2.Password.ToString())
                {
                    _Com.Password = passBoxREgister2.Password.ToString();
                    PasswordControl = true;
                }


                if (PasswordControl==true && matchControl==true)
                {
                    _PI.UName = txtUserName.Text;
                    if (comboBoxCinsiyet.Text=="Erkek")
                    {
                        _PI.Cinsiyet = "E";
                    }
                    else
                    {
                        _PI.Cinsiyet = "K";
                    }
             
                    _PI.BirdDay = DateTime.Parse(datePickerDay.Text);
                    
                    _Com.Phone = long.Parse(txtTelNu.Text.ToString());
                    _Com.Adress = new TextRange(rchBoxAdress.Document.ContentStart, rchBoxAdress.Document.ContentEnd).Text.Trim();

                    _PI.IMG = file;



                    services.NewUser1(_Com,_PI);
                    // services.NewUser(_PI,_Pass,_Com,_IMG);


                    MessageBox.Show("Kayıt Başarılı bir şekilde Yapıldı"); 

                }
                else
                {
                    MessageBox.Show("Kayıt Yapılamadı!");
                }
            }
            else
            {
                MessageBox.Show("Bütün Bilgileri eksiksiz doldurunuz");
            }
        }

        private void BtnProfilPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            DialogResult result = dialog.ShowDialog();
            dialog.RestoreDirectory = true;
            if (result.ToString() == "OK")
            {
                file = dialog.FileName;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(file);
                bitmapImage.EndInit();
                IMGProfilPhoto.Source = bitmapImage;
            }
        }

        private void TxtTelNu_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex tx = new Regex("[^0-9.-]+");
            e.Handled = tx.IsMatch(e.Text);
        }

        private void PassBoxRegister1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passBoxRegister1.Password.ToString()!=passBoxREgister2.Password.ToString())
            {
                lblPass.Content = "Şifreler Eşleşmedi!";
                lblPass.Foreground = new SolidColorBrush(Colors.Crimson);
            }
            else
            {
                lblPass.Content="";
            }
        }
    }
}
