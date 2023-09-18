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
using System.IO;

namespace OOP1_v2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Странички

        ConsultPage consultPage = new ConsultPage();
        ManagerPage managerPage = new ManagerPage();

        #endregion

        #region Конструктор этого класса
        public MainWindow()
        {
            InitializeComponent();

            CheckOnExist(Worker.pathToFile);
        }

        #endregion

        #region События

        /// <summary>
        /// Клик на кнопку Сотрудник
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Consult_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = consultPage;
        }

        /// <summary>
        /// Клик на кнопку Менеджер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = managerPage;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Проверка на существованме фалйа и создание если нету
        /// </summary>
        /// <param name="path">Путь файла</param>
        static void CheckOnExist(string path)
        {

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        #endregion
    }
}
