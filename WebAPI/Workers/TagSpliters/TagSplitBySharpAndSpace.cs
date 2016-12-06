using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models.Entities;

namespace WebAPI.Workers.TagSpliters
{
    public class TagSplitBySharpAndSpace : ITagSplit
    {
        public IEnumerable<string> TagStringSplit(string tagString)
        {
            return tagString.Split(" #".ToCharArray()).Distinct().ToList();
        }
    }
}