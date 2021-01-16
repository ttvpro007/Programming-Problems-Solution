using System.Collections.Generic;
using System.Linq;

namespace Codewars.Solutions._6_Kyu
{
    class TheSupermarketQueue
    {
        public static long Solution(int[] customers, int n)
        {
            long time = 0;
            var customerQueue = new Queue<int>();
            var lanes = new int[n];

            for (int i = 0; i < customers.Length; i++)
            {
                customerQueue.Enqueue(customers[i]);
            }

            while (customerQueue.Count > 0 || !lanes.All(customer => customer == 0))
            {
                for (int i = 0; i < lanes.Length; i++)
                {
                    if (lanes[i] == 0 && customerQueue.Count > 0)
                    {
                        lanes[i] = customerQueue.Dequeue();
                    }

                    lanes[i] = lanes[i] == 0 ? 0 : lanes[i] - 1;
                }
                time++;
            }

            return time;
        }
    }
}
