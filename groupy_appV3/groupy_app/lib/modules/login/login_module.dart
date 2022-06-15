import 'package:get/get_navigation/src/routes/get_route.dart';
import 'package:groupy_app/modules/login/login_page.dart';

import '../../application/module/module.dart';

class LoginModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(name: '/login', page: () => LoginPage()),
  ];
}
