namespace ConsoleUtilsCore
{
    public static class ConsoleInput
    {
        private static IEnumerator<string> _stream = GetInputStream();

        #region Initialize
        private static IEnumerator<string> GetInputStream()
        {
            while (true)
            {
                var stream = GetSafeStream();
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
        public static string GetString()
        {
            _stream.MoveNext();
            return _stream.Current;
        }

        public static byte ReadByte() => byte.Parse(GetString());
        public static int ReadInt() => int.Parse(GetString());
        public static long ReadLong() => long.Parse(GetString());

        public static float ReadFloat() => float.Parse(GetString());
        public static double ReadDouble() => double.Parse(GetString());
        #endregion

        //#region Getters
        //public static string String => GetString();

        //public static byte Byte => ReadByte();
        //public static int Int => ReadInt();
        //public static long Long => ReadLong();

        //public static float Float => ReadFloat();
        //public static double Double => ReadDouble();
        //#endregion
    }
}
