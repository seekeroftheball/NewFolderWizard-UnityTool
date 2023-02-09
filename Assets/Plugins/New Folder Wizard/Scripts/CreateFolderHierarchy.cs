using UnityEditor;
using UnityEngine;

namespace NewFolderWizard
{
    public class CreateFolderHierarchy
    {   
        public static void CreateFolders()
        {            
            DirectoryData rootDirectory = Resources.Load<DirectoryData>(FilePaths.ResourceFolderRelativePathToRootDirectory);

            ParseFolders(rootDirectory, FilePaths.PathToAssetsFolder);

            Debug.Log("Folder Creation Successful!");
        }

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