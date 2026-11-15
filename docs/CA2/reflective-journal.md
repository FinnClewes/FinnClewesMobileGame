# CA2 Reflective Journal: Adventurer's Path
 
**Build:** v0.5.0 (9735e937]) · **Device:** Samsung Galaxy A55 (SM-A556B), Android 14, 120 Hz panel · **Date:** 15/11/2026
 
## 1. The slice
 
Adventurer's Path is a micro turn-based tactics game. The player fights a sequence of enemies, each round choosing between **Attack** and **Defend**, and spends the coins earned from kills on damage and health upgrades. Waves get harder as the run goes on, and every third wave is a boss fight against the Ogre. My target device was my own A55, which I treat as a mid-tier phone. All the evidence in this journal comes from that device, sideloaded over `adb`.
 
The game I shortlisted in Week 1 was more ambitious than this, with an energy bar spent on moving, attacking and healing. I said then that if time ran out I would cut healing and enemy variety to protect the core combat. In practice I kept enemy variety but dropped movement and the energy system, because the turn loop needed to be fun before it needed to be deep, and I decided the grid element of the game was unnessary. I substituted this element for an endless fast paced loop
 
## 2. Design: from one button to a decision every turn
 
My first playable version on 05/10 had a basic turn-based loop, but my own journal notes say "player always wins". The 01/11 playtest confirmed this: the outcome felt fixed and the Attack button was too small. Telemetry later showed the cause clearly. Every `level_complete` event logged `score=1`, because `enemiesInLevel` was still set to 1 in the scene even though the script defaulted to 3. The player had one button and no decisions to make, so in MDA terms there were mechanics but almost no dynamics.
 
I used MDA to decide what to add next, rather than just adding features:
 
| Mechanic | Dynamic | Aesthetic |
|---|---|---|
| Enemy intent (normal hit, then a HEAVY hit every 3rd turn, shown in red) | Reading the threat and planning a response | Challenge, tension |
| Defend action | Choosing between attacking and blocking each turn | Mastery |
| Enemy types as ScriptableObjects (Slime, Goblin, Ogre) | Changing approach for each enemy | Variety |
| Escalating waves, boss every 3rd wave | Managing HP and coins across a run | Progression, climax |
 
The most useful lesson came from balancing. After adding waves, the player got their first upgrade on the wave 3 boss and then died there. Instead of guessing, I simulated 500 runs with my real values. The original tuning died on **wave 2** on average. The simulation also exposed a design flaw I hadn't noticed: **Defend was never worth pressing.** Halving a heavy hit saved exactly the same damage as the extra enemy turn it caused, so "always defend on heavy" and "never defend" died on the same wave. Making Defend block a heavy hit fully, healing 50% between waves, roughly doubling coin rewards, weakening the Ogre and lowering `scalePerWave` to 0.15 moved the median death to **wave 5–6**. What I learned is that a mechanic existing doesn't mean it creates a decision. The numbers have to make both options sensible at different times.
 
## 3. Economy and ethics
 
The loop is earn, spend, upgrade: coins per kill, spent on upgrades costing 100, 150, 225 and so on (`100 × 1.5^level`). In my Week 6 timing test the first upgrade came at **41 s**, with 3 upgrades bought in 3 minutes at 25 coins per kill. I chose damage as the first upgrade because players feel it immediately (enemies die in fewer hits), and later added a health upgrade so that spending became a choice rather than a single obvious button.
 
The Pack 1 reading made me look critically at the `CoinShop` stub. Three problems stood out. First, the 500-coin pack doesn't match any upgrade price, so it leaves coins over, which goes against the CPC principles. Second, the button sits on the win/lose screen, which can read as nagging right after a failure. Third, once I made death reset the wallet for a roguelite structure, a player could pay real money and lose it on their next death. While i have not done so yet, I plan to move it to be inside the pause menu, this makes it so there is no easily accessible buy button, but it still exists. The game has no paid randomness, so it avoids the PEGI 16 "paid random items" rating, and telemetry never leaves the device.
 
## 4. Performance
 
My early profiling in Week 3 found a worst frame of 22.1 ms, with `PostLateUpdate.FinishFrameRendering` the tallest marker. In the render-scale probe, the main thread took 21.29 ms at scale 0.5 and 30.63 ms at 1.0, and from that I concluded the game was **CPU-bound**. After moving from coroutines to `Awaitable` and removing per-frame allocations, the baseline settled at **16.67 ms mean / 17.01 ms 99th** with **0 B GC allocated per frame**.
 
The Week 7 thermal soak held a locked 60 fps for 10 minutes with **no throttling** (maximum thermal level 0.64, 99th percentile 16.94 → 17.25 ms). The temperature actually fell by 1.1 °C, which I put down to the phone already being warm from setup. The soak was also done while plugged in, which the lab notes makes the delta unreliable, so [I would repeat it unplugged / I repeated it unplugged and got …]. An important realisation was that a frame time locked at 16.7 ms only shows the frame cap, not the real cost. The phone was finishing early and waiting, so the headroom is hidden unless you look at the PlayerLoop time.
 
The Balanced / High FPS toggle worked, and Adaptive Performance reported `bottleneck=TargetFrameRate` in both modes. However, my perf-modes table shows High FPS still running at about 16.8 ms with the panel at **60 Hz**, so the screen was capping it, not the game. Memory is well within budget: total PSS is **354 MB**, and textures are tiny because the art is three 256 px sprites compressed as ASTC 4x4. The APK is about 33 MB.
 
I also learned to be careful with measurement definitions. My 01/11 cold start figures were around 3,000 ms (`WaitTime`), but on 15/11 I recorded **165 ms cold / 52 ms warm** using `TotalTime`, which stops at the first frame. These numbers aren't comparable, and in future I will record which metric I used every time.
 
## 5. QA and robustness
 
The Week 8 test plan covered six scenarios. Resume, force-stop, no network, battery saver, low RAM and cold/warm start all passed. Saving on every `Changed` event, plus `OnApplicationPause`, meant coins survived force-stop and `am kill`. **Rotation failed on the first run.** Player Settings were on Auto Rotation, and logcat showed the window resizing to `2340x1080` within the same process. I fixed it by locking the Default Orientation to Portrait, rebuilt, and re-ran the row as a pass. This was my "fix the worst failure" item. It's a small change, but without the test plan I wouldn't have known it was broken.
 
## 6. What went wrong and what I learned
 
Most of my bugs weren't in game logic. They were about how Unity connects things:
 
- **Cross-scene references.** The `Wallet` and `PerformanceMode` live on the persistent `App` object in `Boot`, so I couldn't drag them into scripts in `Game`. I learned to use a static `Instance`, and that pressing Play from any scene other than `Boot` breaks everything. My Retry button "stopped working" for exactly this reason.
- **Hidden errors.** `_ = SceneLoader.Instance.Load(...)` threw away the task, so failures never reached the Console. Awaiting it inside a `try/catch` with `Debug.LogException` made problems visible.
- **Order of operations.** In `Enemy.Setup` I applied the wave scaling *after* setting `Health` and planning the first move, so scaled enemies spawned with unscaled HP and the increase did nothing. Setting values before reading them is obvious in hindsight.
- **Wiring mistakes.** The High FPS toggle had nothing listening to it, and the Haptics button updated the Probe button's label instead of its own. Both were found by checking the scene, not the code.
- **IDE auto-imports.** `using UnityEditor…` and `using NUnit…` were added automatically and would have broken the Android build.
 
## 7. Next steps for CA3
 
For CA3 I will run the peer survey across at least 10 testers and several device tiers, using the build stamp so every response maps to one APK. I'll use telemetry (`level_fail` with the wave reached) to check whether the new tuning holds for real players and not just the simulation, and add combat feedback (hit flash, damage numbers), which the thermal headroom allows. I want to make the buttons more accesssible, and the gameplay difficulty more balanced.