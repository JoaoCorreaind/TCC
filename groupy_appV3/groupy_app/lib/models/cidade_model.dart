import 'package:groupy_app/models/estado_model.dart';

class Cidade {
  int? id;
  String? nome;
  Estado? estado;

  Cidade({this.id, this.nome, this.estado});

  Cidade.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    nome = json['nome'];
    estado = json['estado'] != null ? Estado.fromJson(json['estado']) : null;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = Map<String, dynamic>();
    data['id'] = id;
    data['nome'] = nome;
    if (estado != null) {
      data['estado'] = estado!.toJson();
    }
    return data;
  }
}
