# NewFolderWizard - v1.1
An organizational tool for the Unity Game Engine for quickly importing and customizing your personal folder structure into every project.

Save and reuse your templates. Templates are saved as ScriptableObjects and may be customized to your liking. 

Simply import your folder-templates, open New Folder Wizard, select folders to create, click "Create Folders". 

Folders will appear in example folder. Demo project comes with an example template.

# How It Works

Create new folders from an auto-populated check-list.

![01 Create New Folders from Selection Window](https://user-images.githubusercontent.com/8204808/217860073-61ffd324-e091-40af-a7e2-b9ce657b205f.gif)



Create nested folder hierarchies.

![02 Create Nested Folders](https://user-images.githubusercontent.com/8204808/217860113-19f4af49-ffb1-47f8-b768-8246207943d6.gif)



Customize template to your liking.

![03 Create New Folder Templates to be Used in All New Projects](https://user-images.githubusercontent.com/8204808/217860142-8d9217cc-448a-45f4-8030-69eaa96494f8.gif)

## Update 1.1 Notes

- Added select/unselect all feature to selection menu
- Added horizontal scroll to selection menu
- Fixed notes on the demo root directory
- Added placeholder file to the empty demo folder to fix issue with it not uploading to GitHub
- Removed empty variables leftover from development
- Other minor cosmetic improvements

![04 Update 1 1 - Select All](https://user-images.githubusercontent.com/8204808/218281092-0e09c2fb-9853-473b-a214-2800c2c1fc6d.gif)

Known issues: Infinite loop possible by placing a parent as the child of it's own child directory. This will crash the editor.
