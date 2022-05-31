import 'package:flutter/material.dart';
import 'package:get/get_navigation/get_navigation.dart';
import 'package:groupy/app/pages/welcome/welcome_page.dart';
import 'package:groupy/repositories/repositories/grupo.repository.dart';
import 'package:groupy/theme/app.theme.dart';
import 'package:groupy/repositories/interfaces/Igrupo.repository.dart';

import 'app/routes/app.pages.dart';
import 'app/routes/app.routes.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Move App v1',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home : WelcomePage(),
    );
  }
}
