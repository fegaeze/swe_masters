
// The task is to model an object that can move on a plane where
// coordinates are specified as integers.

// A position of an object is given as a pair of x and y coordinates as type XY:

type XY = int * int

// The object also has a direction. The possible directions are given as
// an enumeration (a simple version of discriminated unions).

type Dir = N | E | S | W

// If the object moves, then it moves in its current direction.

// Note that moving in a given direction means:
// N - moving in parallel and in the direction of the y axis
// E - moving in parallel and in the direction of the x axis
// S - moving in parallel and in the opposite direction of the y axis
// W - moving in parallel and in the opposite direction of the x axis

// We also keep track of the history of the movement of the object. 

type History = XY list 

// We represent the object by the following record type. In other words,
// we track the current state (position and direction) and the history of
// the object.

type State = { position  : XY
               direction : Dir
               history   : History }

// The possible ways in which we can command the object to move on the
// plane is given by the following discriminated union:

type Command = Step of int       
             | Turn of int       
             | Loop of int * int 

// The meaning of the commands is the following:
// Step n      - the object must move n steps in the current direction;
// Turn n      - the object must turn right n quarter turns, e.g.,
//               when the current direction is N, then Turn 3 indicates
//               that the new direction must be W;
// Loop (m, n) - the object must make a rectangular "loop" with dimensions m and n, e.g.,
//               Loop (m, n) indicates that the object must:
//               - take m steps (in the current direction)
//               - turn right
//               - take n steps
//               - turn right
//               - take m steps
//               - turn right
//               - take n steps
//               - turn right

// You are given the function
//
// iterate : int -> ('a -> 'a) -> 'a -> 'a
//
// that will take a function f as an argument and compose it with
// itself n times.
//
// In other words,
//
// iterate 5 f x
//
// applies the function f to x five times, i.e., f (f (f (f (f x))))

let rec iterate (n : int) (f : 'a -> 'a) : 'a -> 'a =
  if n <= 0
  then id
  else f << iterate (n - 1) f

// 1. We start by defining some basic functions.

// Define the function
// 
// turn : Dir -> Dir
// 
// that will compute the new direction after making one quarter turn to
// the right. A quarter turn is our unit of measurement for turning.

let turn (d : Dir) : Dir =
  match d with
  | N -> E
  | E -> S
  | S -> W
  | W -> N

// Define the function
//
// step : Dir -> XY -> XY
//
// that computes the new position after taking
// one step in the given direction.
//
// The distance between the new position and the old position must be 1.

let step (d: Dir) ((x, y): XY) : XY =
  match d with
  | N -> (x, y + 1)
  | E -> (x + 1, y)
  | S -> (x, y - 1)
  | W -> (x - 1, y)

// 2. We now define how to move the object. Define the function
//
// perform : (c : Command) -> (s : State) -> State
//
// that moves the object from state s according to c.
//
// Use the function iterate to perform the move, i.e.,
// Step n, Turn n and Loop (m, n) must be implemented using the functions
// iterate, step and turn.
//
// Note that it is perfectly valid for the object to step a negative
// amount in the current direction. Taking m steps in the current
// direction and then taking -m steps in the current direction should put
// the object back to where it was.
// 
// Similarly, taking m turns followed by -m turns should result in the
// same direction that the object had before.
// 
// You also need to track the history of the object.
// If performing a move makes the position of the object change,
// then you add the previous position to the path. For a Loop, you do this
// every time you reach a corner.
//
// The history is represented so that the most recent item is at the head.

let performStep (num:int) (s:State) =
  match num with
  | n when n < 0 -> 
    let newDir = iterate 2 turn s.direction
    let newPos = iterate (abs n) (step newDir) s.position
    {s with position = newPos; history = s.position::s.history }
  | n when n > 0 ->
    let newPos = iterate n (step s.direction) s.position
    {s with position = newPos; history = s.position::s.history }
  | _ -> s

let performTurn (num:int) (s:State) =
  let newDir = iterate (((num % 4) + 4) % 4) turn s.direction
  {s with direction = newDir}

let performLoop (num1:int) (num2:int) (s:State) =
  performStep num1 s
  |> performTurn 1
  |> performStep num2
  |> performTurn 1
  |> performStep num1
  |> performTurn 1
  |> performStep num2
  |> performTurn 1

let performCommand (c : Command) (s : State) : State =
  match c with
  | Step n -> performStep n s
  | Turn n -> performTurn n s
  | Loop (x, y) -> performLoop x y s

// 3. Define the function
//
// perform : Command list -> State -> State
//
// that performes the moves according to the given list of commands
// starting from the given initial state.
//
// This must be implemented using a fold over the list of moves. (You
// can choose whether to use fold or foldBack.)

let performCommands (cs : Command list) (s : State) : State = List.fold (fun s c -> performCommand c s) s cs

// 4. Define the function
//
// flipSteps : Command list -> Command list
//
// that transforms the given list of commands so that each command Step n
// is replaced with Step -n. Leave other commands the same.
//
// Implement this using List.map

let fsMap = function
  | Step n -> Step -(n)
  | c -> c

let flipSteps (cs : Command list) : Command list = List.map fsMap cs

// 5. Define the function
//
// flipTurns : Command list -> Command list
//
// that transforms the given list of commands so that each Turn n
// is replaced with Turn m so that performing Turn m makes the object
// face the opposite direction compared to what it would be facing
// after performing Turn n. Leave other commands the same.
//
// Implement this using List.map

let ftMap = function
  | Turn n when (abs n) % 2 = 0 -> Turn ((abs n) + 2)
  | Turn n when n < 0 -> Turn (abs n)
  | Turn n when n > 0 -> Turn -(n)
  | c -> c

let flipTurns (cs : Command list) : Command list = List.map ftMap cs

// 6. Define the function
//
// singleSteps : Command list -> Command list
//
//
// that removes from the given list of commands all those that are of the
// form Loop (m, n) or that are of the form Step k where the absolute
// value of k is not equal to 1.

let ssFilter = function
  | Loop (x,y) -> false
  | Step k when abs(k) <> 1 -> false
  | _ -> true

let singleSteps (cs : Command list) : Command list = List.filter ssFilter cs

// 7. Define the function
//
// unpackLoops : Command list -> Command list
//
// that replaces a command of the form Loop (m, n) with an equivalent
// list of commands consisting only of Step k and Turn l commands. Leave
// other commands the same.
//
// Implement this using List.collect

let ulCollect = function
  | Loop (x,y) -> [Step x; Turn 1; Step y; Turn 1; Step x; Turn 1; Step y; Turn 1]
  | c -> [c]

let unpackLoops (cs : Command list) : Command list = List.collect ulCollect cs

// 8. Define the function
//
// simplify : Command list -> Command list
//
// that accumulates adjacent Step moves into a single Step
// and adjacent Turn moves into a single Turn. In other words,
// the resulting list of moves must not contain adjacent Step moves
// or adjacent Turn moves. 
//
// The idea is to replace
// - adjacent Step m and Step n with Step (m + n)
// - adjacent Turn m and Turn n with Turn k so that
//   0 <= k <= 3 and
//   after performing Turn k the object is facing the same direction
//   as it would after performing Turn m followed by Turn n.
//
// Implement this using a fold (choose which one) over the input list.
// Do not traverse the input list several times. 
//
// Here is a hint: the type of the accumulator (partial result) you use
// for the fold should allow you to keep track of at least two things:
// - The part of the list that is already simplified.
// - The part that you have seen but have not yet simplified. This is
//   the current sequence of (adjacent) moves of the same kind, i.e.,
//   a sequence of Step or a sequence of Turn moves. If the next move
//   you see is of a different kind, then you first simplify this part,
//   add it to the simplified part and then continue.

let sFold n acc =
  match n, acc with
  | Step m, Step n::t -> Step (m + n) :: t
  | Turn m, Turn n::t -> Turn ((((m + n) % 4) + 4) % 4) :: t
  | Turn m, _ -> Turn (((m % 4) + 4) % 4) :: acc
  | _, _ -> n :: acc

let simplify = function
  | [] -> []
  | cs -> List.foldBack sFold cs []