import 'package:get/get_navigation/src/routes/get_route.dart';
import 'package:groupy_app/modules/grupo/chat/chat_page.dart';

import '../../../application/module/module.dart';

class ChatModule implements Module {
  @override
  List<GetPage> routers = [
    GetPage(
        name: '/grupo/chat', page: () => const ChatPage()),
  ];
}