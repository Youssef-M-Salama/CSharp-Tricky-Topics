using System;
using System.Threading;

namespace CADeadLock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main Thread";

            var wallet1 = new Wallet("Issam", 100, 1);
            var wallet2 = new Wallet("Reem", 50, 2);

            Console.WriteLine("Before Transaction:");
            Console.WriteLine($"{wallet1}, {wallet2}");
            Console.WriteLine("\n--------------------");
            Console.WriteLine("After Transaction:");

            var transferManager1 = new TransferManager(wallet1, wallet2, 50);
            var transferManager2 = new TransferManager(wallet2, wallet1, 30);

            var t1 = new Thread(transferManager1.TransferWithOrderedLocks);
            var t2 = new Thread(transferManager2.TransferWithOrderedLocks);

            t1.Name = "T1";
            t2.Name = "T2";

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine($"{wallet1}, {wallet2}");
            Console.WriteLine("\n--------------------");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Represents a basic wallet with ID and balance.
    /// </summary>
    class Wallet
    {
        private readonly object bitcoinsLock = new object();

        public Wallet(string name, int bitcoins, int id)
        {
            Name = name;
            Bitcoins = bitcoins;
            Id = id;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Bitcoins { get; private set; }

        public void Debit(int amount)
        {
            lock (bitcoinsLock)
            {
                if (Bitcoins >= amount)
                {
                    Thread.Sleep(1000); // Simulate work
                    Bitcoins -= amount;
                }
            }
        }

        public void Credit(int amount)
        {
            Thread.Sleep(1000); // Simulate work
            Bitcoins += amount;
        }

        public override string ToString()
        {
            return $"[{Name} -> {Bitcoins} Bitcoins]";
        }
    }

    /// <summary>
    /// Handles money transfer between two Wallets.
    /// </summary>
    class TransferManager
    {
        private Wallet from;
        private Wallet to;
        private int amountToTransfer;

        public TransferManager(Wallet from, Wallet to, int amountToTransfer)
        {
            this.from = from;
            this.to = to;
            this.amountToTransfer = amountToTransfer;
        }

        /// <summary>
        /// Risky transfer using nested locks (deadlock possible if order isn't consistent).
        /// </summary>
        public void TransferWithMonitorTry()
        {
            Console.WriteLine($"{Thread.CurrentThread.Name} trying to lock {from}");
            lock (from)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} locked {from}");
                Thread.Sleep(1000); // Simulate processing

                Console.WriteLine($"{Thread.CurrentThread.Name} trying to lock {to}");

                if (Monitor.TryEnter(to, 1000))
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} locked {to}");
                    try
                    {
                        from.Debit(amountToTransfer);
                        to.Credit(amountToTransfer);
                    }
                    finally
                    {
                        Monitor.Exit(to);
                    }
                }
                else
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} failed to lock {to} — potential deadlock avoided.");
                }
            }
        }

        /// <summary>
        /// Safe transfer by locking wallets in order of their IDs to avoid deadlocks.
        /// </summary>
        public void TransferWithOrderedLocks()
        {
            Wallet first = from.Id < to.Id ? from : to;
            Wallet second = from.Id < to.Id ? to : from;

            Console.WriteLine($"{Thread.CurrentThread.Name} trying to lock {first}");
            lock (first)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} locked {first}");
                Thread.Sleep(1000); // Simulate processing

                Console.WriteLine($"{Thread.CurrentThread.Name} trying to lock {second}");
                lock (second)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name} locked {second}");
                    from.Debit(amountToTransfer);
                    to.Credit(amountToTransfer);

                    Console.WriteLine($"{Thread.CurrentThread.Name} transferred {amountToTransfer} from {from.Name} to {to.Name}");
                }
            }
        }
    }
}
