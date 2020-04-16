using System;
using System.Collections.Generic;
namespace DataProcessing
{
    

    class Program
    {



        private static void MENU() {

            List<Student> students = new List<Student>();

            bool selecting = true;
            while (selecting) { 
            Console.WriteLine("Menu:" +
                "\n1 - student creation" +
                "\n2 - speed rate analysis with file creation(v0.4)" +
                "\n3 - speed rate analysis without file creation(v0.5)" +
                "\n4 - speed rate analysis for strategy 1 and strategy 2(v1.0)" +
                "\n5 - TERMINATE;");

            Console.WriteLine("Your input: ");
            string menuSelection = Console.ReadLine();
            bool wrongInput = true;
            int caseSwitch=0;
            while (wrongInput) {
                if (int.TryParse(menuSelection, out int number))
                {
                    caseSwitch = number;
                    wrongInput = false;
                }
                else {
                    Console.WriteLine("Wrong input, try again: ");
                    menuSelection = Console.ReadLine();
                    wrongInput = true;
                }
            }

                switch (caseSwitch)
            {
                case 1:
                        Console.WriteLine("Student creation selected.");
                        StudentManagement.AddFromFileOrManually(students);
                        StudentManagement.PrintResults(students);
                        break;
                case 2:
                        Console.WriteLine("Speed rate analysis with file creation selected.");
                        // generating - y/n, container: list - 0, linkedList - 1, Queue - 2 
                        StudentManagement.RandomStudentSpeedRateAnalysis(true, 0);
                    break;
                case 3:
                        Console.WriteLine("Speed rate analysis without file creation selected.");
                        // generating - y/n, container: list - 0, linkedList - 1, Queue - 2
                        for (int container = 0; container <= 2; container++)
                        {
                            StudentManagement.RandomStudentSpeedRateAnalysis(false, container);
                        }
                        break;
                case 4:
                        Console.WriteLine("Speed rate analysis for strategy 1 and strategy 2 selected.");
                        // generating - y/n, container: list - 0, linkedList - 1, Queue - 2 , strategy: 1 or 2
                        for (int strategy=1; strategy <= 2; strategy++) {
                            Console.WriteLine("=================");
                            Console.WriteLine("Using strategy: " + strategy);
                            Console.WriteLine("=================");
                            for (int container = 0; container <= 2; container++)
                            {
                                StudentManagement.RandomStudentSpeedRateAnalysis(false, container, strategy);
                            }
                        }

                        break;
                case 5:
                        Console.WriteLine("Terminating...");
                        selecting = false;
                        break;
                default:
                        Console.WriteLine("Student creation selected by default.");
                        StudentManagement.AddFromFileOrManually(students);
                        StudentManagement.PrintResults(students);
                        break;
                }

            }
        }



            static void Main(string[] args){MENU();}


        }
    }
