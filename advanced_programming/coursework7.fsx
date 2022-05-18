
#if INTERACTIVE 
//#r    "FsCheck.dll" // in .Net Framework and .Net core versions prior to 5.0
#r    "nuget: FsCheck, Version=2.14.3" // You can also use nuget directly with F# 5 and 6.
#load "FileSystem.fs"
#endif


open FsCheck

open FileSystem

(*
   Question 1

   Define the predicates

   fsTreeWf : FsTree -> bool

   pathWf   : Path -> bool

   that evaluate to true on precisely those values of FsTree and Path
   that are well-formed.

   The directory names in a well-formed Path cannot be empty.

   A value of type FsTree is well-formed when:
   - the path to any directory in the filesystem is well-formed
   
   - the filesystem is deterministic (for any given path, there is at
     most one directory in the filesystem with that path)

   Define these predicates so that they traverse their input only
   once.
*)

let pathWf (path: Path): bool =
   match path with
   | [] -> false
   | _ -> not(path |> List.exists System.String.IsNullOrEmpty)

let rec fsTreeWf (tree: FsTree): bool =
   match tree with
   | tree when tree.name |> System.String.IsNullOrEmpty -> false
   | tree when tree.children |> List.isEmpty -> true
   | _ -> 
      let ls = tree.children |> List.map (fun tree -> tree.name)
      let wfPath = ls |> pathWf
      let mostOne = (ls |> List.length) = (ls |> List.distinct |> List.length)
      let validChldn = tree.children |> List.forall (fun tree -> tree |> fsTreeWf)

      mostOne && wfPath && validChldn

(*
   Question 2

   Define an FsCheck property

   createIsWf : Path -> FsTree -> Property

   which checks that creating a well-formed path p in a well-formed
   filesystem fs results in a well-formed filesystem.

   Define this using a conditional property (==>).

   Is this a good way to check such a property? Why?

   What percentage of the generated test inputs trivially satisfy this
   property?
*)

let createIsWf (p:Path) (fs:FsTree) : Property = 
   let isWellFormed = pathWf p && fsTreeWf fs
   let result = 
      match isWellFormed with
      | true -> fsTreeWf (fs |> create p)
      | false -> false

   isWellFormed ==> result 

(*
   Question 3

   Define a generator

   wfTrees : Gen<FsTree>

   that only generates well-formed filesystems.


   Define a generator

   wfPaths : Gen<Path>

   that only generates well-formed paths.


   Define these generators in such a way that none of the generated
   data is wasted (i.e., discarded). In other words, all the data that
   you (randomly) generate must be used in the the output of the
   generator.

   You may wish to use the predicates defined above to check that the
   generators indeed only generate well-formed data. Or that the
   predicates are defined correctly.
*)

let wfTrees : Gen<FsTree> =
   let rec createfTree (n:int) =
      match n with
      | n when n > 0 ->
         gen {
            let! nm = Arb.generate<string> |> Gen.filter(fun x -> not(System.String.IsNullOrEmpty x))
            let! ch = (n / 2) |> createfTree |> Gen.listOf
            return {name = nm; children = ch |> List.distinctBy (fun x -> x.name)}
         }
      | _ -> 
         gen {
            let! a = Arb.generate<string> |> Gen.filter(fun x -> not(System.String.IsNullOrEmpty x))
            return {name = a; children = []}
         }

   Gen.sized createfTree

let wfPaths : Gen<Path> =
   let a = Arb.generate<string> |> Gen.filter(fun x -> not(System.String.IsNullOrEmpty x))
   Gen.nonEmptyListOf a


(*
   Question 4

   Define an FsCheck property

   deleteIsWellFormed : Path -> FsTree -> bool

   which checks that given
   
   p  : Path
   fs : FsTree

   we have that the result of deleting p from fs is well-formed.

   You may assume that this property is only used with "well-formed"
   generators (meaning that p and fs are well-formed).
*)

let deleteIsWellFormed (p:Path) (fs:FsTree) = fs |> delete p |> fsTreeWf

(*
   Question 5

   Define an FsCheck property

   createCreates : Path -> FsTree -> bool

   which checks that given

   p  : Path
   fs : FsTree

   we have that the path p is included (exactly once) in the
   result of show after we have created the directory p in fs.

   You may assume that this property is only used with "well-formed"
   generators (meaning that p and fs are well-formed).
*)

let createCreates (p:Path) (fs:FsTree) : bool = 
   (fs |> create p |> show) 
   |> List.filter (fun x -> x = p) 
   |> List.length = 1

(*
   Question 6

   Define an FsCheck property

   deleteDeletes : Path -> FsTree -> bool

   which checks that given

   p  : Path
   fs : FsTree

   we have that the path p is not in the result of show after we have
   deleted the directory p from fs.

   You may assume that this property is only used with "well-formed"
   generators (meaning that p and fs are well-formed).
*)

let deleteDeletes (p:Path) (fs:FsTree) : bool = 
   (fs |> delete p |> show) 
   |> List.filter (fun x -> x = p) 
   |> List.length = 0

(*
   Question 7

   Define an FsCheck property

   showShowsEverything : FsTree -> bool

   which checks that given an
   
   fs : FsTree

   we have that by deleting one by one all of the items in the result
   of show fs we end up with an empty filesystem.

   You may assume that this property is only used with "well-formed"
   generators (meaning that fs is well-formed).
*)

let showShowsEverything (fs:FsTree) : bool = 
   show fs
   |> List.fold (fun fs path -> delete path fs) fs 
   |> isEmpty

(*
   Question 8

   Define an FsCheck property

   createAndDelete : FsTree -> Path -> Path -> Property

   which checks that given
   
   fs : FsTree
   p1 : Path
   p2 : Path

   we have that, if p1 is not a prefix of p2, then

   1) creating directory p1 in fs
   2) creating directory p2 in the result
   3) deleting p1 from the result

   produces a filesystem that still contains p2.

   You may assume that this property is only used with "well-formed"
   generators (meaning that fs, p1 and p2 are well-formed).
*)

let rec prefix (p1:Path) (p2:Path) : bool = 
   match (p1, p2) with
   | [], _ -> true
   | h1::t1, h2::t2 when h1 = h2 -> prefix t1 t2
   | _, _ -> false 
   
let createAndDelete (fs:FsTree) (p1:Path) (p2:Path) : Property =
   let operations = lazy(fs |> create p1 |> create p2 |> delete p1 |> show |> List.contains p2)
   not(prefix p1 p2) ==> operations