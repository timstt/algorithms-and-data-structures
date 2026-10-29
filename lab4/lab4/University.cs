using System;

public class University
{
    private string _name;
    private double _annualBudget;
    private double _rating;
    private int _facultyCount;
    private int _studentCount;
    private int _scientificPapers;
    private int _internationalProjects;
    private double _averageZno;

    private static Random _rnd = new Random();

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public double Rating
    {
        get { return _rating; }
    }

    public double AnnualBudget
    {
        get { return _annualBudget; }
    }

    public University()
    {
        _name = "None";
        _annualBudget = 0;
    }

    public University(string name, int students, int faculties, double zno, int papers, int projects)
    {
        _name = name;
        _studentCount = students;
        _facultyCount = faculties;
        _averageZno = zno;
        _scientificPapers = papers;
        _internationalProjects = projects;
    }

    public void CalculateRating()
    {
        int scopus = _rnd.Next(0, 201);
        int top200 = _rnd.Next(0, 201);
        int znoPoints = _rnd.Next(0, 201);
        double bonus = (_scientificPapers * 0.1) + (_internationalProjects * 2.5);

        _rating = scopus + top200 + znoPoints + bonus;
    }

    public void CalculateBudget()
    {
        double baseFund = 40000000;
        double ratingCoeff = _rating / 650.0;
        double adminExpenses = baseFund * 0.25;
        double structureCosts = _facultyCount * 150000;

        _annualBudget = (baseFund * ratingCoeff) - (adminExpenses + structureCosts);
        if (_annualBudget < 0)
        {
            _annualBudget = 0;
        }
    }
    public void AddFaculty()
    {
        _facultyCount++;
    }

    public void RemoveFaculty()
    {
        if (_facultyCount > 0)
        {
            _facultyCount--;
        }
    }
}