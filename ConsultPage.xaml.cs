using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace OOP1_v2
{
    /// <summary>
    /// Логика взаимодействия для ConsultPage.xaml
    /// </summary>
    public partial class ConsultPage : Page
    {
        Worker consult = new Consult();

        ObservableCollection<Worker> worker;

        public ConsultPage()
        {
            InitializeComponent();

            worker = consult.RefreshDB(consult);

            WorkerConsultList.ItemsSource = worker;

            DataContext = new Consult();
        }

        private void ChangeData(object sender, RoutedEventArgs e)
        {
            consult.CheckAndWrite(worker);
            //MessageBox.Show("");
        }
    }
}
