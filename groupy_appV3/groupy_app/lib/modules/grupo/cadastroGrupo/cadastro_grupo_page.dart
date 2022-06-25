import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/models/tags.model.dart';
import 'package:groupy_app/repositories/tag_repository.dart';
import 'package:validatorless/validatorless.dart';
import 'package:image_picker/image_picker.dart';

import '../../../application/ui/widgets/custom.text_form_widget.dart';
import 'cadastro_grupo_controller.dart';

class CadastroGrupoPage extends GetView<CadastroGrupoController> {
  final grupoCadastroController = Get.put(CadastroGrupoController());
  CadastroGrupoPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Novo Grupo'),
      ),
      body: Center(
        child: SingleChildScrollView(
            child: Column(
          children: [
            Image.asset('assets/chat_image.png'),
            Padding(
              padding: const EdgeInsets.all(10),
              child: CustomTextFormField(
                  label: "Titulo do Grupo",
                  validator: Validatorless.min(
                      5, 'Titulo deve possuir ao menos 5 caracteres')),
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
          ],
        )),
      ),
    );
  }

  _getFromGallery() async {
    final ImagePicker _picker = ImagePicker();

    final XFile? image = await _picker.pickImage(source: ImageSource.gallery);
    grupoCadastroController.setValueGrupoImage(image as XFile);
  }
}
