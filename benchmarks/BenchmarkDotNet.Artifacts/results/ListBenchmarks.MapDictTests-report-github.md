```

BenchmarkDotNet v0.13.12, Ubuntu 22.04.5 LTS (Jammy Jellyfish)
12th Gen Intel Core i7-12800H, 1 CPU, 20 logical and 14 physical cores
.NET SDK 8.0.402
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2 DEBUG
  DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT AVX2


```
| Method                      | size   | Mean          | Error       | StdDev      | Median        | Ratio | RatioSD | Gen0      | Gen1      | Gen2     | Allocated   | Alloc Ratio |
|---------------------------- |------- |--------------:|------------:|------------:|--------------:|------:|--------:|----------:|----------:|---------:|------------:|------------:|
| **&#39;Map add + contains&#39;**        | **100**    |      **7.410 μs** |   **0.1438 μs** |   **0.2239 μs** |      **7.313 μs** |  **2.07** |    **0.06** |    **1.7853** |    **0.0153** |        **-** |    **21.98 KB** |        **1.92** |
| &#39;Dictionary add + Contains&#39; | 100    |      3.652 μs |   0.0559 μs |   0.0549 μs |      3.635 μs |  1.00 |    0.00 |    0.9308 |    0.0153 |        - |    11.44 KB |        1.00 |
|                             |        |               |             |             |               |       |         |           |           |          |             |             |
| **&#39;Map add + contains&#39;**        | **1000**   |    **126.519 μs** |   **2.5248 μs** |   **4.6799 μs** |    **124.998 μs** |  **2.98** |    **0.14** |   **27.7100** |    **4.2725** |        **-** |   **339.69 KB** |        **2.21** |
| &#39;Dictionary add + Contains&#39; | 1000   |     42.333 μs |   0.8268 μs |   1.6320 μs |     41.725 μs |  1.00 |    0.00 |   12.5122 |    2.4414 |        - |   153.95 KB |        1.00 |
|                             |        |               |             |             |               |       |         |           |           |          |             |             |
| **&#39;Map add + contains&#39;**        | **10000**  |  **1,861.278 μs** |  **30.6749 μs** |  **28.6933 μs** |  **1,849.042 μs** |  **1.79** |    **0.08** |  **355.4688** |  **273.4375** |        **-** |  **4361.94 KB** |        **2.61** |
| &#39;Dictionary add + Contains&#39; | 10000  |  1,003.619 μs |  20.0681 μs |  40.5386 μs |    987.254 μs |  1.00 |    0.00 |  179.6875 |   89.8438 |  89.8438 |  1672.89 KB |        1.00 |
|                             |        |               |             |             |               |       |         |           |           |          |             |             |
| **&#39;Map add + contains&#39;**        | **100000** | **33,820.253 μs** | **624.5721 μs** | **767.0307 μs** | **33,652.125 μs** |  **2.19** |    **0.07** | **4437.5000** | **1000.0000** | **437.5000** | **51595.74 KB** |        **3.13** |
| &#39;Dictionary add + Contains&#39; | 100000 | 15,288.581 μs | 299.2031 μs | 483.1572 μs | 15,180.148 μs |  1.00 |    0.00 | 2015.6250 | 1765.6250 | 968.7500 | 16459.27 KB |        1.00 |
