using System;

// Base Class: Student
class Student
{
    // Properties
    public string Name { get; set; }
    public int ID { get; set; }
    public int Marks { get; set; }

    // Constructor
    public Student(string name, int id, int marks)
    {
        Name = name;
        ID = id;
        Marks = marks;
    }

    // Method to calculate grade based on marks
    public string GetGrade()
    {
        if (Marks >= 90)
            return "A";
        else if (Marks >= 75)
            return "B";
        else if (Marks >= 50)
            return "C";
        else
            return "F";
    }

    // Method to display student details
    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"ID: {ID}");
        Console.WriteLine($"Marks: {Marks}");
        Console.WriteLine($"Grade: {GetGrade()}");
    }
}

// Derived Class: StudentIITGN
class StudentIITGN : Student
{
    // New Property
    public string Hostel_Name_IITGN { get; set; }

    // Constructor (calls base class constructor)
    public StudentIITGN(string name, int id, int marks, string hostelName)
        : base(name, id, marks)
    {
        Hostel_Name_IITGN = hostelName;
    }

    // Override DisplayDetails method to include hostel info
    public override void DisplayDetails()
    {
        base.DisplayDetails();
        Console.WriteLine($"Hostel Name (IITGN): {Hostel_Name_IITGN}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Creating a Student object
        Student student1 = new Student("Vipul Sunil Patil", 22110189, 88);
        Student student2 = new Student("Ranit Biswas", 22110217, 85);
        Console.WriteLine("=== Student Details ===");
        student1.DisplayDetails();
        Console.WriteLine();
        student2.DisplayDetails();

        Console.WriteLine("\n=== IITGN Student Details ===");
        // Creating a StudentIITGN object
        StudentIITGN iitgnStudent1 = new StudentIITGN("Ranit Biswas", 22110217, 85, "G Hostel");
        iitgnStudent1.DisplayDetails();
    }
}