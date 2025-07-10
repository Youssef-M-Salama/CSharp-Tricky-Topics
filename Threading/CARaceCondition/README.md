# 🛡️ Race Conditions & Locking in C#

This example demonstrates:
- How race conditions occur
- How to **prevent them using `lock`**
- Importance of synchronizing access to shared data

---

## 💥 The Problem

If two threads **access and modify** shared data (like `Bitcoins`) at the same time **without control**, they can interfere with each other.

Here, two threads try to debit:
```csharp
wallet.Debit(40); // May succeed
wallet.Debit(50); // Should fail if 40 was debited first
