using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Створюємо сервіс (атрибути: формат та шлях)
        Service service = new Service("Звіт", "log.txt");
        service.ClearFile();

        // 1. Університет
        University knu = new University("КНУ ім. Шевченка", 25000, 15, 192.5, 450, 12);
        knu.CalculateRating();
        knu.CalculateBudget();

        // Виводимо розрахункові значення (вимога таблиці 5.1)
        service.SaveAndPrint("Назва університету: " + knu.Name);
        service.SaveAndPrint("Розрахований рейтинг: " + knu.Rating.ToString("F1"));
        service.SaveAndPrint("Річне фінансування: " + knu.AnnualBudget.ToString("N0"));

        // 2. Факультет
        Faculty fit = new Faculty("ФІТ", "Декан ФІТ", 1200, knu.Name);
        fit.AddSpecialty("121 - ІПЗ");
        fit.AddSpecialty("122 - КН");
        fit.AddSpecialty("125 - КБ");
        fit.AddSpecialty("126 - ІСТ");

        service.SaveAndPrint("Назва факультету: " + fit.Name);
        service.SaveAndPrint("Кількість кафедр: " + fit.DepartmentCount.ToString());
        service.SaveAndPrint("Кількість студентів: " + fit.StudentCount.ToString());

        Console.WriteLine("\nРоботу завершено. Натисніть Enter.");
        service.ReadFromConsole();
    }
}