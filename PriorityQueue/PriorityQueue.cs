using System;
using System.Collections.Generic;

namespace PriorityQueue
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private readonly List<T> _pq = new List<T>();

        public int Count => _pq.Count;

        public void Enqueue(T item)
        {
            _pq.Add(item);
            BubbleUp();
        }

        private void BubbleUp()
        {
            var childIndex = _pq.Count - 1;
            while (childIndex > 0)
            {
                var parentIndex = (childIndex - 1) / 2;
                if (_pq[childIndex].CompareTo(_pq[parentIndex]) >= 0)
                {
                    break;
                }
                Swap(childIndex, parentIndex);
                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            var highestPrioritizedItem = _pq[0];

            MoveLastItemToTheTop();
            SinkDown();

            return highestPrioritizedItem;
        }

        private void MoveLastItemToTheTop()
        {
            var lastIndex = _pq.Count - 1;
            _pq[0] = _pq[lastIndex];
            _pq.RemoveAt(lastIndex);
        }

        private void SinkDown()
        {
            var lastIndex = _pq.Count - 1;
            var parentIndex = 0;

            while (true)
            {
                var firstChildIndex = parentIndex * 2 + 1;
                if (firstChildIndex > lastIndex)
                {
                    break;
                }
                var secondChildIndex = firstChildIndex + 1;
                if (secondChildIndex <= lastIndex && _pq[secondChildIndex].CompareTo(_pq[firstChildIndex]) < 0)
                {
                    firstChildIndex = secondChildIndex;
                }
                if (_pq[parentIndex].CompareTo(_pq[firstChildIndex]) < 0)
                {
                    break;
                }
                Swap(parentIndex, firstChildIndex);
                parentIndex = firstChildIndex;
            }
        }

        private void Swap(int index1, int index2)
        {
            var tmp = _pq[index1];
            _pq[index1] = _pq[index2];
            _pq[index2] = tmp;
        }
    }
}
