import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/models/uf/ufs.dart';
import 'package:validatorless/validatorless.dart';

import '../../../application/ui/widgets/custom.text_form_widget.dart';
import '../../../application/ui/widgets/custom_button_widget.dart';
import '../../../models/user/user.model.dart';
import 'editar_user_controller.dart';

class EditarUserPage extends GetView<EditarUserController> {
  final User user;
  var _emailController = TextEditingController();

  final _editarUserController = Get.put(EditarUserController());
  var _passwordController = TextEditingController();
  var _nameController = TextEditingController();
  var _rgController = TextEditingController();
  var _cpfController = TextEditingController();

  EditarUserPage({required this.user, Key? key}) : super(key: key) {
    _emailController = TextEditingController(text: user.email);
    _passwordController = TextEditingController(text: user.email);
    _nameController = TextEditingController(text: user.nome);
    _rgController = TextEditingController(text: user.rg);
    _cpfController = TextEditingController(text: user.cpf);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Editar usuário'),
      ),
      body: SingleChildScrollView(
          child: Center(
        child: Column(
          children: [
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
                      _editarUserController.userRepository.update(User(
                          email: _emailController.text,
                          password: _passwordController.text,
                          cpf: _cpfController.text,
                          rg: _rgController.text));
                    },
                    title: 'Atualizar Usuário',
                  ),
                ],
              ),
            ),
          ],
        ),
      )),
    );
  }
}
