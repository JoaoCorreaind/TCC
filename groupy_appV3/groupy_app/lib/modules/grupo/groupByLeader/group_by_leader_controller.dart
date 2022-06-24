import 'package:get/get.dart';
import 'package:groupy_app/models/grupo.model.dart';
import 'package:groupy_app/repositories/grupo/grupo_repository.dart';
import 'package:groupy_app/repositories/user/user_repository.dart';
import 'package:shared_preferences/shared_preferences.dart';

class GroupByLeaderController extends GetxController {
  Rx<List<Grupo>> grupos = Rx<List<Grupo>>([]);
  var grupoRepository = GrupoRepository();
  var userRepository = UserRepository();

  @override
  void onInit() async {
    super.onInit();
    getInitialValues();
  }

  @override
  void onClose() {}

  void filterPlayer(String title) {
    List<Grupo> results = [];
    if (title.isEmpty) {
      results = grupos.value;
    } else {
      results = grupos.value
          .where((element) => element.title!.contains(title.toLowerCase()))
          .toList();
    }
    grupos.value = results;
  }

  getInitialValues() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    int? id = prefs.getInt('user');
    if (id != null) {
      grupos.value = await grupoRepository.getGroupsByleader(id: id.toString());
    } else {
      Get.offAllNamed('/login');
      Get.snackbar("Erro",
          "Usuário não encontrado no armazenamento interno, tente logar novamente");
    }
  }
}
