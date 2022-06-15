import 'package:get/get.dart';
import 'package:groupy_app/modules/home/home_controller.dart';

class HomeBindings implements BindingsInterface {
  @override
  void dependencies() {
    Get.lazyPut(() => HomeController());
  }
}
