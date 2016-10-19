using System.Collections.Generic;
using WebAPI.Models.Entities;

namespace WebAPI.Workers.TagSpliters
{
    public interface ITagSplit
    {
        IEnumerable<string> TagStringSplit(string tagString);
    }
}
