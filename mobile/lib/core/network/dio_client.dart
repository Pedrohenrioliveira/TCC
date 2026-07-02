import 'dart:io';
import 'package:dio/dio';
import 'package:flutter/foundation.dart';

class DioClient {
  final Dio _dio;

  DioClient() : _dio = Dio() {
    // Escolhe a URL com base na plataforma (Android Emulator usa 10.0.2.2)
    String baseUrl = 'http://localhost:5200/api/';
    if (!kIsWeb && Platform.isAndroid) {
      baseUrl = 'http://10.0.2.2:5200/api/';
    }

    _dio.options = BaseOptions(
      baseUrl: baseUrl,
      connectTimeout: const Duration(seconds: 15),
      receiveTimeout: const Duration(seconds: 15),
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
      },
    );

    // Adiciona Interceptadores para Logging e Tratamento de Erros
    _dio.interceptors.add(
      InterceptorsWrapper(
        onRequest: (options, handler) {
          if (kDebugMode) {
            print('REQUEST[${options.method}] => PATH: ${options.path}');
            print('HEADERS: ${options.headers}');
            print('BODY: ${options.data}');
          }
          return handler.next(options);
        },
        onResponse: (response, handler) {
          if (kDebugMode) {
            print('RESPONSE[${response.statusCode}] => PATH: ${response.requestOptions.path}');
            print('DATA: ${response.data}');
          }
          return handler.next(response);
        },
        onError: (DioException e, handler) {
          if (kDebugMode) {
            print('ERROR[${e.response?.statusCode}] => PATH: ${e.requestOptions.path}');
            print('MESSAGE: ${e.message}');
          }
          return handler.next(e);
        },
      ),
    );
  }

  Dio get dio => _dio;
}
