# UC / BR / FR → Module map

Use this when picking up a ticket: find your UC code here, go to the listed folder(s).
Full specs for every code are in `docs/requirements/SLMS_UC_BR_Requirements.docx`.

## B.1 Traveler — Mobile App (UC-C01–C19)
`mobile-app/lib/features/*`, backed by `backend/src/SLMS.Application/Modules/{Auth,Stations,Bookings,Payments,Iot}`

| UC | Use case | Mobile feature folder | Backend module |
|---|---|---|---|
| UC-C01–C05 | Register, Login, Logout, Recover Password, Manage Profile | `features/auth`, `features/profile` | `Modules/Auth` |
| UC-C06–C08 | Find/View Stations, Locker Availability | `features/stations` | `Modules/Stations` |
| UC-C09, C15, C16 | Create/Extend/Cancel Booking | `features/booking` | `Modules/Bookings` |
| UC-C10, C17 | Make Payment, Pay Overdue Fee | `features/payment` | `Modules/Payments` |
| UC-C11, C19 | Enroll/Delete Face | `features/face_enrollment` | `Modules/Auth` (FaceProfile) |
| UC-C12, C13, C14 | View Credentials / Booking Details / History | `features/credentials`, `features/booking` | `Modules/Bookings` |
| UC-C18 | Receive Notification | `features/notifications` | `Modules/Notifications` |

## B.2 Station / Kiosk (UC-K01–K11)
`kiosk-app/src/features/*`, backed by `backend/src/SLMS.Application/Modules/{Bookings,Lockers,Iot}`

| UC | Use case | Kiosk feature folder |
|---|---|---|
| UC-K01 | Start Kiosk Session | `features/session` (add when built) |
| UC-K02–K04 | Verify Identity (Face/QR/OTP/PIN) | `features/verify-identity` |
| UC-K05, K06 | Deposit / Retrieve Luggage | `features/deposit`, `features/retrieve` |
| UC-K07 | Pay Overdue Fee at Kiosk | `features/retrieve` |
| UC-K08, K09 | Offline Retrieval, Sync Offline Events | `features/retrieve` (offline mode) |
| UC-K10, K11 | Confirm Manual Retrieval, Request Support | `features/retrieve` |

## B.3 IoT Controller (UC-I01–I08)
`iot-firmware/src/main.cpp`

| UC | Use case |
|---|---|
| UC-I01, I02 | Connectivity (WiFi/MQTT+TLS), Heartbeat |
| UC-I03, I04 | Execute Unlock Command, Detect/Publish Door Events |
| UC-I05 | Detect Intrusion |
| UC-I06 | Verify Offline Credential and Unlock |
| UC-I07, I08 | Buffer/Replay Offline Events, Sync RTC |

## B.4 Administration & Operation (UC-A01–A15)
`web-admin/src/features/*`, backed by `backend/src/SLMS.Application/Modules/Admin`

| UC | Use case | Web admin feature folder |
|---|---|---|
| UC-A01, A02 | Login, Operations Dashboard | `features/dashboard` |
| UC-A03, A04 | Manage Station, Manage Locker | `features/stations`, `features/lockers` |
| UC-A05 | Manage Pricing Policy | `features/pricing` |
| UC-A06 | Manage Internal User and Role | `features/users` |
| UC-A07 | Monitor IoT Devices | `features/lockers` (device status tab) |
| UC-A08, A09, A10 | Incident, Remote Unlock, Emergency Retrieval | `features/incidents` |
| UC-A11 | Set Locker Maintenance | `features/lockers` |
| UC-A12 | Process Abandoned Property | `features/incidents` |
| UC-A13 | View Booking and Payment | `features/bookings` |
| UC-A14 | View Audit Log | `features/audit-log` |
| UC-A15 | Generate Operational Report | `features/dashboard` |

## Business Rule prefixes (BR-*) — where to enforce

- `BR-A*` (availability/allocation) → `Modules/Bookings` (slot calc + distributed lock)
- `BR-P*` (payment) → `Modules/Payments`
- `BR-S*` (security/access) → `Modules/Auth` + `Modules/Iot` (signed credentials, ownership checks)
- `BR-D*` (biometric data) → `Modules/Auth` (FaceProfile), encrypted-embedding-only storage
- `BR-O*` (offline behavior) → `kiosk-app`, `iot-firmware`, `mobile-app` (cached credentials)
- `BR-T*` (time/overdue) → `Modules/Bookings` + scheduled jobs (overdue fee accrual, expiry sweep)
