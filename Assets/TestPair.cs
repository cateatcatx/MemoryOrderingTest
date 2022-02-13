using System;
using System.Threading;
using System.Threading.Tasks;

namespace DefaultNamespace
{
    public class TestPair
    {
        public int running;
        public int memoryBarrier;
        public int curRound;
        public int maxRound;
        public int error;
        public int a;
        public int b;
        public int c;
        public int d;

        public void Start(int memoryBarrier, int maxRound)
        {
            a = 0;
            b = 0;
            c = 0;
            d = 0;
            running = 1;
            this.memoryBarrier = memoryBarrier;
            curRound = 0;
            this.maxRound = maxRound;

            Task.Run(Foo1);
            Task.Run(Foo2);
        }

        private void Foo1()
        {
            var r = new Random();
            
            for (int i = 0; i < maxRound; ++i)
            {
                a = 1;
                if (memoryBarrier == 1) Thread.MemoryBarrier();
                b = r.Next(1, 10);

                while (d * r.Next(1, 10) == 0) {}
                if (memoryBarrier == 1) Thread.MemoryBarrier();
                if (c != 1)
                    Interlocked.Increment(ref error);
                c = 0;
                d = 0;

                curRound = i + 1;
                // Thread.Sleep(1);
            }

            a = 1;
            if (memoryBarrier == 1) Thread.MemoryBarrier();
            b = r.Next(1, 10);
        
            running = 0;
        }

        private void Foo2()
        {
            var r = new Random();
            
            while (running == 1)
            {
                while (b * r.Next(1, 10) == 0) {}
                if (memoryBarrier == 1) Thread.MemoryBarrier();
                if (a != 1)
                    Interlocked.Increment(ref error);
                a = 0;
                b = 0;

                c = 1;
                if (memoryBarrier == 1) Thread.MemoryBarrier();
                d = r.Next(1, 10);
            }
        }
    }
}