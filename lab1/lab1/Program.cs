using System;
using System.Diagnostics;

namespace Lab1_Search
{
    public static class DataGenerator
    {
        public static int[] GenerateRandomArray(int size)
        {
            int[] arr = new int[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                arr[i] = rnd.Next(1, 10000000);
            }
            return arr;
        }
    }

    public class Search
    {
        private int[] array;

        public Search(int[] input)
        {
            array = input;
        }

        public int LinerSearch(int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target) return i;
            }
            return -1;
        }

        public int SentinelSearch(int target)
        {
            int n = array.Length;
            int last = array[n - 1];
            array[n - 1] = target;

            int i = 0;
            while (array[i] != target)
            {
                i++;
            }

            array[n - 1] = last;
            if (i < n - 1 || last == target) return i;
            return -1;
        }

        public int BinarySearch(int target)
        {
            int L = 0;
            int R = array.Length - 1;

            while (L <= R)
            {
                int m = L + (R - L) / 2;
                if (array[m] == target) return m;
                else if (array[m] < target) L = m + 1;
                else R = m - 1;
            }
            return -1;
        }

        public int GoldenSectionSearch(int target)
        {
            int L = 0;
            int R = array.Length - 1;
            double phi = 0.618;

            while (L < R)
            {
                int m = L + (int)(phi * (R - L));
                if (array[m] < target) L = m + 1;
                else R = m;
            }

            if (array[L] == target) return L;
            return -1;
        }
    }

    public class UserInterface
    {
        private bool isSorted = false;

        public void RunMenu()
        {
            int[] array = DataGenerator.GenerateRandomArray(1000000);
            Search searcher = new Search(array);
            int choice;

            do
            {
                Console.WriteLine("====== Меню ======");
                if (isSorted)
                {
                    Console.WriteLine("Стан масиву: ВІДСОРТОВАНИЙ");
                }
                else
                {
                    Console.WriteLine("Стан масиву: НЕВІДСОРТОВАНИЙ");
                }
                Console.WriteLine("1. Пошук перебором");
                Console.WriteLine("2. Пошук з бар'єром");
                Console.WriteLine("3. Бiнарний пошук (авто-сортування)");
                Console.WriteLine("4. Золотий перерiз (авто-сортування)");
                Console.WriteLine("0. Exit");

                Console.Write("Ваш вибiр: ");
                int.TryParse(Console.ReadLine(), out choice);

                if (choice != 0)
                {
                    if ((choice == 3 || choice == 4) && !isSorted)
                    { 
                        Array.Sort(array);
                        isSorted = true;
                    }

                    Console.Write("Яке число шукаємо? ");
                    int.TryParse(Console.ReadLine(), out int target);

                    Stopwatch sw = new Stopwatch();
                    int result = -1;

                    switch (choice)
                    {
                        case 1:
                            sw.Restart();
                            result = searcher.LinerSearch(target);
                            sw.Stop();
                            break;
                        case 2:
                            sw.Restart();
                            result = searcher.SentinelSearch(target);
                            sw.Stop();
                            break;
                        case 3:
                            sw.Restart();
                            result = searcher.BinarySearch(target);
                            sw.Stop();
                            break;
                        case 4:
                            sw.Restart();
                            result = searcher.GoldenSectionSearch(target);
                            sw.Stop();
                            break;
                    }

                    if (result != -1)
                    {
                        Console.WriteLine("Елемент знайдено на індексі: " + result);
                        Console.WriteLine("Час виконання: " + sw.ElapsedTicks + " тіків");
                    }
                    else
                        Console.WriteLine("Елемент не знайдено.");
                }
            } while (choice != 0);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Студент Стукало Тимофiй");

            UserInterface ui = new UserInterface();
            ui.RunMenu();
        }
    }
}