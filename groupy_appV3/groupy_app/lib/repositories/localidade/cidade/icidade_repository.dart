import 'package:groupy_app/models/cidade_model.dart';

abstract class ICidadeRepository {
  Future<List<Cidade>> getAll();
  Future<Cidade?> getOne(String id);
  Future<Cidade?> getByUser(String userId);
  Future<List<Cidade>> getByUf(String uf);
}
