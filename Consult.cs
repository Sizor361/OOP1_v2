using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP1_v2
{
    class Consult : Worker, IChangedData
    {
        #region Конструкторы

        /// <summary>
        /// Полный констуктор на основе Worker
        /// </summary>
        /// <param name="secondName">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="middleName">Отчество</param>
        /// <param name="telephone">Телефон</param>
        /// <param name="dataPassport">Данные паспорта</param>
        public Consult(string secondName, string name, string middleName, string telephone, string dataPassport)
            : base(secondName, name, middleName, telephone, dataPassport)
        {

        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Consult() : this("", "", "", "", "")
        {

        }

        #endregion

        #region Свойства

        /// <summary>
        /// Маска на паспорт
        /// </summary>
        public new string DataPassport
        {
            get { return "***-*****"; }
            private set { dataPassport = value; }
        }

        #endregion

        #region Интерфейсы

        public List<Worker> ChangeData(List<Worker> consults, int index, List<String> newData)
        {
            if (consults[index].Telephone != newData[0])
            {
                consults[index].WhichDataChange = $"{consults[index].Telephone}->{newData[0]}\\";
                consults[index].TypeOfChange = $"Телефон\\";
                consults[index].TimeChangeOrder = Convert.ToString(DateTime.Now);
                consults[index].WhoChanged = "Консультант";
            }

            return consults;
        }

        public List<Worker> AddOrder(List<Worker> managers, List<string> newData)
        {
            return managers;
        }

        #endregion
    }
}