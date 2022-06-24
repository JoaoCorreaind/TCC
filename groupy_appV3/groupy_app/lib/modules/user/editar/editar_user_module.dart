import 'package:get/get_navigation/src/routes/get_route.dart';

import '../../../application/module/module.dart';
import '../../../models/user/user.model.dart';
import 'editar_user_page.dart';

class EditarUserModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(
        name: '/user/update',
        page: () => EditarUserPage(
              user: User(),
            )),
  ];
}
