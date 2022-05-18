(*

For introduction to FSharpON please check coursework4.fsx for references.

In this coursework we continue with the topic of trees and object notation. This
time the task is, given a description of a set of values in an Ecma,
how to retrieve, modify and delete those values. This is a bit similar
to questions 6 and 7 in coursework 4.

The following material of basic definitions is taken from CW4. You are
free to use your solution to CW4 or try something different.

*)






type Name = string


// 0. Define data structure(s) for representing FsharpON (same as in CW4)

type Ecma =
  | Null
  | Bool of bool
  | Num of float
  | Str of string
  | Arr of Ecma list
  | Obj of (Name * Ecma) list

(*
Define the function

  mkObject : unit -> Ecma

that creates a representation for an empty object structure.
*)

let mkObject (_:unit) : Ecma = Obj []

(*
Define the function

  mkNumber : float -> Ecma

that creates a representation for the given floating-point number.
*)

let mkNumber (f:float) : Ecma = Num f

(*
Define the function

  mkBool : bool -> Ecma

that creates a representation for the given Boolean value.
*)

let mkBool = function
  | true -> Bool true
  | false -> Bool false

(*
Define the function

  mkString : string -> Ecma

that creates a representation for the given string value.
*)

let mkString (s:string) : Ecma = Str s

(*
Define the function

 mkArray : Ecma list -> Ecma

that creates a representation for an array whose elements are
represented by the given list of `Ecma` values.
*)

let mkArray (e:Ecma list) : Ecma = 
  match e with
  | [] -> Arr []
  | e  -> Arr e  

(*
Define the function

  mkNull : unit -> Ecma

that creates a representation of the ECMA-404 `null` value.
*)

let mkNull (_:unit) : Ecma = Null

// Define the function
//
//   addNameValue : Name * Ecma -> Ecma -> Ecma
//
// so that
//
//   addNameValue (n, v) e
//
// evaluates to an ECMA representation e' that is:
// - equal to e if e is not an object representation
//
// - a representation for the object e extended with the name-value
//   pair (n, v), otherwise.

let addNameValue ((x,y):Name * Ecma) (e:Ecma) : Ecma =
  match e with
  | Obj [] -> Obj [(x,y)]
  | Obj e ->  Obj (e @ [(x,y)])
  | _ -> e

// Define the function
//
//   addValue : Ecma -> Ecma -> Ecma
//
// so that
//
//   addValue v e
//
// evaluates to an ECMA representation e' that is:
// - equal to e if e is not an array representation
//
// - a representation for the array e with the value v added as the last
//   element, otherwise.

let addValue (e1:Ecma) (e2:Ecma) : Ecma =
  match e2 with
  | Arr [] -> Arr [e1]
  | Arr e -> Arr (e @ [e1])
  | _ -> e2
  

////////////////////////////////////////////////////////////////////////
// The content of this coursework begins here //////////////////////////


// You are given a type of expressions.

type BExpr = True
           | Not      of BExpr
           | And      of BExpr * BExpr
           | Or       of BExpr * BExpr
           | HasKey   of Name
           | HasStringValue of string
           | HasNumericValueInRange of (float*float)
           | HasBoolValue of bool
           | HasNull

(*

The type BExpr is just a discriminated union. The intended
interpretation of values of type BExpr is as predicates on values of
type Ecma.

 - True: evaluates to true on any Ecma

 - Not b: evaluates to true on precisely those Ecma for which b
          evaluates to false

 - And (b1, b2): evaluates to true on precisely those Ecma for which
                 both b1 and b2 evaluate to true

 - Or (b1, b2): evaluates to true on precisely those Ecma for which at least
                one of b1 and b2 evaluates to true

 - HasKey k: evaluates to true on precisely those Ecma that are objects and
             that contain a key k.

 - HasStringValue s: evaluates to true on precisely those Ecma that are 
            either Ecma strings with value s, objects that contain a value s,
            or arrays that contain a value s.

 - HasNumericValueInRange (xmin,xmax): evaluates to true on precisely those Ecma
               that either are
               numeric Ecma with value in closed range xmin,xmax,
               objects with a numeric value in closed range xmin,xmax,
               arrays with a numeric value in closed range xmin,xmax.

  - HasBoolValue b: evaluates to true on precisely those Ecma that are either
                    Boolean Ecma with value b,
                    objects that contain a Boolean value b,
                    arrays that contain a Boolean value b.
  - HasNull : evaluates to true on precisely those Ecma that are either
                    null Ecmas,
                    objects that conitain a null value,
                    arrays that contain a null value.

*)



// Here is a type of selector expressions.

type Selector = Match     of BExpr
              | Sequence  of Selector * Selector
              | OneOrMore of Selector


(*

The type Selector is just a discriminated union. The intended
interpretation of values of type Selector on values of type Ecma is as
sets of values in that Ecma. We also refer to the set of values
described by s : Selector as the set of values selected by s.

 - Match b: the singleton set consisting of the root value if the
            expression b evaluates to true and the empty set otherwise.
 
 - Sequence (s, s'): the set consisting of those values in the Ecma tree
                     that are selected by the selector s' starting from
                     any child value of a value that is selected by the
                     selector s (starting from the root value).

                     In other words, first determine the set of values
                     selected by s (starting from the root value). For
                     every child c of a value in this set, determine
                     the set of values selected by s' (starting from c)
                     and take the union of such sets as the result.

                     In other words, start from the root value with the
                     selector s. For the values that it selects,
                     continue with selector s' from their child values
                     and collect the results.
 
 - OneOrMore s: select the values selected by the selector s and, in
                addition, from the child values of the values in this
                set select the values selected by OneOrMore s.
 
                Thus, you can think of the values selected by OneOrMore s
                as the union of the following sets:
                - values selected by s
                - values selected by Sequence (s, OneOrMore s)
*)




// 1. Translate the following informal descriptions into values of
// type BExpr and Selector.
// 
// Define the values b1, b2 and b3 of type BExpr so that:
// 
//  - b1 evaluates to true on those Ecma that are object values
//    containing the keys "blue" and "left" but do not have the key "red".
// 
//  - b2 evaluates to true on those Ecma that are numeric values with
//    the value in the range [-5, 5).
// 
//  - b3 evaluates to true on those Ecma that have the string value "b3"
//    or that are object values which have the key "b3".
//
// Define the values s1, s2 and s3 of type Selector so that:
// 
//  - s1 selects all object values with key "abc" that are at depth 3
// 
//  - s2 selects all values v such that v is a child of some value
//    and all of the ancestors of v have the string value "xyz"
// 
//  - s3 selects all values v such that:
//    * v is a child of a value t
//    * t does not have a string value "xyz"
//    * t is the root value
// 
// We consider the root value to be at depth 1.

let b1 = And (And (HasKey "blue", HasKey "left"), Not (HasKey "red"))
let b2 = HasNumericValueInRange (-5.0, 4.999999999)
let b3 = Or (HasStringValue "b3", HasKey "b3")

let s1 = Sequence (Match True, Sequence(Match True, Match (HasKey "abc")))
let s2 = Sequence (OneOrMore (Match (HasStringValue "xyz")), Match True)
let s3 = Sequence (Match (Not (HasStringValue "xyz")), Match True)

// 2. Define the function
//
// eval : BExpr -> Ecma -> bool
//
// which evaluates the given expression on the given Ecma.
//
// Evaluating a BExpr only considers properties of the root value of
// the Ecma and its immediate child values that are leaves (if there are any).
//
// In other words, for any  b : BExpr  and  e : Ecma
//
//    eval b e = eval b e'
//
// where e' is e with all of its non-leaf child values replaced
// with the representation for null.

let checkIfString (ecma:Ecma) (s:string) : bool =
  match ecma with
  | Str s' when s' = s -> true
  | Arr el -> el |> List.exists (fun x -> x = Str s)
  | Obj el -> el |> List.exists (fun (_,y) -> y = Str s) 
  | _ -> false

let checkIfNum (ecma:Ecma) ((f1, f2):float * float) : bool =
  match ecma with
  | Num e when (f1 <= e) && (e <= f2) -> true
  | Obj el -> el |> List.exists (fun (_,y) ->
    match y with
    | Num e when (f1 <= e) && (e <= f2) -> true
    | _ -> false)
  | Arr el -> el |> List.exists (fun x -> 
    match x with
    | Num e when (f1 <= e) && (e <= f2) -> true
    | _ -> false)
  | _ -> false

let checkIfBool (ecma:Ecma) (b:bool) : bool =
  match ecma, b with
  | Bool true, true -> true
  | Bool false, false -> true
  | Arr el, true -> el |> List.exists (fun x -> x = Bool true)
  | Arr el, false -> el |> List.exists (fun x -> x = Bool false)
  | Obj el, true -> el |> List.exists (fun (_,y) -> y = Bool true)
  | Obj el, false -> el |> List.exists (fun (_,y) -> y = Bool false)
  | _ -> false

let checkIfNull (ecma:Ecma) : bool =
  match ecma with
  | Null -> true
  | Obj el -> el |> List.exists (fun (_,y) -> y = Null)
  | Arr el -> el |> List.exists (fun x -> x = Null)
  | _ -> false

let rec eval (exp:BExpr) (ec:Ecma) : bool =
  match exp, ec with
  | True, _ -> true
  | Not bExpr, ecma -> not (eval bExpr ecma)
  | And (bExpr1, bExpr2), ecma -> (eval bExpr1 ecma) && (eval bExpr2 ecma)
  | Or (bExpr1, bExpr2), ecma -> (eval bExpr1 ecma) || (eval bExpr2 ecma)
  | HasKey n, Obj el -> el |> List.exists (fun (x,y) -> x = n)
  | HasStringValue s, ecma -> checkIfString ecma s
  | HasNumericValueInRange (f1,f2), ecma -> checkIfNum ecma (f1,f2)
  | HasBoolValue b, ecma -> checkIfBool ecma b
  | HasNull, ecma -> checkIfNull ecma
  | _ -> false


type Description = Key   of string
                 | Index of int

type Path = Description list


// 3. Define the function
//
// select : Selector -> Ecma -> (Path * Ecma) list
//
// that computes the set of values in the given Ecma described by the
// given Selector. The result is a list of pairs where the second
// component is a selected value and the first component is the full path
// to this value.
//
// The path to the root value is the empty list.
//
// If you follow a child value of an object, then you add the key of
// that value to the path. If you follow a child of an array, then you
// add the index of that value in the array to the path (the oldest value
// has index 0).
//
// The order of values in the result list must respect the order of values
// in the given Ecma. More precisely, in the result list:
// - a value must appear before any of its children
// - a value must not appear before its older siblings and their
//   descendants
//
// This task is similar to evaluating a BExpr on an Ecma. The difference is
// that instead of a BExpr we have a Selector and instead of a bool we
// compute a (Path * Ecma) list. In this case we also consider child
// values.

let rec select (sel:Selector) (ecma:Ecma) : (Path * Ecma) list =
  match sel with
  | Match b when (eval b ecma) -> [([], ecma)]
  | Sequence (s, s') -> (select s ecma) |> List.collect (fun (path,ecma1) -> 
    match ecma1 with
    | Arr el -> el |> List.indexed |> List.collect (fun (n, x) -> (select s' x) |> List.map (fun (path1,ecma2) -> ((path @ [Index n] @ path1), ecma2)))
    | Obj el -> el |> List.collect (fun (x,y) -> (select s' y) |> List.map (fun (path1,ecma1) -> ((path @ [Key x] @ path1), ecma1)))
    | _ -> [])
  | OneOrMore (OneOrMore s) 
  | OneOrMore s -> select s ecma @ select (Sequence (s, OneOrMore s)) ecma
  | _ -> []

// 4. Define the function
//
// update :  (string -> string)
//        -> (float  -> float)
//        -> Selector
//        -> Ecma
//        -> Ecma
//
// such that
//
//    update su nu s e
//
// evaluates to an Ecma that is otherwise the same as e except that,
// for the values selected by s, the string values and numeric values
// of that value have been updated according to the functions su and nu.

let rec iterate (n : int) (f : 'a -> 'a) : 'a -> 'a =
  if n <= 0
  then id
  else f << iterate (n - 1) f

let update (su:string -> string) (nu:float  -> float) (sel:Selector) (ecma:Ecma) : Ecma =
  let rec _update su nu selVals ecma count =
    match ecma with
    | Obj el as obj when selVals |> List.contains obj -> Obj (el |> List.map (fun (name,ecma1) -> (name, _innerUpdate su nu selVals ecma1 (count + 1))))
    | Arr el as arr when selVals |> List.contains arr -> Arr (el |> List.map (fun ecma1 -> _innerUpdate su nu selVals ecma1 (count + 1)))
    | Obj el -> Obj (el |> List.map (fun (name,ecma1) -> (name, _update su nu selVals ecma1 count)))
    | Arr el -> Arr (el |> List.map (fun ecma1 -> _update su nu selVals ecma1 count))
    | Num e as num when selVals |> List.contains num -> Num (nu e)
    | Str e as str when selVals |> List.contains str -> Str (su e)
    | _  -> ecma
    
  and _innerUpdate su nu selVals ecma count =
    match ecma with
    | Num e as num when not (selVals |> List.contains num) -> Num (iterate count nu e)
    | Str e as str when not (selVals |> List.contains str) -> Str (iterate count su e)
    | Num e as num -> Num (iterate (count + 1) nu e)
    | Str e as str -> Str (iterate (count + 1) su e)
    | _ -> _update su nu selVals ecma (count - 1)

  _update su nu ((select sel ecma) |> List.map snd) ecma 0

// 5. Define the function
//
// delete : Selector -> Ecma -> Ecma option
//
// which removes from the given Ecma all values that are selected by
// the given Selector. Removing a value means removing the entire
// subtree rooted at that value.
//
// The result should be `None` when after the delete operation there
// is no `Ecma` value left. Otherwise use `Some`.

let rec traverse (path:Path) (ecma:Ecma) (ecmalist: Ecma list) : Ecma = 
  match ecma with 
  | Obj el -> Obj (el |> List.collect(fun (n, v) -> 
    match path with
    | [h] when (ecmalist |> List.contains v) -> []
    | h::t when (compare (Key n) h) = 0 -> [(n, traverse t v ecmalist)]
    | _ -> [(n, v)]))
  | Arr el -> Arr (el |> List.indexed |> List.collect (fun (n, v) -> 
    match path with
    | [h] when (ecmalist |> List.contains v) -> []
    | h::t when (compare (Index n) h) = 0 -> [traverse t v ecmalist]
    | _ -> [v]))
  | _ -> ecma

let delete (sel:Selector) (ecma:Ecma) : Ecma option =  
  let pathList = (select sel ecma) |> List.map fst
  let ecmaList = (select sel ecma) |> List.map snd

  if (ecmaList |> List.contains ecma) then None
  else Some (pathList |> List.fold (fun state item -> traverse item state ecmaList) ecma)

// 6. Using the function update, define the functions
//
//   toZero : float -> Selector -> Ecma -> Ecma
//
// and
//
//   truncate : int -> Selector -> Ecma -> Ecma
//
// so that
//
//   toZero x s e
//
// evaluates to an Ecma that is otherwise the same as e except that the
// values selected by s have each of their numeric values y replaced by 0
// if y is in the range [-x, x].
//
//   truncate n s e
//
// evaluates to an Ecma that is otherwise the same as e except that the
// values selected by s have each of their string values y truncated to
// length n.
//
// These functions should not be defined recursively; define them in
// terms of update.

let toZero (f:float) (sel:Selector) (ecma:Ecma) : Ecma = 
  let changNum (f1:float) (f2:float) = if (-(f1) < f2) && (f2 < f1) then 0.0 else f2
  update id (changNum f) sel ecma

let truncate (n:int) (sel:Selector) (ecma:Ecma) : Ecma = 
  let changStr (n:int) (str:string) =
    match str with
    | null -> null
    | _ -> str.[0..(n-1)]

  update (changStr n) id sel ecma

// 7. Using the function update, define the function
// 
//   mapEcma : (string -> string) -> (float -> float) -> Ecma -> Ecma
//
// such that
//
//   mapEcma f g e
//
// evaluates to an Ecma obtained by updating every value in the
// given Ecma value according to f and g.
//
// This function should not be defined recursively; define it in
// terms of update.

let mapEcma (strFun:(string -> string)) (numFun:(float -> float)) (ecma:Ecma) : Ecma = 
  update strFun numFun (OneOrMore (Match True)) ecma
  
