import 'package:flutter_secure_storage/flutter_secure_storage.dart';

/// UC-C12 / BR-O09: access tokens AND the offline access credential (signed QR payload,
/// TOTP seed, PIN) must live here so a traveler can retrieve luggage with no network.
/// UC-C03: Log Out must wipe every key below.
class SecureStorage {
  SecureStorage() : _storage = const FlutterSecureStorage();

  final FlutterSecureStorage _storage;

  static const _accessTokenKey = 'access_token';
  static const _refreshTokenKey = 'refresh_token';
  static const _offlineCredentialPrefix = 'offline_credential_';

  Future<void> saveTokens({required String accessToken, required String refreshToken}) async {
    await _storage.write(key: _accessTokenKey, value: accessToken);
    await _storage.write(key: _refreshTokenKey, value: refreshToken);
  }

  Future<void> saveOfflineCredential(String bookingId, String signedPayload) =>
      _storage.write(key: '$_offlineCredentialPrefix$bookingId', value: signedPayload);

  Future<String?> readOfflineCredential(String bookingId) =>
      _storage.read(key: '$_offlineCredentialPrefix$bookingId');

  /// UC-C03: clear all tokens and cached credentials on logout.
  Future<void> clearAll() => _storage.deleteAll();
}
