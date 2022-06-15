
abstract class IAuthRepository {
  Future<bool> doLogin({required email, required password});
}