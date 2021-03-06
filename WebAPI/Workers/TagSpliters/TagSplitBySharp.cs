﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Workers.TagSpliters
{
    public class TagSplitBySharp : ITagSplit
    {
        public IEnumerable<string> TagStringSplit(string tagString)
        {
            return tagString.Split("#".ToCharArray()).Distinct().ToList();
        }
    }
}