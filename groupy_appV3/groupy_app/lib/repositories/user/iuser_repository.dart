import '../../models/user/user.model.dart';

abstract class IUserRepository {
  Future<void> create(User user);
}
