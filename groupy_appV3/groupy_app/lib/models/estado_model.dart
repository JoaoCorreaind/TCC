class Estado {
  int? id;
  String? nome;
  String? uf;

  Estado({this.id, this.nome, this.uf});

  Estado.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    nome = json['nome'];
    uf = json['uf'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = Map<String, dynamic>();
    data['id'] = id;
    data['nome'] = nome;
    data['uf'] = uf;
    return data;
  }
}
