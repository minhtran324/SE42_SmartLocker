# SLMS — Architecture Overview

## Component diagram (logical)

```
                       ┌───────────────────────┐
                       │   Payment Gateway      │  VNPAY / PayOS
                       │ (webhook + signature)  │
                       └───────────▲────────────┘
                                   │ webhook
┌────────────┐   REST/HTTPS   ┌────┴─────────────────────────┐   REST/HTTPS   ┌───────────────┐
│ Mobile App │◄──────────────►│                               │◄──────────────►│  Web Admin     │
│ (Traveler) │                │        Backend (SLMS.Api)     │                │ (Admin/Op/Tech)│
└────────────┘                │  Auth · Stations · Bookings   │                └───────────────┘
                               │  Payments · Lockers · Iot     │
┌────────────┐   REST/HTTPS   │  Notifications · Admin        │   REST/HTTPS   ┌───────────────┐
│ Station    │◄──────────────►│                               │◄──────────────►│ Face Recognition│
│ Kiosk App  │                │  Redis: distributed locks,    │                │ Engine (external)│
└─────┬──────┘                │         offline-credential    │                └───────────────┘
      │ cached QR/OTP/PIN     │         cache                 │
      │ (offline mode)        │  Postgres: bookings, users,   │   REST/HTTPS   ┌───────────────┐
      │                       │            stations, lockers  │◄──────────────►│ Map Service    │
      │ MQTT (TLS)            │  MQTT client: locker commands │                │ (Google Maps)  │
      │                       │               & device events │                └───────────────┘
      ▼                       └──────────────┬────────────────┘
┌────────────┐    MQTT (TLS)                 │ push/email/SMS
│ ESP32 Locker│◄─────────────────────────────►│  Notification Provider
│ Controller  │        (via Mosquitto broker) └────────────────
└────────────┘
```

## Why these boundaries

- **Backend** owns every business decision (locker allocation, payment verification, credential
  issuance). Kiosk and ESP32 never grant access on their own authority — they only ever act on a
  signed command or a cached, pre-authorized offline credential (BR-O09, UC-K08, UC-I06).
- **Kiosk app** is a thin verification/orchestration layer: face/QR/OTP/PIN capture, talks to the
  backend when online, falls back to cached signed credentials when offline, and replays buffered
  events once connectivity returns (UC-K09).
- **ESP32 firmware** only understands: connect, heartbeat, receive UNLOCK command (with requestId +
  expiry), read the door limit switch, publish door/heartbeat/intrusion events, and verify an
  offline credential against its RTC. It never talks to Postgres/business logic directly — MQTT only.
- **Mobile app** is the traveler-facing surface for the whole booking lifecycle plus offline access
  credentials (QR/TOTP/PIN cached in secure storage so a traveler can retrieve luggage without
  network, per UC-C12).
- **Web admin** covers station/locker/pricing management, IoT device monitoring, incident and
  emergency-retrieval handling, abandoned-property workflow, audit log, and reporting (UC-A01–A15).

## Key cross-cutting mechanisms to implement early

| Mechanism | Where | Why |
|---|---|---|
| Distributed lock (`lock:alloc:{stationId}:{size}`, Redis, TTL 5s, Lua-script release) | `backend/src/SLMS.Infrastructure/Locking` | Prevents double-booking the same slot (UC-C09) |
| Idempotent payment webhook handling by `orderCode` | `backend/src/SLMS.Application/Modules/Payments` | Payment Gateway may retry webhooks (UC-C10) |
| Signed offline credential (QR/TOTP seed/PIN) + RTC-based expiry check on-device | `iot-firmware`, `kiosk-app`, `mobile-app`, `backend` | Lets travelers retrieve luggage with no internet (UC-K08, UC-I06) |
| Role-based access (Admin / Operator / Technician) with segregation of duties (an Operator cannot approve their own request) | `backend` Auth module, `web-admin` | UC-A09, UC-A12 |
| Audit log for every sensitive action (remote unlock, incident handling, abandoned-property approval) | `backend/src/SLMS.Application/Modules/Admin` | UC-A14, BR-S04 |

## Data model starting point

See entities under `backend/src/SLMS.Domain/Entities`: `User`, `Station`, `Locker`, `Booking`,
`Payment`, `FaceProfile`, `AccessCredential`, `Incident`, `AuditLog`. Statuses/enums under
`backend/src/SLMS.Domain/Enums` reflect the state machines described in the SRS (e.g. Booking:
`PendingPayment → Confirmed → Stored → Completed`, with `Cancelled`/`Expired` branches).
