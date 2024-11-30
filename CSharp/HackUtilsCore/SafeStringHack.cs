using System;
using System.Runtime.InteropServices;

namespace HackUtilsCore
{
    public static class SafeStringHack
    {
        [StructLayout(LayoutKind.Explicit)]
        struct Writer
        {
            private class Helper<T>
            {
                public T value;
            }

            [FieldOffset(0)]
            private Helper<object> obj;
            [FieldOffset(0)]
            private Helper<IntPtr> ptr;

            private static Writer w;

            public static IntPtr ToPointer(object obj)
            {
                if (w.obj == null)
                {
                    w.obj = new Helper<object>();
                }

                w.obj.value = obj;
                return w.ptr.value;
            }

            public static object ToObject(IntPtr ptr)
            {
                if (w.obj == null)
                {
                    w.obj = new Helper<object>();
                }

                w.ptr.value = ptr;
                return w.obj.value;
            }
        }

        public static void Test()
        {
            // Не работает корректно, изменяется не нулевой символ

            const string x = "azaza";

            short[] temp = new short[0];
            IntPtr xptr = Writer.ToPointer(x);
            Marshal.WriteInt32(xptr, Marshal.ReadInt32(Writer.ToPointer(temp)));

            short[] arrToChange = (short[])Writer.ToObject(xptr);
            arrToChange[0] = (short)'b';

            Console.WriteLine("azaza");
        }
    }
}
