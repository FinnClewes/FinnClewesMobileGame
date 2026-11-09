# Economy Telemetry Map

| Event | Parameters (type) | Fires when | Question it answers |
|---|---|---|---|
| session_start | app_version (string), device_model (string), android (string) | first frame of Boot | which build and device produced this log |
| level_start | level_id (int), attempt (int), coins (int) | combat scene starts, before the first enemy spawns | how many retries; entry wealth |
| level_complete | level_id (int), time_s (float), coins_earned (int), score (int) | last enemy killed | run length and earn rate on success |
| level_fail | level_id (int), time_s (float), cause (id), coins_earned (int) | player HP reaches 0 | where and why players die; earn rate on failure |
| upgrade_purchased | upgrade_id (id), level (int), cost (int), coins_left (int) | inside Wallet.TryUpgrade on success | when the first upgrade lands; spend pacing |

Events are written to logcat and to `telemetry.log` in `Application.persistentDataPath` on the device; nothing is sent off the device. The same statement appears in `docs/CA1/privacy-statement.md`.