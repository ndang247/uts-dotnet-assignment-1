using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    class Login
    {
        private string id, password;

        public void LoginMenu()
        {
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |                LOGIN                 |");
            Console.WriteLine(" |______________________________________|");

            Console.Write("ID: ");
            id = Console.ReadLine();
            Console.Write("Password: ");
            password = MaskPassword();
            HandleLogin();
        }

        private string MaskPassword()
        {
            string password = "";

            // Read a key without displaying it
            ConsoleKeyInfo info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    // Display an asterisk to mask the character
                    Console.Write("*");
                    // Append the character to the password
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // Handle backspace by removing the last character
                        password = password.Remove(password.Length - 1);
                        // Move the cursor back by one character
                        Console.Write("\b \b");
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }


        private void HandleLogin()
        {
            // Try catch to handle login
            try
            {
                // Get the credentials from file
                string file = $"{id}.txt";

                // Check if the file exists in three role directories
                if (File.Exists($"Administrators\\{file}"))
                {
                    CheckCredentails("Administrators", file);
                }
                else if (File.Exists($"Doctors\\{file}"))
                {
                    CheckCredentails("Doctors", file);
                }
                else if (File.Exists($"Patients\\{file}"))
                {
                    CheckCredentails("Patients", file);
                }
                else
                {
                    throw new Exception("Invalid ID or account user doesn't exist, press any key to try again");
                }
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "Invalid password, press any key to try again":
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Console.Clear();
                        LoginMenu();
                        break;
                    case "Invalid ID or account user doesn't exist, press any key to try again":
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Console.Clear();
                        LoginMenu();
                        break;
                    default:
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Console.Clear();
                        LoginMenu();
                        break;
                }
            }
        }

        private void CheckCredentails(string role, string file)
        {
            // Read the file
            string[] lines = File.ReadAllLines($"{role}\\{file}");
            string[] details = lines[0].Split(';');

            // Check if the id and password is correct
            if (id == details[0] && password == details[1])
            {
                Console.WriteLine("Valid Credentials");
                Console.ReadKey();

                switch (role)
                {
                    case "Patients":
                        Patient patient = new Patient(details[0], details[1], details[2], details[3], details[4], details[5], "Patient");
                        patient.Menu();
                        break;
                    case "Doctors":
                        Doctor doctor = new Doctor(details[0], details[1], details[2], details[3], details[4], details[5], "Doctor");
                        doctor.Menu();
                        break;
                    //case "Administrators":
                    //    Administrator administrator = new Administrator(details[0], details[1], details[2], details[3], details[4], details[5]);
                    //    string[] options3 =
                    //    {
                    //        "1. List administrator details",
                    //        "2. List all doctors",
                    //        "3. List all patients",
                    //        "4. List all appointments",
                    //        "5. Exit to login",
                    //        "6. Exit System"
                    //    };
                    //    administrator.Menu(options3);
                    //    break;
                    default:
                        break;
                }
            }
            else
            {
                throw new Exception("Invalid password, press any key to try again");
            }
        }
    }
}
