# SLMS Mobile App (Traveler)

Flutter app covering UC-C01–C19 (see `docs/module-map.md` at the repo root).

This folder ships only `lib/`, `pubspec.yaml`, and `analysis_options.yaml` — the platform
folders (`android/`, `ios/`, etc.) are generated locally so each contributor's Flutter/toolchain
version is used instead of one committed to git:

```bash
cd mobile-app
flutter create --project-name slms_mobile --org com.se42.slms .
flutter pub get
flutter run
```

`flutter create` will not overwrite `lib/main.dart` or `pubspec.yaml` if they already look like a
real project — if it complains, generate into a scratch folder and copy just the platform
directories (`android/`, `ios/`, `web/`, etc.) over.

## Structure

```
lib/
  main.dart
  core/            # api client, secure storage wrapper (offline credential cache — BR-O09)
  features/
    auth/          # UC-C01–C05
    stations/      # UC-C06–C08
    booking/       # UC-C09, C13-C17
    payment/       # UC-C10, C17
    face_enrollment/  # UC-C11, C19
    credentials/   # UC-C12 (QR/TOTP/PIN, works offline)
    notifications/ # UC-C18
    profile/       # UC-C05
```
