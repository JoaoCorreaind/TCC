import '../modules/cadastro/cadastro_module.dart';
import '../modules/home/home_module.dart';
import '../modules/login/login_module.dart';

final dynamic getPages = [
  ...LoginModule().routers,
  ...CadastroModule().routers,
  ...HomeModule().routers,
];
// ignore: constant_identifier_names
const URL_IMAGES = "http://10.0.2.2:5000/";
