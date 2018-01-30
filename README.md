# materialGate


An editor tool to dynamically create textures in edit and runtime modes for cool results - after getting aquainted

- Take any source texture or image, and make it a uniqe texture after playing around a bit :)
- Allows for saving textures directly to Asset folder.
- Output texture saved and applied to material
- Add to Shader properties for awesome results
- Pixellation, Large or Small Randomization options

Warning: If you drag a texure onto the destination texture input on the controller - it will be overridden.
 - Backup your textures or use test textures until you know how it works
 - Always use duplicates of textures just in case - not originals.
 - The source should never be overriden, but until it's clear, use backup textures

Directions:

 - Place the Editor script in your Editor folder under Assets. Create one if it doens't exist
 - Create a mesh with a renderer, and place the controller script onto it
 - Under the controller script, drage your source texture into the source tex
 - Duplicate your source texture in your assets folder (CTRL+D, or Command+D)
 - Drag the duplicated texture onto the dest tex input on the controller
 - Create a new material, and put it onto the mesh
 - Drag the duplicated texture from your assets folder, onto the mesh in scene view
 
 - Now you can begin experimenting with the settings:
 - In edit mode, you can change the values and hit 'Update' to see the changes
 - You can also use the Randomization buttons
 - In game mode, it is all updated in realtime - However the randomization buttons will not function
 - Add to the bump map to add an additional UV layer to animate and bump - As well as experiment with adding to the material
 
Note:
 - On the textures used, you will need to make it readable.
 - Click on the texture within your assets folder, and check 'Read/Write Enabled'
 - You may also need to use the Advanced section and override the settings to use RGBA 32 Bit
 

Examples:

![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/console2.png?raw=true)
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/tower.png?raw=true)<p><p>
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/example2.png?raw=true)<p><p>
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/waterWorld2.gif?raw=true)<p><p>
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/1.png?raw=true)
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/2.png?raw=true)
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/3.png?raw=true)<p><p>
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/4.png?raw=true)<p><p>
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/5.png?raw=true)<p><p>
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/example1.png?raw=true)<p><p>
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/example4.gif?raw=true)
![anim1](https://github.com/eagleEggs/materialGate/blob/master/screenShots/steelTower1.png?raw=true)<p><p>



