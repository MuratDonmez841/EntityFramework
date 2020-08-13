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
using deneme.Views;

namespace deneme.Views
{
    /// <summary>
    /// UCUserOldFeedBacks.xaml etkileşim mantığı
    /// </summary>
    public partial class UCUserOldFeedBacks : UserControl
    {
        int? ID;
        Services.Services services = new Services.Services();

        public UCUserOldFeedBacks(int? id)
        {
            InitializeComponent();

            ID = id;
            
          getUserOldFeedBacks();
        }


        public void getUserOldFeedBacks() {

            var feedbacks = services.getFeedBackList(ID);
            dtGridUserFeedBack.ItemsSource = feedbacks;
        }

        private void DtGridUserFeedBack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtGridUserFeedBack.SelectedItem!=null)
            {
                var row = (vFeedBack)dtGridUserFeedBack.SelectedItem;
                gridUserFeedBack.Children.Clear();
                UCFeedBackInfo uCFeedBackInfo = new UCFeedBackInfo(row.Bildirim_Numarası, ID, true);
                gridUserFeedBack.Children.Add(uCFeedBackInfo);
            }
        }

        private void DtGridUserFeedBack_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="Company")
            {
                e.Cancel=true;
            }
            if (e.Column.Header.ToString()== "Kullanıcı_Adı")
            {
                e.Cancel=true;
            }
        }


    }
}
