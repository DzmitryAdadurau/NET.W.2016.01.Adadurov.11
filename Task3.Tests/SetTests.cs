using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task3.Logic;

namespace Task3.Tests
{
    [TestFixture]
    public class SetTests
    {
        [Test]
        public void Add_20_objects()
        {
            Set<Box> set = new Set<Box>(new BoxEqualityComparer(), 10);
            for (int i = 0; i < 20; i++)
            {
                Box b = new Box(i, i, i);
                set.Add(b);
            }
            Assert.AreEqual(set.Count, 20);
        }

        [Test]
        public void EqualsTest_NotEqualSets()
        {
            Set<Box> set1 = new Set<Box>(new BoxEqualityComparer(), 10);
            Set<Box> set2 = new Set<Box>(new BoxEqualityComparer(), 10);
            for (int i = 0; i < 20; i++)
            {
                Box b = new Box(i, i, i);
                if (i % 2 == 0)
                {
                    set1.Add(b);
                }
                else
                {
                    set2.Add(b);
                }
            }

            Assert.IsFalse(set1.Equals(set2));
        }

        [Test]
        public void EqualsTest_EqualSets()
        {
            Set<Box> set1 = new Set<Box>(new BoxEqualityComparer(), 10);
            Set<Box> set2 = new Set<Box>(new BoxEqualityComparer(), 10);
            for (int i = 0; i < 20; i++)
            {
                Box b = new Box(i, i, i);
                set1.Add(b);
                set2.Add(b);
            }

            Assert.IsTrue(set1.Equals(set2));
        }


        [Test]
        public void GetEnumeratorTest_EqualSets()
        {
            Set<Box> set1 = new Set<Box>(new BoxEqualityComparer(), 10);
            Set<Box> set2 = new Set<Box>(new BoxEqualityComparer(), 10);
            for (int i = 0; i < 10; i++)
            {
                Box b = new Box(i, i, i);
                set1.Add(b);
            }

            foreach (Box item in set1)
            {
                set2.Add(item);
            }

            Assert.IsTrue(set1.Equals(set2));
        }

    }

    #region Help Classes

    public class Box
    {
        public Box(int h, int l, int w)
        {
            this.Height = h;
            this.Length = l;
            this.Width = w;
        }

        public int Height { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }

        public override String ToString()
        {
            return String.Format("({0}, {1}, {2})", Height, Length, Width);
        }
    }


    class BoxEqualityComparer : IEqualityComparer<Box>
    {
        public bool Equals(Box b1, Box b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null | b2 == null)
                return false;
            else if (b1.Height == b2.Height && b1.Length == b2.Length
                                && b1.Width == b2.Width)
                return true;
            else
                return false;
        }

        public int GetHashCode(Box bx)
        {
            int hCode = bx.Height ^ bx.Length ^ bx.Width;
            return hCode.GetHashCode();
        }
    }
#endregion

}
