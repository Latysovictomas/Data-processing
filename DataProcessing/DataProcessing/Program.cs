using System;
using System.Collections.Generic;

namespace DataProcessing
{
    

    class Program
    {



        private static void run() {
            List<Student> students = new List<Student>();
            StudentManagement.AddFromFileOrManually(students);
            StudentManagement.PrintResults(students);
        }



            static void Main(string[] args){run();}




        }
    }
