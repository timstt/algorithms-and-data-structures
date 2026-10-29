using System;

class Program
{
    static void DirectSearch(string text, string pattern) //Прямий пошук рядка
    {
        bool found = false;
        int n = text.Length;
        int m = pattern.Length;

        for (int i = 0; i <= n - m; i++)  // Перебираємо всі можливі початкові позиції
        {
            int j = 0;
            while (j < m && text[i + j] == pattern[j])// Перевіряємо збіг символів
            {
                j++;
            }

            if (j == m) // Якщо всі символи підрядка збіглися
            {
                Console.WriteLine("Введений підрядок було знайдено на позиції: " + i);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Підрядок у тексті не знайдено.");
        }
    }
    static int[] ComputePrefixFunction(string pattern)
    {
        int m = pattern.Length;
        int[] pi = new int[m];
        int k = 0;

        for (int q = 1; q < m; q++)
        {
            while (k > 0 && pattern[k] != pattern[q])
            {
                k = pi[k - 1];
            }
            if (pattern[k] == pattern[q])
            {
                k++;
            }
            pi[q] = k;
        }
        return pi;
    }
    static void KmpSearch(string text, string pattern)
    {
        int n = text.Length;
        int m = pattern.Length;

        if (m == 0) return;

        int[] pi = ComputePrefixFunction(pattern);
        int q = 0; // кількість символів, що збіглися
        bool found = false;

        for (int i = 0; i < n; i++) // один прохід по тексту
        {
            while (q > 0 && pattern[q] != text[i])
            {
                q = pi[q - 1];
            }

            if (pattern[q] == text[i])
            {
                q++;
            }

            if (q == m) // Знайшли повний збіг
            {
                Console.WriteLine("Введений підрядок було знайдено на позиції: " + (i - m + 1));
                found = true;
                q = pi[q - 1]; // продовжуємо пошук наступних входжень
            }
        }

        if (!found)
        {
            Console.WriteLine("Підрядок у тексті не знайдено.");
        }
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.Unicode;

        Console.WriteLine("Введіть текст:");
        string text = Console.ReadLine();

        Console.WriteLine("Введіть підрядок для пошуку:");
        string pattern = Console.ReadLine();

        while (true)
        {
            Console.WriteLine("\nОберіть спосіб пошуку:");
            Console.WriteLine("1. Прямий пошук рядка");
            Console.WriteLine("2. Алгоритм Кнута, Морриса і Пратта");
            Console.WriteLine("0. Завершити");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                DirectSearch(text, pattern);
            }
            else if (choice == "2")
            {
                KmpSearch(text, pattern);
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Невірний вибір. Будь ласка, введіть 1, 2 або 0.");
            }
        }
    }
}