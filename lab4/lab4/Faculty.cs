using System;
using System.Collections.Generic;

public class Faculty
{
    private string _name;
    private string _dean;
    private int _departmentCount;
    private int _specialtyCount;
    private int _studentCount;
    private string _linkedUniversityName;
    private List<string> _specialties;

    // Властивості з повним розписом (п. 4 завдання)
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string Dean
    {
        get { return _dean; }
        set { _dean = value; }
    }

    public int StudentCount
    {
        get { return _studentCount; }
        set { _studentCount = value; }
    }

    public string LinkedUniversityName
    {
        get { return _linkedUniversityName; }
        set { _linkedUniversityName = value; }
    }

    public int DepartmentCount
    {
        get { return _departmentCount; }
    }

    public int SpecialtyCount
    {
        get { return _specialtyCount; }
    }

    // Конструктор за замовчуванням (п. 2 завдання)
    public Faculty()
    {
        _name = "Unknown Faculty";
        _dean = "None";
        _studentCount = 0;
        _linkedUniversityName = "None";
        _specialties = new List<string>();
        _departmentCount = 0;
        _specialtyCount = 0;
    }

    // Конструктор з параметрами (п. 3 завдання)
    public Faculty(string name, string dean, int students, string university)
    {
        _name = name;
        _dean = dean;
        _studentCount = students;
        _linkedUniversityName = university;
        _specialties = new List<string>();
        _departmentCount = 0;
        _specialtyCount = 0;
    }

    // Конструктор копії (п. 3 завдання)
    public Faculty(Faculty other)
    {
        _name = other._name;
        _dean = other._dean;
        _studentCount = other._studentCount;
        _linkedUniversityName = other._linkedUniversityName;
        _departmentCount = other._departmentCount;
        _specialtyCount = other._specialtyCount;
        _specialties = new List<string>(other._specialties);
    }

    // Методи (п. 5 завдання)
    public void AddSpecialty(string specialtyName)
    {
        if (!string.IsNullOrEmpty(specialtyName) && !_specialties.Contains(specialtyName))
        {
            _specialties.Add(specialtyName);
            _specialtyCount = _specialties.Count;
            _departmentCount = _specialtyCount; // Умова: Кафедри = Спеціальності
        }
    }

    public void RemoveSpecialty(string specialtyName)
    {
        if (_specialties.Remove(specialtyName))
        {
            _specialtyCount = _specialties.Count;
            _departmentCount = _specialtyCount;
        }
    }
    public void UpdateStudentCount(int delta)
    {
        _studentCount += delta;
        if (_studentCount < 0)
        {
            _studentCount = 0;
        }
    }
}