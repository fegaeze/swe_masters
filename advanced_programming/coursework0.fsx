
// 1. Load the  following function into fsi
let greeting name = printfn "Hello: %s" name

// 2. Run the function greeting and  say hello to yourself.
greeting "Chioma Nkem-Eze"

// 3. Create a value myName : string that contains your name.
let myName = "Chioma Nkem-Eze"

// 4.Define
// splitAtChar : text:string -> sep:char -> list<string>
// is equivalent to
// splitAtChar : text:string -> sep:char -> string list
let splitAtChar (text:string) (sep:char) : string list = text.Split(sep, System.StringSplitOptions.RemoveEmptyEntries) |> Array.toList

// 5. Write a function splitAtSpaces in such a way that it uses splitAtChar
// Hint: we defined splitAtSpaces in the lecture, now you need to modify it.
let splitAtSpaces text = splitAtChar text ' '

// 6. Define sentenceCount : text:string -> int
let sentenceCount (text:string) = 
  text.Split([|". "; "? "; "! "|], System.StringSplitOptions.RemoveEmptyEntries) 
  |> Array.toList 
  |> List.length

// 7. Define stats : text:string -> unit
// which prints the same stats as showWordCount and
// the number of sentences and average length of sentences
// hint: try float: int -> float
let wordCount text =
  let words    = splitAtSpaces text
  let wordSet  = Set.ofList words
  let numWords = words.Length
  let numDups  = words.Length - wordSet.Count
  numWords, numDups

let stats (text:string) = 
  let numWords, numDups = wordCount text
  let numSentence = sentenceCount text
  let avgSentence = float(splitAtSpaces text |> List.length) / float numSentence

  printfn "--> %d words in text" numWords
  printfn "--> %d duplicate words" numDups
  printfn "--> %d number of sentences" numSentence
  printfn "--> %f average length of sentences" avgSentence

// 8. Use the 'http' function from the lecture to download the file
// http://dijkstra.cs.ttu.ee/~juhan/itt8060/text.txt as a string
// NOTE: you cannot use this function in tryfsharp. Instead you can
// paste the text into your file as a string and process it locally
open System.IO
open System.Net

let http (url: string) = 
  let req = System.Net.WebRequest.Create url

  use resp = req.GetResponse()
  use stream = resp.GetResponseStream()
  use reader = new StreamReader(stream)

  let html = reader.ReadToEnd()
  resp.Close()
  html

let x = http "http://dijkstra.cs.ttu.ee/~juhan/itt8060/text.txt"


// 9. run stats on the downloaded file
stats x
