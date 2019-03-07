using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySynchronization;
using System.Threading;
using System.Threading.Tasks;

namespace ReadWriteLockTests
{
    [TestClass]
    public class RWTests
    {
        /// <summary>
        /// Make sure there can be multiple readers at the same time.
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            int SIZE = 100;
            IMyReadWriteLock rwLock = new MyReadWriteLock1();
            ManualResetEvent mre = new ManualResetEvent(true);
            Task[] tasks = new Task[SIZE];
            int count = SIZE;

            for (int i = 0; i < SIZE; i++)
            {
                tasks[i] = Task.Run(() => Reader());
            }

            Assert.IsTrue(SpinWait.SpinUntil(() => count == 0, 5000), "Timed out waiting for tasks to enter critical section");
            mre.Set();
            Assert.IsTrue(Task.WaitAll(tasks, 5000), "Timed out waiting for tasks to terminate");


            void Reader()
            {
                rwLock.EnterReadLock();
                try
                {
                    Interlocked.Decrement(ref count);
                    mre.WaitOne();
                }
                finally
                {
                    rwLock.ExitReadLock();
                }

            }
        }
    }
}
