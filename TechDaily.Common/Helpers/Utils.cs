using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechDaily.Common.Helpers
{
    public class Utils
    {
        public static List<T> Paginate<T>(List<T> data, ref int pageSize, ref int pageNumber)
        {
            if (pageSize == 0) pageSize = 10;
            if (pageNumber == 0) pageNumber = 1;

            return data.Skip(pageSize * (pageNumber-1)).Take(pageSize).ToList();
        }
    }
}
