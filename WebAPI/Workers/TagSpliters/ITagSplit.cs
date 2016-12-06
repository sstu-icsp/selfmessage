using System.Collections.Generic;
using WebAPI.Models.Entities;

namespace WebAPI.Workers.TagSpliters
{
    public interface ITagSplit
    {
        //Интерфейс для реализации стратегии по тэгам
        IEnumerable<string> TagStringSplit(string tagString);
    }
}
