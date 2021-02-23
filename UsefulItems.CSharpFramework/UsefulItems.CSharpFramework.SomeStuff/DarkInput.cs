using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SomeStuff
{
    public class DarkInput
    {
        private IEnumerator<string> istream;

        private static IEnumerable<string> GetNullStream()
        {
            while (true) yield return null;
        }

        private static IEnumerator<string> GetIStream()
        {
            while (true)
            {
                IEnumerable<string> sstream;
                try
                {
                    sstream = Console.ReadLine().Split().Where(s => s.Length > 0);
                }
                catch (Exception)
                {
                    sstream = GetNullStream();
                }

                foreach (string s in sstream)
                    yield return s;
            }
        }

        public DarkInput()
        {
            istream = GetIStream();
        }

        public string GetString()
        {
            istream.MoveNext();
            return istream.Current;
        }

        public int GetInt()
        {
            return int.Parse(GetString());
        }
    }
}
