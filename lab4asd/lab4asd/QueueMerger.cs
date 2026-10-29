using System.Collections.Generic;

public class Task2Queues
{
    public Queue<int> Merge(Queue<int> q1, Queue<int> q2)
    {
        Queue<int> result = new Queue<int>();

        // Об'єднуємо, поки в обох є елементи
        while (q1.Count > 0 && q2.Count > 0)
        {
            if (q1.Peek() < q2.Peek())
            {
                result.Enqueue(q1.Dequeue());
            }
            else
            {
                result.Enqueue(q2.Dequeue());
            }
        }

        // Докидаємо те, що залишилося
        while (q1.Count > 0)
        {
            result.Enqueue(q1.Dequeue());
        }
        while (q2.Count > 0)
        {
            result.Enqueue(q2.Dequeue());
        }

        return result;
    }
}