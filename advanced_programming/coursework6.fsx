(*
  Task 1:

  Write a function
  pHoldsForAllSequentialElements : (int -> int -> bool) -> int list -> bool

  that when calling the function

  pHoldsForAllSequentialElements p xs

  will check if predicate p holds for all consequtive elements in the list. If it does,
  the function should return true, else false.

  In the case there is less than two elements in the list it is assumed that
  the predicate does not hold as there are no sequential elements present.

  NB! You are required to implement the function in a tail recursive way using
  explicit recursion.

*)

let pHoldsForAllSequentialElements (p:(int -> int -> bool)) (xs:int list) : bool =
  let rec inner p xs acc =
    match xs with
    | [] -> false
    | [_] -> false
    | (h1::(h2::[])) when (p h1 h2) -> true
    | (h1::(h2::_)) when not (p h1 h2) -> false
    | (_::t) -> inner p t (acc || true)
    | _ -> false

  inner p xs false

(*
  Task 2:

  Write a function
  createTwoTuplesOfList : 'a -> 'a list -> ('a * 'a) list
  that takes a list of 'a-s and returns a list of two element tuples of 'a-s that are taken
  sequentially from the list passed as the second argument to the function.
  In case the list has odd number of elements make the firstate argument of the
  function be the second element in the lastate tuple. 
  Make sure your implementation uses explicit tail recursion.
*)

let createTwoTuplesOfList (x:'a) (xs:'a list) : ('a * 'a) list =
  let rec loop (x:'a) (xs:'a list) (acc:('a * 'a) list) : ('a * 'a) list =
    match xs with
    | [] -> acc
    | [h1] -> ((h1,x)::acc)
    | (h1::(h2::t)) -> loop x t ((h1,h2)::acc)

  (loop x xs []) |> List.rev

(*
  Task 3:

  Write a function
  createTwoTuplesOfListFold : 'a -> 'a list -> ('a * 'a) list
  that takes a list of 'a-s and returns a list of two element tuples of 'a-s that are taken
  sequentially from the list passed as the second argument to the function. In case
  the list has odd number of elements make the firstate argument of the function be the
  second element in the lastate tuple. 
  Make sure your implementation uses List.fold or List.foldBack appropriately.
  Testate yourself if this implementation appears to be tail recursive.

*)

let createTwoTuplesOfListFold (x:'a) (xs:'a list): ('a * 'a) list =
  let inner (count,prev,x,acc) item =
    match (count % 2) with
    | 0 -> 
      match acc with
      | []
      | [_] -> (count+1,item,x,[(prev,item)])
      | _::t -> (count+1,item,x,(prev,item)::t)
    | _ -> (count+1,item,x,(item,x)::acc)

  let (_,_,_,tl) = xs |> List.fold inner (1,x,x,[])
  tl |> List.rev

(*
  Task 4:

  Below you find the definition of a type Tr of leaf-labeled trees. Write a
  function
  
  medianAndAverageInTree : int Tr -> int * float
  
  that returns a pair where the firstate element is the median label in the
  given tree and the second an average across all nodes. The median is the label
  for which the difference in counts of elements to the right and left is
  either 0 or the count of elements to the right is exactly 1 greater than the
  count of elements to the left. The average is the sum of all elements divided with
  the number of elements.
  Use continuation-passing style in your implementation and perform the operations
  in a single pass of the tree.
*)

type 'a Tr =
  | Lf of 'a
  | Br of 'a Tr * 'a Tr

let getMedian (ml:int list) (length:int) : int = 
  if (length % 2 = 0) then ml.[(length / 2) - 1]
  else ml.[length / 2]

let medianAndAverageInTree (tree: int Tr) : (int * float) =
  let rec loop (tree:int Tr) func : (int list * int * int) =
    match tree with
    | Lf v -> ([v], v, 1) |> func
    | Br (left, right) -> 
      loop left (fun (list1, sum1, count1) -> 
        loop right (fun (list2, sum2, count2) -> 
          ((list1 @ list2), (sum1+sum2), (count1+count2)) |> func
        )
      )

  let (ml, sum, count) = loop tree id
  (getMedian ml count, float(sum) / float(count))