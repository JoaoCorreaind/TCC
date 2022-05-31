import 'package:groupy/models/grupo.model.dart';

abstract class IGrupoRepository {
  Future<Grupo?> GetById({required String id});
  Future<List<Grupo>?> GetAll();
  Future Delete ({required String id});
  Future Update({required String id, required Grupo grupo});
}