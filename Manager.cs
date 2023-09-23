using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOP1_v2
{
    class Manager : Worker, IChangedData
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
        public Manager(string secondName, string name, string middleName, string telephone, string dataPassport)
            : base(secondName, name, middleName, telephone, dataPassport)
        {

        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Manager() : this("", "", "", "", "")
        {

        }

        #endregion

        #region Интерфейсы

        public List<Worker> ChangeData(List<Worker> managers, int index, List<String> newData)
        {
            if (managers[index].SecondName != newData[0] || managers[index].Name != newData[1] || 
                managers[index].MiddleName != newData[2] || managers[index].Telephone != newData[3] || 
                managers[index].DataPassport != newData[4])
            {
                managers[index].WhichDataChange = "";
                managers[index].TypeOfChange = "";
                managers[index].TimeChangeOrder = Convert.ToString(DateTime.Now);
                managers[index].WhoChanged = "Менеджер";

                if (managers[index].SecondName != newData[0])
                {
                    managers[index].WhichDataChange += $"{managers[index].SecondName}->{newData[0]}\\";
                    managers[index].TypeOfChange += $"Фамилия\\";
                }
                if (managers[index].Name != newData[1])
                {
                    managers[index].WhichDataChange += $"{managers[index].Name}->{newData[1]}\\";
                    managers[index].TypeOfChange += $"Имя\\";
                }
                if (managers[index].MiddleName != newData[2])
                {
                    managers[index].WhichDataChange += $"{managers[index].MiddleName}->{newData[2]}\\";
                    managers[index].TypeOfChange += $"Отчество\\";
                }
                if (managers[index].Telephone != newData[3])
                {
                    managers[index].WhichDataChange += $"{managers[index].Telephone}->{newData[3]}\\";
                    managers[index].TypeOfChange += $"Телефон\\";
                }
                if (managers[index].DataPassport != newData[4])
                {
                    managers[index].WhichDataChange += $"{managers[index].DataPassport}->{newData[4]}\\";
                    managers[index].TypeOfChange += $"Паспортные данные\\";
                }

            }

            return managers;
        }

        public List<Worker> AddOrder(List<Worker> managers, List<string> newData)
        {
            managers.Add(new Manager(newData[0], newData[1], newData[2], newData[3], newData[4]));
            return managers;
        }

        #endregion
    }
}
