| Field | Row 1 | Row 2 |
|-------|-------|-------|
| Date, commit, versionName / versionCode, Release or Dev, Unity version | 01/10/26, , 0.1.1, Dev, 6000.6.0f1 | 18/10/26, dev, 6000.6.0f1 |
| Device model, Android version, SoC / GPU, graphics API, refresh rate | SM-A556B, 14, , , 120 | SM-A556B, 14, , , 120
| Target fps | 60 | 60 |
| Menu: avg ms / p99 ms | 16.66 ms / 17.01 ms | 15.66ms / 17.09ms
| Steady gameplay: avg ms / p99 ms | 16.67 ms / 17.01 ms |  16.56ms / 17.16ms
| Worst case: avg ms / p99 ms | 16.67 ms / 17.01 ms | 17.32ms / 21.24ms
| GC allocated per frame (bytes), allocating markers | 0 / n/a | 0 / n/a
| Peak Total Reserved (MB) / TOTAL PSS (MB) | 300.9MB / 360449 |  284.6MB 
| Worst case: SetPass / batches / triangles | 5 / 0 / 137 | 6 / 0 / 67
| Cold start ms (median of 3), first interactive s | +1s193ms | +1s231ms (fast)
| APK size (MB) | 33MB | 33.2MB 
| Thermal delta, throttling (Week 7) | |
| Load time (Week 5) | | 227ms
| Time to interactive | | 2s430ms

For Row 2 I added multiple scenes such as the start menu, the game, and the end screen. I also added player and enemy spawning and death.

## Temperature
| Field | Value |
|-------|-------|
| Build ID and mode | v0.4.0, Balanced |
| Start temperature (C) | 28.4 |
| End temperature (C) and 10-minute delta | 27.3, -1.1 |
| Overlay level at minute 0 / 5 / 10 | |
| First `ThrottlingImminent` (minute, logcat time) | none |
| `Throttling` observed (yes/no, minute) | no |
| Frame time before: mean / 99th (ms) | 16.78 / 16.94 |
| Frame time after: mean / 99th (ms) | 16.67 / 17.25 |
| Conditions | plugged in, brightness 50%, case on, ambient about 21 C |