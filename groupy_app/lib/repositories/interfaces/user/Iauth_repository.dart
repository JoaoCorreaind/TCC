import 'package:groupy/models/user/auth.model.dart';
import 'package:groupy/models/user/user.model.dart';

abstract class IAuthRepository {
  Future<bool> DoLogin ({required String email, required String password});
}