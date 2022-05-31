import 'package:groupy/app/app.configs.dart';
import 'package:groupy/models/grupo.model.dart';
import 'package:groupy/repositories/interfaces/Igrupo.repository.dart';
import 'package:dio/dio.dart';
class GrupoRepository implements IGrupoRepository {
  final dio = Dio();
  final url = URL + 'grupo/';
  @override
  Future Delete({required String id}) async {
    try {
      await dio.delete(url + id);
    } on DioError catch (err) {
      print('Erro ao realizar update ${err.response?.statusMessage}');
    }
  }

  @override
  Future<List<Grupo>?> GetAll() async  {
    try {
      final response = await dio.get(url);
      final list = response.data as List;
      List<Grupo> listGrupo = [];
      for(var json in list){
        final grupo = Grupo.fromJson(json);
        listGrupo.add(grupo);
      }
      return listGrupo;
    } on DioError catch (err) {
      print('Erro ao realizar update ${err.response?.statusMessage}');
    }
    
  }

  @override
  Future<Grupo?> GetById({required String id}) async {
    try {
      final response = await dio.get(url + id);
      return Grupo.fromJson(response.data);
    } on DioError catch (err) {
      print('Erro ao buscar grupo ${err.response?.statusMessage}');
    }
  }

  @override
  Future Update({required String id, required Grupo grupo}) async {
    try {
      await dio.put(url + id, data : grupo); 
    } on DioError catch (err) {
      print('Erro ao realizar update ${err.response?.statusMessage}');
    }
  }
}