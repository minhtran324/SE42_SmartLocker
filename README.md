# SmartLocker (SLMS)

An IoT-Driven Autonomous Luggage Storage Management System — FA26 Capstone, Team SE_42.

This repository is a **starter scaffold** for the whole team: the folder structure, module
boundaries, and placeholder code all mirror the Actors / Use Cases / Business Rules defined in
[`docs/requirements/SLMS_UC_BR_Requirements.docx`](docs/requirements/SLMS_UC_BR_Requirements.docx)
(the shared reference for Report 3 — SRS). Nothing here is production logic; every module contains
`TODO` markers tagged with the UC/BR/FR codes they must implement.

## System overview

SLMS lets travelers book, pay for, and access lockers at unmanned stations using QR/OTP/PIN or
face recognition, opening lockers via ESP32-controlled solenoids over MQTT. Staff manage stations,
lockers, pricing, incidents, and abandoned-property handling from a web admin console.

| Component | Covers | Tech |
|---|---|---|
| [`backend/`](backend) | Auth, Stations, Bookings, Payments, Lockers, IoT command/event handling, Notifications, Admin APIs | ASP.NET Core (C#) |
| [`web-admin/`](web-admin) | UC-A01–A15 — Admin/Operator/Technician console | React + Vite + TypeScript |
| [`kiosk-app/`](kiosk-app) | UC-K01–K11 — Station Kiosk tablet UI | React + Vite + TypeScript |
| [`mobile-app/`](mobile-app) | UC-C01–C19 — Traveler mobile app | Flutter |
| [`iot-firmware/`](iot-firmware) | UC-I01–I08 — ESP32 locker controller | PlatformIO / C++ |
| [`infra/`](infra) | Local MQTT broker config for dev | Mosquitto |

See [`docs/architecture.md`](docs/architecture.md) for the system diagram and data flow, and
[`docs/module-map.md`](docs/module-map.md) for exactly which UC/BR/FR codes map to which folder.

## Actors (from the SRS)

Guest, Traveler, System Administrator, Station Operator, Technician, Station Kiosk (device),
IoT Locker Controller (device), Payment Gateway (VNPAY/PayOS), Face Recognition Engine,
Map Service (Google Maps), Notification Provider. Full descriptions: `docs/requirements`.

## Getting started (local dev)

### Prerequisites
- [.NET SDK 8+](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org) and npm
- [Flutter SDK 3.22+](https://flutter.dev)
- [PlatformIO](https://platformio.org) (VS Code extension or CLI) for the ESP32 firmware
- Docker Desktop (for Postgres, Redis, Mosquitto)

### 1. Start local infrastructure
```bash
cp .env.example .env
docker compose up -d
```
This starts PostgreSQL (bookings, stations, users…), Redis (distributed locks, caching),
and a Mosquitto MQTT broker (kiosk ↔ backend ↔ ESP32 messaging).

### 2. Run the backend
```bash
cd backend
dotnet restore
dotnet run --project src/SLMS.Api
```
Swagger UI: `http://localhost:5080/swagger`

### 3. Run the web admin console
```bash
cd web-admin
npm install
npm run dev
```

### 4. Run the kiosk app
```bash
cd kiosk-app
npm install
npm run dev
```

### 5. Run the mobile app
```bash
cd mobile-app
flutter pub get
flutter run
```

### 6. Build/flash the IoT firmware
```bash
cd iot-firmware
pio run           # build
pio run -t upload # flash to ESP32
```

## Repository conventions

See [`CONTRIBUTING.md`](CONTRIBUTING.md) for branch naming, commit message format, and PR checklist.

## Project info

- Project code: **SLMS** — SmartLocker
- Team: **SE_42**, FA26 Capstone
- Source spec: SmartLocker Final Project Specification v2.0
