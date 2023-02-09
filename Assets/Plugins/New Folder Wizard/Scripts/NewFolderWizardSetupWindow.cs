using UnityEditor;
using UnityEngine;

namespace NewFolderWizard
{
    public class NewFolderWizardSetupWindow : EditorWindow
    {
        private struct WindowBounds
        {
            public const float WindowWidth = 280;
            public const float WindowHeight = 244;

            public static Vector2 WindowSize = new(WindowWidth, WindowHeight);

            public static Vector2 ScrollPosition;
            public const float ScrollViewHeight = 184;

            public static Rect CheckboxArea = new(20, 0, ScrollViewHeight, WindowWidth - 20);
        }

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

            WindowBounds.ScrollPosition = GUILayout.BeginScrollView(WindowBounds.ScrollPosition, false, true, GUILayout.Width(WindowBounds.WindowWidth), GUILayout.Height(WindowBounds.ScrollViewHeight));
            
            NewFoldersSetupWizard.ParseRootDirectory();

            GUILayout.EndScrollView();

            if (GUILayout.Button("Create Folders"))
            {
                CreateFolderHierarchy.CreateFolders();
                Close();
            }                         
        }

        [MenuItem(FilePaths.MenuItemPath + "Folder Selection", priority = 11)]
        [MenuItem("New Folder Wizard/Folder Selection", priority = 11)]
        private static void DisplayNewFolderWizardWindow()
        {
            NewFolderWizardSetupWindow popupModal = (NewFolderWizardSetupWindow)EditorWindow.GetWindow(typeof(NewFolderWizardSetupWindow), true, "NEW FOLDER WIZARD", true);
            popupModal.ShowModalUtility();
            popupModal.Focus();
        }
    }
}