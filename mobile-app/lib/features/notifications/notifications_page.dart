import 'package:flutter/material.dart';

/// UC-C18: Receive Notification. Channel matrix (push/email/SMS per event) is in the SRS.
/// Every notification must remain visible in-app even if the push delivery fails (E1).
class NotificationsPage extends StatelessWidget {
  const NotificationsPage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: Text('TODO (UC-C18): notification list + deep link to related booking'),
      ),
    );
  }
}
