# CSharp Tricky Topics

This repository is dedicated to mastering the most **tricky**, **misunderstood**, or **low-level advanced** topics in C#.  
It’s designed as a **code-based knowledge base** with working examples, explanations, and real output, all carefully structured.

>  This repo focuses only on **C# Core Topics** (not LINQ, EF, or ASP.NET — those will be in separate repositories).

---

##  What This Repo Is About

Most tutorials explain C# in a very abstract way — this repo changes that.

 We **simulate real problems**, like race conditions and deadlocks  
 We **analyze and trace thread behavior** with actual thread IDs and outputs  
 We **solve tricky problems step by step**, with comments, explanations, and clean code  
 We **organize code per concept**, each in its own project and `README.md`  
 We treat this repo as a **knowledge log** to review and reuse anytime  

If you want to **truly master what’s happening under the hood in C#**, this repo is for you.

---

##  What We’ve Done So Far

We completed **6 deep projects** covering **multithreading and concurrency**, including:

| Folder               | Concept                           | Description |
|----------------------|------------------------------------|-------------|
| `CAProcessAndThread` | Process vs Thread vs Processor     | How to access and print process/thread/processor IDs |
| `CASequential`       | Sequential Code                    | Simulates wallet transactions without threading |
| `CAMultiThreading`   | Multiple Threads                   | Creates multiple threads, tracks their state and execution |
| `CARaceCondition`    | Race Conditions                    | Demonstrates unsafe memory access and how to fix with locks |
| `CADeadLock`         | Deadlocks and Solutions            | Shows deadlock and solves it using `Monitor.TryEnter` and ordered locking |
| `CAThreadPool`       | ThreadPool and Tasks               | Demonstrates how ThreadPool and `Task.Run()` execute work |

Each project contains:
- `Program.cs` → the code
- `README.md` → the explanation and diagrams or stories when needed

---

##  Folder Structure

```bash
CSharpTrickyTopics/
│
├── README.md               # This file
│
└── Threading/
    ├── CAProcessAndThread/
    ├── CASequential/
    ├── CAMultiThreading/
    ├── CARaceCondition/
    ├── CADeadLock/
    └── CAThreadPool/
