using System;
using System.Collections.Generic;

namespace BinaryTreeLab
{
    public class Node
    {
        public int Key;
        public Node Left, Right, Parent;

        public Node(int item)
        {
            Key = item;
            Left = Right = Parent = null;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // --- ЗАВДАННЯ 5: Рекурсивна версія TREE-INSERT ---
            Node root = null;
            int[] keys = { 10, 4, 17, 1, 5, 16, 21 };
            foreach (var key in keys)
            {
                root = InsertRecursive(root, key);
            }

            // --- ЗАВДАННЯ 2: Обходи Θ(n) ---
            Console.WriteLine("ЗАВДАННЯ 2: РЕКУРСИВНІ ОБХОДИ");
            Console.Write("Прямий (Pre-order):  "); PreOrder(root); Console.WriteLine();
            Console.Write("Зворотний (Post-order): "); PostOrder(root); Console.WriteLine();
            Console.WriteLine("-------------------------------------------");

            // --- ЗАВДАННЯ 3: Нерекурсивні обходи ---
            Console.WriteLine("ЗАВДАННЯ 3: НЕРЕКУРСИВНИЙ IN-ORDER");
            Console.Write("Зі стеком:          "); InOrderIterative(root); Console.WriteLine();
            Console.WriteLine("-------------------------------------------");

            // --- ЗАВДАННЯ 4: Рекурсивні екстремуми ---
            Console.WriteLine("ЗАВДАННЯ 4: РЕКУРСИВНІ MIN ТА MAX");
            Console.WriteLine("TREE-MINIMUM: {0}", FindMinRecursive(root));
            Console.WriteLine("TREE-MAXIMUM: {0}", FindMaxRecursive(root));
            Console.WriteLine("-------------------------------------------");

            // --- ЗАВДАННЯ 5: Попередник ---
            Console.WriteLine("ЗАВДАННЯ 5: TREE-PREDECESSOR");
            Node target = root; // Шукаємо для кореня (10)
            Node pred = TreePredecessor(target);

            // Створюємо змінну для збереження результату
            string predResult;

            // Звичайна і зрозуміла перевірка: якщо попередник є, беремо його ключ
            if (pred != null)
            {
                predResult = pred.Key.ToString();
            }
            else // Інакше записуємо слово "немає"
            {
                predResult = "немає";
            }

            Console.WriteLine("Попередник для {0}: {1}", target.Key, predResult);

            Console.WriteLine("-------------------------------------------");
            Console.ReadKey();
        }

        // =====================================================================
        // ЗАВДАННЯ 2: Рекурсивні обходи за час Θ(n)
        // =====================================================================

        static void PreOrder(Node n)
        {
            if (n == null) return;
            Console.Write(n.Key + " "); // Корінь спочатку
            PreOrder(n.Left);
            PreOrder(n.Right);
        }

        static void PostOrder(Node n)
        {
            if (n == null) return;
            PostOrder(n.Left);
            PostOrder(n.Right);
            Console.Write(n.Key + " "); // Корінь в кінці
        }

        // =====================================================================
        // ЗАВДАННЯ 3: Нерекурсивні алгоритми (In-order)
        // =====================================================================

        // Варіант А: З використанням допоміжного стека
        static void InOrderIterative(Node root)
        {
            if (root == null)
                return;
            Stack<Node> s = new Stack<Node>();
            Node curr = root;
            while (curr != null || s.Count > 0)
            {
                while (curr != null) 
                { 
                    s.Push(curr); 
                    curr = curr.Left; 
                }
                curr = s.Pop();
                Console.Write(curr.Key + " ");
                curr = curr.Right;
            }
        }

        
        // =====================================================================
        // ЗАВДАННЯ 4: Рекурсивні версії TREE-MINIMUM та TREE-MAXIMUM
        // =====================================================================

        static int FindMinRecursive(Node n)
        {
            if (n == null) 
                return -1;
            if (n.Left == null) 
                return n.Key; // Базовий випадок
            return FindMinRecursive(n.Left); // Рекурсивний виклик
        }

        static int FindMaxRecursive(Node n)
        {
            if (n == null) 
                return -1;
            if (n.Right == null) 
                return n.Key;
            return FindMaxRecursive(n.Right);
        }

        // =====================================================================
        // ЗАВДАННЯ 5: Процедура TREE-PREDECESSOR
        // =====================================================================

        static Node TreePredecessor(Node x)
        {

            // Випадок 1: Є ліве піддерево - шукаємо максимум там
            if (x.Left != null)
            {
                Node temp = x.Left;
                while (temp.Right != null)
                    temp = temp.Right;
                return temp;
            }
            // Випадок 2: Лівого піддерева немає - йдемо вгору до Parent
            Node y = x.Parent;
            while (y != null && x == y.Left) 
            { 
                x = y; y = y.Parent; 
            }
            return y;
        }

        // =====================================================================
        // ЗАВДАННЯ 6: Рекурсивна версія TREE-INSERT
        // =====================================================================
        static Node InsertRecursive(Node root, int key, Node parent = null)
        {
            if (root == null)
            {
                Node newNode = new Node(key);
                newNode.Parent = parent; 
                return newNode;
            }
            if (key < root.Key) 
                root.Left = InsertRecursive(root.Left, key, root);
            else if (key > root.Key) 
                root.Right = InsertRecursive(root.Right, key, root);
            return root;
        }
    }
}