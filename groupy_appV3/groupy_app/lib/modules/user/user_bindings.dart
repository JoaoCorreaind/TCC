import 'package:get/get.dart';

import 'user_controller.dart';

class UserBindings implements BindingsInterface {
  @override
  void dependencies() {
    Get.lazyPut(() => UserController());
  }
}
