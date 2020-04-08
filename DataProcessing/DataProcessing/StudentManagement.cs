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
                    Console.WriteLine("{0,-20}{1}{2,30}", st.GetSurname(), st.GetName(), st.CalcFinal(true));
                }
            }
            else if (caseNumber == 2)
            {
                Console.WriteLine("{0,-20}{1}{2,30}", "Surname", "Name", "Final points (Avg.)");
                Console.WriteLine("{0}", "-------------------------------------------------------");
                foreach (Student st in students)
                {
                    Console.WriteLine("{0,-20}{1}{2,30}", st.GetSurname(), st.GetName(), st.CalcFinal());
                }
            }
            else if (caseNumber == 3)
            {
                Console.WriteLine("{0,-15}{1}{2,30}{3,25}", "Surname", "Name", "Final points (Avg.)", "Final points (Med.)");
                Console.WriteLine("{0}", "--------------------------------------------------------------------------");
                foreach (Student st in students)
                {
                    Console.WriteLine("{0,-15}{1}{2,27}{3,25}", st.GetSurname(), st.GetName(), st.CalcFinal(), st.CalcFinal(true));
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

        public static List<Student> ReadStudentsFromFile(List<Student> students, string textFile = "students.txt")
        {
            //string textFile = "students.txt";
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

        private static int GenerateGrade()
        {
            Random rnd = new Random();
            int grade = rnd.Next(1, 11);
            return grade;
        }



        private static List<string> GenerateRandomStudentList(int length)
        {
            // Surname Name HW1 HW2 HW3 HW4 HW5 Exam

            string surname = "Surname";
            string name = "Name";
            List<string> lines = new List<string>();
            for (int i = 1; i < length + 1; i++)
            {

                lines.Add(surname + i + " " + name + i + " " + GenerateGrade() + " " + GenerateGrade() + " " + GenerateGrade() + " " + GenerateGrade() + " " + GenerateGrade() + " " + GenerateGrade());
            }

            return lines;
        }

        //static void FileCreation(List<string> lines, int length1, int length2, int length3, int length4) {


        private static void FileCreation(List<string> lines)
        {

            string filename = "studentList";
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathToFile = Path.Combine(currentDirectory, @"..\..\..\" + filename + lines.Count + ".txt");



            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(fullPathToFile))
            {


                file.WriteLine("Surname Name HW1 HW2 HW3 HW4 HW5 Exam");

                for (int i = 0; i < lines.Count; i++)
                {
                    file.WriteLine(lines[i]);
                }


            }
        }


        private static void SaveListToFile(List<Student> studentList, string filename)
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathToFile = Path.Combine(currentDirectory, @"..\..\..\" + filename + ".txt");
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fullPathToFile))
            {

                file.WriteLine("Surname Name Final Status");
                foreach (Student stud in studentList)
                {
                    file.WriteLine(stud.GetSurname() + " " + stud.GetName() + " " + stud.GetFinalAVG() + " " + stud.GetStatus());
                }

            }
        }

        private static void SaveLinkedListToFile(LinkedList<Student> studentList, string filename)
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathToFile = Path.Combine(currentDirectory, @"..\..\..\" + filename + ".txt");
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fullPathToFile))
            {

                file.WriteLine("Surname Name Final Status");
                foreach (Student stud in studentList)
                {
                    file.WriteLine(stud.GetSurname() + " " + stud.GetName() + " " + stud.GetFinalAVG() + " " + stud.GetStatus());
                }

            }
        }

        private static void SaveQueueToFile(Queue<Student> studentList, string filename)
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathToFile = Path.Combine(currentDirectory, @"..\..\..\" + filename + ".txt");
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(fullPathToFile))
            {

                file.WriteLine("Surname Name Final Status");
                foreach (Student stud in studentList)
                {
                    file.WriteLine(stud.GetSurname() + " " + stud.GetName() + " " + stud.GetFinalAVG() + " " + stud.GetStatus());
                }

            }
        }


        public static void RandomStudentSpeedRateAnalysis(bool generate = false, int container = 0)
        {
            // v0.4, v0.5

            //Console.WriteLine("Student generation...");
            

            List<Student> studentsFromFiles = new List<Student>();
            int length1 = 10000;
            int length2 = 100000;
            int length3 = 1000000;
            int length4 = 10000000;
            int[] lengths = { length1, length2, length3 };
            string currentDirectory = Directory.GetCurrentDirectory();
            string fullPathToFile10000 = Path.Combine(currentDirectory, @"..\..\..\" + "studentList" + 10000 + ".txt");
            string fullPathToFile100000 = Path.Combine(currentDirectory, @"..\..\..\" + "studentList" + 100000 + ".txt");
            string fullPathToFile1000000 = Path.Combine(currentDirectory, @"..\..\..\" + "studentList" + 1000000 + ".txt");

            if (generate == false){
                List<string> lines;
                if (File.Exists(fullPathToFile10000) != true)
            {
                Console.WriteLine("File 10000 does not exist. Creating it...");
                lines = GenerateRandomStudentList(length1);
                //Console.WriteLine("Saving to files...");
                FileCreation(lines);
                lines.Clear(); // Clear list
            }
            if (File.Exists(fullPathToFile100000) != true)
            {
                Console.WriteLine("File 100000 does not exist. Creating it...");
                lines = GenerateRandomStudentList(length2);
                //Console.WriteLine("Saving to files...");
                FileCreation(lines);
                lines.Clear(); // Clear list
            }
            if (File.Exists(fullPathToFile1000000) != true)
            {
                Console.WriteLine("File does not exist. Creating it...");
                lines = GenerateRandomStudentList(length3);
                //Console.WriteLine("Saving to files...");
                FileCreation(lines);
                lines.Clear(); // Clear list
                }
            }


            DateTime totalTime1 = DateTime.Now;
            foreach (int length in lengths)
            {
                DateTime dt1 = DateTime.Now;
                List<string> lines;

                if (generate == true)
                {
                    Console.WriteLine("Generating of length: " + length);
                    lines = GenerateRandomStudentList(length);
                    //Console.WriteLine("Saving to files...");
                    FileCreation(lines);
                    lines.Clear(); // Clear list
                }

                    //Console.WriteLine("Reading students...");
                    studentsFromFiles = StudentManagement.ReadStudentsFromFile(studentsFromFiles, "studentList" + length + ".txt");
                //Console.WriteLine("Sorting passed and failed students...");
                if (container == 0) {
                    Console.WriteLine("Using List with file length: " + length);
                    List<Student> passedList = new List<Student>();
                    List<Student> failedList = new List<Student>();
                    foreach (Student stud in studentsFromFiles)
                    {
                        double final = stud.CalcFinal();
                        stud.SetStatus(final);

                        if (stud.GetStatus() == "passed")
                        {
                            passedList.Add(stud);
                        }
                        else
                        {
                            failedList.Add(stud);
                        }
                    }
                    //Console.WriteLine("Saving sorted students...");
                    SaveListToFile(passedList, "passedStudents");
                    SaveListToFile(failedList, "failedStudents");
                }
                else if (container == 1) { // LinkedList
                    Console.WriteLine("\nUsing LinkedList with file length: " + length);
                    LinkedList<Student> passedList = new LinkedList<Student>();
                    LinkedList<Student> failedList = new LinkedList<Student>();

                    foreach (Student stud in studentsFromFiles)
                    {
                        double final = stud.CalcFinal();
                        stud.SetStatus(final);

                        if (stud.GetStatus() == "passed")
                        {
                            passedList.AddLast(stud);
                        }
                        else
                        {
                            failedList.AddLast(stud);
                        }
                    }
                    //Console.WriteLine("Saving sorted students...");
                    SaveLinkedListToFile(passedList, "passedStudents");
                    SaveLinkedListToFile(failedList, "failedStudents");
                }
                else
                { // Queue
                    Console.WriteLine("\nUsing Queue with file length: " + length);
                    Queue<Student>  passedList = new Queue<Student>();
                    Queue<Student>  failedList = new Queue<Student>();

                    foreach (Student stud in studentsFromFiles)
                    {
                        double final = stud.CalcFinal();
                        stud.SetStatus(final);

                        if (stud.GetStatus() == "passed")
                        {
                            passedList.Enqueue(stud);

                        }
                        else
                        {
                            failedList.Enqueue(stud);
                        }
                    }
                    //Console.WriteLine("Saving sorted students...");
                    SaveQueueToFile(passedList, "passedStudents");
                    SaveQueueToFile(failedList, "failedStudents");
                }





                    DateTime dt2 = DateTime.Now;
                    Console.WriteLine("Elapsed time: " + (dt2 - dt1));
                    Console.WriteLine();

            }

   
                DateTime totalTime2 = DateTime.Now;              
                Console.WriteLine("Total elapsed time: " + (totalTime2 - totalTime1));
            
 

        }



        public static void AskSpeedRateAnalysisNoGeneration(List<Student> students)
        {

            Console.WriteLine("v0.5: Perform speed rate analysis with no automatic generation for List, QUEUE and LinkedList? (y/n): ");
            string yesno = Console.ReadLine();
            bool notValid = true;
            while (notValid)
            {
                if (yesno.ToLower().Equals("y"))
                {
                    // generating - y/n, container: list - 0, linkedList - 1, Queue - 2 
                    for (int container = 0; container <= 2; container++)
                    {
                        StudentManagement.RandomStudentSpeedRateAnalysis(false, container);
                    }

                    StudentManagement.AddFromFileOrManually(students);
                    StudentManagement.PrintResults(students);
                    notValid = false;


                }
                else if (yesno.ToLower().Equals("n"))
                {

                    StudentManagement.AddFromFileOrManually(students);
                    StudentManagement.PrintResults(students);
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
    }

}