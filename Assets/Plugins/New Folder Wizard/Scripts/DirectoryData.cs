using UnityEngine;
using SerializableDictionary;

namespace NewFolderWizard
{
    /// <summary>
    /// ScriptableObject housing for serializable dictionary of folder properties.
    /// </summary>
    [CreateAssetMenu(fileName = "Directory", menuName = "New Folder Wizard/Directory Data")]
    public class DirectoryData : ScriptableObject
    {
        [SerializeField, TextArea(2, 11)] private string editorNotes;
        [Space(10)]
        [Header("ADD FOLDERS AND CHILD DIRECTORIES TO THIS DIRECTORY", order = 0)]
        [Header("New Folder Name, *Child Directory Data (*optional)", order = 1)]
        public SerializableDictionary<string, FolderProperties> Folders = new();
    }

    /// <summary>
    /// Folder property data contains an 'enable' toggle and child directory data.
    /// This allows for each folder to be toggled individually and the toggle state to be cached.
    /// </summary>
    [System.Serializable]
    public struct FolderProperties
    {
        [HideInInspector] public bool IsEnabled;
        public DirectoryData ChildDirectoryData;
    }
}