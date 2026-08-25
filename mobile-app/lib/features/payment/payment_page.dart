import 'package:flutter/material.dart';

/// UC-C10: Make Payment · UC-C17: Pay Overdue Fee
/// Never treat a return-from-gateway as success — poll for the backend-confirmed
/// webhook result instead (BR-P02).
class PaymentPage extends StatelessWidget {
  const PaymentPage({super.key});

  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      body: Center(
        child: Text('TODO (UC-C10): open gateway checkout, poll booking status up to 60s'),
      ),
    );
  }
}
