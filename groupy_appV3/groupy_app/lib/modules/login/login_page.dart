import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:validatorless/validatorless.dart';

import '../../application/ui/widgets/custom.text_form_widget.dart';
import '../../application/ui/widgets/custom_button_widget.dart';
import 'login_controller.dart';

class LoginPage extends GetView<LoginController> {
  final _loginController = Get.put(LoginController());
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  LoginPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: SingleChildScrollView(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              Image.asset('assets/peopleTalk.png'),
              const Text(
                "Conecte-se de forma facil em grupos de ações sociais!",
                textAlign: TextAlign.center,
                style: TextStyle(
                    fontSize: 24,
                    fontWeight: FontWeight.w800,
                    color: Colors.white,
                    wordSpacing: 1.0),
              ),
              Padding(
                padding:
                    const EdgeInsets.symmetric(horizontal: 10, vertical: 10),
                child: CustomTextFormField(
                    label: 'E-mail',
                    controller: _emailController,
                    validator: Validatorless.email('E-mail obrigatório')),
              ),
              Padding(
                padding:
                    const EdgeInsets.symmetric(horizontal: 10, vertical: 10),
                child: CustomTextFormField(
                  label: 'Senha',
                  controller: _passwordController,
                  obscureText: true,
                ),
              ),
              const SizedBox(height: 20),
              CustomButtonWidget(
                onPressed: () async {
                  var response = await _loginController.authRepository.doLogin(
                      email: _emailController.text,
                      password: _passwordController.text);
                  handleResponseLogin(response);
                },
                title: 'Efetuar Login',
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  const Text('Não possuí uma conta?',
                      style: TextStyle(
                        color: Colors.white,
                      )),
                  TextButton(
                      onPressed: () => {
                            Get.toNamed('/cadastro'),
                          },
                      child: const Text('Registre-se')),
                ],
              )
            ],
          ),
        ),
      ),
    );
  }

  clearLogin() {
    _emailController.text = '';
    _passwordController.text = '';
  }

  handleResponseLogin(bool result) {
    if (result) {
      Get.snackbar('Login', 'Login Efetuado com sucesso',
          backgroundColor: Colors.green);
      Get.offAllNamed('/home');
    } else {
      clearLogin();
      Get.snackbar('Login', 'Dados Incorretos, tente novamente',
          backgroundColor: Colors.red);
    }
  }
}
