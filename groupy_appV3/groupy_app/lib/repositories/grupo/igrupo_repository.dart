import 'package:groupy_app/models/user/user.model.dart';

import '../../models/grupo.model.dart';

abstract class IGrupoRepository {
  Future<List<Grupo>> getAll();
  Future<Grupo?> getOne({required String id});
  Future<List<Grupo>> getGroupsByUser({required String id});
  Future<List<Grupo>> getGroupsByleader({required String id});
  Future<void> addMember({required String idUsuario, required String idGrupo});
  Future<void> removeMember(
      {required String idUsuario, required String idGrupo});
  Future<void> create({required Grupo grupo});
  Future<void> update({required Grupo grupo});
  Future<List<User>> getMembers({required String idGrupo});
}
