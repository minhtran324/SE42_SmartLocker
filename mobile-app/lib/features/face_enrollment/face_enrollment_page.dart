import 'package:flutter/material.dart';

/// UC-C11: Enroll Face (quality + passive liveness check, encrypted embedding only — BR-D01-03)
/// UC-C19: Delete Biometric Data
class FaceEnrollmentPage extends StatelessWidget {
  const FaceEnrollmentPage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: Text('TODO (UC-C11): camera capture + quality/liveness check, POST to backend'),
      ),
    );
  }
}
