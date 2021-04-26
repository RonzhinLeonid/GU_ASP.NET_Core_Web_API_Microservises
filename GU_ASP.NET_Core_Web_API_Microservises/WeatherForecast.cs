using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GU_ASP.NET_Core_Web_API_Microservises
{
    public class WeatherForecast
    {
        private readonly Dictionary<DateTime, int> _weather = new Dictionary<DateTime, int>();
        /// <summary>
        /// Добавление температуры
        /// </summary>
        /// <param name="time"></param>
        /// <param name="temperature"></param>
        public void AddTemperature(DateTime time, int temperature)
        {
            if (_weather.ContainsKey(time))
                throw new ArgumentException($"Указанная дата ({time}) уже есть в базе данных");
            _weather.Add(time, temperature);
        }
        /// <summary>
        /// Изменения температуры в указанную даты
        /// </summary>
        /// <param name="time"></param>
        /// <param name="temperature"></param>
        public void UpdateTemperature(DateTime time, int temperature)
        {
            if (!_weather.ContainsKey(time))
                throw new ArgumentException($"Указанной даты ({time}) нет в базе данных");
            _weather[time] = temperature;
        }
        /// <summary>
        /// Удаление температуры в указанном интервале дат
        /// </summary>
        /// <param name="time"></param>
        /// <param name="time"></param>
        public void deleteRangeTemperature(DateTime beginTime, DateTime endTime)
        {
            if(endTime < beginTime)
                throw new ArgumentException($"Указан неверный диапазон дат");
            var temp = _weather.Where(x => x.Key >= beginTime && x.Key <= endTime).ToList();
            if (!temp.Any()) return;
            foreach (var item in temp)
            {
                _weather.Remove(item.Key);
            }
        }
        /// <summary>
        /// Удаление температуры в указанном интервале дат
        /// </summary>
        /// <param name="time"></param>
        /// <param name="time"></param>
        public List<Weather> GetTemperatures(DateTime beginTime, DateTime endTime)
        {
            if(endTime < beginTime)
                throw new ArgumentException($"Указан неверный диапазон дат");
            return _weather.Where(x => x.Key >= beginTime && x.Key <= endTime).OrderBy(y => y.Key).Select(z => new Weather() { Date = z.Key, Temperature = z.Value }).ToList();

        }
        /// <summary>
        /// Вывод всей погоды
        /// </summary>
        /// <returns></returns>
        public List<Weather> GetAllTemperatures()
        {
            return _weather.OrderBy(x => x.Key).Select(y => new Weather() { Date = y.Key, Temperature = y.Value }).ToList();
        }
    }
}
