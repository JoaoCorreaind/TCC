
import 'package:groupy/models/user/user.model.dart';

abstract class IUserRepository {
  Future<User?> GetById({required String id});
  Future<List<User>?> GetAll();
  Future Delete ({required String id});
  Future Update({required String id, required User user});
}