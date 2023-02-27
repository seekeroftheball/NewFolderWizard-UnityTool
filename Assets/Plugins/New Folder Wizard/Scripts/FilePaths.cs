namespace NewFolderWizard
{
    /// <summary>
    /// All file paths for directories and menu items.
    /// </summary>
    struct FilePaths
    {
        private const string RootDirectoryFileName = "RootDirectory";

        public const string PathToAssetsFolder = "Assets/Example Folders Will Be Made Here";
        public const string ResourceFolderRelativePathToDirectories = "New Folder Wizard/DirectoryData/";
        public const string ResourceFolderRelativePathToRootDirectory = ResourceFolderRelativePathToDirectories + RootDirectoryFileName;        
        public const string MenuItemPath = "Window/Plugins/New Folder Wizard/";
    }
}