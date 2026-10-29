using System;
using System.Text;

namespace RedBlackTreeLab
{
    public enum Color { Red, Black }

    public class Node
    {
        public int Data; // Це Low (нижня межа інтервалу)
        public int High; // Верхня межа інтервалу
        public int Max;  // Максимальне значення High у піддереві
        public Color NodeColor;
        public Node Left, Right, Parent;

        public Node(int data)
        {
            Data = data;
            High = data; // За замовчуванням інтервал [data, data]
            Max = data;
            NodeColor = Color.Red;
        }

        // Перевантажений конструктор для інтервалів
        public Node(int low, int high)
        {
            Data = low;
            High = high;
            Max = high;
            NodeColor = Color.Red;
        }
    }

    public class RedBlackTree
    {
        private Node root;
        private readonly Node TNULL;

        public RedBlackTree()
        {
            TNULL = new Node(0) { NodeColor = Color.Black, Left = null, Right = null };
            TNULL.Max = int.MinValue; // Важливо для коректних розрахунків Max
            root = TNULL;
        }

        // --- ІСНУЮЧІ МЕТОДИ (БЕЗ ЗМІН) ---

        public void LeftRotate(Node x)
        {
            Node y = x.Right;
            x.Right = y.Left;
            if (y.Left != TNULL) y.Left.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;
            y.Left = x;
            x.Parent = y;
        }

        public void RightRotate(Node x)
        {
            Node y = x.Left;
            x.Left = y.Right;
            if (y.Right != TNULL) y.Right.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) root = y;
            else if (x == x.Parent.Right) x.Parent.Right = y;
            else x.Parent.Left = y;
            y.Right = x;
            x.Parent = y;
        }

        public void Insert(int key)
        {
            // Для сумісності з твоїм кодом викликаємо розширений Insert
            InsertInterval(key, key);
        }

        // Розширений Insert для підтримки інтервалів та атрибута Max
        public void InsertInterval(int low, int high)
        {
            Node node = new Node(low, high) { Left = TNULL, Right = TNULL };
            Node y = null;
            Node x = root;

            while (x != TNULL)
            {
                y = x;
                // Оновлюємо Max при русі вниз
                if (node.High > x.Max) x.Max = node.High;
                x = (node.Data < x.Data) ? x.Left : x.Right;
            }

            node.Parent = y;
            if (y == null) root = node;
            else if (node.Data < y.Data) y.Left = node;
            else y.Right = node;

            if (node.Parent == null)
            {
                node.NodeColor = Color.Black; return;
            }
            if (node.Parent.Parent == null) return;

            Node k = node;
            while (k.Parent != null && k.Parent.NodeColor == Color.Red)
            {
                if (k.Parent == k.Parent.Parent.Left)
                {
                    Node uncle = k.Parent.Parent.Right;
                    if (uncle.NodeColor == Color.Red)
                    {
                        uncle.NodeColor = Color.Black;
                        k.Parent.NodeColor = Color.Black;
                        k.Parent.Parent.NodeColor = Color.Red;
                        k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Right)
                        {
                            k = k.Parent;
                            LeftRotateInterval(k); // Використовуємо метод Завдання 4
                        }
                        k.Parent.NodeColor = Color.Black;
                        k.Parent.Parent.NodeColor = Color.Red;
                        RightRotate(k.Parent.Parent);
                    }
                }
                else
                {
                    Node uncle = k.Parent.Parent.Left;
                    if (uncle.NodeColor == Color.Red)
                    {
                        uncle.NodeColor = Color.Black;
                        k.Parent.NodeColor = Color.Black;
                        k.Parent.Parent.NodeColor = Color.Red;
                        k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Left)
                        {
                            k = k.Parent;
                            RightRotate(k);
                        }
                        k.Parent.NodeColor = Color.Black;
                        k.Parent.Parent.NodeColor = Color.Red;
                        LeftRotateInterval(k.Parent.Parent); // Використовуємо метод Завдання 4
                    }
                }
                if (k == root) break;
            }
            root.NodeColor = Color.Black;
        }

        public void Delete(int key)
        {
            Node z = TNULL;
            Node temp = root;
            while (temp != TNULL)
            {
                if (temp.Data == key) z = temp;
                temp = (key < temp.Data) ? temp.Left : temp.Right;
            }
            if (z == TNULL)
            {
                Console.WriteLine($"\n[!] Вузол {key} не знайдено."); return;
            }

            Node x, y = z;
            Color yOriginalColor = y.NodeColor;
            if (z.Left == TNULL)
            {
                x = z.Right;
                if (z.Parent == null) root = z.Right;
                else if (z == z.Parent.Left) z.Parent.Left = z.Right;
                else z.Parent.Right = z.Right;
                z.Right.Parent = z.Parent;
            }
            else if (z.Right == TNULL)
            {
                x = z.Left;
                if (z.Parent == null) root = z.Left;
                else if (z == z.Parent.Left) z.Parent.Left = z.Left;
                else z.Parent.Right = z.Left;
                z.Left.Parent = z.Parent;
            }
            else
            {
                y = z.Right;
                while (y.Left != TNULL) y = y.Left;
                yOriginalColor = y.NodeColor;
                x = y.Right;
                if (y.Parent == z) x.Parent = y;
                else
                {
                    if (y.Parent == null) root = y.Right;
                    else if (y == y.Parent.Left) y.Parent.Left = y.Right;
                    else y.Parent.Right = y.Right;
                    y.Right.Parent = y.Parent;

                    y.Right = z.Right;
                    y.Right.Parent = y;
                }
                if (z.Parent == null) root = y;
                else if (z == z.Parent.Left) z.Parent.Left = y;
                else z.Parent.Right = y;
                y.Parent = z.Parent;
                y.Left = z.Left;
                y.Left.Parent = y;
                y.NodeColor = z.NodeColor;
            }

            // Оновлення Max вгору від точки видалення
            Node u = x.Parent;
            while (u != null && u != TNULL)
            {
                u.Max = Math.Max(u.High, Math.Max(u.Left.Max, u.Right.Max));
                u = u.Parent;
            }

            if (yOriginalColor == Color.Black)
            {
                Node fixNode = x;
                while (fixNode != root && fixNode.NodeColor == Color.Black)
                {
                    if (fixNode == fixNode.Parent.Left)
                    {
                        Node s = fixNode.Parent.Right;
                        if (s.NodeColor == Color.Red)
                        {
                            s.NodeColor = Color.Black; fixNode.Parent.NodeColor = Color.Red;
                            LeftRotateInterval(fixNode.Parent); s = fixNode.Parent.Right;
                        }
                        if (s.Left.NodeColor == Color.Black && s.Right.NodeColor == Color.Black)
                        {
                            s.NodeColor = Color.Red; fixNode = fixNode.Parent;
                        }
                        else
                        {
                            if (s.Right.NodeColor == Color.Black)
                            {
                                s.Left.NodeColor = Color.Black; s.NodeColor = Color.Red;
                                RightRotate(s); s = fixNode.Parent.Right;
                            }
                            s.NodeColor = fixNode.Parent.NodeColor; fixNode.Parent.NodeColor = Color.Black;
                            s.Right.NodeColor = Color.Black; LeftRotateInterval(fixNode.Parent);
                            fixNode = root;
                        }
                    }
                    else
                    {
                        Node s = fixNode.Parent.Left;
                        if (s.NodeColor == Color.Red)
                        {
                            s.NodeColor = Color.Black; fixNode.Parent.NodeColor = Color.Red;
                            RightRotate(fixNode.Parent); s = fixNode.Parent.Left;
                        }
                        if (s.Right.NodeColor == Color.Black && s.Left.NodeColor == Color.Black)
                        {
                            s.NodeColor = Color.Red; fixNode = fixNode.Parent;
                        }
                        else
                        {
                            if (s.Left.NodeColor == Color.Black)
                            {
                                s.Right.NodeColor = Color.Black; s.NodeColor = Color.Red;
                                LeftRotateInterval(s); s = fixNode.Parent.Left;
                            }
                            s.NodeColor = fixNode.Parent.NodeColor; fixNode.Parent.NodeColor = Color.Black;
                            s.Left.NodeColor = Color.Black; RightRotate(fixNode.Parent);
                            fixNode = root;
                        }
                    }
                }
                fixNode.NodeColor = Color.Black;
            }
            Console.WriteLine($"\n[Успіх] Вузол {key} оброблено.");
        }

        public void Display()
        {
            if (root == TNULL) Console.WriteLine("Порожньо.");
            else PrintPretty(root, "", true);
        }

        private void PrintPretty(Node node, string indent, bool isLeft)
        {
            if (node != TNULL)
            {
                PrintPretty(node.Right, indent + (isLeft ? "│   " : "    "), false);
                Console.Write(indent + (isLeft ? "└── " : "┌── "));
                Console.ForegroundColor = (node.NodeColor == Color.Red) ? ConsoleColor.Red : ConsoleColor.Cyan;
                // Виводимо також High та Max для візуалізації
                Console.WriteLine($"[{node.Data}, {node.High}] Max: {node.Max}");
                Console.ResetColor();
                PrintPretty(node.Left, indent + (isLeft ? "    " : "│   "), true);
            }
        }


        // ЗАВДАННЯ №4: Left-Rotate з оновленням атрибута Max за O(1)
        public void LeftRotateInterval(Node x)
        {
            Node y = x.Right;
            x.Right = y.Left;
            if (y.Left != TNULL) y.Left.Parent = x;
            y.Parent = x.Parent;

            if (x.Parent == null) root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;

            // Оновлення Max атрибутів: спочатку для нижнього вузла, потім для верхнього
            x.Max = Math.Max(x.High, Math.Max(x.Left.Max, x.Right.Max));
            y.Max = Math.Max(y.High, Math.Max(y.Left.Max, y.Right.Max));
        }

        // ЗАВДАННЯ №5: Пошук перекриття інтервалів за O(log n)
        public Node SearchOverlap(int low, int high)
        {
            Node x = root;
            // Поки не дійшли до листка NIL і поки не знайшли перекриття
            while (x != TNULL && (high < x.Data || low > x.High))
            {
                if (x.Left != TNULL && x.Left.Max >= low)
                    x = x.Left;
                else
                    x = x.Right;
            }
            return x;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            RedBlackTree tree = new RedBlackTree();

            // Тестове заповнення
            tree.InsertInterval(15, 20);
            tree.InsertInterval(10, 30);
            tree.InsertInterval(17, 19);
            tree.InsertInterval(5, 20);
            tree.InsertInterval(12, 15);
            tree.InsertInterval(30, 40);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("ЧЧ-ДЕРЕВО ІНТЕРВАЛІВ: [Low, High] Max: Value\n");
                tree.Display();
                Console.WriteLine("\n1. Додати | 2. Видалити | 3. Пошук перекриття (Завд. 5) | 4. Вийти");
                Console.Write("Вибір: ");
                string c = Console.ReadLine();
                if (c == "4") break;

                if (c == "1")
                {
                    Console.Write("Low: "); int l = int.Parse(Console.ReadLine());
                    Console.Write("High: "); int h = int.Parse(Console.ReadLine());
                    tree.InsertInterval(l, h);
                }
                else if (c == "2")
                {
                    Console.Write("Введіть Low для видалення: ");
                    if (int.TryParse(Console.ReadLine(), out int v)) tree.Delete(v);
                    Console.ReadKey();
                }
                else if (c == "3")
                {
                    Console.Write("Шукаємо перекриття для Low: "); int l = int.Parse(Console.ReadLine());
                    Console.Write("Шукаємо перекриття для High: "); int h = int.Parse(Console.ReadLine());
                    Node res = tree.SearchOverlap(l, h);
                    if (res.Max != int.MinValue)
                        Console.WriteLine($"\nЗнайдено перетин: [{res.Data}, {res.High}]");
                    else
                        Console.WriteLine("\nПеретинів не знайдено.");
                    Console.ReadKey();
                }
            }
        }
    }
}