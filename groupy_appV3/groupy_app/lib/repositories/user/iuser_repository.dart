import '../../models/user/user.model.dart';

abstract class IUserRepository {
  Future<void> create(User user);
  Future<User?> getOne(int id);
  Future<void> update(User user);
}
