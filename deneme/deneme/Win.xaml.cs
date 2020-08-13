using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using deneme.Services;
namespace deneme
{
    /// <summary>
    /// Win.xaml etkileşim mantığı
    /// </summary>
    public partial class Win : Window
    {
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        Services.Services services = new Services.Services();

        public Win()
        {
            InitializeComponent();

            services.getViews();
        }
    }
}
