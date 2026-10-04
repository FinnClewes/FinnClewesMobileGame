# Privacy Statement
## Data Collected
None

## Data Stored on the Device
- PlayerPrefs keys (settings only, no personal data):
  - `[haptics key]`: whether haptic feedback is on (0 or 1)
  - `[text scale key]`: the player's chosen text size (a number)
- Save files: none yet
- Local telemetry log (Week 6): stored on the device only and never uploaded
- How a player removes it: uninstall the app, which deletes all of the above

## Network Activity
None. The game makes no network requests and contacts no endpoints.
The INTERNET permission is declared because Unity's built-in analytics
module (com.unity.modules.unityanalytics) is enabled in the project. No
game code uses it, no analytics are initialised, and no data is sent.

## Third-party SDKs
None

## Permissions requested
- android.permission.INTERNET
    - required by unity analytics
- android.permission.VIBRATE
    - required for haptics functionality (not vital to gameplay)
- com.finnclewes.mobilegame.DYNAMIC_RECEIVER_NOT_EXPORTED_PERMISSION

## Google Play Declaration Mapping
| Your heading | Location on Play form | Declaration |
|---|---|---|
| Data collected | Data safety > Data collection and security practices: "Does your app collect or share any of the required user data types?" | No. None collected. |
| Data stored on the device | Not asked directly. It matters only if data leaves the device. The form covers data collected and sent off the device. | Local-only saves and PlayerPrefs are not "collected", so nothing to declare. |
Network activity | Data collection and security practices: data in transit encryption question (only if data is collected). Also the basis for the "collected" answer above. | No data is sent, so no endpoints to declare. |
Third-party SDKs | Data sharing: SDKs that collect data count as sharing or collecting on your behalf. | None, so nothing shared.
| Permissions requested | Not on the Data safety form itself. Permissions appear in the manifest and Play Console checks them against your declarations. | INTERNET and VIBRATE are declared but no data is sent. |
| Privacy policy | Store listing > App content > Privacy policy URL | Required for Play. You'd host this statement (or a version of it) at a public URL.

**Release tracks:** the Data safety form must be completed before release on
the closed testing, open testing or production tracks. The internal testing
track is exempt.