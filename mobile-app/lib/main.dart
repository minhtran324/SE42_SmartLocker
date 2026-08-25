import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

import 'features/auth/login_page.dart';
import 'features/stations/stations_page.dart';
import 'features/booking/booking_page.dart';
import 'features/payment/payment_page.dart';
import 'features/face_enrollment/face_enrollment_page.dart';
import 'features/credentials/credentials_page.dart';
import 'features/notifications/notifications_page.dart';
import 'features/profile/profile_page.dart';

void main() {
  runApp(const SlmsApp());
}

final _router = GoRouter(
  initialLocation: '/login',
  routes: [
    GoRoute(path: '/login', builder: (context, state) => const LoginPage()),
    GoRoute(path: '/stations', builder: (context, state) => const StationsPage()),
    GoRoute(path: '/booking', builder: (context, state) => const BookingPage()),
    GoRoute(path: '/payment', builder: (context, state) => const PaymentPage()),
    GoRoute(path: '/face-enrollment', builder: (context, state) => const FaceEnrollmentPage()),
    GoRoute(path: '/credentials', builder: (context, state) => const CredentialsPage()),
    GoRoute(path: '/notifications', builder: (context, state) => const NotificationsPage()),
    GoRoute(path: '/profile', builder: (context, state) => const ProfilePage()),
  ],
);

class SlmsApp extends StatelessWidget {
  const SlmsApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(
      title: 'SLMS — SmartLocker',
      theme: ThemeData(colorSchemeSeed: Colors.indigo, useMaterial3: true),
      routerConfig: _router,
    );
  }
}
