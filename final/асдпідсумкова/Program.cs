using System;
using System.Text;
using System.Collections.Generic;

namespace TreeExpertLab
{
    // --- ПЕРЕРАХУВАННЯ ТА ВУЗОЛ ---
    public enum Color { Red, Black }

    public class Node
    {
        public int Data;           // Значення (або Low для відрізків)
        public int High;           // Верхня межа відрізка
        public int Max;            // Максимальний High у піддереві
        public int Height = 1;      // Для AVL
        public Color NodeColor = Color.Red; // Для RBT
        public Node Left, Right, Parent;

        public Node(int data)
        {
            Data = data;
            High = data + 10; // Демонстраційне значення для відрізка
            Max = High;
        }
    }

    // --- ВІЗУАЛІЗАТОР ---
    public static class TreePrinter
    {
        public static void Print(Node root, string indent = "", bool last = true)
        {
            if (root == null) return;
            Console.Write(indent);
            if (last) { Console.Write("└── "); indent += "    "; }
            else { Console.Write("┌── "); indent += "│   "; }

            if (root.NodeColor == Color.Red) Console.ForegroundColor = ConsoleColor.Red;
            else Console.ForegroundColor = ConsoleColor.Cyan;

            string colorMark = root.NodeColor == Color.Red ? "(R)" : "[B]";
            // Виводимо дані, колір, висоту та Max (для завдання 4)
            Console.WriteLine(root.Data + " " + colorMark + " [Max:" + root.Max + "] (h:" + root.Height + ")");
            Console.ResetColor();

            List<Node> children = new List<Node>();
            if (root.Left != null) children.Add(root.Left);
            if (root.Right != null) children.Add(root.Right);

            for (int i = 0; i < children.Count; i++)
                Print(children[i], indent, i == children.Count - 1);
        }
    }

    // --- БАЗОВЕ ДЕРЕВО (Завдання 1 та 2) ---
    public class BST
    {
        public Node root;
        public virtual void Insert(int key) { root = InsertRec(root, key, null); }
        protected Node InsertRec(Node node, int key, Node parent)
        {
            if (node == null) return new Node(key) { Parent = parent };
            if (key < node.Data) node.Left = InsertRec(node.Left, key, node);
            else if (key > node.Data) node.Right = InsertRec(node.Right, key, node);
            return node;
        }
        public virtual void Delete(int key) { root = DeleteRec(root, key); }
        protected Node DeleteRec(Node node, int key)
        {
            if (node == null) return null;
            if (key < node.Data) node.Left = DeleteRec(node.Left, key);
            else if (key > node.Data) node.Right = DeleteRec(node.Right, key);
            else
            {
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;
                node.Data = MinValue(node.Right);
                node.Right = DeleteRec(node.Right, node.Data);
            }
            return node;
        }
        private int MinValue(Node n) { int m = n.Data; while (n.Left != null) n = n.Left; return m; }
        public void Prepopulate()
        {
            int[] values = { 50, 30, 70, 20, 40, 60, 80 };
            foreach (int v in values) Insert(v);
        }
    }

    // --- RBT: ПЕРЕФАРБУВАННЯ ТА ПОВОРОТИ (Завдання 3) ---
    public class RBTree : BST
    {
        private void LeftRotate(Node x)
        {
            Console.WriteLine(">>> RBT: ЛІВИЙ ПОВОРОТ навколо " + x.Data);
            Node y = x.Right; x.Right = y.Left;
            if (y.Left != null) y.Left.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) root = y;
            else if (x == x.Parent.Left) x.Parent.Left = y;
            else x.Parent.Right = y;
            y.Left = x; x.Parent = y;
        }
        private void RightRotate(Node x)
        {
            Console.WriteLine(">>> RBT: ПРАВИЙ ПОВОРОТ навколо " + x.Data);
            Node y = x.Left; x.Left = y.Right;
            if (y.Right != null) y.Right.Parent = x;
            y.Parent = x.Parent;
            if (x.Parent == null) root = y;
            else if (x == x.Parent.Right) x.Parent.Right = y;
            else x.Parent.Left = y;
            y.Right = x; x.Parent = y;
        }
        public override void Insert(int key)
        {
            Node newNode = new Node(key);
            root = InsertWithRef(root, newNode, null);
            FixInsert(newNode);
        }
        private Node InsertWithRef(Node node, Node newNode, Node parent)
        {
            if (node == null) { newNode.Parent = parent; return newNode; }
            if (newNode.Data < node.Data) node.Left = InsertWithRef(node.Left, newNode, node);
            else node.Right = InsertWithRef(node.Right, newNode, node);
            return node;
        }
        private void FixInsert(Node k)
        {
            while (k != root && k.Parent.NodeColor == Color.Red)
            {
                if (k.Parent == k.Parent.Parent.Left)
                {
                    Node uncle = k.Parent.Parent.Right;
                    if (uncle != null && uncle.NodeColor == Color.Red)
                    {
                        Console.WriteLine(">>> Case 1: ПЕРЕФАРБУВАННЯ БРАТІВ (батько та дядько " + uncle.Data + ")");
                        k.Parent.NodeColor = Color.Black; uncle.NodeColor = Color.Black;
                        k.Parent.Parent.NodeColor = Color.Red; k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Right) { k = k.Parent; LeftRotate(k); }
                        k.Parent.NodeColor = Color.Black; k.Parent.Parent.NodeColor = Color.Red;
                        RightRotate(k.Parent.Parent);
                    }
                }
                else
                {
                    Node uncle = k.Parent.Parent.Left;
                    if (uncle != null && uncle.NodeColor == Color.Red)
                    {
                        k.Parent.NodeColor = Color.Black; uncle.NodeColor = Color.Black;
                        k.Parent.Parent.NodeColor = Color.Red; k = k.Parent.Parent;
                    }
                    else
                    {
                        if (k == k.Parent.Left) { k = k.Parent; RightRotate(k); }
                        k.Parent.NodeColor = Color.Black; k.Parent.Parent.NodeColor = Color.Red;
                        LeftRotate(k.Parent.Parent);
                    }
                }
            }
            root.NodeColor = Color.Black;
        }
    }

    // --- AVL ТА ДЕРЕВО ВІДРІЗКІВ (Завдання 4) ---
    public class AVLTree : BST
    {
        private int GetH(Node n) => n == null ? 0 : n.Height;
        private int GetM(Node n) => n == null ? 0 : n.Max;

        public override void Insert(int key) { root = InsertAVL(root, key); }
        private Node InsertAVL(Node node, int key)
        {
            if (node == null) return new Node(key);
            if (key < node.Data) node.Left = InsertAVL(node.Left, key);
            else if (key > node.Data) node.Right = InsertAVL(node.Right, key);
            else return node;

            node.Height = 1 + Math.Max(GetH(node.Left), GetH(node.Right));
            // Оновлення Max для дерева відрізків
            node.Max = Math.Max(node.High, Math.Max(GetM(node.Left), GetM(node.Right)));

            int balance = GetH(node.Left) - GetH(node.Right);
            if (balance > 1 && key < node.Left.Data) return RightRotate(node);
            if (balance < -1 && key > node.Right.Data) return LeftRotate(node);
            if (balance > 1 && key > node.Left.Data) { node.Left = LeftRotate(node.Left); return RightRotate(node); }
            if (balance < -1 && key < node.Right.Data) { node.Right = RightRotate(node.Right); return LeftRotate(node); }
            return node;
        }

        private Node LeftRotate(Node x)
        {
            Console.WriteLine(">>> ПОВОРОТ ЛІВИЙ: Оновлення Max за O(1)");
            Node y = x.Right; Node T = y.Left;
            y.Left = x; x.Right = T;
            // Оновлення атрибутів
            x.Height = 1 + Math.Max(GetH(x.Left), GetH(x.Right));
            y.Height = 1 + Math.Max(GetH(y.Left), GetH(y.Right));
            x.Max = Math.Max(x.High, Math.Max(GetM(x.Left), GetM(x.Right)));
            y.Max = Math.Max(y.High, Math.Max(GetM(y.Left), GetM(y.Right)));
            return y;
        }
        private Node RightRotate(Node y)
        {
            Console.WriteLine(">>> ПОВОРОТ ПРАВИЙ: Оновлення Max за O(1)");
            Node x = y.Left; Node T = x.Right;
            x.Right = y; y.Left = T;
            y.Height = 1 + Math.Max(GetH(y.Left), GetH(y.Right));
            x.Height = 1 + Math.Max(GetH(x.Left), GetH(x.Right));
            y.Max = Math.Max(y.High, Math.Max(GetM(y.Left), GetM(y.Right)));
            x.Max = Math.Max(x.High, Math.Max(GetM(x.Left), GetM(x.Right)));
            return x;
        }
    }

    // --- SPLAY (Завдання 5: Без тернарних операторів) ---
    public class SplayTree : BST
    {
        public void Splay(int key) 
        {
            root = SplayInternal(root, key);
        }
        private Node SplayInternal(Node r, int k)
        {
            if (r == null || r.Data == k) return r;
            if (r.Data > k)
            {
                if (r.Left == null) return r;
                if (r.Left.Data > k)
                { 
                    r.Left.Left = SplayInternal(r.Left.Left, k); r = RR(r);
                }
                else if (r.Left.Data < k)
                {
                    r.Left.Right = SplayInternal(r.Left.Right, k); 
                    if (r.Left.Right != null) 
                        r.Left = LR(r.Left); 
                }

                if (r.Left == null)
                { 
                    return r; 
                }
                else
                { 
                    return RR(r);
                }
            }
            else
            {
                if (r.Right == null) return r;
                if (r.Right.Data < k) 
                {
                    r.Right.Right = SplayInternal(r.Right.Right, k); r = LR(r); 
                }
                else if (r.Right.Data > k) 
                {
                    r.Right.Left = SplayInternal(r.Right.Left, k); 
                    if (r.Right.Left != null) r.Right = RR(r.Right); 
                }

                if (r.Right == null) 
                {
                    return r; 
                }
                else 
                { 
                    return LR(r); 
                }
            }
        }
        private Node LR(Node x) 
        {
            Node y = x.Right; x.Right = y.Left; y.Left = x;
            return y;
        }
        private Node RR(Node x)
        { 
            Node y = x.Left; x.Left = y.Right; y.Right = x;
            return y; 
        }
    }

    class Program
    {
        static BST currentTree = new BST();

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            currentTree.Prepopulate();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("    КОМПЛЕКСНА ЛАБОРАТОРНА РОБОТА: АЛГОРИТМИ     ");
                Console.WriteLine("==================================================");
                TreePrinter.Print(currentTree.root);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("1. Завдання 1: Вставка вузла (BST)");
                Console.WriteLine("2. Завдання 2: Видалення вузла (BST)");
                Console.WriteLine("3. Завдання 3: Перефарбування та повороти (RBT)");
                Console.WriteLine("4. Завдання 4: Повороти + Max O(1) (AVL/Interval)");
                Console.WriteLine("5. Завдання 5: Splay алгоритм (Splay Tree)");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("--------------------------------------------------");
                Console.Write("Оберіть номер завдання: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1": case "2": currentTree = new BST(); break;
                    case "3": currentTree = new RBTree(); break;
                    case "4": currentTree = new AVLTree(); break;
                    case "5": currentTree = new SplayTree(); break;
                    default: continue;
                }
                currentTree.Prepopulate();
                Run(currentTree, choice);
                currentTree = new BST(); currentTree.Prepopulate(); // Скидання
            }
        }

        static void Run(BST tree, string t)
        {
            string status = "Дерево ініціалізовано.";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ВИКОНАННЯ ЗАВДАННЯ " + t + " ===");
                TreePrinter.Print(tree.root);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Статус: " + status);
                Console.WriteLine("1. Додати | 2. " + (t == "5" ? "Splay" : "Видалити") + " | 3. НАЗАД");
                string act = Console.ReadLine();
                if (act == "3") break;
                Console.Write("Число: ");
                if (int.TryParse(Console.ReadLine(), out int val))
                {
                    if (act == "1") { tree.Insert(val); status = "Додано " + val; }
                    else if (act == "2")
                    {
                        if (t == "5") { ((SplayTree)tree).Splay(val); status = "Splay " + val; }
                        else { tree.Delete(val); status = "Видалено " + val; }
                    }
                    Console.WriteLine("\nНатисніть Enter для продовження..."); Console.ReadLine();
                }
            }
        }
    }
}