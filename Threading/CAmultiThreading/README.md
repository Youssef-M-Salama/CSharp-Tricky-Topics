# 🔀 Multi-Threaded Wallet Transactions in C#

This example demonstrates how to run wallet operations in multiple threads and track which thread performs which operation.

---

## 🧵 Thread Features Used

- `Thread` creation via:
  - Method group (`new Thread(wallet.RunRandomTransactions)`)
  - Delegate syntax (`new Thread(new ThreadStart(...))`)
- `Thread.Name` for identification
- `Thread.Join()` to block the **main thread** until another thread finishes
- `Thread.CurrentThread` to show which thread is executing
- `Thread.GetCurrentProcessorId()` to reveal which CPU core is running the thread

---

## 🧠 What does `Join()` really do?

```csharp
t1.Start();
t1.Join(); // Blocks the main thread until t1 completes
