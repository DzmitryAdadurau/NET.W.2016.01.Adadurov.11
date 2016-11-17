using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    public class FibonacciGenerator
    {
        /// <summary>
        /// Returns Fibonacci sequence
        /// </summary>
        /// <param name="target">Wil be working until reaches target number</param>
        /// <returns>Next Fibonacci number</returns>
        public IEnumerable<int> GetNumbers(int target)
        {
            int prev = -1, next = 1;

            for (int i = 0; i < target; i++)
            {
                int sum = prev + next;
                prev = next;
                next = sum;
                yield return sum;
            }
        }
    }
}
