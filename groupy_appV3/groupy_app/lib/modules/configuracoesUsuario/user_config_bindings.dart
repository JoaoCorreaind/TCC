import 'package:get/get.dart';
import 'package:groupy_app/modules/configuracoesUsuario/user_config_controller.dart';

class UserConfigBindings implements BindingsInterface {
  @override
  void dependencies() {
    Get.lazyPut(() => UserConfigController());
  }
}
