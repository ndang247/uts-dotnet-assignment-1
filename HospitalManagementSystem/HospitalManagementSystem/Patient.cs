using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    public class Patient : User
    {
        private string address, email, phone;
        public Patient(string id, string password, string fullName, string address, string email, string phone, string role) : base(id, password, fullName, role)
        {
            this.address = address;
            this.email = email;
            this.phone = phone;
        }

        private void MyDetails()
        {
            Console.Clear();
            Console.WriteLine(" ______________________________________");
            Console.WriteLine("|                                      |");
            Console.WriteLine("|   DOTNET Hospital Managment System   |");
            Console.WriteLine("|______________________________________|");
            Console.WriteLine("|               My Details             |");
            Console.WriteLine("|______________________________________|");
            Console.WriteLine();
            Console.WriteLine($"{fullName}'s Details");
            Console.WriteLine();
            Console.WriteLine($"Patient ID: {id}\nFull Name: {fullName}\nAddress: {address}\nEmail: {email}\nPhone: {phone}");
            Console.ReadKey();
            Menu();
        }

        private void MyDoctor()
        {
            Console.Clear();
            Console.WriteLine(" ______________________________________");
            Console.WriteLine("|                                      |");
            Console.WriteLine("|   DOTNET Hospital Managment System   |");
            Console.WriteLine("|______________________________________|");
            Console.WriteLine("|               My Doctor              |");
            Console.WriteLine("|______________________________________|");
            Console.WriteLine();
            Console.WriteLine("Your doctor:");
            Console.WriteLine();
            Console.WriteLine("Name | Email Address | Phone | Address");
            Console.WriteLine("--------------------------------------");
            if (File.Exists($"Patients\\RegisteredDoctors\\{id}.txt"))
            {
                string[] registeredDoctors = File.ReadAllLines($"Patients\\RegisteredDoctors\\{id}.txt");
                foreach (string registeredDoctor in registeredDoctors)
                {
                    string[] doctor = File.ReadAllLines($"Doctors\\{registeredDoctor}.txt");
                    string[] doctorInfo = doctor[0].Split(';');
                    Doctor d = new Doctor(doctorInfo[0], doctorInfo[1], doctorInfo[2], doctorInfo[3], doctorInfo[4], doctorInfo[5], "Doctor");
                    Console.WriteLine(d);
                }
            }
            Console.ReadKey();
            Menu();
        }

        private void MyAppointment()
        {
            Console.Clear();
            Console.WriteLine(" ______________________________________");
            Console.WriteLine("|                                      |");
            Console.WriteLine("|   DOTNET Hospital Managment System   |");
            Console.WriteLine("|______________________________________|");
            Console.WriteLine("|            My Appointment            |");
            Console.WriteLine("|______________________________________|");
            Console.WriteLine();
            Console.WriteLine($"Appointment for {fullName}");
            Console.WriteLine();
            Console.WriteLine("Doctor | Patient | Description");
            Console.WriteLine("------------------------------");
            if (File.Exists($"Appointments\\Patients\\{id}.txt"))
            {
                string[] lines = File.ReadAllLines($"Appointments\\Patients\\{id}.txt");
                foreach (string line in lines)
                {
                    string[] appointmentInfo = line.Split('|');
                    Appointment appointment = new Appointment(appointmentInfo[0], appointmentInfo[1], appointmentInfo[2]);
                    Console.WriteLine(appointment);
                }
            }
            Console.ReadKey();
            Menu();
        }

        private void BookAppointment()
        {
            Console.Clear();
            Console.WriteLine(" ______________________________________");
            Console.WriteLine("|                                      |");
            Console.WriteLine("|   DOTNET Hospital Managment System   |");
            Console.WriteLine("|______________________________________|");
            Console.WriteLine("|           Book Appointment           |");
            Console.WriteLine("|______________________________________|");

            Booking();

            Console.ReadKey();
            Menu();
        }

        private void Booking()
        {
            if (File.Exists($"Patients\\RegisteredDoctors\\{id}.txt"))
            {
                // Read the file in RegisteredDoctor
                string[] line = File.ReadAllLines($"Patients\\RegisteredDoctors\\{id}.txt");

                // Lookup the doctor with the ID
                string[] doctor = File.ReadAllLines($"Doctors\\{line[0]}.txt");
                string[] doctorInfo = doctor[0].Split(';');

                Console.WriteLine();
                Console.WriteLine($"You are booking a new appointment with {doctorInfo[2]}");
                Console.Write("Description of the appointment: ");
                string description = Console.ReadLine();

                //if (string.IsNullOrEmpty(description))
                //{
                //    Console.WriteLine("Description cannot be empty, press any key to try again");
                //    Console.ReadKey();
                //    BookAppointment();
                //}

                // Check if the patient already has an appointment with this doctor
                if (File.Exists($"Appointments\\Patients\\{id}.txt"))
                {
                    // Append a new appointment to the file on a new line
                    File.AppendAllText($"Appointments\\Patients\\{id}.txt", $"\n{doctorInfo[0]}|{id}|{description}");
                }
                else File.WriteAllText($"Appointments\\Patients\\{id}.txt", $"{doctorInfo[0]}|{id}|{description}");

                // Check if the doctor already has an appointment with this patient
                if (File.Exists($"Appointments\\Doctors\\{doctorInfo[0]}.txt"))
                {
                    // Append a new appointment to the file on a new line
                    File.AppendAllText($"Appointments\\Doctors\\{doctorInfo[0]}.txt", $"\n{doctorInfo[0]}|{id}|{description}");
                }
                else File.WriteAllText($"Appointments\\Doctors\\{doctorInfo[0]}.txt", $"{doctorInfo[0]}|{id}|{description}");

                Console.WriteLine("The appointment has been booked successfully");
            }
            else
            {
                Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with");
                // Read all the files in doctors
                string[] files = Directory.GetFiles("Doctors");
                for (int i = 0; i < files.Length; i++)
                {
                    // Read each file in doctors
                    string[] lines = File.ReadAllLines(files[i]);
                    string[] doctorInfo = lines[0].Split(';');
                    // Display the doctor info
                    Console.WriteLine($"{i + 1} {doctorInfo[2]} | {doctorInfo[4]} | {doctorInfo[5]} | {doctorInfo[3]}");
                }
                try
                {
                    Console.Write("Please choose a doctor: ");
                    int choice = int.Parse(Console.ReadLine());

                    string choosenDoctor = files[choice - 1];
                    string[] lines = File.ReadAllLines(choosenDoctor);
                    string[] doctorInfo = lines[0].Split(';');

                    // Add the choosen doctor ID to the RegisteredDoctors
                    File.WriteAllText($"Patients\\RegisteredDoctors\\{id}.txt", $"{doctorInfo[0]}");

                    // Also add the patient ID to the RegisteredPatients
                    if (File.Exists($"Doctors\\RegisteredPatients\\{doctorInfo[0]}.txt"))
                    {
                        File.AppendAllText($"Doctors\\RegisteredPatients\\{doctorInfo[0]}.txt", $"\n{id}");
                    }
                    else File.WriteAllText($"Doctors\\RegisteredPatients\\{doctorInfo[0]}.txt", $"{id}");
                    Booking();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Invalid choice {e.Message} Press any key to try again");
                    Console.ReadKey();
                    BookAppointment();
                }
            }
        }

        public override void Menu()
        {
            Console.Clear();
            Console.WriteLine(" ______________________________________");
            Console.WriteLine("|                                      |");
            Console.WriteLine("|   DOTNET Hospital Managment System   |");
            Console.WriteLine("|______________________________________|");
            Console.WriteLine("|             Patient Menu             |");
            Console.WriteLine("|______________________________________|");
            Console.WriteLine();

            Console.WriteLine($"Welcome to DOTNET Hospital Managment System {fullName}");
            Console.WriteLine();
            Console.WriteLine("Please choose an option:");
            foreach (string option in options)
            {
                Console.WriteLine(option);
            }

            try
            {
                ConsoleKeyInfo info = Console.ReadKey(true);

                switch (info.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        MyDetails();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        MyDoctor();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        MyAppointment();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        BookAppointment();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.Clear();
                        Program.Main();
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Environment.Exit(0);
                        break;
                    default:
                        throw new Exception($"Invalid option, please choose from 1-{options.Length}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Menu();
            }
        }

        public override string ToString()
        {
            // Get the registered doctor
            string[] registeredDoctor = File.ReadAllLines($"Patients\\RegisteredDoctors\\{id}.txt");
            string[] doctor = File.ReadAllLines($"Doctors\\{registeredDoctor[0]}.txt");
            string doctorName = doctor[0].Split(';')[2];

            return $"{fullName} | {doctorName} | {email} | {phone} | {address}";
        }
    }
}
