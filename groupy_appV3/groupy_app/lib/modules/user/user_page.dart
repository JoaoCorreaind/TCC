import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:groupy_app/models/user/user.model.dart';
import 'package:groupy_app/modules/user/editar/editar_user_page.dart';
import 'package:groupy_app/modules/user/user_controller.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../globals/globals.dart';

class UserPage extends GetView<UserController> {
  UserPage({Key? key}) : super(key: key);
  final _userController = Get.put(UserController());

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Usuário'),
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            Row(
              children: [
                Padding(
                    padding: EdgeInsets.fromLTRB(24, 24, 0, 0),
                    child: Obx(
                      () => CircleAvatar(
                          minRadius: 25,
                          maxRadius: 40,
                          backgroundImage: _userController.user.value.image !=
                                  null
                              ? NetworkImage(URL_IMAGES +
                                  _userController.user.value.image.toString())
                              : const AssetImage('assets/no_image.png')
                                  as ImageProvider),
                    )),
                Padding(
                    padding: EdgeInsets.fromLTRB(24, 24, 0, 0),
                    child: Obx(
                      () => Column(
                        mainAxisAlignment: MainAxisAlignment.start,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            _userController.user.value.nome.toString(),
                            style: const TextStyle(
                                color: Colors.white, fontSize: 18),
                          ),
                          Padding(
                            padding: EdgeInsets.only(top: 10),
                            child: Text(
                              _userController.user.value.email.toString(),
                              style: const TextStyle(
                                  color: Color(0xffADB5BD), fontSize: 16),
                            ),
                          )
                        ],
                      ),
                    ))
              ],
            ),
            Padding(
              padding: EdgeInsets.fromLTRB(8, 32, 0, 0),
              child: Column(
                children: [
                  Card(
                    color: const Color(0xff0F1828),
                    child: InkWell(
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: const [
                          Icon(
                            Icons.change_circle,
                            color: Colors.white,
                            size: 24.0,
                          ),
                          Padding(
                            padding: EdgeInsets.only(left: 12),
                            child: Text(
                              "Editar dados",
                              style:
                                  TextStyle(fontSize: 20, color: Colors.white),
                            ),
                          ),
                          Padding(
                            // padding: EdgeInsets.only(left: 175),
                            padding: EdgeInsets.only(left: 175),

                            child: Icon(
                              Icons.arrow_right_sharp,
                              color: Colors.white,
                              size: 40,
                            ),
                          ),
                        ],
                      ),
                      onTap: () async {
                        Get.to(() =>
                            EditarUserPage(user: _userController.user.value));
                      },
                    ),
                  ),
                  Card(
                    color: const Color(0xff0F1828),
                    child: InkWell(
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: const [
                          Icon(
                            Icons.groups_sharp,
                            color: Colors.white,
                            size: 24.0,
                          ),
                          Padding(
                            padding: EdgeInsets.only(left: 12),
                            child: Text(
                              "Meus grupos",
                              style:
                                  TextStyle(fontSize: 20, color: Colors.white),
                            ),
                          ),
                          Padding(
                            padding: EdgeInsets.only(left: 170),
                            child: Icon(
                              Icons.arrow_right_sharp,
                              color: Colors.white,
                              size: 40,
                            ),
                          ),
                        ],
                      ),
                      onTap: () async {
                        Get.toNamed('/group/byUser');
                      },
                    ),
                  ),
                  Card(
                    color: const Color(0xff0F1828),
                    child: InkWell(
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: const [
                          Icon(
                            Icons.supervised_user_circle,
                            color: Colors.white,
                            size: 24.0,
                          ),
                          Padding(
                            padding: EdgeInsets.only(left: 12),
                            child: Text(
                              "Grupos criados",
                              style:
                                  TextStyle(fontSize: 20, color: Colors.white),
                            ),
                          ),
                          Padding(
                            padding: EdgeInsets.only(left: 150),
                            child: Icon(
                              Icons.arrow_right_sharp,
                              color: Colors.white,
                              size: 40,
                            ),
                          ),
                        ],
                      ),
                      onTap: () async {
                        Get.toNamed('/group/byLeader');
                      },
                    ),
                  ),
                  Card(
                    color: const Color(0xff0F1828),
                    child: InkWell(
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.start,
                        children: const [
                          Icon(
                            Icons.exit_to_app,
                            color: Colors.white,
                            size: 24.0,
                          ),
                          Padding(
                            padding: EdgeInsets.only(left: 12),
                            child: Text(
                              "Sair",
                              style:
                                  TextStyle(fontSize: 20, color: Colors.white),
                            ),
                          ),
                          Padding(
                            padding: EdgeInsets.only(left: 250),
                            child: Icon(
                              Icons.arrow_right_sharp,
                              color: Colors.white,
                              size: 40,
                            ),
                          ),
                        ],
                      ),
                      onTap: () async {
                        final pref = await SharedPreferences.getInstance();
                        await pref.clear();
                        Get.offAllNamed('/login');
                      },
                    ),
                  )
                ],
              ),
            )
          ],
        ),
      ),
    );
  }
}
