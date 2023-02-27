//Author : https://github.com/seekeroftheball   https://gist.github.com/seekeroftheball
//Version : 1.1
//Updated : Feb 2023

using UnityEditor;
using UnityEngine;

namespace NewFolderWizard
{
    /// <summary>
    /// Editor window data for New Folder Wizard
    /// </summary>
    public class NewFolderWizardSetupWindow : EditorWindow
    {
        /// <summary>
        /// Properties defining the scale of the editor window.
        /// </summary>
        private struct WindowBounds
        {
            public const float WindowWidth = 280;
            public const float WindowHeight = 266;

            public static Vector2 WindowSize = new(WindowWidth, WindowHeight);

            public static Vector2 ScrollPosition;
            public const float ScrollViewHeight = 184;
        }

        private static bool SelectAll;

        /// <summary>
        /// Constructor to set the window scale to the WindowBounds properties
        /// </summary>
        private NewFolderWizardSetupWindow()
        {
            minSize = WindowBounds.WindowSize;
            maxSize = WindowBounds.WindowSize;
        }

        /// <summary>
        /// Load the root directory when the editor window is opened.
        /// </summary>
        private void Awake()
        {
            NewFoldersSetupWizard.LoadDirectories();
        }

        // Draw the editor window
        private void OnGUI() => DrawWindow();
        private void OnInspectorUpdate() => Repaint();

        /// <summary>
		/// Layout the editor window
		/// </summary>
        private void DrawWindow()
        {
            GUILayout.Space(4);
            GUILayout.Label("Select folders to create");

            GUILayout.Space(10);

            // Select all
            bool selectAllCache = SelectAll;
            SelectAll = GUILayout.Toggle(SelectAll, "Select All");
            if (!selectAllCache.Equals(SelectAll))
                NewFoldersSetupWizard.SelectAllChanged(SelectAll);

            GUILayout.Space(4);

            // Scroll
            WindowBounds.ScrollPosition = GUILayout.BeginScrollView(WindowBounds.ScrollPosition, true, true, GUILayout.Width(WindowBounds.WindowWidth), GUILayout.Height(WindowBounds.ScrollViewHeight));

            // Draw folder list
            NewFoldersSetupWizard.ParseRootDirectory();

            GUILayout.Space(2);
            GUILayout.EndScrollView();
            GUILayout.Space(2);

            // Create Folders button
            if (GUILayout.Button("Create Folders"))
            {
                CreateFolderHierarchy.CreateFolders();
                Close();
            }                         
        }

        /// <summary>
        /// Menu item to display editor window.
        /// </summary>
        [MenuItem(FilePaths.MenuItemPath + "Folder Selection")]
        [MenuItem("New Folder Wizard/Folder Selection")]
        private static void DisplayNewFolderWizardWindow()
        {
            NewFolderWizardSetupWindow popupModal = (NewFolderWizardSetupWindow)GetWindow(typeof(NewFolderWizardSetupWindow), true, "New Folder Wizard", true);
            popupModal.ShowModalUtility();
            popupModal.Focus();
        }        
    }
}