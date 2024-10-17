# Immutable Data Structures

# Immutable Data

## Definition
* no part of object can be changed after it's created

## Why use them?

- mutation is a common source of bugs
- immutable data are easier to reason about
  - value passed to a function, can't be changed
  - easier refactoring
- immutable data structures are **thread-safe**
- bonus: memory efficient time travelling

What's Immutable data? One of definitions is that no part of the object cannot be changed after it's creation. That gives us many advantages.

Mutation is common source of bugs. Procedures changing data are order dependent and harder to refactor.

Immutable data are easier to reason about. Value passed to a function can't be changed - big part of uncertainty disappear.

Maybe most important: immutable data is thread-safe, no race condition possible.

As a bonus we got the ability to store history of changes in memory efficient way.

# Why immutability - example

---

# Immutable update
MYTH: to "change" immutable value, you need to copy the whole thing

There is common misconception that for every small update of immutable data we need to copy all the data and then make the change. And that making immutable data slow and memory expensive. That's not true, we can use clever data structures that can share parts of the structure between old and new value. And also it turns out that performance penalty is not that big.

---

# How?
* we can share parts of the structure between old and new value
* **Structural sharing**

The trick is to use references inside the data structure to share parts of the structure.

In the rest of the talk I will show you two most common immutable data structures: linked list and set represented by balanced tree.

---

# (Linked) list

In linked list every item contains link to rest of the list.

We can insert new item only at front of list.

When we need to retrieve item at n-th position, we need to go through list.

---

## (Linked) list sharing

When we append new item to list, we just link to original list from new item. Old list reference is still valid.

List don't have explicit functions to change item, instead we can go through sublists and link them to some new item.
---

# List - update head

To change value of item, we create new item and link it to rest of list after original item.

To change value of item inside the list, we first split item into two parts, and join them with new item.

---

Git in principle is just linked list (with few more features) and branches are just named references.  

---

# List Benchmark

FsListWorkload compared to CsListWorkload

For benchmark, we create list of given size, filter out half of items and then sum all items.

We can see that immutable list is slower, but not much.

---

# Notes on Benchmarks

- hard and time expensive to write correct benchmarks
- there are always ways to make them faster
- at best they are only indicative
- all benchmarks are wrong

---

# Set
- unordered set of values

- typically implemented as a balanced tree (AVL)

Dictionary or Map are important data structure. We start by Set, Map is based on it.

Set is a structure that represent collection of unique items. It can answer if it contains some item. Duplicates are ignored. There is no ordering guarantee.

To enable structural sharing, we represent Set as tree. Unchanged subtrees are shared across instances.

The Set is typically represented as balanced binary tree (AVL tree).

---

## Insert = search + add

To insert item to Set, we try to search for it in tree. If item is not found, we add it to tree to proper location. Then the tree is re-balanced.

---

## Insert - structural sharing

All unchanged part of the tree is shared. Sharing is done on subtree level. When subtree is not changed , reference ti it remains the same and it is shared between old and new instance.

---

## Building new Set

Let's see how building new Set by inserting items one-by-one looks like.

---

# Set Benchmark
Immutable `Set` / mutable `HashSet`

Immutable Set is on par with mutable HashSet for contains operation. It's slower for creation. That's because of need to rebalancing tree during inserts.

---

# Map
- dictionary like immutable data structure
- like `Set`, but with value linked with each key (node)

---

## Map sharing

Values are linked to keys through references. That means that even if we changing keys, the (possibly big) values are shared.

---

# Map Benchmark

Immutable `Map` / mutable `Dictionary`

# Thank you!

---
