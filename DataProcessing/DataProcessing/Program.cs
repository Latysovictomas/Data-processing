using System;
using System.Collections.Generic;

namespace DataProcessing
{
    class Student {
        private string name;
        private string surname;
        private List<double> grades = new List<double>();
        private double average;
        private double median;
        private double exam;

        public Student() { }

        public Student(string name, string surname) {
            this.name = name;
            this.surname = surname;
        }

        public void SetName(string name) {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetExamGrade(double examGrade)
        {
            this.exam = examGrade;
        }

        public double GetExamGrade()
        {
            return this.exam;
        }

        public void SetGrades(List<double> grades)
        {
            this.grades = grades;
        }

        public void AddGrade(double grade)
        {
            this.grades.Add(grade);
        }

        public List<double> getGrades()
        {
            return this.grades;

        }

        public void SetSurname(string surname)
        {
            this.surname = surname;
        }

        public string GetSurname()
        {
            return this.surname;
        }

        public void CalAverage()
        {
            double container = 0;
            foreach (double grade in this.grades)
            {
                container += grade;
            }
            this.average = container / this.grades.Count;
        }
        public void CalMedian()
            {
                this.grades.Sort();
                int size = this.grades.Count;
            
                int middle = size / 2;
                this.median = (size % 2 != 0) ? (double)this.grades[middle] : ((double)this.grades[middle] + (double)this.grades[middle - 1]) / 2;
            }

        public double GetFinal(bool isMedian=false) {
            
            
            if (isMedian == true) {
                this.CalMedian();
                return Math.Round((0.3 * this.median) + (0.7 * this.exam), 2);
            }    
            else {
                this.CalAverage();
                return Math.Round((0.3 * this.average) + (0.7 * this.exam),2);
            }
        }

        public void PrintStudentInfo() {
            Console.WriteLine("Name: " + this.name);
            Console.WriteLine("Surname: " + this.surname);
            Console.WriteLine("final average: " + this.GetFinal(false));
            Console.WriteLine("final median: " + this.GetFinal(true));
            foreach (double grade in this.grades) {
                Console.WriteLine("Grade: " + grade);
            }
            Console.WriteLine("Exam: " + this.exam);

        }

    }

    class Program
    {


        static Student GenerateGradesRandomly(Student st)
        {
            Random rnd = new Random();
            int max = 10;
            int min = 1;
            int gradeCount = rnd.Next(1, 11); // generates 10 grades
            double randomGrade;
            for(int i=0; i<gradeCount; i++)
            {
                randomGrade = min + (rnd.NextDouble() * (max - min));
                st.AddGrade(Math.Round(randomGrade, 1));
            }
            double examGrade = min + (rnd.NextDouble() * (max - min));
            st.SetExamGrade(Math.Round(examGrade,1));

            return st;
        }
            static Student AskGrades(Student st) {
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
        static bool ValidateGrade(double grade) {
            return grade >= 1 & grade <= 10;
        }

        static void PrintTable(List<Student> students, bool isMedian)
        {
            if (isMedian)
            {
                Console.WriteLine("{0,-20}{1}{2,30}", "Surname", "Name", "Final points (Med.)");
                Console.WriteLine("{0}", "-------------------------------------------------------");
                foreach (Student st in students)
                {
                    Console.WriteLine("{0,-20}{1}{2,30}", st.GetSurname(), st.GetName(), st.GetFinal(true));
                }
            }
            else {
                Console.WriteLine("{0,-20}{1}{2,30}", "Surname", "Name", "Final points (Avg.)");
                Console.WriteLine("{0}", "-------------------------------------------------------");
                foreach (Student st in students)
                {
                    Console.WriteLine("{0,-20}{1}{2,30}", st.GetSurname(), st.GetName(), st.GetFinal());
                }
            }

        }

        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
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
                    else {
                        Console.WriteLine("Wrong input. Enter y or n: ");
                        yesno = Console.ReadLine();
                        notValid = true;
                    }
                }
            }



            


            // Print results
            bool printingResults = true;
            while (printingResults)
            {
                Console.WriteLine("To print average results enter '1', to print median results enter '2', to terminate enter '3': ");
                int caseSwitch = int.Parse(Console.ReadLine());
                switch (caseSwitch)
                {
                    case 1:
                        Console.WriteLine("Printing average results...");
                        PrintTable(students, false);
                        break;
                    case 2:
                        Console.WriteLine("Printing median results...");
                        PrintTable(students, true);
                        break;
                    case 3:
                        printingResults = false;
                        break;
                    default:
                        Console.WriteLine("Printing average results by default...");
                        PrintTable(students, false);
                        Console.WriteLine("Default case");
                        break;
                }


            }


        }
    }
}
