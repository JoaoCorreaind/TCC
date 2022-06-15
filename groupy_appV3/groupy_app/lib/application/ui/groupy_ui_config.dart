import 'package:flutter/material.dart';

class GroupyUiConfig {
  GroupyUiConfig._();

  static String title = 'Groupy';

  static ThemeData get theme => ThemeData(
        scaffoldBackgroundColor: const Color(0xff0F1828),
        appBarTheme: const AppBarTheme(
          elevation: 0,
          backgroundColor: Color(0xff152033),
          iconTheme: IconThemeData(color: Colors.white),
          titleTextStyle: TextStyle(
              color: Colors.white, fontSize: 18, fontWeight: FontWeight.w500),
        ),
      );
}
