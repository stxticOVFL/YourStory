# YourStory
### A small mod that lets you easily modify the backstory text behind Neon White. 

![image](https://github.com/stxticOVFL/YourStory/assets/29069561/9ab0d184-5354-4344-aa02-e85261724a28)

## Features
- Accurate replication of the backstory texture, now in engine for potentially higher quality
- Easy editing using MelonPreferencesManager
- I made this in a day just because I could, I don't have much more for you
  
## Installation
## __*Automatic*__ (preferred)
**This mod is part of [White's Storage!](https://github.com/stxticOVFL/WhitesStorage)**
Follow the install instructions for that instead, and enable it in the `White's Storage` category in the preferences manager (F5 by default).
### Manual
1. Download [MelonLoader](https://github.com/LavaGang/MelonLoader/releases/latest) and install it onto your `Neon White.exe`.
2. Run the game once. This will create required folders.
3. Download the **Mono** version of [Melon Preferences Manager](https://github.com/Bluscream/MelonPreferencesManager/releases/latest), and put the .DLLs from that zip into the `Mods` folder of your Neon White install.
    - The preferences manager **is required** to use YourStory, using the options menu to enable the mod (F5 by default).
4. Download `YourStory.dll` from the [Releases page](https://github.com/stxticOVFL/YourStory/releases/latest) and drop it in the `Mods` folder.

## Building & Contributing
This project uses Visual Studio 2022 as its project manager. When opening the Visual Studio solution, ensure your references are corrected by right clicking and selecting `Add Reference...` as shown below. 
Most will be in `Neon White_data/Managed`. Some will be in `MelonLoader/net35`, **not** `net6`. Select the `MelonPrefManager` mods for that reference. 
If you get any weird errors, try deleting the references and re-adding them manually.

![image](https://github.com/stxticOVFL/NeonCapture/assets/29069561/67c946de-2099-458d-8dec-44e81883e613)

Once your references are correct, build using the keybind or like the picture below.

![image](https://github.com/stxticOVFL/EventTracker/assets/29069561/40a50e46-5fc2-4acc-a3c9-4d4edb8c7d83)

Make any edits as needed, and make a PR for review. PRs are very appreciated.
