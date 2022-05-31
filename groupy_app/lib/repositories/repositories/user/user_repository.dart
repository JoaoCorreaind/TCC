import 'package:dio/dio.dart';
import 'package:groupy/models/user/user.model.dart';

import '../../../app/app.configs.dart';
import '../../interfaces/user/Iuser_repository.dart';

class UserRepository implements IUserRepository {
  final dio = Dio();
  final url = URL + 'user/';
  @override
  Future Delete({required String id}) async {
    try {
      await dio.delete(url + id);
    } on DioError catch (err) {
      print('Erro ao realizar update ${err.response?.statusMessage}');
    }
  }

  @override
  Future<List<User>?> GetAll() async  {
    try {
      final response = await dio.get(url);
      final list = response.data as List;
      List<User> listGrupo = [];
      for(var json in list){
        final grupo = User.fromJson(json);
        listGrupo.add(grupo);
      }
      return listGrupo;
    } on DioError catch (err) {
      print('Erro ao realizar update ${err.response?.statusMessage}');
    }
    
  }

  @override
  Future<User?> GetById({required String id}) async {
    try {
      final response = await dio.get(url + id);
      return User.fromJson(response.data);
    } on DioError catch (err) {
      print('Erro ao buscar usuario ${err.response?.statusMessage}');
    }
  }

  @override
  Future Update({required String id, required User user}) async {
    try {
      await dio.put(url + id, data : user); 
    } on DioError catch (err) {
      print('Erro ao realizar update ${err.response?.statusMessage}');
    }
  }
}