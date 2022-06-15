import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'dart:convert';
import '../../models/user/user.model.dart';

class UserConfigController extends GetxController {
  final user = User().obs;

  @override
  void onInit() async {
    super.onInit();
    getInitialValues();
  }

  getInitialValues() async {
    SharedPreferences pref = await SharedPreferences.getInstance();
    user(jsonDecode(pref.getString('currentUser').toString()));
  }
}
