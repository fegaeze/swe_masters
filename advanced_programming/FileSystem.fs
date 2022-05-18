module FileSystem

(*
   This module provides a type FsTree. We often refer to elements of
   this type as filesystems. But FsTree is just a datatype of certain
   trees.

   In particular, there is no difference between files and directories
   in this representation. We often refer to a node in the tree as a
   directory.

   This module also provides incomplete definitions of certain
   functions operating on filesystems. Note that you have to define
   the required FsCheck properties against these functions.
*)

    type Path = string list 

    type FsTree = { name     : string
                  ; children : FsTree list }



    // Evaluates to true on those FsTree that consist of only the root
    // node (i.e., without any child nodes)

    let isEmpty (fs : FsTree) : bool = failwith "not implemented"


    // The list of paths to all of the directories in the filesystem

    let show (fs : FsTree) : Path list =
        failwith "not implemented"


    // Create a new directory at path p in the silesystem fs. If the
    // directory exists, then return the filesystem as is.

    let create (p : Path) (fs : FsTree) : FsTree =
        failwith "not implemented"


    // Delete the directory at path p in the filesystem fs. If the
    // directory does not exist, then return the filesystem as is. If the
    // path p denotes the root node of the filesystem, then return the
    // filesystem as is.

    let delete (p : Path) (fs : FsTree) : FsTree =
        failwith "not implemented"
