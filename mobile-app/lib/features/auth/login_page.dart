import 'package:flutter/material.dart';

/// UC-C01: Register Account · UC-C02: Log In · UC-C03: Log Out
/// UC-C04: Recover Password · UC-C05: Manage Profile (see features/profile)
class LoginPage extends StatelessWidget {
  const LoginPage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: Text('TODO (UC-C02): email/phone + password form, calls POST /api/auth/login'),
      ),
    );
  }
}
