import 'dart:io';

import 'package:get/get.dart';
import 'package:groupy_app/models/user/user.model.dart';

import '../../../repositories/user/user_repository.dart';

class EditarUserController extends GetxController {
  var user = User().obs; // declare just like any other variable
  var userRepository = UserRepository();
  var userImage = File("assets/no_image.png").obs;

  void changeUserEmail(value) => user.value.email = value;
  void changeUserPassword(value) => user.value.password = value;
  void changeUserRg(value) => user.value.rg = value;
  void changeUserNome(value) => user.value.nome = value;
  void changeUserCpf(value) => user.value.cpf = value;

  void changeUser(user) => user.value = user;
}
