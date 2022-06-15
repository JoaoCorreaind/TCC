import 'package:get/get.dart';
import 'package:groupy_app/modules/cadastroGrupo/cadastro_grupo_controller.dart';

class CadastroGrupoBindings implements BindingsInterface {
  @override
  void dependencies() {
    Get.lazyPut(() => CadastroGrupoController());
  }
}
