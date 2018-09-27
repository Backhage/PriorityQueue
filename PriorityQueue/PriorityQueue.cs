using System;
using System.Collections.Generic;

namespace PriorityQueue
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private readonly List<T> _items = new List<T>();

        public void Enqueue(T item)
        {
            _items.Add(item);
            BubbleUp();
        }

        private void BubbleUp()
        {
            var childIndex = _items.Count - 1;
            while (childIndex > 0)
            {
                var parentIndex = (childIndex - 1) / 2;
                if (_items[childIndex].CompareTo(_items[parentIndex]) >= 0)
                {
                    break;
                }
                Swap(childIndex, parentIndex);
                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            var highestPrioritizedItem = _items[0];

            MoveLastItemToTheTop();
            SinkDown();

            return highestPrioritizedItem;
        }

        private void MoveLastItemToTheTop()
        {
            var lastIndex = _items.Count - 1;
            _items[0] = _items[lastIndex];
            _items.RemoveAt(lastIndex);
        }

        private void SinkDown()
        {
            var lastIndex = _items.Count - 1;
            var parentIndex = 0;

            while (true)
            {
                var leftChildIndex = parentIndex * 2 + 1;
                if (leftChildIndex > lastIndex)
                {
                    break;
                }
                var rightChildIndex = leftChildIndex + 1;
                if (rightChildIndex <= lastIndex && _items[rightChildIndex].CompareTo(_items[leftChildIndex]) < 0)
                {
                    leftChildIndex = rightChildIndex;
                }
                if (_items[parentIndex].CompareTo(_items[leftChildIndex]) <= 0)
                {
                    break;
                }
                Swap(parentIndex, leftChildIndex);
                parentIndex = leftChildIndex;
            }

        }

        private void Swap(int index1, int index2)
        {
            var tmp = _items[index1];
            _items[index1] = _items[index2];
            _items[index2] = tmp;
        }
    }
}
