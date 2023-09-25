using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
                DeleteOrderButton.IsEnabled = true;
                isSelect = true;
            }
            else
            {
                ChangeOrderButton.IsEnabled = false;
                DeleteOrderButton.IsEnabled = false;
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

        /// <summary>
        /// Сортировка нажатием на заголовок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            string header = Convert.ToString(headerClicked.Content);

            if (header == "Фамилия")
            {
                managers.Sort(Worker.SortedBy(SortedCriterion.SecondName));
            }
            else if (header == "Имя")
            {
                managers.Sort(Worker.SortedBy(SortedCriterion.Name));
            }
            else if (header == "Отчество")
            {
                managers.Sort(Worker.SortedBy(SortedCriterion.MiddleName));
            }
            else if (header == "Телефон")
            {
                managers.Sort(Worker.SortedBy(SortedCriterion.Telephone));
            }
            else if (header == "Паспортные данные")
            {
                managers.Sort(Worker.SortedBy(SortedCriterion.DataPassport));
            }
            else if (header == "Время изменения")
            {
                managers.Sort(Worker.SortedBy(SortedCriterion.TimeChangeOrder));
            }
            else if (header == "Что поменялось")
            {
                managers.Sort(Worker.SortedBy(SortedCriterion.WhichDataChange));
            }
            else if (header == "Тип изменений")
            {
                managers.Sort(Worker.SortedBy(SortedCriterion.TypeOfChange));
            }
            else if (header == "Кто изменил")
            {
                managers.Sort(Worker.SortedBy(SortedCriterion.WhoChanged));
            }

            managersView.Clear();

            foreach (var item in managers)
            {
                managersView.Add(item);
            }
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            managers = manager.DeleteOrder(managers, WorkerManagerList.SelectedIndex);
            manager.CheckAndWrite(managers);
            managers = manager.RefreshDBView(managersView, managers);
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