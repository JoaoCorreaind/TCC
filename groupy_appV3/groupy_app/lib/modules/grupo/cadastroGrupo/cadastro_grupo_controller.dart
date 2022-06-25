import 'dart:io';

import 'package:get/get.dart';
import 'package:groupy_app/models/tags.model.dart';
import 'package:groupy_app/repositories/tag_repository.dart';
import 'package:image_picker/image_picker.dart';

class CadastroGrupoController extends GetxController {
  //tag para mudar valor selecionado do dropdown
  final drowpDownTagValue = TagRepository.getTags[0].obs;
  final grupoMainImage = File('initial path');
  drowpDownTagValueChange(Tag tag) {
    drowpDownTagValue(tag);
  }

  setValueGrupoImage(XFile image) {
    // grupoMainImage = File(image.path);
    //grupoMainImage(image as File);
    //grupoMainImage(image;
  }
}
