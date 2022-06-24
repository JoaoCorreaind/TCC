import 'package:get/get_navigation/src/routes/get_route.dart';
import 'package:groupy_app/modules/grupo/groupByLeader/group_by_Leader_page.dart';

import '../../../application/module/module.dart';

class GroupByLeaderModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(name: '/group/byLeader', page: () => GroupByLeaderPage()),
  ];
}
