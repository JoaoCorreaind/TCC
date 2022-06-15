import '../../models/grupo.model.dart';

abstract class IGrupoRepository {
  Future<List<Grupo>> getAll();
  Future<Grupo> getById({required String id});
  Future<void> create({required Grupo grupo});
  Future<void> update({required String id, required Grupo grupo});
}
