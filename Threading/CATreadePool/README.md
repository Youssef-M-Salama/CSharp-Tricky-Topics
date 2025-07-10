# ?? Using ThreadPool in C#

This project demonstrates how to use the **ThreadPool** in C# to run tasks efficiently on background threads.

---

## ?? What is a Thread Pool?

A **ThreadPool** is a pool of worker threads managed by .NET.  
Instead of creating new threads for every task (which is expensive), the runtime **reuses** threads from this pool.

This saves:
- CPU time
- Memory
- Context switching

---

## ? Key Methods Used

### 1. `ThreadPool.QueueUserWorkItem`

```csharp
ThreadPool.QueueUserWorkItem(MyFunction);          // No state
ThreadPool.QueueUserWorkItem(MyFunction, myObj);   // With state object
