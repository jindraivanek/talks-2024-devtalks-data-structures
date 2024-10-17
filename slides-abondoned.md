---

## Insert - structural sharing

```fsharp
let s2 = s |> Set.add 35
```

```mermaid
graph TD
classDef cluster fill:#efefea
41(("41"))
20(("20"))
11(("11"))
29(("29"))
32(("32"))
65(("65"))
50(("50"))
91(("91"))
72(("72"))
99(("99"))

subgraph sub_s["s"]
s[s] --> 41
41 --> 20
41 --> 65
20 --> 11
20 --> 29
29 --> 32
65 --> 50
65 --> 91
91 --> 72
91 --> 99
end

style 41 fill: orange
style 20 fill: orange
style 29 fill: orange
style 35 fill: red

41b(("41"))
20b(("20"))
32b(("32"))
29b(("29"))
35(("35"))

subgraph sub_s2["s2"]
s2[s2] --> 41b
41b --> 20b
41b --> 65
20b --> 32b
20b --> 11
32b --> 29b
32b --> 35
end
```

---

## Map sharing

```fsharp
let mapA = Map.ofList [1, "A"; 2, "B"; 3, "C"; 4, "D"; 5, "E"; 6, "F"; 7, "G"]
let mapB = Map.add 8 "H" mapA
```
<Transform :scale="0.7">

```mermaid
graph TD
classDef cluster fill:#efefea
subgraph smapA["mapA"]
mapA("mapA") --> 4
1(("1"))
2(("2"))
3(("3"))
4(("4"))
5(("5"))
6(("6"))
7(("7"))
2 --> 1
2 --> 3
4 --> 2
4 --> 6
6 --> 5
6 --> 7
subgraph values
A
B
C
D
E
F
G
end
1 --- A
2 --- B
3 --- C
4 --- D
5 --- E
6 --- F
7 --- G
end
subgraph smapB["mapB"]
4b(("4"))
6b(("6"))
7b(("7"))
8b(("8"))
4b --> 2
4b --> 6b
6b --> 5
6b --> 7b
7b --> 8b
mapB("mapB") --> 4b
subgraph values-MapB
H
end
end

4b --- D
6b --- F
7b --- G
8b --- H
```

</Transform>
