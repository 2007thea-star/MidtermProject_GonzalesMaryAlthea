using System;
using System.IO;


namespace StudentSystem
{
    class Program
    {
        static string filePath = @"C:\Users\Mary Althea Gonzales\OneDrive\me\students.txt";
        static void Main(string[] args)
        {
            string folderPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            while (true)
            {
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

                if (choice == "1")
                {
                    RegisterStudent();
                }
                else if (choice == "2")
                {
                    EnrollSubjects();
                }
                else if (choice == "3")
                {
                    EnterGrades();
                }
                else if (choice == "4")
                {
                    ShowGrades();
                }
                else if (choice == "5")
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option. Please try again.");
                }

                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();



                
                
                static void RegisterStudent()
                {


                    Console.WriteLine("\n--- Register Student ---");


                    string studentName = "";
                    while (true)
                    {
                        Console.Write("Enter Student First Name: ");
                        studentName = Console.ReadLine();
                        bool isValid = !string.IsNullOrWhiteSpace(studentName) && studentName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));

                        if (!isValid)
                        {
                            Console.WriteLine("Invalid Name! (Letters and spaces only).");
                            if (AskToTryAgain()) continue; else return;
                        }
                        break;
                    }


                    string studentMiddleInitial = "";
                    while (true)
                    {
                        Console.Write("Enter Student Middle Initial: ");
                        studentMiddleInitial = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(studentMiddleInitial) || studentMiddleInitial.Length != 1 || !char.IsLetter(studentMiddleInitial[0]))
                        {
                            Console.WriteLine("Invalid! Must be a single letter.");
                            if (AskToTryAgain()) continue; else return;
                        }
                        break;
                    }


                    string studentLastName = "";
                    while (true)
                    {
                        Console.Write("Enter Student Last Name: ");
                        studentLastName = Console.ReadLine();
                        bool isValid = !string.IsNullOrWhiteSpace(studentLastName) && studentLastName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));

                        if (!isValid)
                        {
                            Console.WriteLine("Invalid Last Name!");
                            if (AskToTryAgain()) continue; else return;
                        }
                        break;
                    }


                    DateTime birthdate;
                    while (true)
                    {
                        Console.Write("Birthdate (mm/dd/yyyy): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out birthdate) || birthdate > DateTime.Now)
                        {
                            Console.WriteLine("Invalid date or future date.");
                            if (AskToTryAgain()) continue; else return;
                        }
                        break;
                    }


                    int age;
                    while (true)
                    {
                        Console.Write("Age: ");
                        if (!int.TryParse(Console.ReadLine(), out age) || age < 0)
                        {
                            Console.WriteLine("Invalid age.");
                            if (AskToTryAgain()) continue; else return;
                        }
                        break;
                    }

                    string address = "";
                    while (true)
                    {
                        Console.Write("Address: ");
                        address = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(address))
                        {
                            Console.WriteLine("Address cannot be empty.");
                            if (AskToTryAgain()) continue; else return;
                        }
                        break;
                    }


                    string contact = "";
                    while (true)
                    {
                        Console.Write("Contact Number (11 digits): ");
                        contact = Console.ReadLine();
                        if (!long.TryParse(contact, out _) || contact.Length != 11)
                        {
                            Console.WriteLine("Invalid! Must be exactly 11 digits.");
                            if (AskToTryAgain()) continue; else return;
                        }
                        break;
                    }

                    string course = "";
                    while (true)
                    { 

                        Console.Write("Course (BSIT): ");
                        course = Console.ReadLine().ToUpper();
                        if (course != "BSIT")
                        {
                            Console.WriteLine("Invalid! Must be BSIT");
                            if (AskToTryAgain()) continue; else return;
                        }
                        break;
                    }

                    int yearLevel;
                    while (true)
                    {
                        Console.Write("Year Level (1-5): ");
                        if (!int.TryParse(Console.ReadLine(), out yearLevel) || yearLevel < 1 || yearLevel > 5)
                        {
                            Console.WriteLine("Invalid! Numbers 1-5 only.");
                            if (AskToTryAgain()) continue; else return;
                        }
                        break;
                    }


                    string record = $"{studentLastName}|{studentName}|{studentMiddleInitial}|{birthdate.ToShortDateString()}|{age}|{address}|{contact}|{course}|{yearLevel}";
                    File.AppendAllText(filePath, record + Environment.NewLine);
                    Console.WriteLine("\nStudent registered successfully!");



                    static bool AskToTryAgain()
                    {
                        Console.Write("Input again? (Y/N): ");
                        string response = Console.ReadLine().ToUpper();
                        return response == "Y";
                    }
                }

                //prob in course: only bsit ky bsit subs ra akong gi add pero prob sd sa subjects ky any sub can be saved bisag wala sa choices
                //did not put "subjects:" in register students kay mag register paman, didto ra makita sa "EnrollSubjects" and also sa txt file if nag save nag subs enrolled.





                static void EnrollSubjects()
                {
                    Console.Write("Enter the Last Name of the student to enroll: ");
                    string searchName = Console.ReadLine();

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine("Sorry, No records found.");
                        return;
                    }

                    List<string> studentList = new List<string>(File.ReadAllLines(filePath));
                    bool found = false;

                    for (int i = 0; i < studentList.Count; i++)
                    {
                        string[] parts = studentList[i].Split('|');


                        if (parts.Length < 10)
                        {
                            Array.Resize(ref parts, 10);
                        }

                        if (parts[0].Equals(searchName, StringComparison.OrdinalIgnoreCase))
                        {
                            found = true;
                            string wantAnother;

                            do
                            {
                                Console.WriteLine("\nAvailable Subjects: Theo 102A, IT 104B, Rizal 101A, IT 106A, GEC 104A, GEC 103A, PE 102A, COMP 102IT, IT 104A, IT 105A");

                                Console.Write("Enter Subject Name: ");
                                string sName = Console.ReadLine();

                                Console.Write("Enter Subject ID: ");
                                string sID = Console.ReadLine();

                                string newSub = $"{sName}:{sID}:N/A";


                                if (string.IsNullOrWhiteSpace(parts[9]))
                                    parts[9] = newSub;
                                else
                                    parts[9] = parts[9] + "," + newSub;

                                Console.Write("Do you want to add another subject for this student? (Y/N): ");
                                wantAnother = Console.ReadLine().ToUpper();

                            } while (wantAnother == "Y");


                            studentList[i] = string.Join("|", parts);
                            break;
                        }
                    }

                    if (found)
                    {
                        File.WriteAllLines(filePath, studentList);
                        Console.WriteLine("\nEnrollment process completed and saved!");
                    }
                    else
                    {
                        Console.WriteLine("Sorry, Student not found.");
                    }
                }

                //prob in subject: if mag input og lain sub wala sa choices kay mo dawat ra, then ma save sa file.
                //prob in "subject id": maka enter rag any numbers bisag wala sa choices, then ma save ra sa file.






                static void EnterGrades()
                {
                    Console.Write("Enter Last Name: ");
                    string searchName = Console.ReadLine();
                    if (!File.Exists(filePath)) return;

                    List<string> studentList = new List<string>(File.ReadAllLines(filePath));
                    bool found = false;

                    for (int i = 0; i < studentList.Count; i++)
                    {
                        string[] parts = studentList[i].Split('|');
                        if (parts[0].Equals(searchName, StringComparison.OrdinalIgnoreCase))
                        {
                            found = true;
                            if (string.IsNullOrEmpty(parts[9])) { Console.WriteLine("No subjects enrolled."); return; }

                            string[] subs = parts[9].Split(',');
                            for (int j = 0; j < subs.Length; j++)
                            {
                                string[] details = subs[j].Split(':');
                                Console.Write($"Enter grade for {details[0]} ({details[1]}): ");
                                details[2] = Console.ReadLine();
                                subs[j] = string.Join(":", details);
                            }
                            parts[9] = string.Join(",", subs);
                            studentList[i] = string.Join("|", parts);
                            break;
                        }
                    }

                    if (found)
                    {
                        File.WriteAllLines(filePath, studentList);
                        Console.WriteLine("Grades updated!");
                    }
                    else Console.WriteLine("Sorry,Student not found.");
                }





                static void ShowGrades()
                {
                    Console.Write("Enter Last Name: ");
                    string searchName = Console.ReadLine();

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine("No records file found.");
                        return;
                    }

                    List<string> studentList = new List<string>(File.ReadAllLines(filePath));
                    bool found = false; 

                    foreach (string line in studentList)
                    {
                        string[] p = line.Split('|');


                        if (p.Length < 10) continue;

                        if (p[0].Equals(searchName, StringComparison.OrdinalIgnoreCase))
                        {
                            found = true; 
                            Console.WriteLine($"\nName: {p[0]}, {p[1]}");
                            Console.WriteLine($"Course/Year: {p[7]} - {p[8]}");
                            Console.WriteLine("------------------------------");

                            if (!string.IsNullOrEmpty(p[9]))
                            {
                                string[] subs = p[9].Split(',');
                                foreach (string s in subs)
                                {
                                    string[] d = s.Split(':');

                                    Console.WriteLine($"{d[0].PadRight(15)} Grade: {d[2]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No subjects enrolled yet.");
                            }

                            break;
                        }
                    }


                    if (!found)
                    {
                        Console.WriteLine("\nSorry, Student not found in our records.");
                    }
                }



            }
        }
    }
}
