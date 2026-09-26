# Week 1 — C# + OOP + Coding Drill

## Setup checklist

- [x] .NET SDK installed (`dotnet --version` — this machine has 10.x; projects target `net10.0`)
- [ ] VS Code extension: **C# Dev Kit** (search in Extensions)
- [ ] Optional: Docker Desktop/daemon for SQL Server in Week 2–3 (SQLite works without it)

```bash
# From repo root
dotnet run --project week1/BankAccount
dotnet run --project week1/LibrarySystem
dotnet run --project week1/StudentGrades
dotnet run --project week1/CodingDrill
dotnet run --project week1/CodingDrill -- 1
```

## Daily rule (90 min, no AI)

1. 2–3 easy problems in `CodingDrill/Problems.cs` stubs
2. Rebuild yesterday's class from memory (blank file)
3. Only after 20+ min: peek `Solutions.cs`, close it, rewrite

## OOP interview answers (say out loud)

| Pillar | One-liner | Where in BankAccount |
|--------|-----------|----------------------|
| Encapsulation | Hide data behind methods | `Balance` private set; `Deposit`/`Withdraw` |
| Abstraction | Expose contract, hide details | `IBankAccount`, `IAccountService` |
| Inheritance | Child reuses parent | `SavingsAccount : BankAccountBase` |
| Polymorphism | Same call, different behavior | `Withdraw` override on `CheckingAccount` |

**Interface vs abstract class:** interface = pure contract (can implement many). Abstract class = shared state + partial implementation (single inheritance).

## Rebuild-from-memory challenge

Close all files. Create `Scratch/` and rewrite:

1. Day 2: `IBankAccount` + `SavingsAccount` only
2. Day 4: Library `Borrow`/`Return`
3. Day 6: GradeBook `GroupBy` averages with LINQ

## CCAT (every other day)

- 50 questions / 15 minutes — you will not finish
- Aim: accuracy on first ~30
- Practice: search "CCAT practice test free" / JobTestPrep sample
- Log score in `progress/progress.md`

## Problem list (15)

1 TwoSum  2 ReverseString  3 IsPalindrome  4 FindMax  5 RemoveDuplicates  
6 MoveZeroes  7 IsAnagram  8 FirstUniqChar  9 MergeSorted  10 ContainsDuplicate  
11 LongestCommonPrefix  12 Rotate  13 CountVowels  14 Intersection  15 PlusOne
