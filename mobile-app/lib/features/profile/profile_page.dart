import 'package:flutter/material.dart';

/// UC-C05: Manage Profile. Phone number changes require OTP re-verification;
/// email can't be self-edited (must go through support).
class ProfilePage extends StatelessWidget {
  const ProfilePage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: Text('TODO (UC-C05): profile view/edit form, change password'),
      ),
    );
  }
}
