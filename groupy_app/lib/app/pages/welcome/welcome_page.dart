import 'package:flutter/material.dart';
import 'package:groupy/app/pages/home/home_page.dart';
import 'package:groupy/app/pages/login/login_page.dart';
import 'package:shared_preferences/shared_preferences.dart';
class WelcomePage extends StatefulWidget {
  const WelcomePage({ Key? key }) : super(key: key);

  @override
  State<WelcomePage> createState() => _WelcomePageState();
}

class _WelcomePageState extends State<WelcomePage> {
  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    verifyToken().then((value) {
      if(value){
        Navigator.pushReplacement(context, MaterialPageRoute(builder: (context)=> HomePage()));
      }else{
        Navigator.pushReplacement(context, MaterialPageRoute(builder: (context)=> LoginPage()));
      }
    });
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center (
        child: CircularProgressIndicator(),
      )
    );
  }


  Future<bool> verifyToken() async {
    SharedPreferences sharedPreferences = await  SharedPreferences.getInstance();
    if(sharedPreferences.getString('token') != null){
      return true;
    }else{
      return false;
    }
  }
}