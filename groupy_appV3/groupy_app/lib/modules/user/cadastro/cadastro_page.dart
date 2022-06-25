import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/models/uf/ufs.dart';
import 'package:validatorless/validatorless.dart';

import '../../../application/ui/widgets/custom.text_form_widget.dart';
import '../../../application/ui/widgets/custom_button_widget.dart';
import '../../../models/user/user.model.dart';
import 'cadastro_controller.dart';

class CadastroPage extends GetView<CadastroController> {
  final User? user;
  final _cadastroController = Get.put(CadastroController());
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final _nameController = TextEditingController();
  final _rgController = TextEditingController();
  final _cpfController = TextEditingController();

  CadastroPage({this.user, Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Cadastrar Usuário'),
      ),
      body: Center(
        child: SingleChildScrollView(
            child: Column(
          children: [
            Image.asset('assets/peopleTalkRegister.png'),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: CustomTextFormField(
                controller: _emailController,
                label: 'Email',
                validator: Validatorless.email('Um e-mail válido é requirido'),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: CustomTextFormField(
                controller: _passwordController,
                label: 'Senha',
                obscureText: true,
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: CustomTextFormField(
                controller: _nameController,
                label: 'Nome',
                validator: Validatorless.min(
                    10, 'O nome deve possuir ao menos 10 caracteres'),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: CustomTextFormField(
                controller: _cpfController,
                label: 'CPF',
                validator: Validatorless.cpf('Informe um CPF válido'),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: CustomTextFormField(
                label: 'UF',
                validator: Validatorless.cpf('Informe um CPF válido'),
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  CustomButtonWidget(
                    onPressed: () async {
                      _cadastroController.userRepository.create(User(
                          email: _emailController.text,
                          password: _passwordController.text,
                          cpf: _cpfController.text,
                          rg: _rgController.text));
                    },
                    title: 'Efetuar Cadastro',
                  ),
                ],
              ),
            ),
          ],
        )),
      ),
    );
  }
}
