import 'package:get/get.dart';
import 'package:groupy_app/repositories/grupo/grupo_repository.dart';
import 'package:groupy_app/repositories/user/user_repository.dart';

import '../../models/grupo.model.dart';

class HomeController extends GetxController {
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
    grupos.value = await grupoRepository.getAll();
  }
}
