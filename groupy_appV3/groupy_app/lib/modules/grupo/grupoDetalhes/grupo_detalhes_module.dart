import 'package:get/get_navigation/src/routes/get_route.dart';
import 'package:groupy_app/modules/grupo/grupoDetalhes/grupo_detalhes_page.dart';
import 'package:groupy_app/modules/home/home_page.dart';
import '../../../application/module/module.dart';
import '../../../models/grupo.model.dart';

class GrupoDetalhesModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(
        name: '/grupo/detalhes', page: () => GrupoDetalhesPage(grupo: Grupo())),
  ];
}
