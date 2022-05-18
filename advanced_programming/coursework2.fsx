
// You are given a type BibliographyItem that has the following structure:
// string list * string * (int * int) * int
// The meaning of the tuple elements is as follows:
// * The first field represents the list of author names where each name is in the format
//   "Lastname, Firstname1 Firstname2" (i.e. listing all first names after comma)
// * The second field represents the title of the publication
// * The third field represents a pair containing the starting page number and ending page number of the paper.
// * The fourth field represents the year of publication

type BibliographyItem = string list * string * (int * int) * int

// 1. Create a value bibliographyData : BibliographyItem list that contains
// at least 7 different publications on your favourite topic from https://dblp.uni-trier.de/ 
// Please note that you need not read the whole papers, just pick 7 papers that look interesting to you from the database.

let bibliographyData : BibliographyItem list = [
  (["Yousif, Ahmed Hamad"; "Konstantin, Vasilievich Simonov"; "Mohammad, B. Naeem"], "Detection of Brain Tumor in MRI Images, Using a Combination of Fuzzy C-Means and Thresholding", (45, 60), 2019);
  (["Gueidi, Afef"; "Gharsellaoui, Hamza"; "Samir, Ben Ahmed"], "The Rise of Robotics Data for Real-Time Management Based on New NoSQL Solution", (11, 26), 2019);
  (["Ossiannilsson, Ebba"], "Increasing Access, Social Inclusion, and Quality Through Mobile Learning", (29, 44), 2018);
  (["Shuo, Liu"; "Tao, Gao"; "Tainze, Li"], "Design and Implementation of Large-Scale MOOC Platform in Colleges and Universities", (57, 63), 2017);
  (["Tao, Gao"; "Zhenjing, Yao"], "Optimization of Matched Chaotic Frequency Modulation Excitation Sequences for Multichannel Simultaneously Triggered Ultrasonic Ranging System",  (1, 16), 2012);
  (["Alshattnawi, Sawsan"], "Utilizing Cloud Computing in Developing a Mobile Location-Aware Tourist Guide System",  (9, 18), 2013);
  (["Zhiyong, Sun"; "Liu, Ye"; "Jiahui, Chen"], "A Film Criticism Website Based on \"ThinkPHP\"",  (1, 22), 2017)
]

// 2. Make a function compareLists : string list -> string list -> int that takes two string lists and
// returns 
// * Less than zero in case the first list precedes the second in the sort order;
// * Zero in case the first list and second list occur at the same position in the sort order;
// * Greater than zero in case the first list follows the second list in the sort order;
// You need to use the support honouring the default culture for ordering strings, i.e. the built in
// compare does not work properly by default. Look at the
// documentation of the .Net comparison for strings: System.String.Compare
// If the first authors are the same
// then the precedence should be determined by the next author.
// Please note that your implementation should be recursive over the input lists.
//
// The sort order in .Net is defined using System.Globalization.CultureInfo:
// https://docs.microsoft.com/en-us/dotnet/api/system.globalization
// Please note that your solution should not force a particular sort order!

let rec compareLists (l1:string list) (l2:string list) : int = 
  match l1,l2 with
  | [],[] -> 0
  | _,[] -> 1
  | [],_ -> -1
  | x::xs,y::ys when System.String.Compare(x,y) = 0 -> compareLists xs ys
  | x::_,y::_ -> System.String.Compare(x,y)

// 3. Make a function
// compareAuthors : BibliographyItem -> BibliographyItem -> int
// that takes two instances of bibliography items and compares them according to the authors.
// Use solution from task 2.

let compareAuthors (bi1:BibliographyItem) (bi2:BibliographyItem) : int =
  let (al1,_,_,_) = bi1
  let (al2,_,_,_) = bi2
  compareLists al1 al2

// 4. Make a function
// compareAuthorsNumPages : BibliographyItem -> BibliographyItem -> int
// that takes two instances of bibliography items and compares them according to the authors and if the authors are 
// the same then according to the number of pages in the publication.

let compareAuthorsNumPages (bi1:BibliographyItem) (bi2:BibliographyItem) : int =
  match bi1,bi2 with
  | (_,_,pnt1,_),(_,_,pnt2,_) when compareAuthors bi1 bi2 = 0 -> compare (snd(pnt1) - fst(pnt1) + 1) (snd(pnt2) - fst(pnt2) + 1)
  | _,_ -> compareAuthors bi1 bi2

// 5. Make a function 
// sortBibliographyByNumPages : BibliographyItem list -> BibliographyItem list
// That returns a bibliography sorted according to the number of pages in the
// publication in ascending order.
// If two items are at the same level in the sort order, their order should be preserved.

let sortBibliographyByNumPages (bl:BibliographyItem list) : BibliographyItem list = 
  bl |> List.sortBy (fun (_,_,pnt,_) -> snd(pnt) - fst(pnt) + 1)

// 6. Make a function 
// sortBibliographyByAuthorNumPages : BibliographyItem list -> BibliographyItem list
// That returns a bibliography sorted according to the authors and number of pages in the publication in ascending order
// If two items are at the same level in the sort order, their order should be preserved.

let sortBibliographyByAuthorNumPages (bl:BibliographyItem list) : BibliographyItem list = 
  bl |> List.sortWith compareAuthorsNumPages

// 7. Make a function
// groupByAuthor : BibliographyItem list -> (string * BibliographyItem list) list
// where the return list contains pairs where the first element is the name of a single
// author and the second element a list of bibliography items that the author has co-authored.

let rec getAllAuthors (bl:BibliographyItem list) =
  match bl with
  | [] -> []
  | (al,_,_,_)::bl' -> al::getAllAuthors bl'

let rec getAuthorsWorks (al:string list ) (bl:BibliographyItem list) =
  match al with
  | [] -> []
  | x::xs -> 
    let works = List.filter (fun (al,_,_,_) -> List.contains x al) bl
    works::getAuthorsWorks xs bl

let groupByAuthor (bl:BibliographyItem list) : (string * BibliographyItem list) list =
  let allAuthors = getAllAuthors bl |> List.concat |> List.distinct
  let allAuthorsWork = bl |> getAuthorsWorks allAuthors 
  List.map2 (fun x y -> (x,y)) allAuthors allAuthorsWork