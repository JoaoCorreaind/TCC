import 'package:get/get.dart';
import 'package:groupy_app/modules/grupo/groupByLeader/group_by_leader_controller.dart';

class GroupByLeaderBindings implements BindingsInterface {
  @override
  void dependencies() {
    Get.lazyPut(() => GroupByLeaderController());
  }
}
