# Lab 3 - Hash Tables and Fast Lookup

## What this lab is for

This lab explores how a key can guide us toward a small part of the stored data instead of scanning everything.

You will compare:

- a working linear ID lookup;
- a small integer-key hash table with separate chaining;
- standard `Dictionary<TKey,TValue>` / `HashSet<T>` as the production tools you would normally reach for.

The goal is **not** to recreate the full .NET `Dictionary` implementation. The goal is to understand buckets, collisions, lookup behavior, and trade-offs.

This lab belongs in your existing **`gp26-dsa-labs`** repository. It is separate from your individual Frontier Dispatch assignment repository.

## Core path

1. Inspect the working `LinearIdMap<TValue>`.
2. Trace the bucket function by hand.
3. Implement `Set`, `TryGetValue`, and `ContainsKey` in `IntHashTable<TValue>`.
4. Make the collision tests pass.
5. Add one meaningful test.
6. Explain when you would use a standard `Dictionary` or `HashSet` in real game code.

## Run the lab

From the course repository root:

```bash
dotnet restore labs/03-hashing/DSA.Lab03.sln
dotnet build labs/03-hashing/DSA.Lab03.sln
dotnet run --project labs/03-hashing/src/DSA.Lab03.App
dotnet test labs/03-hashing/tests/DSA.Lab03.Tests
```

If you are copying the lab into your own `gp26-dsa-labs` repository, run the equivalent commands from the location where you placed Lab 3.

## Task 1 - Inspect linear lookup

`LinearIdMap<TValue>` stores entries in one `List<Entry<TValue>>` and scans them during lookup.

Ask:

- What is the worst case when the key is missing?
- What changes when the collection grows from 100 entries to 100,000?
- What operation are we trying to improve?
- What repeated work are we doing when many lookups happen?

The important idea is not "List bad, hash table good". The important question is:

> Which operation happens often enough that preparing another representation might be worth the cost?

## Task 2 - Understand buckets

The supplied hash table uses positive integer keys and:

```csharp
bucketIndex = key % bucketCount;
```

With five buckets, calculate the bucket for:

```text
101, 205, 734, 206, 999
```

Which keys collide?

A collision is **not an error**. It means several keys map to the same bucket and the table needs a way to distinguish them inside that bucket.

Hashing gets us to a smaller part of the stored data. We still need to compare keys inside that bucket before we know whether we have found the requested entry.

## Task 3 - Implement `IntHashTable<TValue>`

This lab uses **separate chaining**: every bucket contains a small list of entries.

Implement:

- `Set(int key, TValue value)` - insert a new key or replace its existing value;
- `TryGetValue(int key, out TValue value)`;
- `ContainsKey(int key)`.

The bucket function is already supplied. Keep the implementation small and readable.

While implementing, think about the operations rather than only the syntax:

- Which bucket should be inspected?
- How do you know whether the key already exists?
- What should happen when two different keys share a bucket?
- When should `Count` change?

## Task 4 - Reason about collisions

Run the collision tests and answer:

1. Why can keys 1 and 6 both exist when the table has five buckets?
2. What happens to lookup cost if almost every key ends up in the same bucket?
3. Why does expected O(1) lookup not mean "exactly one CPU operation"?
4. Why must the original key still be stored even after we have calculated its bucket?

A hash table is fast when keys are distributed reasonably well. In the worst case, a badly distributed table can begin to behave much more like a linear scan.

## Task 5 - Add one meaningful test

Examples:

- replacing an existing value does not increase `Count`;
- several keys collide in the same bucket;
- a missing colliding key still returns false;
- zero as a key;
- another case you can justify.

Choose a test because it protects some behavior or edge case, not just because it adds another test number.

## Standard collections

After completing the lab, compare your implementation with:

```csharp
Dictionary<int, TValue>
HashSet<int>
```

A `Dictionary<TKey,TValue>` answers questions such as:

> Given this key, what value belongs to it?

A `HashSet<T>` answers questions such as:

> Is this value already present?

In real application/game code, prefer the standard library unless you have a specific reason not to. We build a small hash table here because seeing the representation makes dictionaries and sets easier to reason about.

## Frontier Dispatch Unity bridge

The **Frontier Dispatch course/reference Unity project** contains a **Hash Lookup** scene with entities identified by integer IDs.

The scene lets you compare a visible linear scan with a prepared dictionary lookup and connect the abstract bucket/lookup discussion to a game-like example.

The **World Map** scene also contains ID-based world queries that we discussed in Wednesday's structure-selection workshop.

These scenes are an application/visualization bridge. You do **not** need to modify the Frontier Dispatch Unity project to complete Lab 3.

Completing the headless C# lab is sufficient for the course exercise.

## Assignment bridge

The individual Frontier Dispatch assignment contains repeated operations such as finding locations by ID and checking whether something has already been seen or processed.

The ideas from this lab therefore connect directly to the assignment's **representation phase**.

What transfers:

- recognizing when repeated linear scans may become expensive;
- using a key to prepare faster lookup;
- understanding `Dictionary` for key-to-value lookup;
- understanding `HashSet` for membership;
- reasoning about preparation cost, runtime cost, memory use and updates;
- testing whether the new representation still gives correct results.

What does **not** transfer automatically:

- "use a `Dictionary` everywhere";
- replacing every `List`;
- assuming expected O(1) means a change is always worthwhile;
- treating the Lab 3 implementation as the assignment solution.

In the assignment, your job is to identify the important operations in the supplied baseline, decide which representation changes are justified, measure their effect, and explain the trade-offs.

Do not copy the learning hash table into the assignment simply because you implemented one here. In normal application code, a standard `Dictionary` or `HashSet` will usually be the appropriate tool unless you have a specific reason to implement something yourself.
