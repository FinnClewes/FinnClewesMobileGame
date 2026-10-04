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
