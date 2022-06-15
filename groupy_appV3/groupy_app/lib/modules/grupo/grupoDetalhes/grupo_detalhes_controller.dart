import 'package:get/get.dart';
import 'package:groupy_app/models/grupo.model.dart';

class GrupoDetalhesController extends GetxController {
  final grupo = Grupo().obs;

  setGrupo(Grupo set) {
    grupo(set);
  }
}
