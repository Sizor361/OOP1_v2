using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP1_v2
{
    class Manager : Worker
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
    }
}
