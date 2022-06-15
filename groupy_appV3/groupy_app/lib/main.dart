import 'package:flutter/material.dart';
import 'package:get/get_navigation/get_navigation.dart';
import 'package:groupy_app/modules/cadastro/cadastro_module.dart';
import 'package:groupy_app/modules/cadastroGrupo/cadastro_grupo_module.dart';
import 'package:groupy_app/modules/grupo/grupoDetalhes/grupo_detalhes_module.dart';
import 'package:groupy_app/modules/home/home_module.dart';
import 'package:groupy_app/modules/login/login_module.dart';

import 'application/bindings/application_bindings.dart';
import 'application/ui/groupy_ui_config.dart';

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
        ...HomeModule().routers,
        ...GrupoDetalhesModule().routers,
        ...CadastroGrupoModule().routers,
      ],
      //home: const MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}
