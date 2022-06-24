import 'package:flutter/material.dart';
import 'package:get/get_navigation/get_navigation.dart';
import 'package:groupy_app/modules/cadastroGrupo/cadastro_grupo_module.dart';
import 'package:groupy_app/modules/grupo/groupByLeader/group_by_leader_module.dart';
import 'package:groupy_app/modules/grupo/grupoDetalhes/grupo_detalhes_module.dart';
import 'package:groupy_app/modules/home/home_module.dart';
import 'package:groupy_app/modules/login/login_module.dart';
import 'package:groupy_app/modules/user/cadastro/cadastro_module.dart';
import 'package:groupy_app/modules/user/user_module.dart';

import 'application/bindings/application_bindings.dart';
import 'application/ui/groupy_ui_config.dart';
import 'modules/grupo/groupByUser/group_by_user_module.dart';
import 'modules/user/editar/editar_user_module.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return GetMaterialApp(
      title: 'Groupy App',
      theme: GroupyUiConfig.theme,
      debugShowCheckedModeBanner: false,
      initialBinding: ApplicationBindings(),
      getPages: [
        ...LoginModule().routers,
        ...CadastroModule().routers,
        ...GroupByUserModule().routers,
        ...HomeModule().routers,
        ...GrupoDetalhesModule().routers,
        ...CadastroGrupoModule().routers,
        ...UserModule().routers,
        ...EditarUserModule().routers,
        ...GroupByLeaderModule().routers,
      ],
      //home: const MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}
