import 'dart:convert';

import 'package:dio/dio.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../application/rest_client/rest_client.dart';
import 'iauth_repository.dart';

class AuthRepository implements IAuthRepository {
  @override
  Future<bool> doLogin({required email, required password}) async {
    try {
      SharedPreferences sharedPreferences =
          await SharedPreferences.getInstance();
      var response = await RestClient()
          .dio
          .post('user/login', data: {"email": email, "password": password});
      if (response.statusCode == 200) {
        sharedPreferences.setString('token', response.data['token']);
        sharedPreferences.setInt('user', response.data['user']['id']);
        print(sharedPreferences.getInt('user'));

        return true;
      }
      return false;
    } on DioError catch (err) {
      print(err.message);
      return false;
    }
  }
}
