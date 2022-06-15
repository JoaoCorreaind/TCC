import 'dart:io';

import 'package:get/get.dart';
import 'package:image_picker/image_picker.dart';

import '../../models/tags.model.dart';
import '../../repositories/tag_repository.dart';

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
