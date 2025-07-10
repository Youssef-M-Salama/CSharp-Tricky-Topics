using System;
using System.Threading;

namespace CARaceCondition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var wallet = new Wallet("Issam", 50);

            // Two threads attempting to debit money at the same time
            var t1 = new Thread(() => wallet.Debit(40)); // should succeed
            var t2 = new Thread(() => wallet.Debit(50)); // should fail if balance is already too low

            t1.Start();
            t2.Start();

            // Wait for both threads to finish
            t1.Join();
            t2.Join();

            // Final wallet state
            Console.WriteLine(wallet);
        }
    }

    /// <summary>
    /// A thread-safe wallet with synchronized debit operations
    /// </summary>
    class Wallet
    {
        private readonly object bitcoinsLock = new object(); // Lock object for synchronizing access

        public Wallet(string name, int bitcoins)
        {
            Name = name;
            Bitcoins = bitcoins;
        }

        public string Name { get; private set; }
        public int Bitcoins { get; private set; }

        /// <summary>
        /// Thread-safe debit operation with lock
        /// </summary>
        public void Debit(int amount)
        {
            lock (bitcoinsLock)
            {
                if (Bitcoins >= amount)
                {
                    // Simulate delay to increase chance of race condition (if not locked)
                    Thread.Sleep(1000);
                    Bitcoins -= amount;
                    Console.WriteLine(
                        $"[Thread ID: {Thread.CurrentThread.ManagedThreadId}] " +
                        $"Debited {amount}, Remaining: {Bitcoins}");
                }
                else
                {
                    Console.WriteLine(
                        $"[Thread ID: {Thread.CurrentThread.ManagedThreadId}] " +
                        $"Failed to debit {amount} (Insufficient funds: {Bitcoins})");
                }
            }
        }

        /// <summary>
        /// Non-thread-safe credit (for learning)
        /// </summary>
        public void Credit(int amount)
        {
            Thread.Sleep(1000);
            Bitcoins += amount;
        }

        public override string ToString()
        {
            return $"[{Name} -> {Bitcoins} Bitcoins]";
        }
    }
}
