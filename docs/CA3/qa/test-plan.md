| # | Scenario | Trigger | Expected | Pass/Fail | Evidence |
|---|---|---|---|---|---|
| 1 | Resume / background | Home, wait 30 s, relaunch ×10; then force-stop and relaunch | Paused, audio silent, resumes in place; save intact after force-stop | Pass | 11-15 12:03:36.856 I/ActivityManager( 1044): Force stopping com.finnclewes.mobilegame appid=10373 user=0: from pid 8983 ( 9021): [telemetry] 2.16 07ed4eb3 session_start |
| 2 | No network | Airplane mode on before launch, and again mid-play | No hang, no unhandled exception | Pass | Game makes no network calls; no errors in Terminal 2 |
| 3 | Rotation | Rotate mid-play | Safe areas respected; no stretched UI; no restart | Pass | game no longer rotates. previous fail: screens/rotation.png |
| 4 | Battery saver | Enable, play 2 min | Frame cap respected; playable | Pass | screens/battery-saver.png |
| 5 | Low RAM | Trim memory, background, return | No black screen; if killed, relaunch restores save | Pass | no kill lines |
| 6 | Cold and warm start | Part A commands | TotalTime recorded; loading never hangs | Pass | cold 165 ms, warm 52 ms |