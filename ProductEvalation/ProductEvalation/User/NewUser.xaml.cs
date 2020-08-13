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
using ProductEvalation.DataBase;
using ProductEvalation.Services;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Threading;

namespace ProductEvalation.User
{
    /// <summary>
    /// NewUser.xaml etkileşim mantığı
    /// </summary>
    public partial class NewUser : Window
    {
        bool control1 = false;
        bool control2 = false;
        Users users = new Users();
        Services.User servi = new Services.User();
        long tel = 0;
        int kod = 0;
        Sells sel = new Sells();
        string file;
        bool timeControl = false;
        public NewUser()
        {
            InitializeComponent();
        }
        private void BtnCon_Click(object sender, RoutedEventArgs e)
        {
            var match = Regex.Match(txtMail.Text, "@hotmail.com");
            var match2 = Regex.Match(txtMail.Text, "@gmail.com");

            if (txtName.Text!="" && txtPass1.Password.ToString()!=""&& txtAdress.Text!=""&& match.Success||match2.Success)
            {
                var mailTekrarControl=servi.control(txtMail.Text);

                if (mailTekrarControl == true)
                {
                    users.UserName = txtName.Text;
                    long.TryParse(txtTelNo.Text, out tel);
                    if (tel != 0)
                    {
                        users.UserTel = tel;
                    }
                    users.UserProvi = txtPr.Text;
                    users.UserDisct = txtDis.Text;
                    int.TryParse(txtPostaKod.Text, out kod);
                    if (kod != 0)
                    {
                        users.UserPostaKod = kod;
                    }
                    users.UserAdress = txtAdress.Text;

                    users.UserMail = txtMail.Text;

                    users.profilPhoto = file;
                    

                    control2 = true; 
                } 
       
            }

            if (txtPass1.Password.ToString() == txtPass2.Password.ToString())
            {
                users.UserPassword = txtPass1.Password.ToString();
                control1 = true;
            }
            else
            {
                MessageBox.Show("Şifreler Eşleşmedi");
            }
            if (control1==true&& control2==true)
            {
                servi.NewUser(users);
                MessageBox.Show("Kaydınız tamamlandı");
            }
            else
            {
                MessageBox.Show("Kaydınız Yapılamadı Alanlardan Hiçbirini Boş Bırakmadığınıza Emin Olun");
            }


        }
  

        private void TxtTelNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex tx = new Regex("[^0-9.-]+");
           e.Handled= tx.IsMatch(e.Text);
        }

        private void BtnMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BtnUserProfilPhoto_Click(object sender, RoutedEventArgs e)
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
                userProfilPhoto.Source = bitmapImage;
            }
        }

        private void TxtMail_TextChanged(object sender, TextChangedEventArgs e)
        {
            var mailTekrarControl = servi.control(txtMail.Text);
            if (mailTekrarControl==false)
            {
                txtMail.Foreground = new SolidColorBrush(Colors.Crimson);
                lblMailControl.Content = "Bu mail adresi zaten kayıtlı";
            }
            else
            {
                txtMail.Foreground = new SolidColorBrush(Colors.Black);
                lblMailControl.Content = "";
            }

        }

        private void TxtPass2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string pass1 = txtPass1.Password.ToString();
            string pass2 = txtPass2.Password.ToString();
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            if (pass1!=pass2)
            {
                lblPass.Foreground = new SolidColorBrush(Colors.Crimson);
                TimeSpan timer;
                timer = TimeSpan.FromSeconds(10);

                dispatcherTimer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate 
                {
                    if (timeControl==false)
                    {
                        var time = timer.TotalSeconds;
                        if (time % 2 == 0)
                        {
                            lblPass.Content = "Şifreler Eşleşmedi";
                        }
                        else
                        {
                            lblPass.Content = "";
                        }
                        if (timer == TimeSpan.Zero)
                        {
                            dispatcherTimer.Stop();
                        }
                        timer = timer.Add(TimeSpan.FromSeconds(-1)); 
                    }

                    if (timeControl == true)
                    {
                        dispatcherTimer.Stop();
                        timeControl = false;
                        lblPass.Content = "";
                    }

                }, Dispatcher);

                dispatcherTimer.Start();
            }
            
            else
            {
                timeControl = true;
            }
        }
    }

   
}
