class User {
  int? id;
  String? email;
  String? password;
  String? nome;
  String? cpf;
  String? rg;
  bool? isDeleted;
  String? createdAt;
  String? updatedAt;
  String? deletedAt;

  User(
      {this.id,
      this.email,
      this.password,
      this.nome,
      this.cpf,
      this.rg,
      this.isDeleted,
      this.createdAt,
      this.updatedAt,
      this.deletedAt});

  User.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    email = json['email'];
    password = json['password'];
    nome = json['nome'];
    cpf = json['cpf'];
    rg = json['rg'];
    isDeleted = json['isDeleted'];
    createdAt = json['createdAt'];
    updatedAt = json['updatedAt'];
    deletedAt = json['deletedAt'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['email'] = this.email;
    data['password'] = this.password;
    data['nome'] = this.nome;
    data['cpf'] = this.cpf;
    data['rg'] = this.rg;
    data['isDeleted'] = this.isDeleted;
    data['createdAt'] = this.createdAt;
    data['updatedAt'] = this.updatedAt;
    data['deletedAt'] = this.deletedAt;
    return data;
  }
}
