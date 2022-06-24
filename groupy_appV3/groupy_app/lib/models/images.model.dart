class Image {
  int? id;
  String? name;
  String? path;

  Image({this.id, this.name, this.path});

  Image.fromJson(Map<String, dynamic> json) {
    id = json['id'];
    name = json['name'];
    path = json['path'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['id'] = id;
    data['name'] = name;
    data['path'] = path;
    return data;
  }
}
