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
    enum SortedCriterion
    {
        SecondName,
        Name,
        MiddleName,
        Telephone,
        DataPassport,
        TimeChangeOrder,
        WhichDataChange,
        TypeOfChange,
        WhoChanged
    }

    abstract class Worker
    {
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

        protected string secondName;

        protected string name;

        protected string middleName;

        protected string telephone;

        protected string dataPassport;

        #endregion

        #region Свойства

        public string SecondName { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string Telephone { get; set; }

        public string DataPassport { get; set; }

        public string TimeChangeOrder { get; set; } = "No data";

        public string WhichDataChange { get; set; } = "No data";

        public string TypeOfChange { get; set; } = "No data";

        public string WhoChanged { get; set; } = "No data";

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
        public string WriteOrder()
        {
            return $"{SecondName}#{Name}#{MiddleName}#{Telephone}#{DataPassport}#{TimeChangeOrder}#{WhichDataChange}#{TypeOfChange}#{WhoChanged}#";
        }

        /// <summary>
        /// Вытаскиваем данные из БД
        /// </summary>
        /// <returns>Получаем список из БД</returns>
        public List<Worker> Deserialize(bool isManager)
        {
            Manager managers;
            Consult consults;

            List<Worker> workerList = new List<Worker>();

            using (StreamReader reader = new StreamReader(pathToFile))
            {
                while (!reader.EndOfStream)
                {
                    string[] args = reader.ReadLine().Split('#');

                    if (isManager)
                    {
                        managers = new Manager(args[0], args[1], args[2], args[3], args[4]);
                        managers.TimeChangeOrder = args[5];
                        managers.WhichDataChange = args[6];
                        managers.TypeOfChange = args[7];
                        managers.WhoChanged = args[8];
                        workerList.Add(managers);
                    }
                    else
                    {
                        consults = new Consult(args[0], args[1], args[2], args[3], args[4]);
                        consults.TimeChangeOrder = args[5];
                        consults.WhichDataChange = args[6];
                        consults.TypeOfChange = args[7];
                        consults.WhoChanged = args[8];
                        workerList.Add(consults);
                    }
                }
                reader.Close();
            }

            return workerList;
        }

        /// <summary>
        /// База данных для показа
        /// </summary>
        /// <param name="view">Кого показываем</param>
        /// <param name="data">Откуда берутся данные</param>
        /// <returns></returns>
        public List<Worker> RefreshDBView(ObservableCollection<Worker> view, List<Worker> data)
        {
            view.Clear();

            foreach (var item in data)
            {
                view.Add(item);
            }

            return data;
        }

        /// <summary>
        /// Проверяется запись телефона на ввод и если всё ок - то будет запись в БД
        /// </summary>
        public void CheckAndWrite(List<Worker> newOrder)
        {
            if (Check(newOrder) == true)
            {
                Write(newOrder);
            }
        }

        /// <summary>
        /// Условие проверки
        /// </summary>
        /// <param name="newOrder">Что проверяем</param>
        /// <returns></returns>
        public bool Check(List<Worker> newOrder)
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

        /// <summary>
        /// Записываем в БД 
        /// </summary>
        /// <param name="newOrder">Что записываем</param>
        public void Write(List<Worker> newOrder)
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

        public static IComparer<Worker> SortedBy(SortedCriterion Criterion)
        {
            if (Criterion == SortedCriterion.SecondName) return new SortBySecondName();
            else if (Criterion == SortedCriterion.Name) return new SortByName();
            else if (Criterion == SortedCriterion.MiddleName) return new SortByMiddleName();
            else if (Criterion == SortedCriterion.Telephone) return new SortByTelephone();
            else if (Criterion == SortedCriterion.DataPassport) return new SortByDataPassport();
            else if (Criterion == SortedCriterion.TimeChangeOrder) return new SortByTimeChangeOrder();
            else if (Criterion == SortedCriterion.WhichDataChange) return new SortByWhichDataChange();
            else if (Criterion == SortedCriterion.TypeOfChange) return new SortByTypeOfChange();
            else if (Criterion == SortedCriterion.WhoChanged) return new SortByWhoChanged();
            else return null;
        }

        #endregion

        #region Классы

        private class SortBySecondName : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return String.Compare(x.SecondName, y.SecondName);
            }
        }

        private class SortByName : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return String.Compare(x.Name, y.Name);
            }
        }

        private class SortByMiddleName : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return String.Compare(x.MiddleName, y.MiddleName);
            }
        }

        private class SortByTelephone : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return String.Compare(x.Telephone, y.Telephone);
            }
        }

        private class SortByDataPassport : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return String.Compare(x.DataPassport, y.DataPassport);
            }
        }

        private class SortByTimeChangeOrder : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return String.Compare(x.TimeChangeOrder, y.TimeChangeOrder);
            }
        }

        private class SortByWhichDataChange : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return String.Compare(x.WhichDataChange, y.WhichDataChange);
            }
        }

        private class SortByTypeOfChange : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return String.Compare(x.TypeOfChange, y.TypeOfChange);
            }
        }

        private class SortByWhoChanged : IComparer<Worker>
        {
            public int Compare(Worker x, Worker y)
            {
                return String.Compare(x.WhoChanged, y.WhoChanged);
            }
        }


        #endregion
    }
}