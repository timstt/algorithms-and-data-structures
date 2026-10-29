using System;
using System.Collections.Generic;

public class Task1Brackets
{
    public void Run()
    {
        string s = Console.ReadLine();
        Stack<char> stack = new Stack<char>();

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                stack.Push(s[i]);
            }
            else if (s[i] == ')')
            {
                if (stack.Count == 0)
                {
                    Console.WriteLine("Ні, не можна");
                    return;
                }
                stack.Pop();
            }
        }

        if (stack.Count == 0)
        {
            Console.WriteLine("Так, можна");
        }
        else
        {
            Console.WriteLine("Ні, не можна");
        }
    }
}