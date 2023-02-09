using UnityEngine;
using SerializableDictionary;

namespace NewFolderWizard
{
    [CreateAssetMenu(fileName = "Directory", menuName = "New Folder Wizard/Directory Data")]
    public class DirectoryData : ScriptableObject
    {
        [SerializeField, TextArea(2, 11)] private string editorNotes;
        [Space(10)]
        [Header("ADD FOLDERS AND CHILD DIRECTORIES TO THIS DIRECTORY", order = 0)]
        [Header("New Folder Name, *Child Directory Data (*optional)", order = 1)]
        public SerializableDictionary<string, FolderProperties> Folders = new();
    }

    [System.Serializable]
    public struct FolderProperties
    {
        [HideInInspector] public bool IsEnabled;
        public DirectoryData ChildDirectoryData;
    }
}