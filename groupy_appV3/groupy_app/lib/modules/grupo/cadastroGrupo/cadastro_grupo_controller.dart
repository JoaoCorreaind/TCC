import 'dart:io';

import 'package:get/get.dart';
import 'package:groupy_app/models/cidade_model.dart';
import 'package:groupy_app/models/tags.model.dart';
import 'package:groupy_app/repositories/localidade/cidade/cidade_repository.dart';
import 'package:groupy_app/repositories/tag_repository.dart';
import 'package:image_picker/image_picker.dart';

class CadastroGrupoController extends GetxController {
  //tag para mudar valor selecionado do dropdown
  final drowpDownTagValue = TagRepository.getTags[0].obs;
  final cidadeRepository = CidadeRepository();
  final grupoMainImage = File('initial path');
  final ImagePicker _picker = ImagePicker();

  Rx<List<Cidade>> cidades = Rx<List<Cidade>>([]);
  Rx<String> selectedUf = Rx<String>('RS');
  Rx<Cidade> selectedCity = Rx<Cidade>(Cidade());
  Rx<dynamic> selectedFile = Rx<dynamic>("");
  //Rx<dynamic> selectedFilesCrrousel = Rx<dynamic>("");
  var selectedFilesCrrousel = [].obs;
  selectedUfChange(String uf) {
    selectedUf(uf);
  }

  fetchCitysByUf(String uf) async {
    var data = await cidadeRepository.getByUf(uf);
    cidades(data);
    selectedCity(data.first);
  }

  drowpDownTagValueChange(Tag tag) {
    drowpDownTagValue(tag);
  }

  getImage() async {
    final XFile? image = await _picker.pickImage(source: ImageSource.gallery);
    if (image != null && image.path != null) {
      selectedFile(File(image.path));
    }
    print(selectedFile.value);
  }

  addImage() async {
    final XFile? image = await _picker.pickImage(source: ImageSource.gallery);
    if (image != null && image.path != null) {
      selectedFilesCrrousel.value.add(File(image.path));
    }
    print(selectedFilesCrrousel.value);
  }

  setValueGrupoImage(XFile image) {
    // grupoMainImage = File(image.path);
    //grupoMainImage(image as File);
    //grupoMainImage(image;
  }
}
