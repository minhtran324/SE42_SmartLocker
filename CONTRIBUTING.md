# Contributing — SLMS (Team SE_42)

## Branching
- `main` — always deployable/demoable.
- `develop` — integration branch (optional, adopt if the team wants a staging gate).
- Feature branches: `feature/<uc-code>-<short-name>`, e.g. `feature/uc-c09-create-booking`.
- Fix branches: `fix/<short-name>`.

## Commits
Use [Conventional Commits](https://www.conventionalcommits.org/):
```
feat(booking): implement distributed lock for slot allocation (UC-C09)
fix(kiosk): correct offline TOTP expiry check (UC-K08)
docs(readme): add docker compose instructions
```

## Pull requests
- Reference the UC/BR/FR code(s) implemented in the PR description.
- Keep PRs scoped to one use case or module where possible.
- Checklist before requesting review:
  - [ ] Builds locally (`dotnet build`, `npm run build`, `flutter analyze`, or `pio run` as applicable)
  - [ ] Linked to the relevant UC in `docs/module-map.md`
  - [ ] No secrets committed (check `.env`, `appsettings.Development.json`)
  - [ ] Tests added/updated where logic changed

## Code style
- **Backend (C#)**: nullable enabled, async all the way down, no business logic in controllers —
  controllers call `Application` services only.
- **Web (TypeScript/React)**: function components + hooks, colocate API calls under `src/api`.
- **Mobile (Flutter/Dart)**: one feature = one folder under `lib/features`, `flutter analyze` clean.
- **Firmware (C++)**: no blocking delays in the main loop where avoidable; MQTT reconnect must be
  non-blocking (UC-I01).

## Environment & secrets
Copy `.env.example` to `.env` and fill in local values. Never commit real Payment Gateway keys,
Face Recognition API keys, or Google Maps API keys — use `.env` / `appsettings.Development.json`,
both git-ignored.
