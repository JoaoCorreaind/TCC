import 'package:get/get.dart';

import 'login_controller.dart';

class LoginBindings implements BindingsInterface {
  @override
  void dependencies() {
    Get.lazyPut(() => LoginController());
  }
}
