import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/globals/globals.dart';
import 'package:groupy_app/modules/grupo/grupoDetalhes/grupo_detalhes_controller.dart';

import '../../../models/grupo.model.dart';

class GrupoDetalhesPage extends GetView<GrupoDetalhesController> {
  final _grupoDetalhesController = Get.put(GrupoDetalhesController());
  var grupo = Grupo();
  GrupoDetalhesPage({required this.grupo, Key? key}) : super(key: key);
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(grupo.title.toString()),
      ),
      body: SingleChildScrollView(
        child: Column(
          children: [
            // Container(
            //   height: 300,
            //   width: 1000,
            //   decoration: BoxDecoration(
            //       borderRadius: BorderRadius.circular(100),
            //       image: DecorationImage(
            //           image: NetworkImage(
            //               URL_IMAGES + grupo.grupoMainImage.toString()))),
            // ),
            Image.network(
              URL_IMAGES + grupo.grupoMainImage.toString(),
              width: double.infinity,
            ),
            Text(grupo.descricao.toString()),
            Column(
              children: [
                const Text('Participantes'),
                Text(
                    '${grupo.participantes!.length.toString()} / ${grupo.maximoUsuarios}'),
              ],
            ),
            Column(
              children: [
                const Text('Criador'),
                Text(grupo.lider!.nome.toString())
              ],
            )
          ],
        ),
      ),
    );
  }
}
