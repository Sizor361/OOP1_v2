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
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        #region Поля и переменные

        Manager manager = new Manager();

        List<Worker> managers = new List<Worker>();

        ObservableCollection<Worker> managersView = new ObservableCollection<Worker>();

        bool newOrder = false;
        bool isManager = true;
        bool isSelect = false;

        #endregion

        #region Конструктор страницы

        public ManagerPage()
        {
            InitializeComponent();
        }

        #endregion

        #region События

        /// <summary>
        /// Кнопка новая запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewOrder(object sender, RoutedEventArgs e)
        {
            ChangePanel.Visibility = Visibility.Visible;

            SecondnameTextBox.Text = "";
            NameTextBox.Text = "";
            MiddlenameTextBox.Text = "";
            TelephoneTextBox.Text = "";
            DataPassportTextBox.Text = "";

            newOrder = true;
        }

        /// <summary>
        /// Кнопка изменить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeOrder(object sender, RoutedEventArgs e)
        {
            if (isSelect)
            {
                ChangePanel.Visibility = Visibility.Visible;

                SecondnameTextBox.Text = managers[WorkerManagerList.SelectedIndex].SecondName;
                NameTextBox.Text = managers[WorkerManagerList.SelectedIndex].Name;
                MiddlenameTextBox.Text = managers[WorkerManagerList.SelectedIndex].MiddleName;
                TelephoneTextBox.Text = managers[WorkerManagerList.SelectedIndex].Telephone;
                DataPassportTextBox.Text = managers[WorkerManagerList.SelectedIndex].DataPassport;
            }
        }

        /// <summary>
        /// Выбираем элемент из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectOrder(object sender, SelectionChangedEventArgs e)
        {
            if (WorkerManagerList.SelectedIndex >= 0)
            {
                ChangeOrderButton.IsEnabled = true;
                isSelect = true;
            }
            else
            {
                ChangeOrderButton.IsEnabled = false;
                isSelect = false;
            }
        }

        /// <summary>
        /// Кнопка сохранить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChabges(object sender, RoutedEventArgs e)
        {
            List<String> newData = new List<String>
                {
                SecondnameTextBox.Text,
                NameTextBox.Text,
                MiddlenameTextBox.Text,
                TelephoneTextBox.Text,
                DataPassportTextBox.Text
                };

            if (!newOrder)
            {
                managers = manager.ChangeData(managers, WorkerManagerList.SelectedIndex, newData);

                managers[WorkerManagerList.SelectedIndex].SecondName = SecondnameTextBox.Text;
                managers[WorkerManagerList.SelectedIndex].Name = NameTextBox.Text;
                managers[WorkerManagerList.SelectedIndex].MiddleName = MiddlenameTextBox.Text;
                managers[WorkerManagerList.SelectedIndex].Telephone = TelephoneTextBox.Text;
                managers[WorkerManagerList.SelectedIndex].DataPassport = DataPassportTextBox.Text;

                manager.CheckAndWrite(managers);
            }
            else
            {
                managers = manager.AddOrder(managers, newData);
                manager.CheckAndWrite(managers);
                newOrder = !newOrder;
            }

            managers = manager.RefreshDBView(managersView, managers);
            WorkerManagerList.ItemsSource = managersView;

            ChangePanel.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Обновление данные при заходе на страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh(object sender, RoutedEventArgs e)
        {
            Refresh();
            ChangePanel.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обновляем страницу с данными
        /// </summary>
        private void Refresh()
        {
            managers.Clear();

            managersView.Clear();

            managers = manager.Deserialize(isManager);

            foreach (var item in managers)
            {
                managersView.Add(item);
            }

            WorkerManagerList.ItemsSource = managersView;
        }

        #endregion
    }
}