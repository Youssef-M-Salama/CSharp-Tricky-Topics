using System;
using System.Threading;

namespace CASequential
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a wallet with a name and initial balance
            var wallet = new Wallet("Issam", 80);

            // Perform the first set of random transactions
            wallet.RunRandomTransactions();
            Console.WriteLine($"{wallet}\n");
            Console.WriteLine("------------");

            // Perform the second set of random transactions
            wallet.RunRandomTransactions();
            Console.WriteLine($"{wallet}\n");

            // Wait for user input before closing
            Console.ReadKey();
        }

        /// <summary>
        /// Represents a simple wallet with Bitcoin balance
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

            /// <summary>
            /// Subtracts Bitcoins from the wallet
            /// </summary>
            public void Debit(int amount)
            {
                Bitcoins -= amount;
            }

            /// <summary>
            /// Adds Bitcoins to the wallet
            /// </summary>
            public void Credit(int amount)
            {
                Bitcoins += amount;
            }

            /// <summary>
            /// Executes a series of random debit/credit operations
            /// </summary>
            public void RunRandomTransactions()
            {
                int[] amounts = { 10, 20, 30, -20, 10, -10, 30, -10, 40, -20 };

                foreach (var amount in amounts)
                {
                    var absValue = Math.Abs(amount);

                    if (amount < 0)
                    {
                        Debit(absValue);
                    }
                    else
                    {
                        Credit(absValue);
                    }

                    // Print thread and processor information during each transaction
                    Console.WriteLine($"[Thread ID]: {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"[Processor/Core ID]: {Thread.GetCurrentProcessorId()}");
                }
            }

            /// <summary>
            /// Custom display of the wallet state
            /// </summary>
            public override string ToString()
            {
                return $"[{Name} -> {Bitcoins} Bitcoins]";
            }
        }
    }
}
