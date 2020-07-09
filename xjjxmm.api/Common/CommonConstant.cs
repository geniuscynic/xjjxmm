using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xjjxmm.Common
{
    public static class CommonConstant
    {

        public  const string Message_Successfully = "成功";
        public enum CategoryType
        {
            Menu,
            Other
        }

        public const int Page_Size = 5;

        public const string Option_JWT = "JWT";
        //public const string JWT_Key = "Key";
    }

    public static class Exten
    {
        public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
            {
                await func(value);
            }
        }
    }
}
