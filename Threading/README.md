#  C# Threading Projects Summary - Advanced Learning Guide

Welcome to the **AsyncProgramming** solution! This collection contains hands-on projects that demonstrate core and advanced concepts of multithreading and asynchronous programming in C#. 

Each mini-project is fully documented with its own `README.md` file. This summary provides a unified, professional reference explaining:

-  Key threading concepts  
-  Essential classes and methods in `System.Threading`  
-  When and how to use each approach  
-  Pros and cons of each threading tool  

---

## 📚 Core Concepts

### 🧵 Thread
A `Thread` represents an independent path of execution within a process.

- **Namespace**: `System.Threading`
- **Manual control**: start, name, join, sleep, priority
- **Used when**: You need full control over thread lifecycle or long-running work

---

### Thread Pool
The ThreadPool is a collection of reusable threads managed by .NET.

- **Namespace**: `System.Threading.ThreadPool`
- **Use**: `ThreadPool.QueueUserWorkItem()` or `Task.Run()`
- **Used when**: You want to execute short-lived background tasks efficiently without managing threads manually

---

###  Task
`Task` is a higher-level API for running asynchronous code

- **Namespace**: `System.Threading.Tasks`
- **Supports**: async/await, continuations, cancellation tokens
- **Used when**: You want asynchronous, composable, and modern code

---

##  Synchronization Tools

###  `lock` (Monitor.Enter)
Used to prevent race conditions by ensuring one thread accesses a resource at a time.

```csharp
lock(obj)
{
   // critical section
}
