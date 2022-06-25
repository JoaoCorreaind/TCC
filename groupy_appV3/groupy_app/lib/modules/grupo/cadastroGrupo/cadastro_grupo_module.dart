import 'package:get/get_navigation/src/routes/get_route.dart';

import '../../../application/module/module.dart';
import 'cadastro_grupo_page.dart';

class CadastroGrupoModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(name: '/grupoCadastro', page: () => CadastroGrupoPage()),
  ];
}
