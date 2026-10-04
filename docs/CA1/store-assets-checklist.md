# Store Assets Checklist
   | Asset | Spec | Status | Notes |
   |-------|------|--------|-------|
   | App icon | 512x512, 32-bit PNG with alpha, up to 1024 KB | planned | no rounded corners, Play masks it |
   | Feature graphic | 1024x500, JPEG or 24-bit PNG, no alpha | planned | shown above a listing video |
   | Phone screenshots | 4; each side 1080 to 7680 px; JPEG or 24-bit PNG; 9:16 | planned | gameplay first |

## Screenshot Plan
Orientation: Portrait (9:16)

| Shot | Working caption | Must be visible |
|---|---|---|
| 1 | "Fight your enemies" | level 1, mid-run, score visible, no UI overlap with the notch |
| 2 | "Arrange your loadout" | enemies already on board, player ally selected, placement selection UI |
| 3 | "Defeat your enemies" | [that feature on screen] |
| 4 | "Explore and fight in new places" | level 2, mid-run |

## Where assets come from

- **App icon:** exported from the key art in MDA, cropped square and scaled to 512x512. Export as PNG with alpha and check the file is under 1024 KB.
- **Feature graphic:** composed from the same key art plus the game logo at 1024x500. Export with no transparency.
- **Screenshots:** in-game capture on the phone from the release build, at 1080 px or more on the short side. Pulled with `adb exec-out screencap -p > shot1.png`