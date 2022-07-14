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
                Print(ps.AddScript("Write-Output 1")
                    .Invoke());
                ps.Commands.Clear();
                Print(ps.AddCommand("Write-Output")
                    .Invoke(new[] { 2, 3, 4 }));
                ps.Commands.Clear();
                Print(ps.AddCommand("Sort-Object")
                    .AddCommand("Write-Output")
                    .Invoke(new[] { 7, 6, 5 }));
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
