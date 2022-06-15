import 'package:get/get_navigation/src/routes/get_route.dart';

import '../../application/module/module.dart';
import 'cadastro_page.dart';

class CadastroModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(name: '/cadastro', page: () => CadastroPage()),
  ];
}
