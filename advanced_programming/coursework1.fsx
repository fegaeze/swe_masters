

// 1. Associate an identifier `intAndBool` with a value that is a pair of
// an `int` and a `bool`.

let intAndBool = (7, true) 

// 2. Define a function
// 
//   atMostHalf : int -> int
// 
// so that `atMostHalf n` evaluates to an integer `m` such that
// 
//   2 * m <= n < 2 * (m + 1)
// 
// You may assume that the argument is non-negative.

let atMostHalf (n:int) : int =  n / 2

// 3. Define a function
// 
// avgAndEq : int * int * int -> float *  (bool * bool * bool)
// 
// so that `avgAndEq (i1, i2, i3)` evaluates to a pair of a float and a
// triple of bool values so that the float value is the average of i1, i2
// and i3 and the three bool values are true precisely when,
// respectively, i1 = i2, i2 = i3, and i3 = i1.
//
// Hint:
//   float : int -> float

let avgAndEq ((i1, i2, i3):int * int * int) : (float * (bool * bool * bool)) =
    let avg = float (i1 + i2 + i3) / float 3
    let bool1 = i1 = i2
    let bool2 = i2 = i3
    let bool3 = i3 = i1
    
    avg, (bool1, bool2, bool3)

// 4. Define a function
// 
//   multSkipFromTo : int -> int -> int -> int
// 
// so that `multFromTo k m n` evaluates to an integer
// 
//   m * (m + k) * ((m + k) + k) * ... * n
// 
// In other words, multiply numbers from `m` to `n`, but also skip some
// of them (given by `k`).
// 
// You may assume that `m <= n` and `k > 0`.
// 
// Examples:
// 
//   mult 1 1 5 = 1 * 2 * 3 * 4 * 5
// 
//   mult 3 1 5 = 1 * 4 * 5
// 
//   mult 4 1 5 = 1 * 5
// 
// Use recursion. Do not use the `match` construct.

let rec multSkipFromTo k m = function
    | x when m >= x -> x
    | x -> m * multSkipFromTo k (m + k) x

// 5. Consider the function given by
// 
//   f n = n / 2        if n is even
//   f n = 3 * n + 1    otherwise
// 
// The 3n + 1 conjecture says that, for any positive n, the sequence
// 
//   n, f n, f (f n), f (f (f n)), ...
// 
// will always reach 1.
// 
// Your task is to define a function
// 
//   threeN : int -> int
//
//
// so that `threeN n` evaluates to the number of steps needed for `n` to
// reach `1` according to `f`. In other words, how many times do we have
// to apply the function `f` to `n` to reach `1`.
// 
// You may assume that the argument is positive.
// 
// Use recursion.
// 
// Number of steps needed to reach `1` is the sequence
// https://oeis.org/A006577

let rec threeN = function
    | 1 -> 0
    | n when n % 2 = 0 -> 1 + threeN (n / 2)
    | n -> 1 + threeN (3 * n + 1)

// 6. Define a function
// 
//   notFibonacci : int -> int * int
// 
// such that
// 
//   fst (notFibonacci 0) = 2
//   fst (notFibonacci 1) = 1
//   fst (notFibonacci n) = fst (notFibonacci (n - 2)) +
//                          fst (notFibonacci (n - 1))
// 
// and
// 
//   snd (notFibonacci n)
// 
// is the number of times the function `notFibonacci` is used. Note that
// both `notFibonacci 0` and `notFibonacci 1` should use the function
// only once (i.e., no recursive calls) and `notFibonacci n` should
// perform two recursive calls (for `n >= 2`).
// 
// You may assume that the argument is non-negative.
// 
// Use recursion.

let rec notFibonacci = function
    | 0 -> (2, 1)
    | 1 -> (1, 1)
    | n -> 
        let sndPlace = notFibonacci (n - 2)
        let fstPlace = notFibonacci (n - 1)
        (fst(sndPlace) + fst(fstPlace), 1 + snd(sndPlace) + snd(fstPlace))

// 7. Define the functions
// 
//   sinApprox : int -> float -> float
// 
//   cosApprox : int -> float -> float
// 
// so that
// 
//   sinApprox d : float -> float
//   
//   cosApprox d : float -> float
// 
// approximate the sine and cosine functions where `d` is the
// approximation parameter which we call depth. You may assume that depth
// is non-negative.
// 
// The idea is to base these functions on the trigonometric identities
// 
//   sin(x) = 2 * sin(x / 2) * cos(x / 2)
// 
//   cos(x) = cos^2(x / 2) - sin^2(x / 2)
// 
// and the fact that
// 
//   sin(x) ~ x
// 
//   cos(x) ~ 1
// 
// for small values of `x`.
// 
// We consider `x` to be small at depth `0`. Otherwise it is not
// small.
// 
// Use recursion. To avoid infinite recursion we decrease the depth by
// one every time we use the trigonometric identity.
//
// You can compare your approximations with results from
// `System.Math.Sin` and `System.Math.Cos`.

let rec sinApprox (d:int) (f:float) = 
    match f with
    | f when d = 0 -> f
    | f -> 2. * (sinApprox (d - 1) (f / 2.)) * (cosApprox (d - 1) (f / 2.)) 

and cosApprox d f = 
    match f with
    | f when d = 0 -> 1.
    | f ->  ((cosApprox (d - 1) (f / 2.)) ** 2.) - ((sinApprox (d - 1) (f / 2.)) ** 2.) 