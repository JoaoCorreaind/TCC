import 'package:get/get.dart';
import 'package:groupy_app/models/grupo.model.dart';
import 'package:groupy_app/models/user/user.model.dart';
import 'package:groupy_app/repositories/grupo/grupo_repository.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../../repositories/user/user_repository.dart';

class GroupManegeUsersController extends GetxController {
  var grupoRepository = GrupoRepository();
  var userRepository = UserRepository();
  Rx<List<User>> users = Rx<List<User>>([]);

  @override
  void onInit() async {
    super.onInit();
  }

  getUsers(idGrupo) async {
    users.value = await grupoRepository.getMembers(idGrupo: idGrupo);
  }
}
