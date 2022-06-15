class Tag {
  int? id;
  String? descricao;
  bool? isDeleted;

  Tag({this.id, this.descricao, this.isDeleted});

  Tag.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    descricao = json['descricao'];
    isDeleted = json['isDeleted'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['descricao'] = descricao;
    data['isDeleted'] = isDeleted;
    return data;
  }
}
