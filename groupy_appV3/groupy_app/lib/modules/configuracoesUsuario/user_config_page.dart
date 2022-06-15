import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/modules/configuracoesUsuario/user_config_controller.dart';

import '../../models/user/user.model.dart';

class UserConfigPage extends GetView<UserConfigController> {
  const UserConfigPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Configurações de usuário'),
      ),
      body: Container(),
      bottomNavigationBar: Container(
        color: const Color(0xff0F1828),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Padding(
              padding: const EdgeInsets.fromLTRB(0, 0, 35, 0),
              child: IconButton(
                onPressed: () {
                  Get.toNamed('/grupoCadastro');
                },
                icon: const Icon(Icons.group_add),
                iconSize: 50,
              ),
            ),
            Padding(
              padding: const EdgeInsets.fromLTRB(35, 0, 35, 0),
              child: IconButton(
                onPressed: () {},
                icon: const Icon(Icons.search),
                iconSize: 50,
              ),
            ),
            Padding(
              padding: const EdgeInsets.fromLTRB(35, 0, 0, 0),
              child: IconButton(
                onPressed: () {},
                icon: const Icon(Icons.groups_sharp),
                iconSize: 50,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
