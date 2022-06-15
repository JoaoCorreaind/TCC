import 'package:get/get_navigation/src/routes/get_route.dart';
import 'package:groupy_app/modules/home/home_page.dart';
import '../../application/module/module.dart';

class HomeModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(name: '/home', page: () => HomePage()),
  ];
}
