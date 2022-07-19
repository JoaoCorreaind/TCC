import 'dart:io';

import '../../models/user/user.model.dart';

abstract class IUserRepository {
  Future<void> create(User user, File? userImage);
  Future<User?> getOne(String id);
  Future<void> update(User user);
}
