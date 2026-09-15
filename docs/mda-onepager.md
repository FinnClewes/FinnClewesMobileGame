# Adventurer's Path
### One-Line Pitch
Fantasy turn-based tactics game inspired by tabletop RPGs where the player fights animals, monsters, and bandits, using limited energy per turn.
### Aesthetics
- Strategic: Players should feel that their decisions matter and that they need to think ahead.
- Rewarding: Successfully defeating enemies or completing a difficult mission should feel satisfying.
- Adventurous: The fantasy setting should create a sense of exploration and adventure.
- Challenging but fair: Players should feel challenged by enemy encounters without feeling that outcomes are random or unfair.
### Core Mechanics
- Move: Move characters around a grid using energy.
- Attack: Spend energy to attack nearby enemies.
- Heal: Spend energy to restore a character's health.
- Manage energy: Decide how much energy to spend each turn.
- Turn-based combat: Alternate between the player's characters and enemy AI.
### Dynamics
The player begins each mission with a small group of characters and must decide how to spend their limited energy each turn. Players position characters, attack enemies, and manage their health while enemies use basic AI to respond. Players must balance offensive and defensive actions, to defeat their enemies while also keeping their allies alive. Completing objectives and defeating enemies allows the player to progress through increasingly difficult encounters.
### Progression & content
- 3 character/unit archetypes with different strengths and abilities.
- 3 enemy types: animals, monsters, and bandits.
- 3 missions with different objectives and enemy combinations.
- Each mission should take approximately 5–10 minutes.
- Increasing difficulty through enemy numbers, enemy types, and mission objectives.
- Save slots to allow players to save and continue their progress.
### Platform features
- Designed for Android mobile devices.
- Touch controls for selecting characters, tiles, and actions.
- UI designed around different phone screen sizes and safe areas/notches.
- Basic haptic feedback for actions such as attacks or selecting abilities, if time allows.
- Android Development Build and device testing through ADB.
### Performance budget
- Target 60 FPS on the development/test phone.
- Avoid unnecessary real-time effects and expensive calculations.
- Keep individual combat maps relatively small (4×4 to 5×5 grid).
- Use simple models/2D assets to keep memory usage low.
- Aim for fast scene loading and avoid unnecessary assets being loaded into memory.
- Profile on the actual Android device rather than relying solely on the Unity Editor.
### Monetisation
There will be no monetisation as a part of this game.
### Risks & cuts list
There is always room for the AI to be more sophisticated meaning that it could waste a significant amount of time best used elsewhere.
The first things I would cut out would be the healing element and variety of enemies.