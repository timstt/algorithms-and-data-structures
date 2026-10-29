using System;
using System.Collections.Generic;

public class SetofStacks
{
    private List<Stack<int>> stacks = new List<Stack<int>>();
    private int limit;

    public SetofStacks(int limit)
    {
        this.limit = limit;
    }

    public void push(int value)
    {
        // Якщо немає стеку або останній заповнений — створюємо новий
        if (stacks.Count == 0 || stacks[stacks.Count - 1].Count == limit)
        {
            stacks.Add(new Stack<int>());
        }

        stacks[stacks.Count - 1].Push(value);
    }

    public int pop()
    {
        if (stacks.Count == 0)
        {
            return -1;
        }

        // Беремо значення з останнього стеку
        int value = stacks[stacks.Count - 1].Pop();

        // Якщо стек став порожнім — видаляємо його
        if (stacks[stacks.Count - 1].Count == 0)
        {
            stacks.RemoveAt(stacks.Count - 1);
        }

        return value;
    }
}