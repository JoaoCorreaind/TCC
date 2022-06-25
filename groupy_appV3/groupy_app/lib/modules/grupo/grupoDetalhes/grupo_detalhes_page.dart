import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/globals/globals.dart';
import 'package:groupy_app/modules/grupo/grupoDetalhes/grupo_detalhes_controller.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../../application/ui/widgets/custom_button_widget.dart';
import '../../../models/grupo.model.dart';

class GrupoDetalhesPage extends GetView<GrupoDetalhesController> {
  final _grupoDetalhesController = Get.put(GrupoDetalhesController());
  var grupo = Grupo();
  GrupoDetalhesPage({required this.grupo, Key? key}) : super(key: key) {
    _grupoDetalhesController.setIsParticipant(grupo.participantes!.toList());
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(grupo.title.toString()),
      ),
      body: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Padding(
            padding: const EdgeInsets.all(12),
            child: ClipRRect(
              borderRadius: BorderRadius.circular(20), // Image border
              child: Image(
                image:
                    NetworkImage(URL_IMAGES + grupo.grupoMainImage.toString()),
              ),
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8),
            child: Text(
              grupo.descricao.toString(),
              style: const TextStyle(fontSize: 16, color: Color(0xffADB5BD)),
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8),
            child: Row(
              children: [
                const Text("Algumas Fotos",
                    style: TextStyle(fontSize: 20, color: Color(0XFFF5F5F5))),
              ],
            ),
          ),
          Container(
            width: double.infinity,
            height: 200,
            child: CarouselSlider(
              options: CarouselOptions(
                enlargeCenterPage: true,
                enableInfiniteScroll: false,
                autoPlay: true,
              ),
              items: grupo == null
                  ? []
                  : grupo.grupoImages
                      ?.map((e) => ClipRRect(
                            borderRadius: BorderRadius.circular(8),
                            child: Stack(
                              fit: StackFit.expand,
                              children: <Widget>[
                                Image.network(
                                  URL_IMAGES + e.path.toString(),
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
          Padding(
            padding: EdgeInsets.all(8),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text(
                  'Participantes',
                  style: TextStyle(fontSize: 20, color: Color(0xffF5F5F5)),
                ),
                Text(
                  '${grupo.participantes!.length.toString()} / ${grupo.maximoUsuarios}',
                  style: TextStyle(fontSize: 12, color: Color(0XffADB5BD)),
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
                const Text(
                  'Criador',
                  style: TextStyle(fontSize: 20, color: Color(0xffF5F5F5)),
                ),
                Text(
                  grupo.lider!.nome.toString(),
                  style: TextStyle(fontSize: 12, color: Color(0XffADB5BD)),
                )
              ],
            ),
          ),
          Spacer(),
          Padding(
            padding: EdgeInsets.all(8),
            child: Center(
                child: Obx(
              () => CustomButtonWidget(
                onPressed: _grupoDetalhesController.isParticipant.value
                    ? () => {
                          Get.snackbar('', 'Você já faz parte desse grupo',
                              backgroundColor: Colors.orange)
                        }
                    : () => addMember(grupo.id),
                title: _grupoDetalhesController.isParticipant.value
                    ? 'Membro'
                    : 'Entrar',
              ),
            )),
          )
        ],
      ),
    );
  }

  addMember(idGrupo) async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    var idUsuario = prefs.getString('user').toString();
    bool response = await _grupoDetalhesController.grupoRepository
        .addMember(idUsuario: idUsuario, idGrupo: idGrupo);
    if (response == true) {
      Get.snackbar(
          'Parabéns',
          'Parabéns você acaba de entrar no grupo ' +
              _grupoDetalhesController.grupo.value.title.toString());
      Get.back();
    } else {
      Get.snackbar(
          'Opa', 'Ocorreu um erro ao tentar entrar no grupo, tente mais tarde');
    }
  }
}
