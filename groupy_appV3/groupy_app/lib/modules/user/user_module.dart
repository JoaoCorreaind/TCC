import 'package:get/get_navigation/src/routes/get_route.dart';
import 'package:groupy_app/modules/login/login_page.dart';
import 'package:groupy_app/modules/user/user_page.dart';

import '../../application/module/module.dart';

class UserModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(name: '/user', page: () => UserPage()),
  ];
}
