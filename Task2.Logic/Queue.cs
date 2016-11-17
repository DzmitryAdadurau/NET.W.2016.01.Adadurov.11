using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Logic
{
    public class Queue<T> : IEnumerable<T>, IEnumerable
    {
        private T[] _array;
        private int _head;
        private int _tail;
        private int _size;

        public Queue()
        {
            _array = new T[0];
        }

        public Queue(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();

            _array = new T[capacity];
            _head = 0;
            _tail = 0;
            _size = 0;
        }

        public void Enqueue(T item)
        {
            if (_size == _array.Length)
            {
                int capacity = _array.Length * 2;
                Expand(capacity);
            }
            _array[_tail] = item;
            _size++;
            _tail++;
        }

        public T Dequeue()
        {
            if (_size == 0)
                throw new InvalidOperationException();

            _array[_head] = default(T);
            _head++;
            _size--;
            return _array[_head];
        }

        public void Clear()
        {
            _head = 0;
            _tail = 0;
            _size = 0;
        }

        public int Count
        {
            get { return _size; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            private Queue<T> _queue;
            private int _index;
            private T _current;

            internal Enumerator(Queue<T> queue)
            {
                _queue = queue;
                _index = -1;
                _current = default(T);
            }

            public T Current
            {
                get
                {
                    if (_index < 0)
                    {
                        throw new InvalidOperationException();
                    }
                    return _current;
                }
            }

            public void Dispose()
            {
                _index = -2;
                _current = default(T);
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public bool MoveNext()
            {
                if (_index == -2)
                {
                    return false;
                }
                _index++;
                if (_index == _queue._size)
                {
                    _index = -2;
                    _current = default(T);
                    return false;
                }
                _current = _queue.GetElement(_index);
                return true;
            }

            public void Reset()
            {
                _index = -1;
                _current = default(T);
            }
        }

        #region Private section

        private void Expand(int capacity)
        {
            T[] destinationArray = new T[capacity];
            if (_size > 0)
            {
                if (_head < _tail)
                {
                    Array.Copy(_array, _head, destinationArray, 0, _size);
                }
                else
                {
                    Array.Copy(_array, _head, destinationArray, 0, _array.Length - _head);
                    Array.Copy(_array, 0, destinationArray, _array.Length - _head, _tail);
                }
            }
            _head = 0;
            _array = destinationArray;
            if (_size == capacity)
            {
                _tail = 0;
            }
            else
            {
                _tail = _size;
            }
        }

        internal T GetElement(int i)
        {
            return _array[_head + i];
        }

        #endregion
    }
}
