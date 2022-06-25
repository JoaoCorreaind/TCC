import 'package:get/get_navigation/src/routes/get_route.dart';
import 'package:groupy_app/modules/user/configuracoesUsuario/user_config_page.dart';

import '../../../application/module/module.dart';

class UserConfig implements Module {
  @override
  List<GetPage> routers = [
    GetPage(name: '/userConfig', page: () => UserConfigPage()),
  ];
}
