using UnityEngine;
using SerializableDictionary;

namespace NewFolderWizard
{
    public class NewFoldersSetupWizard
    {
        private struct ToggleData
        {
            public bool Toggle;
            public FolderProperties NewFolderProperties;            
        }

        public static DirectoryData RootDirectory;
        
        public static void LoadDirectories()
        {
            RootDirectory = Resources.Load<DirectoryData>(FilePaths.ResourceFolderRelativePathToRootDirectory);
        }

        public static void ParseRootDirectory() => ParseFolders(RootDirectory, string.Empty);

        private static SerializableDictionary<string, FolderProperties> serializableDictionary = new();

        private static void ParseFolders(DirectoryData directory, string parentFolder)
        {
            bool dataChanged = false;
            ToggleData toggleData = new();
            parentFolder += parentFolder.Equals(string.Empty) ? string.Empty : "/";

            foreach (var keyValuePair in directory.Folders)
            {
                string folderName = keyValuePair.Key;
                FolderProperties folderProperties = keyValuePair.Value;
                bool folderIsEnabled = folderProperties.IsEnabled;
                                
                toggleData.Toggle = GUILayout.Toggle(folderIsEnabled, parentFolder + folderName);

                GUILayout.Space(1);

                if (toggleData.Toggle && folderProperties.ChildDirectoryData && !folderProperties.ChildDirectoryData.Equals(directory))
                    ParseFolders(folderProperties.ChildDirectoryData, parentFolder + folderName);
                
                if (toggleData.Toggle.Equals(folderIsEnabled))
                    continue;

                toggleData.NewFolderProperties.IsEnabled = toggleData.Toggle;
                toggleData.NewFolderProperties.ChildDirectoryData = folderProperties.ChildDirectoryData;

                serializableDictionary.CopyFrom(directory.Folders);
                serializableDictionary[folderName] = toggleData.NewFolderProperties;

                dataChanged = true;                               
            }

            if (dataChanged)
            {
                directory.Folders.CopyFrom(serializableDictionary);
                serializableDictionary.Clear();
            }                
        }
    }
}