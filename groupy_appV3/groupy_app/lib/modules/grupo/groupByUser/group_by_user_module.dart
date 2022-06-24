import 'package:get/get_navigation/src/routes/get_route.dart';
import 'package:groupy_app/modules/grupo/groupByUser/group_by_user_page.dart';
import 'package:groupy_app/modules/home/home_page.dart';

import '../../../application/module/module.dart';

class GroupByUserModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(name: '/group/byUser', page: () => GroupByUserPage()),
  ];
}
