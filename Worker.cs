using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOP1_v2
{
    abstract class Worker : INotifyPropertyChanged
    {

        public ObservableCollection<Worker> workers = new ObservableCollection<Worker>();

        #region Статичные переменные

        public static string pathToFile;

        


        #endregion

        #region Объявление статических переменных

        static Worker()
        {
            pathToFile = "DataBase.txt";
        }


        #endregion

        #region Поля

        private string secondName;

        private string name;

        private string middleName;

        private string telephone;

        private string dataPassport;

        #endregion

        #region Свойства

        public string SecondName { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Telephone
        {
            get { return telephone; }
            set
            {
                telephone = value;
                OnPropertyChanged("Telephone");
            }
        }

        public virtual string DataPassport { get; set; }

        #endregion

        #region Конструкторы

        /// <summary>
        /// Базовый констуктор
        /// </summary>
        /// <param name="secondName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="telephone">Телефон</param>
        /// <param name="dataPassport">Паспортные данные</param>
        public Worker(string secondName, string name, string middleName, string telephone, string dataPassport)
        {
            SecondName = secondName;
            Name = name;
            MiddleName = middleName;
            Telephone = telephone;
            DataPassport = dataPassport;
        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Worker() : this("", "", "", "", "")
        {

        }

        #endregion

        #region Методы

        /// <summary>
        /// Порядок записи в БД
        /// </summary>
        /// <returns></returns>
        protected string WriteOrder()
        {
            return $"{SecondName}#{Name}#{MiddleName}#{Telephone}#{DataPassport}#";
        }

        /// <summary>
        /// Обновление базы данных
        /// </summary>
        /// <param name="dataBase">Передаем сюда ObservableCollection, который будем потом получать обратно со значениями из БД</param>
        /// <returns></returns>
        public ObservableCollection<Worker> RefreshDB(Worker TheWorker)
        {

            if (workers != null)
            {
                workers.Clear();
            }

            using (StreamReader reader = new StreamReader(pathToFile))
            {
                while (!reader.EndOfStream)
                {
                    string[] args = reader.ReadLine().Split('#');

                    if (TheWorker is Manager)
                    {
                        workers.Add(new Manager(args[0], args[1], args[2], args[3], args[4]));
                    }
                    else if (TheWorker is Consult)
                    {
                        workers.Add(new Consult(args[0], args[1], args[2], args[3], args[4]));
                    }

                }
                reader.Close();
            }

            return workers;
        }

        /// <summary>
        /// Проверяется запись телефона на ввод и если всё ок - то будет запись в БД
        /// </summary>
        public void CheckAndWrite(ObservableCollection<Worker> newOrder)
        {
            if (Check(newOrder) == true)
            {
                Write(newOrder);
            }
        }

        public bool Check(ObservableCollection<Worker> newOrder)
        {
            bool isOkay = true;

            for (int i = 0; i < newOrder.Count; i++)
            {
                if (newOrder[i].SecondName == string.Empty ||
                    newOrder[i].Name == string.Empty ||
                    newOrder[i].MiddleName == string.Empty ||
                    newOrder[i].Telephone == string.Empty ||
                    newOrder[i].DataPassport == string.Empty)
                {
                    isOkay = false;
                    break;
                }
            }

            return isOkay;
        }

        public void Write(ObservableCollection<Worker> newOrder)
        {

            using (StreamWriter writer = new StreamWriter(pathToFile))
            {
                for (int i = 0; i < newOrder.Count; i++)
                {
                    writer.WriteLine(newOrder[i].WriteOrder());
                }
                writer.Close();
            }

        }

        #endregion

        #region Интерфейсы

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion

    }
}
