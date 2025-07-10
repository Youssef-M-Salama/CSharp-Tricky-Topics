using System;
using System.Diagnostics;
using System.Threading;

namespace CAProcessAndThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Display the unique identifier of the current process (useful to distinguish between multiple running apps)
            Console.WriteLine($"[Process ID]: {Process.GetCurrentProcess().Id}");

            // Display the ID of the current thread (ManagedThreadId is unique within the application)
            Console.WriteLine($"[Thread ID]: {Thread.CurrentThread.ManagedThreadId}");

            // Display the processor/core number the current thread is currently executing on
            Console.WriteLine($"[Processor/Core ID]: {Thread.GetCurrentProcessorId()}");

            // Wait for user input to keep the console window open
            Console.ReadKey();
        }
    }
}
