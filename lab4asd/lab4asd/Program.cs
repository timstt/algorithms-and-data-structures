using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ ВИБОРУ ЗАВДАНЬ ===");
            Console.WriteLine("1 - Завдання 1 (Дужки)");
            Console.WriteLine("2 - Завдання 2 (Черги)");
            Console.WriteLine("3 - Завдання 3 (Споруди)");
            Console.WriteLine("4 - Завдання 4 (Стопки)");
            Console.WriteLine("0 - Вихід з програми");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            if (choice == "0")
            {
                break; // Вихід з циклу і завершення програми
            }
            else if (choice == "1")
            {
                Console.WriteLine("\n--- Завдання 1 ---");
                Console.WriteLine("Введіть послідовність дужок (наприклад, (()) ):");
                Task1Brackets task1 = new Task1Brackets();
                task1.Run();
            }
            else if (choice == "2")
            {
                Console.WriteLine("\n--- Завдання 2 ---");
                Console.WriteLine("Демонстрація об'єднання тестових черг {1, 5} та {2, 6}...");
                Queue<int> q1 = new Queue<int>();
                q1.Enqueue(1); q1.Enqueue(5);
                Queue<int> q2 = new Queue<int>();
                q2.Enqueue(2); q2.Enqueue(6);

                Task2Queues task2 = new Task2Queues();
                Queue<int> merged = task2.Merge(q1, q2);

                Console.Write("Результат злиття: ");
                foreach (int item in merged)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine();
            }
            else if (choice == "3")
            {
                Console.WriteLine("\n--- Завдання 3 ---");
                Console.WriteLine("Введіть кількість споруд n, натисніть Enter, а потім висоти через пробіл:");
                Task3Buildings task3 = new Task3Buildings();
                task3.Run();
            }
            else if (choice == "4")
            {
                Console.WriteLine("\n--- Завдання 4 ---");
                Console.WriteLine("Тестування стопок (ліміт 2). Додаємо числа 1, 2, 3...");
                SetofStacks set = new SetofStacks(2);
                set.push(1);
                set.push(2);
                set.push(3);
                Console.WriteLine("Вилучено: " + set.pop()); // Має вивести 30
                Console.WriteLine("Вилучено: " + set.pop()); // Має вивести 20
            }
            else
            {
                Console.WriteLine("Такого завдання немає. Введіть цифру від 0 до 4.");
            }
        }
    }
}