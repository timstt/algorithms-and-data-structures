using System;
using System.Text;

class Program
{
    static void CheckForUniformity()
    {
        Console.WriteLine("Вкажіть розмір масиву:");
        int size = Convert.ToInt32(Console.ReadLine());
        int[] numbers = new int[size];

        Console.WriteLine("Введіть числа через пробіл:");
        string[] rawInputs = Console.ReadLine().Split(' ');

        for (int idx = 0; idx < size; idx++)
        {
            numbers[idx] = Convert.ToInt32(rawInputs[idx]);
        }

        bool isUniform = true;
        for (int idx = 1; idx < size; idx++)
        {
            if (numbers[idx] != numbers[0])
            {
                isUniform = false;
                break;
            }
        }

        if (isUniform)
        {
            Console.WriteLine("Масив складається з однакових чисел. Індекс q = " + ((0 + size - 1) / 2));
        }
        else
        {
            Console.WriteLine("Числа в масиві відрізняються.");
        }
    }

    static int[] GenerateRandomArray()
    {
        Random rndGenerator = new Random();
        Console.WriteLine("Вкажіть кількість елементів:");
        int size = Convert.ToInt32(Console.ReadLine());
        int[] data = new int[size];

        for (int step = 0; step < size; step++)
        {
            data[step] = rndGenerator.Next(-10, 20);
            Console.Write(data[step] + " ");
        }
        return data;
    }

    static void DisplayArray(int[] data)
    {
        Console.WriteLine("\nВідсортований результат:");
        for (int i = 0; i < data.Length; i++)
        {
            Console.Write(data[i] + " ");
        }
        Console.WriteLine();
    }

    static void SortDescendingQuick(int[] data, int p, int r)
    {
        if (p < r)
        {
            int q = SplitArray(data, p, r);
            SortDescendingQuick(data, p, q - 1);
            SortDescendingQuick(data, q + 1, r);
        }
    }

    static int RandomSplit(int[] data, int p, int r)
    {
        Random rnd = new Random();
        int k = rnd.Next(p, r + 1);

        // Обмін з буфером
        int buffer = data[k];
        data[k] = data[r];
        data[r] = buffer;

        return SplitArray(data, p, r);
    }

    static int SplitArray(int[] data, int p, int r)
    {
        int pivot = data[r];
        int i = p - 1;

        for (int j = p; j < r; j++)
        {
            if (data[j] > pivot) // Умова для незростання
            {
                i++;
                int buffer = data[i];
                data[i] = data[j];
                data[j] = buffer;
            }
        }

        int finalBuffer = data[i + 1];
        data[i + 1] = data[r];
        data[r] = finalBuffer;

        return i + 1;
    }

    static void RandomQuickSort(int[] data, int p, int r)
    {
        if (p < r)
        {
            int q = RandomSplit(data, p, r);
            RandomQuickSort(data, p, q - 1);
            RandomQuickSort(data, q + 1, r);
        }
    }
    static void SolvePipelineTask()
    {
        Console.WriteLine("Введіть загальну кількість свердловин:");
        int n = Convert.ToInt32(Console.ReadLine());
        int[] data = new int[n];

        Console.WriteLine("Вкажіть X та Y для кожної свердловини (наприклад: 5 12):");
        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split(' ');
            data[i] = Convert.ToInt32(input[1]); // Беремо тільки Y
        }

        int p = 0;
        int r = n - 1;
        int target = n / 2; // Позиція медіани
        int optimalY = 0;

        while (p <= r)
        {
            if (p == r)
            {
                optimalY = data[p];
                break;
            }

            int pivot = data[r];
            int i = p - 1;

            for (int j = p; j < r; j++)
            {
                if (data[j] <= pivot) // Стандартна умова для пошуку медіани
                {
                    i++;
                    int buffer = data[i];
                    data[i] = data[j];
                    data[j] = buffer;
                }
            }
            int q = i + 1;
            int finalBuffer = data[q];
            data[q] = data[r];
            data[r] = finalBuffer;

            if (q == target)
            {
                optimalY = data[q];
                break;
            }
            else if
                (target < q) r = q - 1;
            else p = q + 1;
        }

        Console.WriteLine("Оптимальна лінія трубопроводу проходить через Y = " + optimalY);
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n=== Головне меню ===");
            Console.WriteLine("2. Перевірка на ідентичність елементів");
            Console.WriteLine("3. Швидке сортування (за спаданням)");
            Console.WriteLine("4. Рандомізоване швидке сортування (за спаданням)");
            Console.WriteLine("9. Трубопровід (пошук медіани Y за один метод)");
            Console.WriteLine("0. Завершити роботу");

            int menuOption = Convert.ToInt32(Console.ReadLine());
            if (menuOption == 0) break;

            switch (menuOption)
            {
                case 2:
                    CheckForUniformity();
                    break;
                case 3:
                    int[] arrayA = GenerateRandomArray();
                    SortDescendingQuick(arrayA, 0, arrayA.Length - 1);
                    DisplayArray(arrayA);
                    break;
                case 4:
                    int[] arrayB = GenerateRandomArray();
                    RandomQuickSort(arrayB, 0, arrayB.Length - 1);
                    DisplayArray(arrayB);
                    break;
                case 9:
                    SolvePipelineTask();
                    break;
            }
        }
    }
}