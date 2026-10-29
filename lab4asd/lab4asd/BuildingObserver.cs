using System;

public class Task3Buildings
{
    public void Run()
    {
        // Читаємо n
        int n = int.Parse(Console.ReadLine());

        // Читаємо рядок з висотами і розбиваємо по пробілу
        string[] parts = Console.ReadLine().Split(' ');

        int max = -1;
        int count = 0;
        string visibleHeights = "";

        for (int i = 0; i < n; i++)
        {
            int h = int.Parse(parts[i]);

            if (h > max)
            {
                max = h;
                count = count + 1;
                visibleHeights = visibleHeights + h + " ";
            }
        }

        Console.WriteLine(count);
        Console.WriteLine(visibleHeights);
    }
}