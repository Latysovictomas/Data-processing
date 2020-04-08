using System;
using System.Collections.Generic;
using System.IO;
namespace DataProcessing
{
    

    class Program
    {



        private static void run() {
            

            List<Student> students = new List<Student>();





            Console.WriteLine("v0.4: Perform speed rate analysis for automatic student generation and sorting with different file sizes? (y/n): ");
            string yesno = Console.ReadLine();
            bool notValid = true;
            while (notValid)
            {
                if (yesno.ToLower().Equals("y"))
                {
                    // generating - y/n, container: list - 0, linkedList - 1, Queue - 2 

                    StudentManagement.RandomStudentSpeedRateAnalysis(true, 0);
                    StudentManagement.AskSpeedRateAnalysisNoGeneration(students);
                    StudentManagement.AddFromFileOrManually(students);
                    StudentManagement.PrintResults(students);
                    notValid = false;


                }
                else if (yesno.ToLower().Equals("n"))
                {

                    StudentManagement.AskSpeedRateAnalysisNoGeneration(students);
                    notValid = false;
                }
                else
                {
                    Console.WriteLine("Wrong input. Enter y or n: ");
                    yesno = Console.ReadLine();
                    notValid = true;
                }
            }



        }



            static void Main(string[] args){run();}




        }
    }
