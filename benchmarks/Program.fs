module ListBenchmarks

open System
open System.Linq
open System.Collections.Generic
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running

type MyRecord = { Id: int; Name: string }

type DU =
    | A of int
    | B of string

let inline test xs f =
    for i in xs do
        f i xs |> ignore
        
[<MemoryDiagnoser>]
type ListListTests() =
    [<Params(100, 1000, 10000, 100000)>]
    member val size = 100000 with get, set
    member this.listOfRecords =
        [ 1..this.size ] |> List.map (fun i -> { Id = i; Name = i.ToString() })
    member this.csList = Enumerable.Range(0, this.size).Select(fun i -> { Id = i; Name = i.ToString() }).ToList()
    
    [<Benchmark(Description = "F# List workload")>]
    member this.ListWorkload() =
        this.listOfRecords
        |> List.map (fun x -> { x with Id = x.Id + 1})
        |> List.filter (fun x -> x.Id % 2 = 0)
        |> List.map (fun x -> int64 x.Id)
        |> List.sum

    [<Benchmark(Baseline = true, Description = "C# List Workload")>]
    member this.CsListWorkload() =
        let csList = this.csList
        for i=0 to csList.Count - 1 do
            csList.[i] <-
              { csList.[i] with Id = csList.[i].Id + 1 }
        csList.RemoveAll(fun x -> x.Id % 2 <> 0)
        let x = csList.Sum(fun x -> int64 x.Id)
        x

[<MemoryDiagnoser>]
type SetSetTests() =
    let mutable setData = set []
    let mutable hashSetData = HashSet()

    [<Params(100, 1000, 10000, 100000)>]
    member val size = 100000 with get, set
    member this.Collection = seq { 1..this.size } |> Seq.map (fun i -> { Id = i; Name = i.ToString() })
    member this.ExtraCollection = seq { this.size+1..2*this.size } |> Seq.map (fun i -> { Id = i; Name = i.ToString() })
    
    member this.Setup() =
       setData <-
          this.Collection |> set
          |> Set.filter (fun x -> x.Id % 2 = 0)
       hashSetData <- this.Collection.ToHashSet()
       hashSetData.RemoveWhere(fun x -> x.Id % 2 <> 0)
    
    [<Benchmark(Description = "Set add + contains")>]
    member this.SetCreate() =
        let s =
          this.Collection
          |> Seq.filter (fun x -> x.Id % 2 = 0) |> set
        this.Collection |> Seq.iter (fun x -> Set.contains x s |> ignore)

    [<Benchmark(Baseline = true, Description = "HashSet add + Contains")>]
    member this.HashSetCreate() =
        let s = this.Collection.ToHashSet()
        s.RemoveWhere(fun x -> x.Id % 2 <> 0)
        for i in this.Collection do hashSetData.Contains(i)
        
    [<Benchmark(Description = "Set contains")>]
    member this.SetWorkload() =
        this.Collection |> Seq.iter (fun x -> Set.contains x setData |> ignore)

    [<Benchmark(Baseline = true, Description = "HashSet Contains")>]
    member this.HashSetWorkload() =
        for i in this.Collection do hashSetData.Contains(i)
        

[<MemoryDiagnoser>]
type MapDictTests() =
    let mutable mapData = Map.ofList []
    let mutable dictData = Dictionary()

    [<Params(100, 1000, 10000, 100000)>]
    member val size = 100000 with get, set
    member this.Collection = seq { 1..this.size } |> Seq.map (fun i -> { Id = i; Name = i.ToString() })
    
    member this.Setup() =
       mapData <-
          this.Collection
          |> Seq.filter (fun x -> x.Id % 2 = 0)
          |> Seq.map (fun x -> x.Id, x)
          |> Map.ofSeq
       dictData <- (this.Collection |> Seq.filter (fun x -> x.Id % 2 = 0)).ToDictionary(_.Id, id)
    
    [<Benchmark(Description = "Map add + contains")>]
    member this.MapCreate() =
        let s =
          this.Collection
          |> Seq.filter (fun x -> x.Id % 2 = 0)
          |> Seq.map (fun x -> x.Id, x)
          |> Map.ofSeq
        this.Collection |> Seq.iter (fun x -> Map.containsKey x.Id s |> ignore)

    [<Benchmark(Baseline=true, Description = "Dictionary add + Contains")>]
    member this.DictCreate() =
        let d = (this.Collection |> Seq.filter (fun x -> x.Id % 2 = 0)).ToDictionary(_.Id, id)
        for i in this.Collection do d.ContainsKey(i.Id)
        
    //[<Benchmark(Description = "Map containsKey")>]
    member this.MapContains() =
        this.Collection |> Seq.iter (fun x -> Map.containsKey x.Id mapData |> ignore)

    //[<Benchmark(Baseline = true, Description = "Dictionary Contains")>]
    member this.DictContains() =
        for i in this.Collection do dictData.ContainsKey(i.Id)


let runList() =
  let X = ListListTests()
  //for _ = 1 to 100 do X.CsListWorkload()
  assert (X.ListWorkload() = X.CsListWorkload())

let runSet() =
  let X = SetSetTests()
  for _ = 1 to 100 do
    X.SetWorkload()
    X.HashSetWorkload()

let runMap() =
  let X = MapDictTests()
  X.Setup()
  for _ = 1 to 100 do
    X.DictCreate()
    X.MapCreate()


//BenchmarkRunner.Run<ListListTests>()
//BenchmarkRunner.Run<SetSetTests>()
BenchmarkRunner.Run<MapDictTests>()


