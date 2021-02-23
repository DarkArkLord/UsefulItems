using System;
using System.Collections.Generic;
using System.Linq;

namespace UsefulItems.CSharpFramework.SomeStuff
{
    public class ConsoleInput
    {
        public static ConsoleInput Instance { get; } = new ConsoleInput();

        private readonly IEnumerator<string> stream;

        public ConsoleInput()
        {
            stream = GetInputStream();
        }

        #region Initialize
        private static IEnumerator<string> GetInputStream()
        {
            while (true)
            {
                IEnumerable<string> stream = GetSafeStream();

                foreach (string current in stream)
                {
                    yield return current;
                }
            }
        }

        private static IEnumerable<string> GetSafeStream()
        {
            try
            {
                return GetConsoleStream();
            }
            catch
            {
                return GetNullStream();
            }
        }

        private static IEnumerable<string> GetConsoleStream()
        {
            return Console.ReadLine().Split().Where(x => x.Length > 0);
        }

        private static IEnumerable<string> GetNullStream()
        {
            while (true)
            {
                yield return null;
            }
        }
        #endregion

        #region GetMethods
        public string GetString()
        {
            stream.MoveNext();
            return stream.Current;
        }

        public byte GetByte() => byte.Parse(GetString());
        public int GetInt() => int.Parse(GetString());
        public long GetLong() => long.Parse(GetString());

        public float GetFloat() => float.Parse(GetString());
        public double GetDouble() => double.Parse(GetString());
        #endregion

        #region Getters
        public string String => GetString();

        public int Byte => GetByte();
        public int Int => GetInt();
        public long Long => GetLong();

        public float Float => GetFloat();
        public double Double => GetDouble();
        #endregion
    }
}
