using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP1_v2
{
    interface IChangedData
    {
        /// <summary>
        /// Изменяемые данные
        /// </summary>
        /// <param name="workers">Что перезаписываем</param>
        /// <param name="index">Кокнетный индекс</param>
        /// <param name="newData">Что изменилось</param>
        /// <returns></returns>
        List<Worker> ChangeData(List<Worker> workers, int index, List<string> newData);

        /// <summary>
        /// Новая запись
        /// </summary>
        /// <param name="workers">Куда записываем</param>
        /// <param name="newData">Что запимсываем</param>
        /// <returns></returns>
        List<Worker> AddOrder(List<Worker> workers, List<String> newData);

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="workers">Откуда удаляем</param>
        /// <param name="index">По какому индексу</param>
        /// <returns></returns>
        List<Worker> DeleteOrder(List<Worker> workers, int index);
    }
}
