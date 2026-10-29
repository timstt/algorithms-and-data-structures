using System;
using System.IO;

public class Service
{
    private string _outputFormat;
    private string _filePath;
    private string _dataToProcess;

    public Service(string format, string path)
    {
        _outputFormat = format;
        _filePath = path;
    }

    // Операція: Вивести дані на консоль та Записати дані у файл
    public void SaveAndPrint(string message)
    {
        _dataToProcess = message;

        // Формуємо рядок згідно з атрибутом "Формат виводу"
        string result = _outputFormat + ": " + _dataToProcess;

        // Вивід на консоль
        Console.WriteLine(result);

        // Запис у файл (значення атрибутів та розрахункові значення)
        File.AppendAllText(_filePath, result + Environment.NewLine);
    }

    // Операція: Читати дані з консолі
    public string ReadFromConsole()
    {
        return Console.ReadLine();
    }

    // Операція: Очистити файл (технічна дія для зручності)
    public void ClearFile()
    {
        File.WriteAllText(_filePath, string.Empty);
    }
    public string ReadFromFile()
    {
        if (File.Exists(_filePath))
        {
            return File.ReadAllText(_filePath);
        }
        return "Файл не знайдено.";
    }
}