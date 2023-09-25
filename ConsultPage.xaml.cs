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
        #region Поля и переменные

        Consult consult = new Consult();

        ObservableCollection<Worker> consultsView = new ObservableCollection<Worker>();

        List<Worker> consults = new List<Worker>();

        bool isSelect = false;

        #endregion

        #region Конструктор страницы

        public ConsultPage()
        {
            InitializeComponent();
            
        }

        #endregion

        #region Методы

        /// <summary>
        /// Обновляем БД
        /// </summary>
        private void Refresh()
        {
            consults.Clear();

            consultsView.Clear();

            consults = consult.Deserialize(false);

            foreach (var item in consults)
            {
                consultsView.Add(item);
            }

            WorkerConsultList.ItemsSource = consultsView;
        }

        #endregion

        #region События

        /// <summary>
        /// Кнопка изменить запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeOrder(object sender, RoutedEventArgs e)
        {
            if (isSelect)
            {
                ChangePanel.Visibility = Visibility.Visible;

                SecondnameTextBox.Text = consults[WorkerConsultList.SelectedIndex].SecondName;
                NameTextBox.Text = consults[WorkerConsultList.SelectedIndex].Name;
                MiddlenameTextBox.Text = consults[WorkerConsultList.SelectedIndex].MiddleName;
                TelephoneTextBox.Text = consults[WorkerConsultList.SelectedIndex].Telephone;
                DataPassportTextBox.Text = consults[WorkerConsultList.SelectedIndex].DataPassport;

                SecondnameTextBox.IsEnabled = false;
                NameTextBox.IsEnabled = false;
                MiddlenameTextBox.IsEnabled = false;
                DataPassportTextBox.IsEnabled = false;
            }
        }

        /// <summary>
        /// Кнопка сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChabges(object sender, RoutedEventArgs e)
        {
            List<String> newData = new List<String> { TelephoneTextBox.Text };

            consults = consult.ChangeData(consults, WorkerConsultList.SelectedIndex, newData);

            consults[WorkerConsultList.SelectedIndex].Telephone = TelephoneTextBox.Text;

            consult.CheckAndWrite(consults);

            Refresh();

            ChangePanel.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Проверка на то, что какая-то запись выбрана
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectOrder(object sender, SelectionChangedEventArgs e)
        {
            if (WorkerConsultList.SelectedIndex >= 0)
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
        /// При заходе на страницу обновляем БД
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
                consults.Sort(Worker.SortedBy(SortedCriterion.SecondName));
            }
            else if (header == "Имя")
            {
                consults.Sort(Worker.SortedBy(SortedCriterion.Name));
            }
            else if (header == "Отчество")
            {
                consults.Sort(Worker.SortedBy(SortedCriterion.MiddleName));
            }
            else if (header == "Телефон")
            {
                consults.Sort(Worker.SortedBy(SortedCriterion.Telephone));
            }
            else if (header == "Время изменения")
            {
                consults.Sort(Worker.SortedBy(SortedCriterion.TimeChangeOrder));
            }
            else if (header == "Что поменялось")
            {
                consults.Sort(Worker.SortedBy(SortedCriterion.WhichDataChange));
            }
            else if (header == "Тип изменений")
            {
                consults.Sort(Worker.SortedBy(SortedCriterion.TypeOfChange));
            }
            else if (header == "Кто изменил")
            {
                consults.Sort(Worker.SortedBy(SortedCriterion.WhoChanged));
            }

            consultsView.Clear();

            foreach (var item in consults)
            {
                consultsView.Add(item);
            }
        }

        #endregion
    }
}