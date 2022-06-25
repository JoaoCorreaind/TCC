import 'package:get/get.dart';

import '../configuracoesUsuario/user_config_controller.dart';

class UserConfigBindings implements BindingsInterface {
  @override
  void dependencies() {
    Get.lazyPut(() => UserConfigController());
  }
}
