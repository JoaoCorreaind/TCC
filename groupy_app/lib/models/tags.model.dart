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
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['descricao'] = this.descricao;
    data['isDeleted'] = this.isDeleted;
    return data;
  }
}