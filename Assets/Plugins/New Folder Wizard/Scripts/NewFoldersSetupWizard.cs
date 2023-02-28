//Author : https://github.com/seekeroftheball   https://gist.github.com/seekeroftheball
//Version : 1.2
//Updated : March 2023

using System.Linq;
using SerializableDictionary;
using UnityEngine;

namespace NewFolderWizard
{
    /// <summary>
    /// Handler logic for New Folder Wizard editor window.
    /// </summary>
    public class NewFoldersSetupWizard
    {
        /// <summary>
        /// Active toggle state for individual folders.
        /// </summary>
        private struct ToggleData
        {
            public bool Toggle;
            public FolderProperties NewFolderProperties;            
        }

        public static DirectoryData RootDirectory;  // Local cache

        /// <summary>
        /// Load the root directory from the Resources folder.
        /// </summary>
        public static void LoadDirectories()
        {
            RootDirectory = Resources.Load<DirectoryData>(FilePaths.ResourceFolderRelativePathToRootDirectory);
        }

        /// <summary>
        /// Begin parsing hierarchies from the root directory.
        /// </summary>
        public static void ParseRootDirectory() => ParseFolders(RootDirectory, string.Empty);

        /// <summary>
        /// Cache a local copy of dictionary for making edits to copy back to the original source.
        /// </summary>
        private static SerializableDictionary<string, FolderProperties> serializableDictionary = new();

        /// <summary>
        /// Loop through each directory and child to create a toggle button for each individual folder.
        /// </summary>
        /// <param name="directory">Directory to parse.</param>
        /// <param name="parentFolder">Name of the parent directory of the directory being parsed.</param>
        private static void ParseFolders(DirectoryData directory, string parentFolder)
        {
            bool dataChanged = false;
            ToggleData toggleData = new();
            parentFolder += parentFolder.Equals(string.Empty) ? string.Empty : "/";

            // Sort folders by name with LINQ
            var sortedDictionary = from entry in directory.Folders orderby entry.Key ascending select entry;

            foreach (var keyValuePair in sortedDictionary)
            {
                string folderName = keyValuePair.Key;
                FolderProperties folderProperties = keyValuePair.Value;
                bool folderIsEnabled = folderProperties.IsEnabled;
                                
                toggleData.Toggle = GUILayout.Toggle(folderIsEnabled, parentFolder + folderName);

                GUILayout.Space(1);

                // If active and has children, parse children.
                if (toggleData.Toggle && folderProperties.ChildDirectoryData && !folderProperties.ChildDirectoryData.Equals(directory))
                    ParseFolders(folderProperties.ChildDirectoryData, parentFolder + folderName);
                
                if (toggleData.Toggle.Equals(folderIsEnabled))
                    continue;

                // Capture and save data to cache
                toggleData.NewFolderProperties.IsEnabled = toggleData.Toggle;
                toggleData.NewFolderProperties.ChildDirectoryData = folderProperties.ChildDirectoryData;

                serializableDictionary.CopyFrom(directory.Folders);
                serializableDictionary[folderName] = toggleData.NewFolderProperties;

                dataChanged = true;                               
            }

            if (dataChanged)
            {
                // Save data back to original source.
                directory.Folders.CopyFrom(serializableDictionary);
                serializableDictionary.Clear();
            }                
        }

        /// <summary>
        /// Processes interactions with the select all toggle.
        /// </summary>
        /// <param name="isTrue">Enabled status.</param>
        public static void SelectAllChanged(bool isTrue) => SetAllCheckValues(isTrue, RootDirectory, string.Empty);

        /// <summary>
        /// Loops through each directory and child to toggle the enabled status of every folder.
        /// </summary>
        /// <param name="isTrue">Enabled status.</param>
        /// <param name="directory">Root directory to populate checklist.</param>
        /// <param name="parentFolder">Name of the parent of this directory.</param>
        private static void SetAllCheckValues(bool isTrue, DirectoryData directory, string parentFolder)
        {
            ToggleData toggleData = new();
            parentFolder += parentFolder.Equals(string.Empty) ? string.Empty : "/";

            // Local cache
            SerializableDictionary<string, FolderProperties> directoryFolders = new(directory.Folders); 

            foreach (var keyValuePair in directory.Folders)
            {
                string folderName = keyValuePair.Key;
                FolderProperties folderProperties = keyValuePair.Value;

                // Save enabled status to cache
                toggleData.NewFolderProperties.IsEnabled = isTrue;
                toggleData.NewFolderProperties.ChildDirectoryData = folderProperties.ChildDirectoryData;

                directoryFolders[folderName] = toggleData.NewFolderProperties;                

                // Parse children
                if (folderProperties.ChildDirectoryData && !folderProperties.ChildDirectoryData.Equals(directory))
                    SetAllCheckValues(isTrue, folderProperties.ChildDirectoryData, parentFolder + folderName);
            }

            // Copy changes back to source directory.
            directory.Folders.CopyFrom(directoryFolders);
            directoryFolders.Clear();
        }
    }
}