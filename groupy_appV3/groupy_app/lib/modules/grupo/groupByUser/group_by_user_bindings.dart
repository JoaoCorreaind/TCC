import 'package:get/get.dart';
import 'package:groupy_app/modules/grupo/groupByUser/group_by_user_controller.dart';

class GroupByUserBindings implements BindingsInterface {
  @override
  void dependencies() {
    Get.lazyPut(() => GroupByUserController());
  }
}
