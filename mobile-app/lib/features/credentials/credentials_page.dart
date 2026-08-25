import 'package:flutter/material.dart';

/// UC-C12: View Access Credentials — QR (refreshed every 5 min online) + 30s TOTP + PIN.
/// Must render from SecureStorage.readOfflineCredential when there's no network (BR-O09).
class CredentialsPage extends StatelessWidget {
  const CredentialsPage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: Text('TODO (UC-C12): QR + TOTP countdown + PIN, with offline fallback'),
      ),
    );
  }
}
