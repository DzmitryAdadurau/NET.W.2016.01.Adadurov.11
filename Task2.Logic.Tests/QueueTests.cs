using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task2.Logic;

namespace Task2.Logic.Tests
{
    [TestFixture]
    public class QueueTests
    {
        [Test]
        public void EnqueueTest_20elements_TestingInternalArrayExpanding()
        {
            Queue<int> q = new Queue<int>();
            for (int i = 0; i < 22; i++)
            {
                q.Enqueue(i);
            }
            q.Dequeue();
            q.Dequeue();
            Assert.AreEqual(20, q.Count);
        }

        [Test]
        public void GetEnumeratorTest_EqualQueues()
        {
            Queue<int> q1 = new Queue<int>();
            Queue<int> q2 = new Queue<int>();

            foreach (int item in q1)
            {
                q2.Enqueue(item);
            }
            Assert.IsTrue(q1.SequenceEqual(q2));
        }
    }
}
