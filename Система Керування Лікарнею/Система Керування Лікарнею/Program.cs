using System;
using System.Collections.Generic;

namespace HospitalManagementSystem
{
    // Клас Doctor відповідає за збереження інформації про лікаря
    public class Doctor
    {
        public int Id { get; set; }                  
        public string Name { get; set; }             
        public string Specialization { get; set; } 

        // Конструктор ініціалізує всі властивості
        public Doctor(int id, string name, string specialization)
        {
            Id = id;
            Name = name;
            Specialization = specialization;
        }

        // Метод для виведення інформації про лікаря
        public void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}, Ім'я: {Name}, Спеціалізація: {Specialization}");
        }
    }

    // Клас Patient відповідає за збереження інформації про пацієнта
    public class Patient
    {
        public int Id { get; set; }     
        public string Name { get; set; } 
        public int Age { get; set; }     

        // Конструктор
        public Patient(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }

    // Клас HospitalRoom відповідає за збереження інформації про палату та госпіталізацію пацієнтів
    public class HospitalRoom
    {
        public int RoomNumber { get; set; }  
        public int Capacity { get; set; }    
        public List<Patient> Patients { get; set; } 

        // Конструктор
        public HospitalRoom(int roomNumber, int capacity)
        {
            RoomNumber = roomNumber;
            Capacity = capacity;
            Patients = new List<Patient>(); 
        }

        // Метод для додавання пацієнта до палати
        public void AddPatient(Patient patient)
        {
            if (Patients.Count < Capacity)
            {
                Patients.Add(patient);
                Console.WriteLine($"Пацієнт {patient.Name} доданий у палату №{RoomNumber}");
            }
            else
            {
                Console.WriteLine($"Палата №{RoomNumber} переповнена! Неможливо додати пацієнта.");
            }
        }
    }

    // Клас MedicalRecord відповідає за збереження інформації про медичні прийоми
    public class MedicalRecord
    {
        public Patient Patient { get; set; }     
        public Doctor Doctor { get; set; }        
        public DateTime Date { get; set; }       
        public string Description { get; set; }   

        // Конструктор
        public MedicalRecord(Patient patient, Doctor doctor, DateTime date, string description)
        {
            Patient = patient;
            Doctor = doctor;
            Date = date;
            Description = description;
        }
    }

    // Клас Hospital відповідає за управління всіма даними лікарні
    public class Hospital
    {
        public List<Doctor> Doctors { get; set; }    
        public List<Patient> Patients { get; set; }   
        public List<HospitalRoom> Rooms { get; set; } 
        public List<MedicalRecord> Records { get; set; } 

        // Конструктор
        public Hospital()
        {
            Doctors = new List<Doctor>();
            Patients = new List<Patient>();
            Rooms = new List<HospitalRoom>();
            Records = new List<MedicalRecord>();
        }

        // 1. Додавання лікаря
        public void AddDoctor(Doctor doctor)
        {
            Doctors.Add(doctor);
            Console.WriteLine($"Лікар {doctor.Name} ({doctor.Specialization}) доданий до системи");
        }

        // 2. Реєстрація пацієнта
        public void RegisterPatient(Patient patient)
        {
            Patients.Add(patient);
            Console.WriteLine($"Пацієнт {patient.Name}, {patient.Age} років, зареєстрований");
        }

        // 3. Створення палати
        public void CreateRoom(HospitalRoom room)
        {
            Rooms.Add(room);
            Console.WriteLine($"Палата №{room.RoomNumber} створена (місткість: {room.Capacity})");
        }

        // 4. Госпіталізація пацієнта
        public void HospitalizePatient(int patientId, int roomNumber)
        {
            Patient patient = Patients.FirstOrDefault(p => p.Id == patientId);
            HospitalRoom room = Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

            if (patient == null)
            {
                Console.WriteLine($"Пацієнт з ID {patientId} не знайдений!");
                return;
            }

            if (room == null)
            {
                Console.WriteLine($"Палата №{roomNumber} не знайдена!");
                return;
            }

            room.AddPatient(patient);
        }

        // 5. Додавання медичного запису
        public void AddMedicalRecord(MedicalRecord record)
        {
            Records.Add(record);
            Console.WriteLine($"Медичний запис створено: {record.Patient.Name} -> {record.Doctor.Name}");
        }

        // 6. Історія прийомів конкретного пацієнта
        public List<MedicalRecord> GetPatientHistory(int patientId)
        {
            return Records.Where(r => r.Patient.Id == patientId).ToList();
        }

        // 7. Отримання статистики лікарні
        public string GetStatistics()
        {
            int totalPatientsInRooms = Rooms.Sum(r => r.Patients.Count);

            return
    $"=== СТАТИСТИКА ЛІКАРНІ ===\n" +
    $"Кількість лікарів: {Doctors.Count}\n" +
    $"Кількість зареєстрованих пацієнтів: {Patients.Count}\n" +
    $"Кількість палат: {Rooms.Count}\n" +
    $"Кількість пацієнтів у палатах: {totalPatientsInRooms}\n" +
    $"Кількість медичних записів: {Records.Count}\n";
        }
    }


    // Клас HospitalDemo відповідає за демонстрацію роботи всієї системи
    public class HospitalDemo
    {
        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== СИСТЕМА УПРАВЛІННЯ ЛІКАРНЕЮ ===\n");

            Hospital hospital = new Hospital();

            // --- Додавання лікарів ---
            Doctor doc1 = new Doctor(1, "Олександр Іваненко", "Хірург");
            Doctor doc2 = new Doctor(2, "Марія Петренко", "Терапевт");
            Doctor doc3 = new Doctor(3, "Ігор Коваль", "Кардіолог");

            hospital.AddDoctor(doc1);
            hospital.AddDoctor(doc2);
            hospital.AddDoctor(doc3);

            // --- Реєстрація пацієнтів ---
            Patient p1 = new Patient(1, "Тарас Коваленко", 35);
            Patient p2 = new Patient(2, "Ірина Шевченко", 28);
            Patient p3 = new Patient(3, "Петро Мельник", 42);
            Patient p4 = new Patient(4, "Наталія Романюк", 50);

            hospital.RegisterPatient(p1);
            hospital.RegisterPatient(p2);
            hospital.RegisterPatient(p3);
            hospital.RegisterPatient(p4);

            // --- Створення палат ---
            HospitalRoom room1 = new HospitalRoom(101, 2);
            HospitalRoom room2 = new HospitalRoom(102, 2);
            HospitalRoom room3 = new HospitalRoom(103, 1);

            hospital.CreateRoom(room1);
            hospital.CreateRoom(room2);
            hospital.CreateRoom(room3);

            // --- Госпіталізація пацієнтів ---
            hospital.HospitalizePatient(1, 101);
            hospital.HospitalizePatient(2, 101);
            hospital.HospitalizePatient(3, 102);
            hospital.HospitalizePatient(4, 103);
            hospital.HospitalizePatient(1, 103); // тест перевищення місткості

            // --- Медичні записи ---
            hospital.AddMedicalRecord(new MedicalRecord(p1, doc1, new DateTime(2025, 10, 9), "Планова операція на апендиксі"));
            hospital.AddMedicalRecord(new MedicalRecord(p2, doc2, new DateTime(2025, 10, 8), "ГРВІ, призначено лікування"));
            hospital.AddMedicalRecord(new MedicalRecord(p1, doc3, new DateTime(2025, 10, 7), "Післяопераційний огляд"));

            // --- Історія пацієнта ---
            Console.WriteLine("\n--- ІСТОРІЯ ПАЦІЄНТА ---");
            var history = hospital.GetPatientHistory(1);
            foreach (var record in history)
            {
                Console.WriteLine($"  Дата: {record.Date.ToShortDateString()}");
                Console.WriteLine($"  Лікар: {record.Doctor.Name}");
                Console.WriteLine($"  Опис: {record.Description}\n");
            }

            // --- Статистика ---
            Console.WriteLine(hospital.GetStatistics());
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            HospitalDemo demo = new HospitalDemo();
            demo.Run();

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }




}