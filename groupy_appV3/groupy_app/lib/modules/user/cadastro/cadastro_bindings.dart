import 'package:get/get.dart';
import 'package:groupy_app/modules/user/cadastro/cadastro_controller.dart';

class CadastroBindings implements BindingsInterface {
  @override
  void dependencies() {
    Get.lazyPut(() => CadastroController());
  }
}
