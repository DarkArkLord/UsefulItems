using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackUtilsCore
{
    public class UnsafeStringHack
    {
        public static void Test()
        {
            Console.WriteLine("abc");
            UpdateString("abc");
            Console.WriteLine("abc");
        }

        private static void UpdateString(string str)
        {
            unsafe
            {
                fixed(char* ptr = str)
                {
                    ptr[0] = '@';
                }
            }
        }
    }
}
