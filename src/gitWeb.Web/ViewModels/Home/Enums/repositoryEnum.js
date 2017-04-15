export const  IsStaged = (fileStatus) => {
  return fileStatus == ModifiedInIndex || fileStatus == DeletedFromIndex || fileStatus == NewInIndex
  || fileStatus == RenamedInIndex || fileStatus == TypeChangeInIndex;
}

//
// Summary:
//     The file hasn't been modified.
export const Unaltered = 0;
//
// Summary:
//     New file has been added to the Index. It's unknown from the Head.
export const NewInIndex = 1;
//
// Summary:
//     New version of a file has been added to the Index. A previous version exists
//     in the Head.
export const ModifiedInIndex = 2;
//
// Summary:
//     The deletion of a file has been promoted from the working directory to the Index.
//     A previous version exists in the Head.
export const DeletedFromIndex = 4;
//
// Summary:
//     The renaming of a file has been promoted from the working directory to the Index.
//     A previous version exists in the Head.
export const RenamedInIndex = 8;
//
// Summary:
//     A change in type for a file has been promoted from the working directory to the
//     Index. A previous version exists in the Head.
export const TypeChangeInIndex = 16;
//
// Summary:
//     New file in the working directory unknown from the Index and the Head.
export const NewInWorkdir = 128;
//
// Summary:
//     The file has been updated in the working directory. A previous version exists
//     in the Index.
export const ModifiedInWorkdir = 256;
//
// Summary:
//     The file has been deleted from the working directory. A previous version exists
//     in the Index.
export const DeletedFromWorkdir = 512;
//
// Summary:
//     The file type has been changed in the working directory. A previous version exists
//     in the Index.
export const TypeChangeInWorkdir = 1024;
//
// Summary:
//     The file has been renamed in the working directory. The previous version at the
//     previous name exists in the Index.
export const RenamedInWorkdir = 2048;
//
// Summary:
//     The file is unreadable in the working directory.
export const Unreadable = 4096;
//
// Summary:
//     The file is LibGit2Sharp.FileStatus.NewInWorkdir but its name and/or path matches
//     an exclude pattern in a gitignore file.
export const Ignored = 16384;
//
// Summary:
//     The file is LibGit2Sharp.FileStatus.Conflicted due to a merge.
export const Conflicted = 32768;
