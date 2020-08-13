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
    /// UCFeedBacksCompany.xaml etkileşim mantığı
    /// </summary>
    public partial class UCFeedBacksCompany : UserControl
    {
        int? ID;
        Services.Services services = new Services.Services();

        public UCFeedBacksCompany(int? id)
        {
            InitializeComponent();
            ID = id;
            getlist();
        }
        public void getlist() {

            var feedbacklist = services.GetIdList(ID);

            if (feedbacklist!=null)
            {
                dtGridFeedBack.ItemsSource = feedbacklist; 
            }
            else
            {
                MessageBox.Show("Hiç Geri Bildirim Bulunmamamktadır");
            }


        }
        private void DtGridFeedBack_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString()=="Company")
            {
                e.Cancel = true;
            }

        }

        private void DtGridFeedBack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtGridFeedBack.SelectedItem!=null)
            {

                var row = (vFeedBack)dtGridFeedBack.SelectedItem;
                gridFeedBacks.Children.Clear();
                UCFeedBackInfo uCFeedBackInfo = new UCFeedBackInfo(row.Bildirim_Numarası,row.Company,false);
                gridFeedBacks.Children.Add(uCFeedBackInfo);

            }
        }


    }
}
