// ignore_for_file: prefer_const_constructors

import 'package:flutter/material.dart';
import 'package:groupy/models/user/auth.model.dart';

import '../../../repositories/repositories/user/auth_repository.dart';
import '../home/home_page.dart';


class LoginPage extends StatefulWidget {
  const LoginPage({ Key? key }) : super(key: key);
  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  var authRepository = AuthRepository();
  final _formKey = GlobalKey<FormState>();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Form(
        key: _formKey,
        child : Center(
          child: SingleChildScrollView(
            padding : EdgeInsets.symmetric(horizontal: 16),
            child : Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                TextFormField(
                  decoration: InputDecoration(
                    labelText: "Email",
                  ),
                  controller: _emailController,
                  keyboardType: TextInputType.emailAddress,
                  validator: (email){
                    if(email == null || email.isEmpty){
                      return 'Por favor, digite seu email';
                    }else if (!RegExp(
                              r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+")
                                .hasMatch(_emailController.text)) {
                      return 'Por favor, digite um e-mail correto';
                    }
                    return null;
                  },
                ),
                TextFormField(
                  decoration: InputDecoration(
                    labelText: "Senha",
                  ), 
                  controller : _passwordController,
                  keyboardType: TextInputType.text,
                  validator : (password){
                    if(password == null || password.isEmpty){
                      return 'Por favor, digite sua senha';
                    }else if(password.length < 6){
                      return 'Por favor, digite uma senha maior que 6 (seis) caracteres';
                    }
                    return null;
                  }
                ),
                ElevatedButton(onPressed: () async {
                  FocusScopeNode currentFocus = FocusScope.of(context);
                  if(_formKey.currentState!.validate()){
                    bool tryLogin = await authRepository.DoLogin(email: _emailController.text, password: _passwordController.text);
                    if(!currentFocus.hasPrimaryFocus){
                      currentFocus.unfocus();
                    }
                    if(tryLogin){
                      Navigator.pushReplacement(context, MaterialPageRoute(builder: (context) => HomePage()));
                    }else{
                      _passwordController.clear();
                      ScaffoldMessenger.of(context).showSnackBar(snackbar);
                    }
                  }
                }, child: Text('ENTRAR'))
              ],
            )
          ),
        )
      ),
    );
  }
}

final snackbar = SnackBar(content:  Text(
  'E-mail ou senha inválidos', textAlign: TextAlign.center,
), backgroundColor: Colors.redAccent,);