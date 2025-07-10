using System;
using System.Threading;

namespace CAmultiThreading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var wallet = new Wallet("Issam", 80);

            // Create first thread using method group syntax
            Thread t1 = new Thread(wallet.RunRandomTransactions);
            t1.Name = "T1";
            Console.WriteLine($"[After Declaration] {t1.Name} State: {t1.ThreadState}");

            t1.Start(); // Start thread T1
            Console.WriteLine($"[After Start] {t1.Name} State: {t1.ThreadState}");

            // Main thread waits for t1 to finish execution before continuing
            // This delays the creation and starting of t2
            t1.Join();

            // Create second thread using ThreadStart delegate syntax
            Thread t2 = new Thread(new ThreadStart(wallet.RunRandomTransactions));
            t2.Name = "T2";
            t2.Start(); // t2 will only start after t1 has completed

            // Name the main thread for better tracking in logs
            Thread.CurrentThread.Name = "Main Thread";
            Console.WriteLine($"[Current Thread] {Thread.CurrentThread.Name}");

            Console.ReadKey();
        }

        /// <summary>
        /// Represents a simple wallet capable of processing transactions.
        /// </summary>
        class Wallet
        {
            public Wallet(string name, int bitcoins)
            {
                Name = name;
                Bitcoins = bitcoins;
            }

            public string Name { get; private set; }
            public int Bitcoins { get; private set; }

            public void Debit(int amount)
            {
                Bitcoins -= amount;
                Console.WriteLine(
                    $"[Thread ID: {Thread.CurrentThread.ManagedThreadId}] " +
                    $"[{Thread.CurrentThread.Name}] " +
                    $"[Processor ID: {Thread.GetCurrentProcessorId()}] " +
                    $"-{amount}");
            }

            public void Credit(int amount)
            {
                // Simulate a delay to make thread behavior easier to observe
                Thread.Sleep(1000);
                Bitcoins += amount;
                Console.WriteLine(
                    $"[Thread ID: {Thread.CurrentThread.ManagedThreadId}] " +
                    $"[{Thread.CurrentThread.Name}] " +
                    $"[Processor ID: {Thread.GetCurrentProcessorId()}] " +
                    $"+{amount}");
            }

            public void RunRandomTransactions()
            {
                int[] amounts = { 10, 20, 30, -20, 10, -10, 30, -10, 40, -20 };

                foreach (var amount in amounts)
                {
                    int absValue = Math.Abs(amount);
                    if (amount < 0)
                        Debit(absValue);
                    else
                        Credit(absValue);
                }
            }

            public override string ToString()
            {
                return $"[{Name} -> {Bitcoins} Bitcoins]";
            }
        }
    }
}
