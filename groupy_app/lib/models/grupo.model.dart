import 'package:groupy/models/tags.model.dart';
import 'package:groupy/models/user/user.model.dart';

class Grupo {
  int? id;
  String? descricao;
  int? maximoUsuarios;
  bool? isDeleted;
  List<User>? participantes;
  List<Tag>? tags;
  String? createdAt;
  String? updatedAt;
  String? deletedAt;

  Grupo(
      {this.id,
      this.descricao,
      this.maximoUsuarios,
      this.isDeleted,
      this.participantes,
      this.tags,
      this.createdAt,
      this.updatedAt,
      this.deletedAt});

  Grupo.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    descricao = json['descricao'];
    maximoUsuarios = json['maximoUsuarios'];
    isDeleted = json['isDeleted'];
    if (json['participantes'] != null) {
      participantes = <User>[];
      json['participantes'].forEach((v) {
        participantes!.add(new User.fromJson(v));
      });
    }
    if (json['tags'] != null) {
      tags = <Tag>[];
      json['tags'].forEach((v) {
        tags!.add(new Tag.fromJson(v));
      });
    }
    createdAt = json['createdAt'];
    updatedAt = json['updatedAt'];
    deletedAt = json['deletedAt'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['descricao'] = this.descricao;
    data['maximoUsuarios'] = this.maximoUsuarios;
    data['isDeleted'] = this.isDeleted;
    if (this.participantes != null) {
      data['participantes'] =
          this.participantes!.map((v) => v.toJson()).toList();
    }
    if (this.tags != null) {
      data['tags'] = this.tags!.map((v) => v.toJson()).toList();
    }
    data['createdAt'] = this.createdAt;
    data['updatedAt'] = this.updatedAt;
    data['deletedAt'] = this.deletedAt;
    return data;
  }
}