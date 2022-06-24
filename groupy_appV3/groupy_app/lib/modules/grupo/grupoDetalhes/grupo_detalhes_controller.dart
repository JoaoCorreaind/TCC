import 'package:get/get.dart';
import 'package:groupy_app/models/grupo.model.dart';
import 'package:groupy_app/models/user/user.model.dart';
import 'package:groupy_app/repositories/grupo/grupo_repository.dart';
import 'package:shared_preferences/shared_preferences.dart';

class GrupoDetalhesController extends GetxController {
  var grupoRepository = GrupoRepository();

  final grupo = Grupo().obs;
  final isParticipant = false.obs;
  setGrupo(Grupo set) {
    grupo(set);
  }

  setIsParticipant(List<User> participantes) async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    var idUsuario = prefs.getInt('user');
    if (participantes.any((element) => element.id == idUsuario)) {
      isParticipant(true);
    }
  }
}
