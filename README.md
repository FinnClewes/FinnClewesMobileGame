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

## Keystore Info
Alias name: mygame

File path: C:\Users\ainec\Documents\College\Semester 7\Mobile Game Dev\keystores\mygame-release.keystore

Creation date: 16 Sep 2026

Entry type: PrivateKeyEntry

Certificate chain length: 1

Certificate[1]:

Owner: O=DefaultCompany

Issuer: O=DefaultCompany

Serial number: 511faa4c

Valid from: Wed Sep 16 14:27:03 IST 2026 until: Thu Sep 03 14:27:03 IST 2076

Validity: 50 years

Certificate fingerprints:

         SHA1: 91:13:01:3C:2F:D7:0E:06:2C:5A:10:27:CE:6D:99:64:01:90:9D:33
         SHA256: 7F:4B:03:CD:5F:2D:A8:5D:18:09:8B:EC:33:4A:37:9D:93:8E:1C:CD:69:98:C7:68:1E:13:3C:14:8C:BD:4A:5E

Signature algorithm name: SHA1withRSA (weak)

Subject Public Key Algorithm: 2048-bit RSA key

Version: 3

## Input Threshold
### TapMax
0.3f
### SwipeDp 
50f



## Target API Policy Page
https://support.google.com/googleplay/android-developer/answer/11926878 

"C:\Users\ainec\Documents\College\Semester 7\Mobile Game Dev\keystores\mygame-release.keystore"


## Build
Requirements: Unity 6000.6.0f1 with the Android Build Support module (including OpenJDK and Android SDK & NDK Tools), and `adb` (Android platform-tools) on your PATH.

1. Clone the repository:
```bash
git clone https://github.com/FinnClewes/FinnClewesMobileGame.git
``` 
2. Open Unity Hub, click **Add > Add project from disk**, and select the cloned folder. Open it with Unity 6000.6.0f1. The first import takes a few minutes.

3. Open **File > Build Profiles**, select **Android** and click **Switch Platform**.

4. Check **Project Settings > Player > Android > Other Settings**:
   - Scripting Backend: **IL2CPP**
   - Target Architectures: **ARM64** only
   - Package Name: `com.finnclewes.mobilegame`
   - Version: `0.2.0`
   - Bundle Version Code: `2`
   - Minimum API Level: [26] (Android 14)

5. In **File > Build Profiles > Android**, turn **Development Build** off and **Build App Bundle (Google Play)** off.

6. Click **Build** and save the file as `releases/MyGame-0.2.0-arm64.apk`.

7. Connect the phone with USB debugging on and install:
```
   adb install -r releases/MyGame-0.2.0-arm64.apk
```
Expected output: `Success`.

8. If you see `INSTALL_FAILED_UPDATE_INCOMPATIBLE`, run `adb uninstall com.setu.mygame` and install again.

## Device Targets
- **Minimum API level:** [26] (Android 14)
- **Architecture:** ARM64 (arm64-v8a)

## AI assistance
AI was used to further explain instructions if I was confused about the wording of a step or if i couldn't find the location of a setting in Unity

AI was also used to write the placeholder moving boxes script that was implemented for testing purposes

AI was used to format the build instructions