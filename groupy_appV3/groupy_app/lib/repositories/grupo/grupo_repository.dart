import 'package:groupy_app/application/rest_client/rest_client.dart';
import 'package:groupy_app/models/grupo.model.dart';

import 'igrupo_repository.dart';

class GrupoRepository implements IGrupoRepository {
  @override
  Future<void> create({required Grupo grupo}) {
    // TODO: implement create
    throw UnimplementedError();
  }

  @override
  Future<List<Grupo>> getAll() async {
    var response = await RestClient().dio.get('/grupo');
    var list = response.data as List;
    List<Grupo> listGrupo = [];
    for (var json in list) {
      final grupo = Grupo.fromJson(json);
      listGrupo.add(grupo);
    }
    return listGrupo;
  }

  @override
  Future<Grupo> getById({required String id}) {
    // TODO: implement getById
    throw UnimplementedError();
  }

  @override
  Future<void> update({required String id, required Grupo grupo}) {
    // TODO: implement update
    throw UnimplementedError();
  }
}



// import 'package:groupy_app/models/tags.model.dart';
// import 'package:groupy_app/models/user/user.model.dart';

// import '../models/grupo.model.dart';

// class GrupoRepository {
//   static List<Grupo> table = [
//     Grupo(
//         id: 1,
//         isDeleted: false,
//         maximoUsuarios: 10,
//         createdAt: DateTime.now().toString(),
//         tags: [Tag(id: 1, descricao: 'tag 1'), Tag(id: 2, descricao: 'tag 2')],
//         title: 'Grupo de combate aos transformers',
//         descricao:
//             'grupo para combater transformers descricaogrupo para combater transformers descricaogrupo para combater transformers descricaogrupo para combater transformers descricao',
//         grupoMainImage:
//             'https://i.pinimg.com/474x/d2/53/cd/d253cde303ec24235223d9f204b8d1df.jpg',
//         participantes: [
//           User(
//               id: 3,
//               nome: 'Juninho pernambucado',
//               cpf: '04526329002',
//               rg: '44515151515'),
//           User(
//               id: 4,
//               nome: 'Juninho brasileira',
//               cpf: '0545564545',
//               rg: '111111111'),
//         ]),
//     Grupo(
//         id: 1,
//         isDeleted: false,
//         maximoUsuarios: 10,
//         createdAt: DateTime.now().toString(),
//         tags: [Tag(id: 1, descricao: 'tag 1'), Tag(id: 2, descricao: 'tag 2')],
//         title: 'Grupo de combate aos transformers',
//         descricao:
//             'grupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformersgrupo de combate aos transformers',
//         participantes: [
//           User(
//               id: 3,
//               nome: 'Juninho pernambucado',
//               cpf: '04526329002',
//               rg: '44515151515'),
//           User(
//               id: 4,
//               nome: 'Juninho brasileira',
//               cpf: '0545564545',
//               rg: '111111111'),
//         ]),
//     Grupo(
//         id: 2,
//         isDeleted: false,
//         maximoUsuarios: 5,
//         createdAt: DateTime.now().toString(),
//         tags: [Tag(id: 1, descricao: 'tag 1'), Tag(id: 2, descricao: 'tag 2')],
//         title: 'Grupo de combate aos deceptiocons',
//         descricao:
//             'grupo de combate aos deceptiocons deceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptioconsdeceptiocons',
//         grupoMainImage:
//             'https://gartic.com.br/imgs/mural/pe/petrovskar/sonic-muito-foda.png',
//         participantes: [
//           User(
//               id: 3,
//               nome: 'Juninho pernambucado',
//               cpf: '04526329002',
//               rg: '44515151515'),
//           User(
//               id: 4,
//               nome: 'Juninho brasileira',
//               cpf: '0545564545',
//               rg: '111111111'),
//         ]),
//     Grupo(
//         id: 33,
//         isDeleted: false,
//         maximoUsuarios: 10,
//         createdAt: DateTime.now().toString(),
//         tags: [Tag(id: 1, descricao: 'tag 1'), Tag(id: 2, descricao: 'tag 2')],
//         title: 'grupo de youtubers kkk',
//         descricao:
//             'grupo de youtubers kkkyoutubers kkkyoutubers kkkyoutubers kkkyoutubers kkkyoutubers kkkyoutubers kkkyoutubers kkkyoutubers kkkyoutubers kkkyoutubers kkk kkk ',
//         grupoMainImage:
//             'https://images7.memedroid.com/images/UPLOADED324/6225453ff2d8a.jpeg',
//         participantes: [
//           User(
//               id: 3,
//               nome: 'Juninho pernambucado',
//               cpf: '04526329002',
//               rg: '44515151515'),
//           User(
//               id: 4,
//               nome: 'Juninho brasileira',
//               cpf: '0545564545',
//               rg: '111111111'),
//         ]),
//     Grupo(
//         id: 3,
//         isDeleted: false,
//         maximoUsuarios: 10,
//         createdAt: DateTime.now().toString(),
//         tags: [Tag(id: 1, descricao: 'tag 1'), Tag(id: 2, descricao: 'tag 2')],
//         title: 'grupo de combate mata gente',
//         descricao:
//             'grupo de combate mata gente gentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegentegente',
//         grupoMainImage: 'https://i.ytimg.com/vi/wyygPx0Feuk/sddefault.jpg',
//         participantes: [
//           User(
//               id: 3,
//               nome: 'Juninho pernambucado',
//               cpf: '04526329002',
//               rg: '44515151515'),
//           User(
//               id: 4,
//               nome: 'Juninho brasileira',
//               cpf: '0545564545',
//               rg: '111111111'),
//         ]),
//     Grupo(
//         id: 5,
//         isDeleted: false,
//         maximoUsuarios: 10,
//         createdAt: DateTime.now().toString(),
//         tags: [Tag(id: 1, descricao: 'tag 1'), Tag(id: 2, descricao: 'tag 2')],
//         title: 'grupo de joga um fut',
//         descricao:
//             'grupo de jogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajogajoga um fut ',
//         participantes: [
//           User(
//               id: 3,
//               nome: 'Juninho pernambucado',
//               cpf: '04526329002',
//               rg: '44515151515'),
//           User(
//               id: 4,
//               nome: 'Juninho brasileira',
//               cpf: '0545564545',
//               rg: '111111111'),
//         ]),
//     Grupo(
//         id: 6,
//         isDeleted: false,
//         maximoUsuarios: 10,
//         createdAt: DateTime.now().toString(),
//         tags: [Tag(id: 1, descricao: 'tag 1'), Tag(id: 2, descricao: 'tag 2')],
//         descricao:
//             'grupo de ve uns animezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezinanimezin',
//         title: 'grupo de ve uns animezin',
//         participantes: [
//           User(
//               id: 3,
//               nome: 'Juninho pernambucado',
//               cpf: '04526329002',
//               rg: '44515151515'),
//           User(
//               id: 4,
//               nome: 'Juninho brasileira',
//               cpf: '0545564545',
//               rg: '111111111'),
//         ]),
//     Grupo(
//         id: 7,
//         isDeleted: false,
//         maximoUsuarios: 10,
//         createdAt: DateTime.now().toString(),
//         tags: [Tag(id: 1, descricao: 'tag 1'), Tag(id: 2, descricao: 'tag 2')],
//         descricao:
//             'party legendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegendslegends',
//         title: 'party legends',
//         participantes: [
//           User(
//               id: 3,
//               nome: 'Juninho pernambucado',
//               cpf: '04526329002',
//               rg: '44515151515'),
//           User(
//               id: 4,
//               nome: 'Juninho brasileira',
//               cpf: '0545564545',
//               rg: '111111111'),
//         ]),
//     Grupo(
//         id: 15,
//         isDeleted: false,
//         maximoUsuarios: 10,
//         createdAt: DateTime.now().toString(),
//         tags: [Tag(id: 1, descricao: 'tag 1'), Tag(id: 2, descricao: 'tag 2')],
//         descricao:
//             'cabo a criatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividadecriatividade',
//         title: 'cabo a criatividade',
//         participantes: [
//           User(
//               id: 3,
//               nome: 'Juninho pernambucado',
//               cpf: '04526329002',
//               rg: '44515151515'),
//           User(
//               id: 4,
//               nome: 'Juninho brasileira',
//               cpf: '0545564545',
//               rg: '111111111'),
//         ]),
//   ];
// }


