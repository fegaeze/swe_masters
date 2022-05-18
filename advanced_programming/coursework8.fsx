(*
  Task 1: Pascal's triangle

             1
            1 1
           1 2 1
          1 3 3 1
         1 4 6 4 1
        ...........
       .............
      ............... 
  

  Define the function

    next : int list -> int list

  that, given a row of the triangle, computes the next row. The
  function List.windowed may be useful here.


  Define the sequence

    triangle : int list seq

  which consists of the rows of Pascal's triangle (represented as int
  list). Do not use sequence expressions. Define this using
  Seq.unfold.


  Define the function

    evens : int -> int list

  so that

    evens n

  evaluates to a list (of length n) consisting of the sums of elements
  in the first n rows of Pascal's triangle that have an even number of
  elements.

*)

let next (l1:int list) : int list = 
  let transformed = l1 |> List.windowed 2 |> List.map (fun xs -> xs |> List.sum)
  [1] @ transformed @ [1]

let triangle = [1] |> Seq.unfold (fun state -> Some (state, (next state)))

let evens (x:int) : int list = 
  triangle
  |> Seq.filter (fun x -> x.Length % 2 = 0)
  |> Seq.map (fun x -> x |> List.sum)
  |> Seq.take x
  |> Seq.toList

(*
  Task 2

  Define the function

    generate : 'a list -> ('a list -> 'a) -> 'a seq

  so that

    generate xs f

  evaluates to a sequence consisting of the elements in xs followed by
  elements computed by the function f.

  More precisely, if List.length xs = n, then s_i (the i-th element in
  the sequence) is

  * the i-th element of the list xs   if i < n
  * f [s_{i - n}; ... ; s_{i - 1}]     otherwise

  Note that f must be applied to lists of same length as xs.

  You may assume that xs is not empty.

  Define this using sequence expressions.

  Make sure that the calculation of an element of the sequence uses
  the function f at most once.

  The function Seq.cache may be useful here.

*)

let generate (l1:'a list) (fn:'a list -> 'a) : 'a seq =
  seq {
    for item in l1 -> item
    yield! l1 |> Seq.unfold (fun state -> 
      let elem = fn state
      Some (elem, ((state |> List.tail) @ [elem]))
    )
  }

(*
  Task 3: Longest common subsequence
  
  We have two arrays, xs and ys, and we wish to find the length of the
  longest common subsequence in these two arrays.
  
  Example:
  
  - xs = [| 1; 2; 3; 4 |]
  - ys = [| 5; 1; 6; 4 |]
  
  Length of the longest common subsequence is 2.
  
  This can be solved using dynamic programming.
  
  Let D be a two-dimensional array that holds the results of the
  subproblems:
  - D[i, j] is the length of the lcs of xs[0 .. i - 1] and ys[0 .. j - 1].
  
  Solving the subproblems:
  - if xs[i - 1] = ys[j - 1] then we follow one subproblem (shorten both sequences):
      D[i, j] = D[i - 1, j - 1] + 1
  
  - otherwise we take the maximum of two subproblems:
      D[i, j] = max D[i - 1, j] D[i, j - 1]
  
  - base cases:
      D[i, 0] = D[0, j] = 0
  
  
  Observation: it is not necessary to fill the entire table D to
  calculate D[i, j].
  
  If we decide to fill only those parts of the table that are necessary
  to compute D[i, j], then we need to be careful to not use the values
  in the unfilled parts in our calculation.
  
  However, we can use lazy values instead and let the runtime figure out
  which entries in the table and in which order need to be calculated.
  
  Define the function
  
    lcs : ((int * int) -> unit) -> 'a [] -> 'a [] -> Lazy<int> [,]
  
  so that
  
    lcs m xs ys
  
  evaluates to the table D for xs and ys except that the entries in the
  table are lazy. An entry in the table is computed only when we ask for
  the value (of the Lazy<int>) or the computation of another entry
  requires the value of this entry.
  
  The function m must be applied to (i, j) when the entry D[i, j] is
  actually computed. For example, you can use printfn as m to make the
  order of calculations visible.

*)

let lcs (m: (int * int) -> unit) (xs: 'a []) (ys: 'a []) : Lazy<int> [,] =
  let fstlength = (xs |> Array.length) + 1
  let sndlength = (ys |> Array.length) + 1
  let D: Lazy<int> [,] = Array2D.create fstlength sndlength (lazy 0)

  Array2D.init fstlength sndlength (fun i j -> 
    match i, j with
      | 0, _ 
      | _, 0 -> 
        D.[i,j] <- lazy(m (i,j); 0)
        D.[i,j]
      | _ when xs.[i - 1] = ys.[j - 1] -> 
        let curr = D.[i - 1, j - 1]
        D.[i,j] <- lazy(m (i,j); curr.Value + 1)
        D.[i,j]
      | _ -> 
        let up = D.[i - 1, j]
        let left = D.[i, j - 1]
        D.[i,j] <- lazy(m (i,j); max up.Value left.Value)
        D.[i,j]
  )

(*
  Task 4:

  A function from a type 'env to a type 'a can be seen as a computation that
  computes a value of type 'a based on an environment of type 'env. We call such
  a computation a reader computation, since compared to ordinary computations,
  it can read the given environment. Below you find the following:

    • the definition of a builder that lets you express reader computations
      using computation expressions

    • the definition of a reader computation ask : 'env -> 'env that returns the
      environment

    • the definition of a function runReader : ('env -> 'a) -> 'env -> 'a that
      runs a reader computation on a given environment

    • the definition of a type Expr of arithmetic expressions

  Implement a function eval : Expr -> Map<string, int> -> int that evaluates
  an expression using an environment which maps identifiers to values.
  
  NB! Use computation expressions for reader computations in your implementation.
  
  Note that partially applying eval to just an expression will yield a function of
  type map <string, int> -> int, which can be considered a reader computation.
  This observation is the key to using computation expressions.

  The expressions are a simplified subset based on
  Section 18.2.1 of the F# 4.1 specification:
  https://fsharp.org/specs/language-spec/4.1/FSharpSpec-4.1-latest.pdf

*)

type ReaderBuilder () =
  member this.Bind   (reader, f) = fun env -> f (reader env) env
  member this.Return x           = fun _   -> x

let reader = new ReaderBuilder ()

let ask = id

let runReader = (<|)

type Expr =
  | Const  of int          // constant
  | Ident  of string       // identifier
  | Neg    of Expr         // unary negation, e.g. -1
  | Sum    of Expr * Expr  // sum 
  | Diff   of Expr * Expr  // difference
  | Prod   of Expr * Expr  // product
  | Div    of Expr * Expr  // division
  | DivRem of Expr * Expr  // division remainder as in 1 % 2 = 1
  | Let    of string * Expr * Expr // let expression, the string is the identifier.

// //Example:
// //keeping in mind the expression: let a = 5 in (a + 1) * 6
// let expr = Let ("a",Const 5, Prod(Sum(Ident("a"),Const 1),Const 6))
// eval expr Map.empty<string,int>
// should return 36    

let rec eval (e:Expr) : (Map<string, int> -> int) = 
  reader {
    match e with
    | Const num -> return num
    | Ident idx ->
      let! env = ask
      return (Map.find idx env)
    | Neg e ->
      let! env = ask
      return - (eval e env)
    | Sum (e1, e2) -> 
      let! env = ask
      return (eval e1 env) + (eval e2 env)
    | Diff (e1, e2) ->
      let! env = ask
      return (eval e1 env) - (eval e2 env)
    | Prod (e1, e2) ->
      let! env = ask
      return (eval e1 env) * (eval e2 env)
    | Div (e1, e2) -> 
      let! env = ask
      return (eval e1 env) / (eval e2 env)
    | DivRem (e1, e2) ->
      let! env = ask
      return (eval e1 env) % (eval e2 env)
    | Let (idx, e1, e2) ->
      let! env = ask
      let fst = eval e1 env
      return eval e2 (Map.add idx fst env)
  }