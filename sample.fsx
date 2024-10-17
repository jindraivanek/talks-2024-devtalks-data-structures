type Account = { Money: decimal } // immutable data
let pay (account: Account) amount = { account with Money = account.Money - amount }

let account = { Money = 1000 }
[ 0 .. 10 ] |> List.map (fun i -> async { return pay account 100 }) 
|> Async.Parallel |> Async.RunSynchronously // 900 for all results