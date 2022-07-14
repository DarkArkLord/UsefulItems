using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace UsefulItems.CSharp.Trash.Hacks
{
    public static class StringHack
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
                    w.obj = new Helper<object>();

                w.obj.value = obj;
                return w.ptr.value;
            }

            public static object ToObject(IntPtr ptr)
            {
                if (w.obj == null)
                    w.obj = new Helper<object>();

                w.ptr.value = ptr;
                return w.obj.value;
            }
        }

        public static void Test()
        {
            const string x = "azaza";
            short[] qq = new short[0];

            IntPtr xptr = Writer.ToPointer(x);
            Marshal.WriteInt32(xptr, Marshal.ReadInt32(Writer.ToPointer(qq)));

            short[] q = (short[])Writer.ToObject(xptr);

            q[0] = (short)'b';

            Console.WriteLine("azaza");
        }
    }
}
