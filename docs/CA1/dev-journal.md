# Tuesday 15/09/2026
### Reflection
- Worked: Unity 6.6 with Android Build Support was installed, and the project was switched to Android.
- Worked: The Development Build was created and installed on my phone using `adb install -r`.
- Did not work: Adding the abd.exe to the path caused a delay due to my lack of knowledge on adding things to the path.
- Did not work: My phone didn't automatically prompt "Allow USB Debugging" and I had to revoke USB debugging authorisations for it to prompt.

### Shortlisted Option
#### Micro Turn-Based Tactics
Fantasy turn-based tactics game inspired by tabletop RPGs where the player fights animals, monsters, and bandits. Where each character, including the player, uses an energy bar that refills each turn. 
The core verb is to make tactical decisions in each turn by spending energy to move, attack, and heal, where the player chooses how to use their limited energy. 
If time ran out I would cut out the healing element and reduce the variety of enemies, this way I can prioritise the core combat system.

# Thursday 17/09/2026
### Profiler Info
CPU main thread ms in a typical frame, SetPass calls, GC allocated in frame.
### CPU main thread
16.55ms
### SetPass Calls
3
### GC Allocated
4

# Monday 21/09/2026
| Test | Expected |  |
|------|----------|---------|
| Press Home, wait 10 s, return | Paused, panel visible, audio silent, progress saved | ✓ |
| Pull the notification shade down and up | Paused | ✓ |
| Neighbour calls you, you hang up | Paused, game resumes only on Resume | ✓ |
| Screen off with the power button, back on | Paused | ✓ |
| Force stop from Settings, relaunch | Progress restored from the save | ✓

# Thursday 30/09/2026
## CPU capture worst case
### Worst Frame Main Thread
22.10ms
### Tallest Marker
| Name | Time (ms) |
|------|-------|
| PostLateUpdate.FinishFrameRendering | 21.81 |
| TimeUpdate.WaitForLastPresentationAndUpdateTime | 12.50 |
| PostLateUpdate.ProfilerEndFrame | 6.85 |

GC.Collect wasn't there

## Rendering and Memory
Gfx.WaitForPresentOnGfxThread - 9.76ms
|   |   |
|---|---|
| Total Reserved | 396.4MB |
| GC Allocated in Frame | 5 |
| Textures | 76 |
| Meshes | 10 |
| Audio | 1.1MB |

## Probe
| Probe Setting | 0.5 | 1 |
|---------------|---|-----|
| Main-Thread (ms) | 21.29 | 30.63 |
| Gfx.WaitForPresentOnGfxThread (ms) | 0.00 | 7.92 |

This is CPU-bound

### Location of captures:
ProfilerCaptures/Probe=1
ProfilerCaptures/Probe=0.5

# Thursday 01/10/2026 
## Baseline
- Gameplay: [Baseline] avg 16.67 ms  p99 17.01 ms
- Pause: avg 16.66 ms  p99 17.01 ms
- refresh rate: 60fps

gc.alloc is always 0

no gc.collect markers appear

- total reserved: 300.9MB /  TOTAL PSS:   360449
- setpass calls: 5
- batches: 0
- triangles: 137

## Dispaly times
- +1s223ms
- +1s38ms
- +1s193ms

median: +1s193ms

APK size: 33001345B / 33MB

# Friday 02/10/2026
## Minimum API level
Android 8.0 (API level 26)

# Sunday 04/10/2026
requested permissions:
      android.permission.INTERNET
      android.permission.VIBRATE
      com.finnclewes.mobilegame.DYNAMIC_RECEIVER_NOT_EXPORTED_PERMISSION

# Monday 05/10/2026
- Worked: Made basic turn-based combat system, health indicator and physical movement on attack/hit
- Worked: Converted TurnBasedCombat script to follow WaveTimer
- Did not work: Player always wins, not set up or tested fir mobile use
- Did not work: 
## Grep Search
```
$ grep -rn "IEnumerator\|StartCoroutine\|WaitForSeconds" Assets/ --include=*.cs
Assets/Scripts/Gameplay/TurnBasedCombat.cs:47:        StartCoroutine(DoAttack(player, enemy, () =>
Assets/Scripts/Gameplay/TurnBasedCombat.cs:68:        StartCoroutine(DoAttack(enemy, player, () =>
Assets/Scripts/Gameplay/TurnBasedCombat.cs:87:    IEnumerator DoAttack(GameObject attacker, GameObject target, System.Action onComplete)
Assets/Scripts/Gameplay/TurnBasedCombat.cs:106:    IEnumerator MoveOverTime(GameObject obj, Vector2 startPos, Vector2 endPosition, float duration)
```

# Tuesday 27/10/2026
instantiate - 0B
GC Alloc - 0B
Added basic turn based functionality - needs to add a randomiser so winner isnt set
Added muliple scenes to handle start and end 
Added multiple enemy spawning in one round and set number of kills to win


# Sunday 01/11/2026
## One Lane
on arena with one wave set (3 waves)

## Launch Component
com.finnclewes.mobilegame/com.unity3d.player.UnityPlayerGameActivity

## Cold start
adb shell am force-stop com.finnclewes.mobilegame
adb shell am start -W -n com.finnclewes.mobilegame/com.unity3d.player.UnityPlayerGameActivity

| | wait time |
|--|--|
| 1 | 3029 |
| 2 | 3022 |
| 3 | 3020 |

## Perf Number
11-01 13:19:38.038 28433 28463 I Unity   : [perf] interactive 2.45s
11-01 13:23:01.422 29637 29663 I Unity   : [perf] interactive 2.42s
11-01 13:24:08.556 30544 30580 I Unity   : [perf] interactive 2.42s

## Playtest
- Attack button is too small
- Win/lose is set 

- Make the UI bigger
      - Make settings more obvious
- Change gameplay so result isnt set

# Tuesday 03/11/2026
## Timings
First Upgrade: 41.02s
Coins at 3 mins: 275
Upgrades bought: 3

Adding an extra damage point for each upgrade makes the player do more damage and defeat the enemies faster, this makes progression increase non-linearly as 25 coins are collected per enemy death. 

# Monday 09/11/2026
5.284 24256 24283 I Unity   : [telemetry] 2.14 4d144d45 session_start app_version=0.3.1 device_model=samsung SM-A556B android=Android OS 14 / API-34 (UP1A.231005.007/A556BXXS7AYC6)

## Telemetry
- Events wired: session_start, level_start, level_complete, level_fail, upgrade_purchased
- Save/resume: background PASS · force-stop PASS · am kill PASS
- Pack 1 due: Sun 1 Nov 2026

## Reading reflection
- In my game loop, the player earns 25 coins per kill, which is spent on upgrades costing 100, 150, 225 and so on. The upgrade increases damage by 1 per level. The game does not contain any premium currencies.
- In my current model there is a coin shop with the option to buy 500 coins. This is a consumable. I think it is useful functionality for testing but I dont think it should be in the final game in its current state. The button is directly on the win/lose scene which makes it very accessible to the player, this makes it ethically questionable as I believe that anything that costs real money should be in a seperate scene. I also think that 500 extra coins is too much as it equates to 20 kills. This specific purchase removes one of the key components of the game: killing enemies to get better. 
- I think the grinding in this game isnt too time consuming which would make the consumable purchase unnessesary. If i were to keep this I would need to make the money earned per kill much less to make the 500 coins worth buying. 
- The pack doesnt currently match the price of the upgrades. This leads to left over coins after upgrades which goes directly against the guardrails
- Another breach of the guardrails is the location of the purchase button, it is located on the win/lose scene. It should be located inside a shop scene that is accessible from the win/lose or menu scenes. The only issue with it being located on the win/lose scene is that it can be seen as a taunt or nagging after a fail. 
- Telemetry stays on the device which doesnt raise any ethical concerns.

## Adaptive performance
adb install -r Builds/MyGame-dev.apk

# Wednesday 11/11/2026
## Temp Capture

start temperature: 284
mean: 16.78
99: 16.94

after temp: 273
mean: 16.67
99: 17.25

differnce temp; 
mean: -0.12
99: 0.31

throttling immenent not observed in 10 mins - highest: 0.64