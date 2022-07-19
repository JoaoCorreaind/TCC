import 'package:groupy_app/application/rest_client/rest_client.dart';
import 'package:groupy_app/models/cidade_model.dart';
import 'package:groupy_app/repositories/localidade/cidade/icidade_repository.dart';

class CidadeRepository implements ICidadeRepository {
  @override
  Future<List<Cidade>> getAll() async {
    var response = await RestClient().dio.get('/Cidade');
    List<Cidade> listCidade = [];
    for (var json in response.data as List) {
      final cidade = Cidade.fromJson(json);
      listCidade.add(cidade);
    }
    return listCidade;
  }

  @override
  Future<List<Cidade>> getByUf(String uf) async {
    var response = await RestClient().dio.get('/Cidade/uf/' + uf);
    List<Cidade> listCidade = [];
    for (var json in response.data as List) {
      final cidade = Cidade.fromJson(json);
      listCidade.add(cidade);
    }
    return listCidade;
  }

  @override
  Future<Cidade?> getByUser(String userId) async {
    var response = await RestClient().dio.get('/Cidade/user' + userId);
    if (response.statusCode == 200) {
      return Cidade.fromJson(response.data);
    }
    return null;
  }

  @override
  Future<Cidade?> getOne(String id) async {
    var response = await RestClient().dio.get('/Cidade/' + id);
    if (response.statusCode == 200) {
      return Cidade.fromJson(response.data);
    }
    return null;
  }
}
