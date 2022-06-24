import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/modules/grupo/chat/chat_controller.dart';

class ChatPage extends GetView<ChatController> {
   
   const ChatPage({Key? key}) : super(key: key);
   
   @override
   Widget build(BuildContext context) {
       return Scaffold(
           appBar: AppBar(title: const Text('ChatPage'),),
           body: Container(),
       );
  }
}


class ChatUsers {
  String? name;
  String? messageText;
  String? time;

  ChatUsers({required this.name, required this.messageText, required this.time});
}

class ChatMessage {
  String? messageContent;
  String? messageType;

  ChatMessage({this.messageContent, this.messageType});
}