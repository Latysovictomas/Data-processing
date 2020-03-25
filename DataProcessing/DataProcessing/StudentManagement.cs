using System;
using System.Collections.Generic;
using System.IO;

namespace DataProcessing
{

    class StudentManagement
    {

        private static Student GenerateGradesRandomly(Student st)
        {
            Random rnd = new Random();
            int max = 10;
            int min = 1;
            int gradeCount = rnd.Next(1, 11); // generates 10 grades
            double randomGrade;
            for (int i = 0; i < gradeCount; i++)
            {
                randomGrade = min + (rnd.NextDouble() * (max - min));
                st.AddGrade(Math.Round(randomGrade, 1));
            }
            double examGrade = min + (rnd.NextDouble() * (max - min));
            st.SetExamGrade(Math.Round(examGrade, 1));

            return st;
        }
        private static Student AskGrades(Student st)
        {
            // Grades and exam by user
            Console.WriteLine("Enter homework grade: ");
            string input = Console.ReadLine();
            while (input != "")
            {
                double grade = double.Parse(input);

                while (ValidateGrade(grade) == false)
                {
                    Console.WriteLine("Grade must be in 10 point system. Try again: ");
                    grade = double.Parse(Console.ReadLine());
                }

                st.AddGrade(grade);
                Console.WriteLine("Enter another grade or press Enter to stop: ");
                input = Console.ReadLine();

            }
            Console.WriteLine("Enter exam grade: ");
            double examGrade = double.Parse(Console.ReadLine());
            while (ValidateGrade(examGrade) == false)
            {
                Console.WriteLine("Grade must be in 10 point system. Try again: ");
                examGrade = double.Parse(Console.ReadLine());
            }
            st.SetExamGrade(examGrade);
            return st;
        }
        private static bool ValidateGrade(double grade)
        {
            return grade >= 1 & grade <= 10;
        }

        private static void PrintTable(List<Student> students, int caseNumber)
        {
            // sort students by surname before printing
            students.Sort((x, y) => String.Compare(x.GetSurname(), y.GetSurname()));


            if (caseNumber == 1)
            {
                Console.WriteLine("{0,-20}{1}{2,30}", "Surname", "Name", "Final points (Med.)");
                Console.WriteLine("{0}", "-------------------------------------------------------");
                foreach (Student st in students)
                {
                    Console.WriteLine("{0,-20}{1}{2,30}", st.GetSurname(), st.GetName(), st.GetFinal(true));
                }
            }
            else if (caseNumber == 2)
            {
                Console.WriteLine("{0,-20}{1}{2,30}", "Surname", "Name", "Final points (Avg.)");
                Console.WriteLine("{0}", "-------------------------------------------------------");
                foreach (Student st in students)
                {
                    Console.WriteLine("{0,-20}{1}{2,30}", st.GetSurname(), st.GetName(), st.GetFinal());
                }
            }
            else if (caseNumber == 3)
            {
                Console.WriteLine("{0,-15}{1}{2,30}{3,25}", "Surname", "Name", "Final points (Avg.)", "Final points (Med.)");
                Console.WriteLine("{0}", "--------------------------------------------------------------------------");
                foreach (Student st in students)
                {
                    Console.WriteLine("{0,-15}{1}{2,27}{3,25}", st.GetSurname(), st.GetName(), st.GetFinal(), st.GetFinal(true));
                }
            }

        }

        private static List<Student> AddStudentsManually(List<Student> students)
        {
            bool addingStudents = true;
            while (addingStudents)
            {
                Student st = new Student();
                Console.WriteLine("Enter Name: ");
                st.SetName(Console.ReadLine());
                Console.WriteLine("Enter Surname: ");
                st.SetSurname(Console.ReadLine());



                Console.WriteLine("Generate grades randomly? (y/n)");
                string genGrades = Console.ReadLine();
                bool notValid = true;
                while (notValid)
                {
                    if (genGrades.ToLower().Equals("n"))
                    {
                        // Grades and exam by user
                        st = AskGrades(st);
                        notValid = false;
                    }
                    else if (genGrades.ToLower().Equals("y"))
                    {
                        // Randomly
                        st = GenerateGradesRandomly(st);

                        notValid = false;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input. Enter y or n: ");
                        genGrades = Console.ReadLine();
                        notValid = true;
                    }
                }



                // Add student
                students.Add(st);


                // Ask for more students
                Console.WriteLine("Create new student? (y/n): ");
                string yesno = Console.ReadLine();
                notValid = true;
                while (notValid)
                {
                    if (yesno.ToLower().Equals("n"))
                    {
                        addingStudents = false;
                        notValid = false;
                    }
                    else if (yesno.ToLower().Equals("y"))
                    {
                        addingStudents = true;
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
            return students;
        }

        private static List<Student> ReadStudentsFromFile(List<Student> students)
        {
            string textFile = "students.txt";
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathToFile = Path.Combine(currentDirectory, @"..\..\..\" + textFile);
            string[] lines;
            try {
                lines = File.ReadAllLines(fullPathToFile);
                for (int i = 1; i < lines.Length; i++) // for each student in file
                {
                    Student st = new Student();
                    string[] line = lines[i].Split(" ");
                    List<string> student = new List<string>(line);

                    st.SetName(student[1]);
                    st.SetSurname(student[0]);
                    double examGrade = double.Parse(student[student.Count - 1]);
                    st.SetExamGrade(examGrade);
                    student.RemoveAt(0);
                    student.RemoveAt(0);
                    student.RemoveAt(student.Count - 1);
                    foreach (string grade in student)
                    {
                        st.AddGrade(double.Parse(grade));
                    }

                    students.Add(st);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e + "\n\nFile not found. Reading user input instead...");
                AddStudentsManually(students);
            }
            


            return students;
        }


        public static List<Student> AddFromFileOrManually(List<Student> students)
        {
            Console.WriteLine("Do you want to add students from file? (y/n)");
            string answer = Console.ReadLine();
            bool notValid = true;
            while (notValid)
            {
                if (answer.ToLower().Equals("n"))
                {

                    //read from user input
                    students = AddStudentsManually(students);
                    notValid = false;
                }

                else if (answer.ToLower().Equals("y"))
                {
                    // Read from file
                    students = ReadStudentsFromFile(students);
                    notValid = false;
                }
                else
                {
                    Console.WriteLine("Wrong input. Enter y or n: ");
                    answer = Console.ReadLine();
                    notValid = true;
                }

            }
            return students;
        }

        public static List<Student> PrintResults(List<Student> students)
        {
            // Print results
            bool printingResults = true;
            while (printingResults)
            {

                Console.WriteLine("To print median results enter '1',\nTo print average results enter '2',\nTo print all results enter '3',\nTo terminate enter '4': ");
                int caseSwitch = int.Parse(Console.ReadLine());
                switch (caseSwitch)
                {
                    case 1:
                        Console.WriteLine("Printing median results...");
                        PrintTable(students, 1);
                        break;
                    case 2:
                        Console.WriteLine("Printing average results...");
                        PrintTable(students, 2);
                        break;
                    case 3:
                        Console.WriteLine("Printing all results...");
                        PrintTable(students, 3);
                        break;
                    case 4:
                        printingResults = false;
                        break;
                    default:
                        Console.WriteLine("Printing average results by default...");
                        PrintTable(students, 2);
                        Console.WriteLine("Default case");
                        break;
                }


            }
            return students;
        }


    }

}