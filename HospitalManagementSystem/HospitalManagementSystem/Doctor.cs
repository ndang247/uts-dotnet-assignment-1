using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HospitalManagementSystem
{
    public class Doctor : User
    {
        public Doctor(string id, string password, string fullName, string address, string email, string phone, string role) : base(id, password, fullName, address, email, phone, role)
        {
        }

        //public Doctor(string id)
        //{
        //    try
        //    {
        //        if (File.Exists($"Doctors\\{id}.txt"))
        //        {
        //            string[] lines = File.ReadAllLines($"Doctors\\{id}.txt");

        //            string[] details = lines[0].Split(';');

        //            this.id = details[0];
        //            this.password = details[1];
        //            this.fullName = details[2];
        //            this.address = details[3];
        //            this.email = details[4];
        //            this.phone = details[5];
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        private void MyDetails()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |              My Details              |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine();
            Console.WriteLine("Name | Email Address | Phone | Address");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine(this);
            Console.ReadKey();
            Menu();
        }

        private void MyPatients()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |              My Patients             |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine();
            Console.WriteLine($"Patients assigned to {fullName}");
            Console.WriteLine("Name | Doctor | Email Address | Phone | Address");
            Console.WriteLine("-----------------------------------------------");

            if (File.Exists($"Doctors\\RegisteredPatients\\{id}.txt"))
            {
                string[] registeredPatients = File.ReadAllLines($"Doctors\\RegisteredPatients\\{id}.txt");
                foreach (string registeredPatient in registeredPatients)
                {
                    string[] patient = File.ReadAllLines($"Patients\\{registeredPatient}.txt");
                    string[] patientInfo = patient[0].Split(';');
                    Patient p = new Patient(patientInfo[0], patientInfo[1], patientInfo[2], patientInfo[3], patientInfo[4], patientInfo[5], "Patient");
                    Console.WriteLine(p);
                }
            }
            Console.ReadKey();
            Menu();
        }

        private void ListAppointments()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |            All Appointments          |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine();
            Console.WriteLine("Doctor | Patient | Description");
            Console.WriteLine("------------------------------");
            if (File.Exists($"Appointments\\Doctors\\{id}.txt"))
            {
                string[] appointments = File.ReadAllLines($"Appointments\\Doctors\\{id}.txt");
                foreach (string appointment in appointments)
                {
                    string[] appointmentInfo = appointment.Split('|');
                    Appointment app = new Appointment(appointmentInfo[0], appointmentInfo[1], appointmentInfo[2]);
                    Console.WriteLine(app);
                }
            }
            Console.ReadKey();
            Menu();
        }

        private void CheckParticularPatient()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |         Check Patient Details        |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine();
            Console.Write("Enter the ID of the patient to check: ");
            try
            {
                string patientID = Console.ReadLine();
                if (File.Exists($"Patients\\{patientID}.txt"))
                {
                    string[] patient = File.ReadAllLines($"Patients\\{patientID}.txt");
                    string[] patientInfo = patient[0].Split(';');

                    Console.WriteLine("Patient | Doctor | Email Address | Phone | Address");
                    Console.WriteLine("--------------------------------------------------");
                    Patient p = new Patient(patientInfo[0], patientInfo[1], patientInfo[2], patientInfo[3], patientInfo[4], patientInfo[5], "Patient");
                    Console.WriteLine(p);

                    Console.ReadKey();
                    Menu();
                }
                else
                {
                    throw new Exception("Patient not found, press any key to return to menu");
                }
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "Patient not found, press any key to return to menu":
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Menu();
                        break;
                    default:
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        CheckParticularPatient();
                        break;
                }
            }
        }

        private void ListAppointmentsWithPatient()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |           Appointment With           |");
            Console.WriteLine(" |______________________________________|");

            Console.WriteLine();
            Console.Write("Enter the ID of the patient you would like to view appointments for: ");

            try
            {
                string patientID = Console.ReadLine();
                if (File.Exists($"Patients\\{patientID}.txt") && File.Exists($"Patients\\RegisteredDoctors\\{patientID}.txt"))
                {
                    // Check if the patient is registered with the current logged doctor
                    string[] registeredDoctor = File.ReadAllLines($"Patients\\RegisteredDoctors\\{patientID}.txt");
                    if (id == registeredDoctor[0])
                    {
                        Console.WriteLine("Doctor | Patient | Description");
                        Console.WriteLine("------------------------------");
                        if (File.Exists($"Appointments\\Patients\\{patientID}.txt"))
                        {
                            string[] appointments = File.ReadAllLines($"Appointments\\Patients\\{patientID}.txt");
                            foreach (string appointment in appointments)
                            {
                                string[] appointmentInfo = appointment.Split('|');
                                Appointment app = new Appointment(appointmentInfo[0], appointmentInfo[1], appointmentInfo[2]);
                                Console.WriteLine(app);
                            }
                        }
                        Console.ReadKey();
                        Menu();
                    }
                    else
                    {
                        throw new Exception("Patient not registered with you, press any key to return to menu");
                    }
                }
                else
                {
                    throw new Exception("Patient not found or currently not registered with any doctor, press any key to return to menu");
                }
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "Patient not found or currently not registered with any doctor, press any key to return to menu":
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Menu();
                        break;
                    case "Patient not registered with you, press any key to return to menu":
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        Menu();
                        break;
                    default:
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        ListAppointmentsWithPatient();
                        break;
                }
            }
        }

        public override void Menu()
        {
            Console.Clear();
            Console.WriteLine("  ______________________________________");
            Console.WriteLine(" |                                      |");
            Console.WriteLine(" |   DOTNET Hospital Managment System   |");
            Console.WriteLine(" |______________________________________|");
            Console.WriteLine(" |              Doctor Menu             |");
            Console.WriteLine(" |______________________________________|");

            Console.WriteLine($"Welcome to DOTNET Hospital Managment System {fullName}");

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
                        MyDetails();
                        break;
                    case ConsoleKey.D2:
                        MyPatients();
                        break;
                    case ConsoleKey.D3:
                        ListAppointments();
                        break;
                    case ConsoleKey.D4:
                        CheckParticularPatient();
                        break;
                    case ConsoleKey.D5:
                        ListAppointmentsWithPatient();
                        break;
                    case ConsoleKey.D6:
                        Console.Clear();
                        Program.Main();
                        break;
                    case ConsoleKey.D7:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine($"Invalid option, please choose from 1-{options.Length}");
                        Console.ReadKey();
                        Menu();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }

        public override string ToString()
        {
            return $"{fullName} | {email} | {phone} | {address}";
        }
    }
}
