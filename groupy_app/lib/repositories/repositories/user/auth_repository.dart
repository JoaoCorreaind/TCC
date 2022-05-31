import 'package:dio/dio.dart';
import 'package:groupy/models/user/auth.model.dart';
import 'package:groupy/repositories/interfaces/user/Iauth_repository.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../../../app/app.configs.dart';

class AuthRepository implements IAuthRepository {
  final dio = Dio();
  final url = URL + 'user/';
  @override
  Future<bool> DoLogin({required String email ,required String password}) async {
    try {
      SharedPreferences sharedPreferences = await SharedPreferences.getInstance();
      var login = new LoginDto(email: email, password: password);
      var response = await Dio().post('http://10.0.2.2:5000/api/user/login', data: {
        "email" : email, 
        "password" : password
      });
      if(response.statusCode == 200){
        sharedPreferences.setString('token', response.data.token);
        return true;
      }
      return false;
    } on DioError catch (err) {
      print(err.message);
      return false;
    }
  }
  
}