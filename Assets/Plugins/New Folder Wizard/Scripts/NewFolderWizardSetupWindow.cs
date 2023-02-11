//Author : https://github.com/seekeroftheball   https://gist.github.com/seekeroftheball
//Version : 1.1
//Updated : Feb 2023

using UnityEditor;
using UnityEngine;

namespace NewFolderWizard
{
    public class NewFolderWizardSetupWindow : EditorWindow
    {
        private struct WindowBounds
        {
            public const float WindowWidth = 280;
            public const float WindowHeight = 266;

            public static Vector2 WindowSize = new(WindowWidth, WindowHeight);

            public static Vector2 ScrollPosition;
            public const float ScrollViewHeight = 184;
        }

        private static bool SelectAll;

        private NewFolderWizardSetupWindow()
        {
            minSize = WindowBounds.WindowSize;
            maxSize = WindowBounds.WindowSize;
        }

        private void Awake()
        {
            NewFoldersSetupWizard.LoadDirectories();
        }

        private void OnGUI() => DrawWindow();

        private void OnInspectorUpdate() => Repaint();
        
        private void DrawWindow()
        {
            GUILayout.Space(4);
            GUILayout.Label("Select folders to create");

            GUILayout.Space(10);

            bool selectAllCache = SelectAll;
            SelectAll = GUILayout.Toggle(SelectAll, "Select All");
            if (!selectAllCache.Equals(SelectAll))
                NewFoldersSetupWizard.SelectAllChanged(SelectAll);

            GUILayout.Space(4);

            WindowBounds.ScrollPosition = GUILayout.BeginScrollView(WindowBounds.ScrollPosition, true, true, GUILayout.Width(WindowBounds.WindowWidth), GUILayout.Height(WindowBounds.ScrollViewHeight));
            
            NewFoldersSetupWizard.ParseRootDirectory();

            GUILayout.Space(2);
            GUILayout.EndScrollView();
            GUILayout.Space(2);

            if (GUILayout.Button("Create Folders"))
            {
                CreateFolderHierarchy.CreateFolders();
                Close();
            }                         
        }

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