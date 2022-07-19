import 'dart:io';

import 'package:carousel_slider/carousel_slider.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/globals/uf_mock.dart';
import 'package:groupy_app/models/cidade_model.dart';
import 'package:groupy_app/models/tags.model.dart';
import 'package:groupy_app/repositories/tag_repository.dart';
import 'package:validatorless/validatorless.dart';
import 'package:image_picker/image_picker.dart';

import '../../../application/ui/widgets/custom.text_form_widget.dart';
import 'cadastro_grupo_controller.dart';

class CadastroGrupoPage extends GetView<CadastroGrupoController> {
  final grupoCadastroController = Get.put(CadastroGrupoController());
  final listUf = ufs;
  CadastroGrupoPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    var mediaWidth = MediaQuery.of(context).size.width;
    return Scaffold(
        appBar: AppBar(
          title: const Text('Novo Grupo'),
        ),
        body: Center(
            child: SingleChildScrollView(
                child: Column(
          mainAxisAlignment: MainAxisAlignment.start,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Center(child: Image.asset('assets/chat_image.png')),
            Padding(
              padding: const EdgeInsets.all(10),
              child: CustomTextFormField(
                  label: "Titulo do Grupo",
                  validator: Validatorless.min(
                      5, 'Titulo deve possuir ao menos 5 caracteres')),
            ),
            Padding(
              padding: const EdgeInsets.all(8.0),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const Text(
                    'Imagem principal :',
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
                            image:
                                grupoCadastroController.selectedFile.value == ''
                                    ? const AssetImage('assets/chose_image.png')
                                    : FileImage(grupoCadastroController
                                        .selectedFile.value) as ImageProvider,

                            //image: AssetImage('assets/chose_image.png')
                          ),
                        ),
                      ),
                    )),
                    onTap: () => grupoCadastroController.getImage(),
                  ),
                ],
              ),
            ),
            Padding(
              padding: EdgeInsets.all(8),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const Padding(
                    padding: EdgeInsets.all(8.0),
                    child: Text(
                      "Mais fotos",
                      style: TextStyle(
                          fontSize: 18,
                          color: Color.fromRGBO(189, 189, 189, 1)),
                    ),
                  ),
                  TextButton(
                      onPressed: () async {
                        await grupoCadastroController.addImage();
                      },
                      child: const Text('Adicionar Imagem')),
                  Obx(
                    () => Padding(
                      padding: const EdgeInsets.all(0),
                      child: grupoCadastroController
                              .selectedFilesCrrousel.value.isEmpty
                          ? const SizedBox(
                              height: 10,
                            )
                          : SizedBox(
                              width: double.infinity,
                              height: 200,
                              child: CarouselSlider(
                                options: CarouselOptions(
                                  enlargeCenterPage: true,
                                  enableInfiniteScroll: false,
                                  autoPlay: true,
                                ),
                                items: grupoCadastroController
                                    .selectedFilesCrrousel.value
                                    .map((e) => ClipRRect(
                                          borderRadius:
                                              BorderRadius.circular(8),
                                          child: Stack(
                                            fit: StackFit.expand,
                                            children: <Widget>[
                                              Image.file(
                                                File(e.path),
                                                width: 1050,
                                                height: 350,
                                                fit: BoxFit.cover,
                                              )
                                            ],
                                          ),
                                        ))
                                    .toList(),
                              ),
                            ),
                    ),
                  )
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.all(10),
              child: CustomTextFormField(
                label: "Numero de Participantes",
              ),
            ),
            const Padding(
                padding: EdgeInsets.all(10),
                child: Card(
                    color: Color(0xff152033),
                    child: Padding(
                      padding: EdgeInsets.all(8),
                      child: TextField(
                          style: TextStyle(
                            color: Colors.white,
                          ),
                          maxLines: 8,
                          decoration: InputDecoration.collapsed(
                              hintStyle: TextStyle(
                                color: Color.fromRGBO(189, 189, 189, 1),
                              ),
                              hintText: "Descrição do Grupo")),
                    ))),
            Padding(
                padding: const EdgeInsets.all(10),
                child: Container(
                    color: const Color(0xff152033),
                    width: 360,
                    child: Obx(
                      () => Theme(
                        data: Theme.of(context).copyWith(
                          canvasColor: const Color(0xff152033),
                        ),
                        child: DropdownButton<Tag>(
                            value:
                                grupoCadastroController.drowpDownTagValue.value,
                            items:
                                TagRepository.getTags.map((Tag dropDownItem) {
                              return DropdownMenuItem<Tag>(
                                  value: dropDownItem,
                                  child: Padding(
                                    padding: const EdgeInsets.all(10.0),
                                    child: Text(
                                      dropDownItem.descricao.toString(),
                                      style:
                                          const TextStyle(color: Colors.white),
                                    ),
                                  ));
                            }).toList(),
                            onChanged: (Tag? selected) {
                              grupoCadastroController
                                  .drowpDownTagValueChange(selected as Tag);
                            }),
                      ),
                    ))),
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
                                    value: grupoCadastroController
                                        .selectedUf.value,
                                    onChanged: (value) {
                                      grupoCadastroController
                                          .selectedUfChange(value as String);
                                      grupoCadastroController
                                          .fetchCitysByUf(value);
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
                        width: mediaWidth * 0.7,
                        child: Theme(
                          data: Theme.of(context).copyWith(
                            canvasColor: const Color(0xff152033),
                          ),
                          child: Obx(() => Padding(
                                padding: EdgeInsets.only(left: 10),
                                child: grupoCadastroController
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
                                            items: grupoCadastroController
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
                                            value: grupoCadastroController
                                                .selectedCity.value,
                                            onChanged: (value) {
                                              grupoCadastroController
                                                  .selectedCity(
                                                      value as Cidade);
                                            }),
                                      ),
                              )),
                        ),
                      ),
                    ),
                  ],
                )),
          ],
        ))));
  }

  _getFromGallery() async {
    final ImagePicker _picker = ImagePicker();

    final XFile? image = await _picker.pickImage(source: ImageSource.gallery);
    grupoCadastroController.setValueGrupoImage(image as XFile);
  }
}
