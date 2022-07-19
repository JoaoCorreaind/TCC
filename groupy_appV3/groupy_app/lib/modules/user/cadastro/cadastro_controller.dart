import 'dart:io';

import 'package:get/get.dart';
import 'package:groupy_app/models/cidade_model.dart';
import 'package:groupy_app/models/user/user.model.dart';
import 'package:groupy_app/repositories/localidade/cidade/cidade_repository.dart';
import 'package:image_picker/image_picker.dart';

import '../../../repositories/user/user_repository.dart';

class CadastroController extends GetxController {
  var user = User().obs; // declare just like any other variable
  var userRepository = UserRepository();
  var cidadeRepository = CidadeRepository();
  final ImagePicker _picker = ImagePicker();

  var userImage = File("assets/no_image.png").obs;
  Rx<List<Cidade>> cidades = Rx<List<Cidade>>([]);
  Rx<String> selectedUf = Rx<String>('RS');
  Rx<Cidade> selectedCity = Rx<Cidade>(Cidade());
  Rx<dynamic> selectedFile = Rx<dynamic>("");

  selectedUfChange(String uf) {
    selectedUf(uf);
  }

  fetchCitysByUf(String uf) async {
    var data = await cidadeRepository.getByUf(uf);
    cidades(data);
    selectedCity(data.first);
  }

  getImage() async {
    final XFile? image = await _picker.pickImage(source: ImageSource.gallery);
    if (image != null) {
      selectedFile(File(image.path));
    }
  }
}
