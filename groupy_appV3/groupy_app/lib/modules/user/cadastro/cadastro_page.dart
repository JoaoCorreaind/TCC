import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/globals/globals.dart';
import 'package:groupy_app/globals/uf_mock.dart';
import 'package:groupy_app/models/cidade_model.dart';
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

  List<String> listUf = ufs;
  CadastroPage({this.user, Key? key}) : super(key: key) {}

  @override
  Widget build(BuildContext context) {
    var mediaWidth =
        MediaQuery.of(context).size.width; //to get the width of screen
    return Scaffold(
      appBar: AppBar(
        title: const Text('Cadastrar Usuário'),
      ),
      body: Center(
        child: SingleChildScrollView(
            child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Center(child: Image.asset('assets/peopleTalkRegister.png')),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const Text(
                    'Imagem do usuário :',
                    style: TextStyle(
                        fontSize: 18, color: Color.fromRGBO(189, 189, 189, 1)),
                  ),
                  InkWell(
                    child: Center(
                        child: Obx(
                      () => Container(
                        width: mediaWidth * 0.8,
                        height: 150,
                        decoration: BoxDecoration(
                          image: DecorationImage(
                            image: _cadastroController.selectedFile.value == ''
                                ? const AssetImage('assets/chose_image.png')
                                : FileImage(
                                        _cadastroController.selectedFile.value)
                                    as ImageProvider,

                            //image: AssetImage('assets/chose_image.png')
                          ),
                        ),
                      ),
                    )),
                    onTap: () => _cadastroController.getImage(),
                  ),
                ],
              ),
            ),
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
                  mainAxisAlignment: MainAxisAlignment.start,
                  children: [
                    Container(
                      decoration: const BoxDecoration(color: Color(0xff152033)),
                      width: mediaWidth * 0.2,
                      child: Theme(
                        data: Theme.of(context).copyWith(
                          canvasColor: const Color(0xff152033),
                        ),
                        child: Obx(() => Padding(
                              padding: EdgeInsets.only(left: 10),
                              child: DropdownButtonHideUnderline(
                                child: DropdownButton<String>(
                                    style:
                                        Theme.of(context).textTheme.titleMedium,
                                    items: listUf.map((String value) {
                                      return DropdownMenuItem<String>(
                                        value: value,
                                        child: Center(
                                          child: Text(
                                            value,
                                            style: const TextStyle(
                                                fontSize: 18,
                                                color: Color.fromRGBO(
                                                    189, 189, 189, 1)),
                                          ),
                                        ),
                                      );
                                    }).toList(),
                                    value: _cadastroController.selectedUf.value,
                                    onChanged: (value) {
                                      _cadastroController
                                          .selectedUfChange(value as String);
                                      _cadastroController.fetchCitysByUf(value);
                                    }),
                              ),
                            )),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Container(
                        decoration:
                            const BoxDecoration(color: Color(0xff152033)),
                        width: mediaWidth * 0.71,
                        child: Theme(
                          data: Theme.of(context).copyWith(
                            canvasColor: const Color(0xff152033),
                          ),
                          child: Obx(() => Padding(
                                padding: EdgeInsets.only(left: 10),
                                child: _cadastroController
                                            .cidades.value.length ==
                                        0
                                    ? Container(
                                        alignment: Alignment.center,
                                        height: 44,
                                        decoration: const BoxDecoration(
                                            color: Color(0xff152033)),
                                        child: const Text(
                                          'Escolha o estado',
                                          style: TextStyle(
                                              fontSize: 18,
                                              color: Color.fromRGBO(
                                                  189, 189, 189, 1)),
                                        ))
                                    : DropdownButtonHideUnderline(
                                        child: DropdownButton(
                                            style: Theme.of(context)
                                                .textTheme
                                                .titleMedium,
                                            items: _cadastroController
                                                .cidades.value
                                                .map((Cidade value) {
                                              return DropdownMenuItem(
                                                value:
                                                    value, // guard it with null if empty
                                                child: Center(
                                                  child: Text(
                                                    value.nome!.toString(),
                                                    style: const TextStyle(
                                                        fontSize: 18,
                                                        color: Color.fromRGBO(
                                                            189, 189, 189, 1)),
                                                  ),
                                                ),
                                              );
                                            }).toList(),
                                            value: _cadastroController
                                                .selectedCity.value,
                                            onChanged: (value) {
                                              _cadastroController.selectedCity(
                                                  value as Cidade);
                                            }),
                                      ),
                              )),
                        ),
                      ),
                    ),
                  ],
                )),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  CustomButtonWidget(
                    onPressed: () async {
                      _cadastroController.userRepository.create(
                          User(
                              email: _emailController.text,
                              password: _passwordController.text,
                              cpf: _cpfController.text,
                              rg: _rgController.text),
                          _cadastroController.selectedFile.value != ""
                              ? _cadastroController.selectedFile.value
                              : null);
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
