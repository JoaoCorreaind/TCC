import 'package:get/get.dart';
import 'package:groupy_app/models/user/user.model.dart';
import 'package:groupy_app/repositories/user/user_repository.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../models/grupo.model.dart';
import '../../repositories/auth/auth_repository.dart';

class UserController extends GetxController {
  final authRepository = AuthRepository();
  final userRepository = UserRepository();

  final user = User().obs;
  var userId = 0;
  @override
  void onInit() async {
    super.onInit();
    getUser();
  }

  getUser() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? id = prefs.getString('user');
    if (id != null) {
      User? user = await userRepository.getOne(id);
      if (user != null) {
        setUser(user);
      }
    } else {
      Get.offAllNamed('/login');
    }
  }

  setUser(User set) {
    user(set);
  }
}
