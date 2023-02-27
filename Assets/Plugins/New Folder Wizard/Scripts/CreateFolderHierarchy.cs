//Author : https://github.com/seekeroftheball   https://gist.github.com/seekeroftheball
//Version : 1.1
//Updated : Feb 2023

using UnityEditor;
using UnityEngine;

namespace NewFolderWizard
{
    /// <summary>
    /// Parses directory data and handles the actual creation of the folder hierarchies.
    /// </summary>
    public class CreateFolderHierarchy
    {
        /// <summary>
        /// Loads root directory then loops through, parsing children.
        /// </summary>
        public static void CreateFolders()
        {            
            DirectoryData rootDirectory = Resources.Load<DirectoryData>(FilePaths.ResourceFolderRelativePathToRootDirectory);

            ParseFolders(rootDirectory, FilePaths.PathToAssetsFolder);

            Debug.Log("Folder Creation Successful!");
        }

        /// <summary>
        /// Loops through dictionary, creates individual folders then parses children.
        /// </summary>
        /// <param name="directory">Directory to parse.</param>
        /// <param name="parentPath">Parent of directory to parse.</param>
        private static void ParseFolders(DirectoryData directory, string parentPath)
        {
            foreach (var keyValuePair in directory.Folders)
            {                
                bool folderIsEnabled = keyValuePair.Value.IsEnabled;

                if (!folderIsEnabled)
                    continue;                

                string guid = AssetDatabase.CreateFolder(parentPath, keyValuePair.Key);
                string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

                FolderProperties folderProperties = keyValuePair.Value;

                if (folderProperties.ChildDirectoryData && folderProperties.ChildDirectoryData != directory)
                    ParseFolders(folderProperties.ChildDirectoryData, newFolderPath);
            }
        }
    }
}