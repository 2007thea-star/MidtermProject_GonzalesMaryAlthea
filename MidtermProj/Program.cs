using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace StudentSystem
{
    public class Subject
    {
        public string SubjectName { get; set; }
        public string SubjectID { get; set; }
        public string Grade { get; set; } = "N/A";
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Course { get; set; }
        public int YearLevel { get; set; }
        public List<Subject> EnrolledSubjects { get; set; } = new List<Subject>();
    }

    class Program
    {

        static string filePath = @"C:\Users\Mary Althea Gonzales\OneDrive\list\students.json";

        static void Main(string[] args)
        {
            string folderPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║          --- MAIN MENU ---           ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║    1. Register Student               ║");
                Console.WriteLine("║    2. Enroll Student Subjects        ║");
                Console.WriteLine("║    3. Enter Grades                   ║");
                Console.WriteLine("║    4. Show Grade                     ║");
                Console.WriteLine("║    5. EXIT                           ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                if (choice == "1") RegisterStudent();
                else if (choice == "2") EnrollSubjects();
                else if (choice == "3") EnterGrades();
                else if (choice == "4") ShowGrades();
                else if (choice == "5") break;
                else Console.WriteLine("Invalid option.");

                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
            }
        }




        static void RegisterStudent()
        {
            Student s = new Student();
            Console.WriteLine("\n--- Register Student ---");


            while (true)
            {
                Console.Write("Enter Student First Name: ");
                string input = Console.ReadLine();
                bool isValid = !string.IsNullOrWhiteSpace(input);

                if (isValid)
                {
                    foreach (char c in input)
                    {
                        if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                if (!isValid)
                {
                    Console.WriteLine("Invalid Name! (Letters and spaces only).");
                    if (AskToTryAgain()) continue; else return;
                }
                s.FirstName = input;
                break;
            }


            while (true)
            {
                Console.Write("Enter Student Middle Initial: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("Invalid! Must be a single letter.");
                    if (AskToTryAgain()) continue; else return;
                }
                s.MiddleInitial = input;
                break;
            }


            while (true)
            {
                Console.Write("Enter Student Last Name: ");
                string input = Console.ReadLine();
                bool isValid = !string.IsNullOrWhiteSpace(input);

                if (isValid)
                {
                    foreach (char c in input)
                    {
                        if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                        {
                            isValid = false;
                            break;
                        }
                    }
                }

                if (!isValid)
                {
                    Console.WriteLine("Invalid Last Name!");
                    if (AskToTryAgain()) continue; else return;
                }
                s.LastName = input;
                break;
            }


            while (true)
            {
                Console.Write("Birthdate (mm/dd/yyyy): ");
                DateTime bday;
                if (!DateTime.TryParse(Console.ReadLine(), out bday) || bday > DateTime.Now)
                {
                    Console.WriteLine("Invalid date or future date.");
                    if (AskToTryAgain()) continue; else return;
                }
                s.Birthdate = bday.ToShortDateString();
                break;
            }


            while (true)
            {
                Console.Write("Age: ");
                int ageInput;
                if (!int.TryParse(Console.ReadLine(), out ageInput) || ageInput < 0)
                {
                    Console.WriteLine("Invalid age.");
                    if (AskToTryAgain()) continue; else return;
                }
                s.Age = ageInput;
                break;
            }


            while (true)
            {
                Console.Write("Address: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Address cannot be empty.");
                    if (AskToTryAgain()) continue; else return;
                }
                s.Address = input;
                break;
            }


            while (true)
            {
                Console.Write("Contact Number (11 digits): ");
                string input = Console.ReadLine();
                bool isNumeric = true;
                foreach (char c in input) if (!char.IsDigit(c)) isNumeric = false;

                if (!isNumeric || input.Length != 11)
                {
                    Console.WriteLine("Invalid! Must be exactly 11 digits.");
                    if (AskToTryAgain()) continue; else return;
                }
                s.Contact = input;
                break;
            }


            while (true)
            {
                Console.Write("Course (BSIT): ");
                string input = Console.ReadLine().ToUpper();
                if (input != "BSIT")
                {
                    Console.WriteLine("Invalid! Must be BSIT");
                    if (AskToTryAgain()) continue; else return;
                }
                s.Course = input;
                break;
            }


            while (true)
            {
                Console.Write("Year Level (1-5): ");
                int yLevel;
                if (!int.TryParse(Console.ReadLine(), out yLevel) || yLevel < 1 || yLevel > 5)
                {
                    Console.WriteLine("Invalid! Numbers 1-5 only.");
                    if (AskToTryAgain()) continue; else return;
                }
                s.YearLevel = yLevel;
                break;
            }


            List<Student> studentList = LoadData();
            studentList.Add(s);
            SaveData(studentList);
            Console.WriteLine("\nStudent registered successfully!");
        }

        static bool AskToTryAgain()
        {
            Console.Write("Input again? (Y/N): ");
            string response = Console.ReadLine().ToUpper();
            return response == "Y";
        }





        static void EnrollSubjects()
        {
            Console.Write("Enter Last Name: ");
            string search = Console.ReadLine();
            List<Student> students = LoadData();
            bool found = false;

            foreach (Student s in students)
            {
                if (s.LastName.Equals(search, StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    string again;
                    do
                    {
                        Console.WriteLine("\nAvailable Subjects: Theo 102A, IT 104B, Rizal 101A, IT 106A, GEC 104A, GEC 103A, PE 102A, COMP 102, IT 104A, IT 105A");
                        Subject sub = new Subject();
                        Console.Write("Enter Subject Name: ");
                        sub.SubjectName = Console.ReadLine();
                        Console.Write("Enter Subject ID: ");
                        sub.SubjectID = Console.ReadLine();

                        s.EnrolledSubjects.Add(sub);

                        Console.Write("Add another subject? (Y/N): ");
                        again = Console.ReadLine().ToUpper();
                    } while (again == "Y");
                    break;
                }
            }

            if (found) SaveData(students);
            else Console.WriteLine("Student not found.");
        }





        static void EnterGrades()
        {
            Console.Write("Enter Last Name: ");
            string search = Console.ReadLine();
            List<Student> students = LoadData();

            foreach (Student s in students)
            {
                if (s.LastName.Equals(search, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (Subject sub in s.EnrolledSubjects)
                    {
                        Console.Write($"Grade for {sub.SubjectName}: ");
                        sub.Grade = Console.ReadLine();
                    }
                    SaveData(students);
                    Console.WriteLine("Grades updated!");
                    return;
                }
            }
            Console.WriteLine("Student not found.");
        }





        static void ShowGrades()
        {
            Console.Write("Enter Last Name: ");
            string search = Console.ReadLine();
            List<Student> students = LoadData();

            foreach (Student s in students)
            {
                if (s.LastName.Equals(search, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\nStudent: {s.LastName}, {s.FirstName}");
                    foreach (Subject sub in s.EnrolledSubjects)
                    {
                        Console.WriteLine($"- {sub.SubjectName} ({sub.SubjectID}): {sub.Grade}");
                    }
                    return;
                }
            }
            Console.WriteLine("Student not found.");
        }





        static List<Student> LoadData()
        {
            if (!File.Exists(filePath)) return new List<Student>();
            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json)) return new List<Student>();

            return JsonSerializer.Deserialize<List<Student>>(json);
        }

        static void SaveData(List<Student> students)
        {
            string json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}