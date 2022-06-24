import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/globals/globals.dart';
import 'package:groupy_app/modules/grupo/groupMangeUsers/group_manege_users_controller.dart';
import 'package:groupy_app/modules/grupo/grupoDetalhes/grupo_detalhes_controller.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../../application/ui/widgets/custom_button_widget.dart';
import '../../../models/grupo.model.dart';

class GroupManegeUsersPage extends GetView<GroupManegeUsersController> {
  final _groupManegeController = Get.put(GroupManegeUsersController());
  var grupo = Grupo();
  GroupManegeUsersPage({required this.grupo, Key? key}) : super(key: key) {
    _groupManegeController.getUsers(grupo.id);
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(grupo.title.toString()),
      ),
      body: Column(
        children: [
          Expanded(
              child: Obx(
            () => ListView.builder(
              itemCount: _groupManegeController.users.value.length,
              itemBuilder: (BuildContext context, int grupo) {
                return Card(
                  elevation: 3,
                  child: ListTile(
                    onTap: () {
                      /*Get.to(() => GrupoDetalhesPage(
                          grupo: _homeController.grupos.value[grupo]));*/
                    },
                    tileColor: const Color(0xff263238),
                    //textColor: const Color(0xff585554),
                    leading: CircleAvatar(
                        radius: 30,
                        backgroundImage: _groupManegeController
                                    .users.value[grupo].image !=
                                null
                            ? NetworkImage(URL_IMAGES +
                                _groupManegeController.users.value[grupo].image
                                    .toString())
                            : const AssetImage('assets/no_image.png')
                                as ImageProvider),
                    title: Text(
                      _groupManegeController.users.value[grupo].nome.toString(),
                      style: const TextStyle(
                          fontSize: 17,
                          fontWeight: FontWeight.w500,
                          color: Colors.white),
                    ),
                  ),
                );
              },
              padding: const EdgeInsets.all(10),
            ),
          )),
        ],
      ),
    );
  }
}
