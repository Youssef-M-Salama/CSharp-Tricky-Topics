# 🔒 Deadlocks in C# and How to Solve Them

This project demonstrates how **deadlocks** can occur in multithreaded C# programs and presents two safe, real-world solutions:

- `Monitor.TryEnter(...)` — non-blocking lock attempt
- Ordered locking based on object `Id` — safe and structured lock order

---

## 🧠 What Is a Deadlock?

A **deadlock** happens when two or more threads each hold a lock, and all are waiting on each other to release another lock — none of them can proceed.

### 🧵 Code Scenario:

You have two wallets:

- 🟢 Wallet A (ID = 1)
- 🔵 Wallet B (ID = 2)

Two threads attempt to transfer funds:

- **Thread T1** wants to transfer A ➝ B
- **Thread T2** wants to transfer B ➝ A

#### ❌ Problem:
- T1: `lock(A)` → waits for `B`
- T2: `lock(B)` → waits for `A`
- Both threads **wait forever** ⇒ 🔒 **Deadlock**

---

## 🧨 Story: The Wallet Deadlock

In a digital bank system, we have two employees:

- **Ali**, owns Wallet A  
- **Sara**, owns Wallet B  

Now imagine:

1. Ali locks his own wallet and wants to lock Sara’s to finish a transfer.
2. At the *same time*, Sara locks her wallet and tries to lock Ali’s.

Both are **holding one lock and waiting on the other**.

🎯 Nobody will release their lock → both freeze → 💥 system stuck = **Deadlock**

> It’s like two people holding doors and saying:
> _“I won’t open mine until you open yours.”_  
> → But neither one gives in.

---

## 📖 Story of Solution 1 – `Monitor.TryEnter`

> "⏳ Withdraw if it's taking too long..."

After experiencing many system freezes due to deadlocks, the dev team came up with a new rule:

> _"If I can't get the second wallet lock in 1 second, I should cancel the transfer."_

This approach is safe.  
Each thread locks the `from` wallet, then tries to lock the `to` wallet **using `Monitor.TryEnter()` with a timeout**.  
If it fails to acquire the second lock, it simply logs a message and skips the transfer — no freezing, no crashing.

🎯 It’s like trying to open a second door — if it doesn't open in a second, you walk away and come back later.  
No one gets stuck inside.

---

## 📖 Story of Solution 2 – Ordered Locking

> "🔢 Always lock the lower ID first."

The development team realized they could prevent deadlocks **completely** by forcing a consistent lock order.

So they introduced a rule:

> _"Always lock the wallet with the smaller ID before the one with the larger ID."_  

No matter which direction the transfer goes (A ➝ B or B ➝ A), both threads **lock resources in the same order**.

🎯 This avoids circular waiting — if everyone follows the rule, deadlocks simply cannot happen.

Think of it like giving people numbers at a queue — whoever has the smaller number goes first, so no one blocks the other.


