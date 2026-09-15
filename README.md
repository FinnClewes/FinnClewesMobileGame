 # Game Title: Adventurer's Path

 ## Option Number: 
 3 - Micro Turn-Based Tactics

## [Boot] Line Info
 [Boot] samsung SM-A556B | Android OS 14 / API-34 (UP1A.231005.007/A556BXXS7AYC6) | Vulkan | 1080x2340 @ 450 dpi

## Build Steps
### Check the phone is connected
```bash
adb devices
# List of devices attached
# R58M12ABCDE   device
```
### Install the APK
```bash
adb install -r Builds/MyGame-dev.apk
# Performing Streamed Install
# Success
```
### Launch the game
```bash
adb shell monkey -p com.yourname.mygame 1
adb logcat -s Unity
# [Boot] SM-S911B | Android OS 15 / API-35 | Vulkan | 1080x2340 @ 425 dpi
```