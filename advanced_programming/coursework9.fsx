
// Our representation of complex numbers.
type Complex = double * double



(*
   Question 1

   The Mandelbrot set.
   
   Formal definition:
   https://en.wikipedia.org/wiki/Mandelbrot_set#Formal_definition

   Basic properties:
   https://en.wikipedia.org/wiki/Mandelbrot_set#Basic_properties

   Define the function

   mandelbrot : int -> Complex -> bool

   so that 'mandelbrot n is the characteristic function of the
   approximation of the Mandelbrot set by n iterations of the
   quadratic map.

   In other words, 'mandelbrot n is a function which decides whether
   a complex number c belongs to the "n-th approximation" of the
   Mandelbrot set.

   In other words, define the function so that given

   n : int
   c : Complex

   'mandelbrot n c evaluates to true precisely when, according to n
   iterations of the quadratic map, c belongs to the Mandelbrot set.

   The quadratic map is given in "Formal definition" and a condition
   for deciding whether c belongs to Mandelbrot (upto n iterations) is
   given in "Basic properties".

   Here are some hints:
   - Start by computing the z_1, z_2, ..., z_n for c. Based on this
     sequence of values decide whether c belongs to the (approximation
     of the) Mandelbrot set or not.

   - How to add and multiply complex numbers?

   - What is the absolute value of a complex number?

*)

let add ((a,b):Complex) ((x,y):Complex) = (a + x, b + y)
let sqr ((a,b):Complex) = (a ** 2.0 - b ** 2.0, double (2.0 * a * b))

let rec generate (n: int) (c: Complex) (prev: Complex): bool = 
   let curr = add (sqr prev) c

   match n with
   | 0 -> 
      let absCurr = sqrt((fst(curr) * fst(curr)) + (snd(curr) * snd(curr)))
      if (absCurr > 2.0) then false else true
   | _ -> 
      generate (n - 1) (c) (curr)

let mandelbrot n c = generate (n - 1) c (double 0.0, double 0.0)

(*
   Question 2

   Define the function
 
   divide : int -> int -> (int * int) seq

   so that given

   m : int
   n : int

   'divide m n evaluates to a sequence of ranges

   (s_1, e_1), ..., (s_m, e_m)

   so that:
   * s_1 = 0 and e_m = n - 1

   * s_{i + 1} = e_i + 1


   That is to say that:   
   * for every x : int such that 0 <= x < n there exists an i such
     that s_i <= x <= e_i

   * the sequence of (s_i, e_i) covers 0, ..., n - 1 without overlap,
     i.e., if s_i <= x <= e_i and s_j <= x <= e_j, then i = j.

   You may think of n as the number of things we have and m as the
   number of buckets among which we distribute them.

   Try to divide fairly.
*)

let rec func (x:int) (y:int) (con:int) (m:int) =
   seq {
      if m = 1 then yield (x,y)
      else
         yield (x, x + con - 1)
         yield! func (x+con) y con (m - 1)
   }

let divide m n = func 0 (n-1) (n/m) m

(*
   Question 3

   Define the function
   
   mandelbrotAsync :  int
                   -> int
		   -> (int -> unit)
		   -> (int -> unit)
		   -> Complex []
		   -> Async<bool []>

   so that given

   m      : int
   n      : int
   start  : int -> unit
   finish : int -> unit
   cs     : Complex []

   'mandelbrotAsync m n s e cs' creates an asynchronous computation
   which computes an array of booleans with the same size as cs. An
   element in this array is true precisely when the complex number at
   the corresponding position in cs belongs to the Mandelbrot set
   (according to n iterations).

   Use the 'divide' function to divide the work into m chunks (ranges)
   and then evaluate those in parallel using the Async mechanism.

   Whenever this function starts computing on a chunk of input (s, e),
   the function must apply the function 'start' to 's'. When it has
   completed working on the chunk, then it must apply 'finish' to 'e'.

*)

let mandelbrotAsync (m:int) (n:int) (start:int -> unit) (finish:int -> unit) (cs:Complex []) : Async<bool []> =
   async {
      let array = Array.create cs.Length true
      let response = Array.fold (fun (i:int) x -> array.[i] <- (mandelbrot n x); i + 1) 0  cs
      return array
   }

(*
  Question 4

  Define the function

  display : int -> bool [] -> string

  so that given

  n  : int
  bs : bool []

  'display n bs' represents the Boolean array bs as rectangle of
  characters with true represented by '*' and false represented by
  '.'.

  The parameter n : int is the length of the rows in the string
  representation.

  For example, if
  
  n  = 2 
  bs = [| true; false; false; false; true; true; false |]

  then

  display n bs = "*.\n..\n**\n.."

  (Note the handling of missing values in the example.)

*)

let getActualValue bool = 
   match bool with 
   | true -> "*" 
   | false -> "."

let stringify (array: bool []) (n': int) : string = 
   Array.fold(fun s (index, c) -> 
      let m = index % n'
      match m with
      | 0 when ((index <> 0)) -> 
         s +  "\n" + getActualValue(c)
      | _ -> 
         s + getActualValue(c)
   ) "" (Array.indexed array)

let display (n: int) (bs: bool []): string =
   match ((bs |> Array.length) % n) with
   | 0 -> stringify bs n
   | rem -> stringify (Array.append bs (Array.create (n - rem) false)) n

(*

  You may use the function display to display the Mandelbrot pattern
  that you compute using mandelbrotAsync.

  For example, you can take an array of complex numbers representing a
  rectangular grid from (-3, -3) to (1, 3) with steps of 0.2 and then
  use display with an appropriate row length.

*)


// The next questions are about observables.
//
// You are not allowed to write any mutable code. Solve these using
// the combinators from FSharp.Control.Observable module.


(*
   Question 5

   Define the function

   accumulate : ('t -> 'a -> 't * 'u option) -> 't -> IObservable<'a> -> IObservable<'u>

   so that given

   f   : 't -> 'a -> 't * 'u option
   t   : 't
   obs : IObservable<'a>

   'accumulate f t obs' accumulates observable events of obs into an
   accumulator of type 't and emits an observable event u when
   'snd (f acc a)' evaluates to 'Some u' for an observed event 'a'.

*)

let accumulate (f: 't -> 'a -> 't * 'u option) t obs =
   obs
   |> Observable.scan (fun (t', _) curr -> f t' curr) (t, None)
   |> Observable.choose snd

(*
   Question 6

   Define the function

   chunks : int -> IObservable<'a> -> IObservable<'a list>

   so that given

   n   : int
   obs : IObservable<'a>

   'chunks n obs' is the observable of chunks (or pieces) of length n
   of the observable obs.

   If n = 3 and obs emits events a_1, a_2, a_3, ... then

   'chunks n obs' emits [a_1; a_2; a_3], [a_4; a_5; a_6], ...

*)

let chunks n obs =   
   accumulate (fun t a -> 
      match t |> List.length with 
      | t' when t' = n - 1 -> ([], Some (t @ [a]))
      | _ -> (t @ [a], None)
   ) [] obs

(*
   Question 7

   Define the function

   sliding : int -> IObservable<'a> -> IObservable<'a list>

   so that given

   n   : int
   obs : IObservable<'a>

   'sliding n obs' is the observable of sliding windows of length n
   over the observable obs.

   If n = 3 and obs emits events a_1, a_2, a_3, ... then

   'sliding n obs' emits [a_1; a_2; a_3], [a_2; a_3; a_4], ...

*)

let sliding n obs = 
   obs 
   |> Observable.scan (fun x y ->
         match x with
         | _::x' when (x' |> List.length) = (n - 1) -> x' @ [y]
         | _ -> x @ [y]
      ) []
   |> Observable.filter (fun x -> (x |> List.length) = n)

(*
   Question 8

   Define the function

   limit : IObservable<unit> -> IObservable<'a> -> IObservable<'a>

   so that given

   clock : IObservable<unit>
   obs   : IObservable<'a>

   'limit clock obs' emits events from obs.

   The observable starts in the emit mode: the next observed event of
   obs can be emitted. After that the observable transitions to
   discard mode: all observed events of obs are discarded until an
   event from clock is observed after which the observable transitions
   back to emit mode and the process repeats.

*)

let limit clock obs = failwith "not implemented"

(*
   Question 9

   Define the function

   alarm : int -> int -> IObservable<unit> -> IObservable<int> -> IObservable<unit>

   so that given

   n         : int
   threshold : int
   clock     : IObservable<unit>
   obs       : IObservable<int>

   'alarm n threshold clock obs' emits an event when the average value
   of last n events of obs has been greater than the threshold.

   Furthermore, after emitting an event, this observable goes into
   silent mode until the next clock event is observed.

   In silent mode, even if there is cause for an alarm, no event is
   emitted.

*)

let alarm n threshold clock obs = failwith "not implemented"
