using System;
using System.Collections.Generic;
using System.Text;

namespace DataProcessing
{
    class Student
    {
        private string name;
        private string surname;
        private List<double> grades = new List<double>();
        private double average;
        private double median;
        private double exam;

        public Student() { }

        public Student(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public void SetName(string name)
        {
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

        public double GetFinal(bool isMedian = false)
        {


            if (isMedian == true)
            {
                this.CalMedian();
                return Math.Round((0.3 * this.median) + (0.7 * this.exam), 2);
            }
            else
            {
                this.CalAverage();
                return Math.Round((0.3 * this.average) + (0.7 * this.exam), 2);
            }
        }

        public void PrintStudentInfo()
        {
            Console.WriteLine("Name: " + this.name);
            Console.WriteLine("Surname: " + this.surname);
            Console.WriteLine("final average: " + this.GetFinal(false));
            Console.WriteLine("final median: " + this.GetFinal(true));
            foreach (double grade in this.grades)
            {
                Console.WriteLine("Grade: " + grade);
            }
            Console.WriteLine("Exam: " + this.exam);

        }

    }
}
