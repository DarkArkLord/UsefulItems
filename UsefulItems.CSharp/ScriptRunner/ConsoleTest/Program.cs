using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START!\n==========");

            using (PowerShell ps = PowerShell.Create())
            {
                Print(ps.AddScript("cd C:/")
                    .Invoke());
            }

            using (PowerShell ps = PowerShell.Create())
            {
                Print(ps.AddScript("dir")
                    .Invoke());
            }

            Console.WriteLine("\n==========\nEND!");
        }

        static void Print(Collection<PSObject> pipeline)
        {
            foreach (var item in pipeline)
            {
                Console.WriteLine(item.BaseObject.ToString());
            }
        }
    }
}
