using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.Logic
{
    public class Set<T> : IEquatable<Set<T>>, IEnumerable<T>, IEnumerable where T : class
    {
        private T[] _array;
        private int _count;
        private IEqualityComparer<T> _comparer;
        private const int defaultInitialSize = 10;

        /// <summary>
        /// Create empty set
        /// </summary>
        public Set() : this(EqualityComparer<T>.Default, defaultInitialSize)
        {
        }

        /// <summary>
        /// Create set with initial size
        /// </summary>
        /// <param name="initialSize">Size of set</param>
        public Set(int initialSize) : this(EqualityComparer<T>.Default, initialSize)
        {
        }

        /// <summary>
        /// Create set with initial size & comparer
        /// </summary>
        /// <param name="comparer">Comparer for the objects</param>
        /// <param name="initialSize">Initial size of the set</param>
        public Set(IEqualityComparer<T> comparer, int initialSize)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }
            _array = new T[initialSize];
            _count = 0;
            _comparer = comparer;
        }

        /// <summary>
        /// Add item to the set.
        /// </summary>
        /// <param name="item">Element to add to the set</param>
        /// <returns>true - if item added to the set; false - if set already have that item;</returns>
        public bool Add(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_comparer.Equals(_array[i], item))
                {
                    return false;
                }
            }

            if (_array.Length == _count + 1)
            {
                Expand(_array.Length * 2);
            }

            _array[_count++] = item;
            return true;
        }

        /// <summary>
        /// Removes element from the set
        /// </summary>
        /// <param name="item">Element to remove</param>
        /// <returns>true - successfully removed; false - there is no such element in the set;</returns>
        public bool Remove(T item)
        {
            int pos = Find(item);
            if (pos > -1)
            {
                _array[pos] = null;
                _count--;
                UniformArray(pos);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Compare two sets by values (not references)
        /// </summary>
        /// <param name="other">Set to compare with</param>
        /// <returns>true - sets are equal; false - sets are not equal</returns>
        public bool Equals(Set<T> other)
        {
            if (ReferenceEquals(this, null))
                return false;

            if (this.Count != other.Count)
                return false;

            for (int i = 0; i < this.Count; i++)
            {
                bool flagFound = false;
                for (int j = 0; j < this.Count; j++)
                {
                    if (_comparer.Equals(this._array[i], other._array[j]))
                    {
                        flagFound = true;
                        break;
                    }
                }
                if (!flagFound)
                    return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Set<T> setObj = obj as Set<T>;
            if (setObj == null)
                return false;
            else
                return Equals(setObj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Count of the elements in the set
        /// </summary>
        public int Count
        {
            get { return _count; }
        }

        public static bool operator ==(Set<T> set1, Set<T> set2)
        {
            return set1.Equals(set2);
        }

        public static bool operator !=(Set<T> set1, Set<T> set2)
        {
            return set1 == set2;
        }

        public static Set<T> operator +(Set<T> set1, Set<T> set2)
        {
            Set<T> newSet = new Set<T>(set1._comparer, set1.Count + set2.Count);
            for (int i = 0; i < set1.Count; i++)
            {
                newSet.Add(set1._array[i]);
            }
            for (int i = 0; i < set2.Count; i++)
            {
                newSet.Add(set2._array[i]);
            }
            return newSet;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>
        {
            private readonly Set<T> _collection;
            private int _current;

            public Enumerator(Set<T> collection)
            {
                _current = -1;
                _collection = collection;
            }

            public T Current
            {
                get
                {
                    if (_current == -1 || _current == _collection.Count)
                    {
                        throw new InvalidOperationException();
                    }
                    return _collection._array[_current];
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public void Reset()
            {
                _current = -1;
            }

            public bool MoveNext()
            {
                return ++_current < _collection.Count;
            }

            public void Dispose() { }
        }

        #region Private Section
        private int Find(T item)
        {
            if (item == null)
                return -1;

            for (int i = 0; i < _count; i++)
            {
                if (_comparer.Equals(_array[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        private void Expand(int capacity)
        {
            T[] destinationArray = new T[capacity];
            Array.Copy(_array, 0, destinationArray, 0, _count);
            _array = destinationArray;
        }

        private void UniformArray(int pos)
        {
            Array.Copy(_array, pos + 1, _array, pos, _count - pos);
        }
        #endregion
    }
}
