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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace deneme.Views
{
    /// <summary>
    /// UCProfil.xaml etkileşim mantığı
    /// </summary>
    public partial class UCProfil : System.Windows.Controls.UserControl
    {
        Services.Services services = new Services.Services();

        int? ID;
        User_Com _Com = new User_Com();
        User_PI _PI = new User_PI();
        string file;
        public UCProfil(int? id)
        {
            InitializeComponent();

            lblUserID.Content = ""+id+"";
            ID = id;

            userInfo();

            txtCin.Items.Add("Erkek");
            txtCin.Items.Add("Kadın");
        }
        public void userInfo() {

            var PI = services.getUSerInfo(ID);
            var Com = services.Get_Com(ID);

            txtUserName.Text = PI.UName;
            if (PI.Cinsiyet=="E")
            {
                txtCin.SelectedItem = "Erkek";
            }
            else
            {
                txtCin.SelectedItem = "Kadın";
            }
            DateBirDay.Text =PI.BirdDay.ToString();

            txtUserMail.Text = Com.Mail;

            txtUserTelNu.Text = Com.Phone.ToString();

            richBoxAdress.Document.Blocks.Clear();
            richBoxAdress.Document.Blocks.Add(new Paragraph(new Run(Com.Adress.ToString())));

        }


        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var match = Regex.Match(txtUserMail.Text,"@hotmail.com");
            var match2 = Regex.Match(txtUserMail.Text,"@gmail.com");

            if (txtCin.Text!=null||txtCin.Text!="" && txtUserMail.Text!=null|| txtUserMail.Text!="" && txtUserName.Text!=null||txtUserName.Text!="" &&
                txtUserTelNu.Text!=null||txtUserTelNu.Text!=""&& match.Success||match2.Success)
            {
                if (MessageBox.Show("Bilgilerinizi güncellemek istiyor musunuz?","",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    _PI.UName = txtUserName.Text;
                    if (txtCin.Text.ToUpper()=="ERKEK")
                    {
                        _PI.Cinsiyet = "E";

                    }
                    else
                    {
                        _PI.Cinsiyet = "K";
                    }
                    _PI.BirdDay =DateTime.Parse(DateBirDay.Text);

                    _Com.Mail = txtUserMail.Text;
                    _Com.Phone =long.Parse (txtUserTelNu.Text);
                    _Com.Adress = new TextRange(richBoxAdress.Document.ContentStart, richBoxAdress.Document.ContentEnd).Text.Trim();

                    _PI.IMG = file;

                    services.UpdateUserInfo(_PI,_Com,ID);

                    MessageBox.Show("Bilgileriniz Güncellendi!");
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Gerekli alanları doldurunuz!");
            }
        }

        private void TxtUserTelNu_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex tx = new Regex("[^0-9.-]+");
            e.Handled = tx.IsMatch(e.Text);
        }

        private void BtnimgUser_Click(object sender, RoutedEventArgs e)
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
                UserProfilimg.Source = bitmapImage;
            }
        }
    }
}
