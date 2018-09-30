using System;
using Xunit;

namespace PriorityQueue.Test
{
    public class PriorityQueueTests
    {
        private readonly PriorityQueue<ComparableThing> _pq = new PriorityQueue<ComparableThing>();

        [Fact]
        public void AnItemThatIsEnqueued_CanBeDequeued()
        {
            var t = new ComparableThing(1);
            _pq.Enqueue(t);

            var r = _pq.Dequeue();

            Assert.Equal(t, r);
        }

        [Fact]
        public void HighestPriorityItems_GetsDequedFirst()
        {
            var lowPrio = new ComparableThing(3);
            var mediumPrio = new ComparableThing(2);
            var highPrio = new ComparableThing(1);

            _pq.Enqueue(lowPrio);
            _pq.Enqueue(highPrio);
            _pq.Enqueue(mediumPrio);

            Assert.Equal(highPrio, _pq.Dequeue());
            Assert.Equal(mediumPrio, _pq.Dequeue());
            Assert.Equal(lowPrio, _pq.Dequeue());
        }

        [Fact]
        public void ItemsWithSamePriority_GetsDequedInTheOrderTheyWereAdded()
        {
            var item1 = new ComparableThing(1);
            var item2 = new ComparableThing(1);
            var item3 = new ComparableThing(1);

            _pq.Enqueue(item1);
            _pq.Enqueue(item2);
            _pq.Enqueue(item3);

            Assert.Same(item1, _pq.Dequeue());
            Assert.Same(item2, _pq.Dequeue());
            Assert.Same(item3, _pq.Dequeue());
        }

        [Fact(Timeout = 10000)]
        public void QueueCanHandleLargeAmountsOfItems_WithGoodPerformance()
        {
            const int itemCount = 1_000_000;
            var rnd = new Random();

            for (var i = 0; i < itemCount; i++)
            {
                var item = new ComparableThing(rnd.Next());
                _pq.Enqueue(item);
            }

            var lastItem = _pq.Dequeue();

            for (var i = 1; i < itemCount; i++)
            {
                var item = _pq.Dequeue();
                Assert.True(item.CompareTo(lastItem) >= 0);
                lastItem = item;
            }
        }
    }

    public class ComparableThing : IComparable<ComparableThing>
    {
        private readonly int _priority;

        public ComparableThing(int priority)
        {
            _priority = priority;
        }

        public int CompareTo(ComparableThing other)
        {
            if (_priority > other._priority)
            {
                return 1;
            }
            else if (_priority < other._priority)
            {
                return -1;
            }
            return 0;
        }
    }
}
