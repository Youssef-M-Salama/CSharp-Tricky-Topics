
namespace CATreadePool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(print));


            //2
            Task.Run(print);

            var employee = new Employee
            {
                Rate = 10,
                TotalHours = 40,
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(CalcSalary),employee);

            Console.ReadKey();
        }

        private static void CalcSalary(object? employee)
        {
            var emp=employee as Employee;
            if(employee is null)
            {
                return;
            }
            emp.TotalSalary = emp.TotalHours * emp.Rate;
            Console.WriteLine($"Thread id:{Thread.CurrentThread.ManagedThreadId} , Thread Name: {Thread.CurrentThread.Name}");
            Console.WriteLine($"Is Pooled Thread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Background: {Thread.CurrentThread.IsBackground}");
            Console.WriteLine(emp.TotalSalary.ToString("C"));
        }

        private static void print(object? state)
        {
            Console.WriteLine($"Thread id:{Thread.CurrentThread.ManagedThreadId} , Thread Name: {Thread.CurrentThread.Name}");
            Console.WriteLine($"Is Pooled Thread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Background: {Thread.CurrentThread.IsBackground}");
            for (int i = 0; i < 10; ++i)
            {
                Console.WriteLine($"Cycle1 {i+1}");
            }
        }  
        private static void print()
        {
            Console.WriteLine($"Thread id:{Thread.CurrentThread.ManagedThreadId} , Thread Name: {Thread.CurrentThread.Name}");
            Console.WriteLine($"Is Pooled Thread: {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Background: {Thread.CurrentThread.IsBackground}");
            for (int i = 0; i < 10; ++i)
            {
                Console.WriteLine($"Cycle2 {i+1}");
            }
        }
    }
    class Employee
    {
        public int TotalHours { get; set; }
        public decimal Rate { get; set; }
        public decimal TotalSalary { get; set; }
    }
}
