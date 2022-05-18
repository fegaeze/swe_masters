(*

The ECMA-404 standard specifies a textual syntax for structured data
interchange.

The specification is available here:
https://www.ecma-international.org/wp-content/uploads/ECMA-404_2nd_edition_december_2017.pdf

The goal of this coursework is to develop a partial implementation of
this specification. In particular, our first goal is to define in F#
the datatype(s) suitable for representing the abstract syntax tree of
this data interchange format. The second goal is to define some
operations on this representation.

*)

// We have the following type alias.

type Name = string

//// Task 1 ////

// Define the type `Ecma` for representing the possible values of the
// ECMA-404 interchange format.
//
// The type must satisfy the constraint `equality`.
type Ecma =
  | Num of float
  | Str of string
  | Arr of Ecma list
  | True | False | Null
  | Obj of (Name * Ecma) list

// Define the following functions for creating ECMA-404
// representations of the given data.

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
  | true -> True
  | false -> False

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

//// Task 2 ////

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

//// Task 3 ////

// Define the function
// 
//   countValues : Ecma -> int
// 
// that counts the number of ECMA values in the given representation.
// 
// Keep in mind that both objects and arrays are themselves values and
// may contain other values inside.
//
// Furthermore, the following should hold:
//
//   1 + countValues e <= countValues (addValue v e)             // if e is an array representation
//
//   1 + countValues e <= countValues (addNameValue (n, v) e)    // if e is an object representation

let rec countValues (e:Ecma) : int = 
  match e with
  | Obj el -> 1 + (List.fold (fun acc (_,ecma) -> acc + countValues ecma) 0 el)
  | Arr el -> 1 + (List.fold (fun acc ecma -> acc + countValues ecma) 0 el)
  | _ -> 1

//// Task 4 ////

type Path = Name list


// Define the function
// 
//   listPaths : Ecma -> Path list
// 
// that computes the full path for all the values in the given ECMA
// representation.
// 
// A path is just a list of names that take us from the root of the
// representation to a particular value.
// 
// For arrays, we consider the same path to take us to the array and to
// all of the elements in the array. Thus, for an array, we include the
// path to it and its elements only once in the result. 
// 
// If `e : Ecma` represents the following structure
// 
//   {
//     "abc" : false,
//     "xs"  : [ { "a" : "a" }, 1.0, true, { "b" : "b" }, false ],
//     "xyz" : { "a" : 1.0,
//               "b" : { "b" : "b" } },
//     "ws"  : [ false ]
//   }
// 
// then  `listPaths e` should result in
// 
// [
//  [];
//  ["abc"];
//  ["xs"];
//  ["xs"; "a"];
//  ["xs"; "b"];
//  ["xyz"];
//  ["xyz"; "a"];
//  ["xyz"; "b"];
//  ["xyz"; "b"; "b"];
//  ["ws"]
// ]
// 
// The ordering of paths in the result list matters:
// - paths to (sub)values in an array respect the order of elements in
//   the array
// 
// - paths to values in an object respect the order in which the values
//   were added to the object (most recently added appears last).
//
// Note that the empty list denotes the path to the root object.

let listPaths (e:Ecma) : Path list = 
  let rec _listPaths (e:Ecma) : Path list =
    match e with
    | Obj ((n,v)::t) -> [n]::(_listPaths v |> List.map (fun x -> n::x)) @ _listPaths (Obj t)
    | Arr (h::t) -> _listPaths h @ _listPaths (Arr t)
    | _ -> []

  [[]] @ _listPaths e

//// Task 5 ////

// Define the function
// 
//   show : Ecma -> string
// 
// that computes a string representation of the given ECMA representation
// in such a way that the ordering requirements from the previous task are
// respected.
// 
// The result should not contain any whitespace except when this
// whitespace was part of a name or a string value.

let rec show (e: Ecma) : string =
  match e with
  | Str e -> sprintf "\"%s\"" e
  | Num e -> string e
  | Null -> "null"
  | True -> "true"
  | False -> "false"
  | Arr el -> "[" + String.concat "," (el |> List.map show) + "]"
  | Obj el -> "{" + String.concat "," (el |> List.map (fun (n,v) -> (sprintf "\"%s\"" n) + ":" + (show v))) + "}"

//// Task 6 ////

// Define the function
// 
//   delete : Path list -> Ecma -> Ecma
// 
// so that
// 
//   delete ps e
// 
// evaluates to a representation `e'` that is otherwise the same as `e` but
// all name-value pairs with paths in the path list `ps` have been removed.
//
// When the user attempts to delete the root object, delete should throw
// an exception. Hint: use `failwith` in the appropriate case.

let rec traverse (path:Path) (ecma:Ecma) : Ecma = 
  match ecma with 
  | Obj el -> Obj (el |> List.collect(fun (n, v) -> 
    match path with
    | [h] when (compare n h) = 0 -> []
    | h::t when (compare n h) = 0 -> [(n, traverse t v)]
    | _ -> [(n, v)]))
  | Arr el -> Arr (el |> List.map (fun v -> traverse path v))
  | _ -> ecma

let delete (ps: Path list) (ecma: Ecma) : Ecma =  
  match ps with
  | [[]] -> failwith "Cannot delete root object"
  | ps -> 
    if (ps |> List.contains []) = true 
    then failwith "Cannot delete root object"
    else ps |> List.fold (fun state item -> traverse item state) ecma

//// Task 7 ////

// Define the function
// 
//   withPath : Path list -> Ecma -> Ecma list
// 
// so that
// 
//   withPath ps e
// 
// evaluates to a list of object representations consisting of those
// objects in `e` that are represented by a path from the list `ps`.
// 
// The result list must respect the ordering requirements from Task 4.

// let rec filterList (ps1:Path list) (ps2:Path list) : Path list = 
//   let subpaths h = ps2 |> List.filter (fun x -> (compare h x) = 0)
//   let otherpaths h = ps2 |> List.filter (fun x -> (compare h x) <> 0)

//   match ps1 with
//   | [] -> []
//   | h::t when (ps2 |> List.contains h) -> (subpaths h) @ filterList t (otherpaths h)
//   | _::t -> filterList t ps2

let withPath (ps: Path list) (ecma: Ecma) : Ecma list =  
  let rec _withPath (path: Path) (ecma: Ecma) : Ecma list =
    match (path, ecma) with
    | ([], ((Obj _) as el)) -> [el]
    | ([h], (Obj ((n,v)::t))) when (compare n h) = 0 -> (_withPath [] v)
    | (h::t, (Obj ((n,v)::t1))) when (compare n h) = 0 -> (_withPath t v)
    | (_, Obj (_::t1)) -> _withPath path (Obj t1)
    | (_, Arr el) -> el |> List.collect (_withPath path)
    | _ -> []

  let newpl = (listPaths ecma) |> List.filter (fun x -> List.contains x ps)
  newpl |> List.fold (fun state p -> state @ _withPath p ecma) []