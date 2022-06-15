import 'package:groupy_app/models/tags.model.dart';
import 'package:groupy_app/models/user/user.model.dart';

class Grupo {
  int? id;
  String? descricao;
  String? title;
  int? maximoUsuarios;
  bool? isDeleted;
  List<User>? participantes;
  List<Tag>? tags;
  String? createdAt;
  String? updatedAt;
  String? deletedAt;
  String? grupoMainImage;
  User? lider;
  int? liderId;

  Grupo(
      {this.id,
      this.descricao,
      this.maximoUsuarios,
      this.isDeleted,
      this.participantes,
      this.tags,
      this.createdAt,
      this.updatedAt,
      this.deletedAt,
      this.grupoMainImage,
      this.title,
      this.lider,
      this.liderId});

  Grupo.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    descricao = json['descricao'];
    maximoUsuarios = json['maximoUsuarios'];
    isDeleted = json['isDeleted'];
    if (json['participantes'] != null) {
      participantes = <User>[];
      json['participantes'].forEach((v) {
        participantes!.add(User.fromJson(v));
      });
    }
    if (json['tags'] != null) {
      tags = <Tag>[];
      json['tags'].forEach((v) {
        tags!.add(Tag.fromJson(v));
      });
    }
    createdAt = json['createdAt'];
    updatedAt = json['updatedAt'];
    deletedAt = json['deletedAt'];
    grupoMainImage = json['grupoMainImage'];
    title = json['title'];
    lider = json['lider'] != null ? new User.fromJson(json['lider']) : null;
    liderId = json['liderId'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = id;
    data['descricao'] = descricao;
    data['maximoUsuarios'] = maximoUsuarios;
    data['isDeleted'] = isDeleted;
    if (participantes != null) {
      data['participantes'] = participantes!.map((v) => v.toJson()).toList();
    }
    if (tags != null) {
      data['tags'] = tags!.map((v) => v.toJson()).toList();
    }
    data['createdAt'] = createdAt;
    data['updatedAt'] = updatedAt;
    data['deletedAt'] = deletedAt;
    data['grupoMainImage'] = grupoMainImage;
    data['title'] = title;
    if (this.lider != null) {
      data['lider'] = this.lider!.toJson();
    }
    data['liderId'] = liderId;

    return data;
  }
}
