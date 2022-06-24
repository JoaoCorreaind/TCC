import 'package:dio/dio.dart';
import 'package:signalr_netcore/hub_connection.dart';

class RestClient {
  final dio = Dio();

  RestClient() {
    dio.options.baseUrl = "http://10.0.2.2:5000/api/";
  }
}
