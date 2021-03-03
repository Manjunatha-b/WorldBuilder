video : drive.google.com/file/d/1mTJgFbiKHpXZgGKyPgGOXMzIkhySBVGA/view?usp=sharing
apk:    drive.google.com/file/d/1sAfGSkQGJXwHrKU5gDI_cxG3dhIXYLu3/view?usp=sharing

# World Builder Game

## Game Features
1. Can add terrain tiles to the world ( snap grid style )
1. Can add decorative items to the terrain at any place.
1. terrain and decorations can be rotated and deleted.
1. Movable character that resets to last terrain location.
1. Can reset whole scene.

## Controls Features
1. Very simple UI, drag and drop for building, click for deleting.
1. Pinch to zoom in and out
1. Entire left side of screen can be us joystick


## Resource Loading
1. All the prefabs related to building of the world were shifted into the 'Resources' Folder.
1. A python script generates the directory list of the entire resources folder. Preferably run before build.
1. Had written a C# script to save prefab preview icons.
1. On menu loading, only these icons are loaded each time to display the buildable item. 
1. On clicking the menu item the resource is loaded into memory. It seems to work pretty fast as well so I did not use any Async features.

## Scripts
1. Player scripts were for handling the player movements and the camera
    1. Player movement is registered on the entire left side of the screen if used as an imaginary joystick. Handled by an invisible button with EventTrigger.
    1. Pinch and Zoom is handled with registering two inputs and calculating their movement ( code adapted from unity forums ).
2. UI:
    1. BaseMenu: This menu is a class that contains the basic structure of a builder menu for both building World using Inventory( items placable anywhere ) and Terrain ( placed as ground tiles ).
    1. TerrainMenu: This menu inherits from the BaseMenu class and overrides the drag method which needs to be able to drag new items in snap fashion across a grid.
    1. InventoryMenu: This menu inherits from BaseMenu class and overrids the drag method which can drag items and put it on any place in the terrain.
    1. BuildMenuHandler: In charge of toggling between deletion, inventory and terrain builders.
    1. Toggle : basic toggle of showing and not showing a menu
    1. ResetScene: For resetting the scene.

## Known Bugs / Problems
1. Deletion of ground tile you're standing on, no way to reset back to a tile yet.
1. Some 'Inventory' menu buttons - nature, props,people & vehicles dont open right now cause of subfolders which I didnt look into yet. Easily fixable.

## FPS:
1. On an Intel HD520 PC it averages above 150 frames ( in editor ) The project was developed with patience on the same laptop with a solid 4 gigs of ram.
2. Redmi 5 looks like its running at a 25-35 fps range.
It can definitely be improved as Ive used the standard rendering pipeline rather than LightWeight RP to just get the prototype up and running. LWRP can certainly give it a boost.
