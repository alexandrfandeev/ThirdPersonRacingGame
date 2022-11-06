# ThirdPersonRacingGame

This project is a small Low-Poly racing game with a third-person controller, 
with two racing levels and a small garage, which is an area where the player can quietly improve his car, 
change its color, buy in-game currency, select the graphics settings and decide which level he wants to pass.

Important parameters such as player name, whether the player was created, graphics level, number of resources, 
level status (whether it was passed earlier), color and health points of the car are saved through PlayerPrefs,
each of these parameters has its own ID and value.

Each car upgrade entity (color, health points) are inherited classes with their own enum key, that helps the developer 
to easily improve/expand the number of different kinds of upgrades in the future. ScriptableObjects, also, are good way 
to create a reliable enhancement, but still needs a key to save them, which is likely to be a kind of int/enum/string.

In-Game Purchases and Ads between scenes are running only in editor-mode. Important to say, that if it's necessary to run the build on OSX 
platform, the player still can interact between levels, "buy" in-game currency, and don't meet any troubles.

To initialize addressables assets (user's vehicle, AI vehicles) it was created an small trick, that represents to the player 
level environment, and after some time (can be editted in prefab) the camera is smooth targeting to spawned vehicle.

Basic game controls are [W, A, S, D] for movement, [Space] for handbraking and [Shift/Control] for gears change. 

Implementations, that I made in free-time are: UI speedometer, level reward depended on level completition state,
starting scene animation (camera view, enabling race-counter), bot AI vehicle wheels rotation, added settings 
in race levels that offers to player possibillity to restart level or return to
main menu, vehicle mesh rotation in garage scene and general pause system.

Total time, that was spent on this project is about 1,5 - 1,7 working days (18 - 20 hours). 

