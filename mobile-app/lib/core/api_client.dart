import 'dart:convert';
import 'package:http/http.dart' as http;

/// Thin REST client for the SLMS backend. Attach the access token (UC-C02) once
/// auth state management is wired up.
class ApiClient {
  ApiClient({this.baseUrl = 'http://localhost:5080/api'});

  final String baseUrl;

  Future<Map<String, dynamic>> get(String path) async {
    final response = await http.get(Uri.parse('$baseUrl$path'));
    _throwIfError(response);
    return jsonDecode(response.body) as Map<String, dynamic>;
  }

  Future<Map<String, dynamic>> post(String path, Map<String, dynamic> body) async {
    final response = await http.post(
      Uri.parse('$baseUrl$path'),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode(body),
    );
    _throwIfError(response);
    return jsonDecode(response.body) as Map<String, dynamic>;
  }

  void _throwIfError(http.Response response) {
    if (response.statusCode >= 400) {
      throw Exception('API request failed: ${response.statusCode} ${response.body}');
    }
  }
}
