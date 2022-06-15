class Uf {
  String? sigla;
  String? nome;

  Uf(this.sigla, this.nome);
}

List<Uf> getUfs() {
  return [
    Uf("AC", "Acre"),
    Uf("AL", "Alagoas"),
    Uf("AP", "Amapá"),
    Uf("AM", "Amazonas"),
    Uf("BA", "Bahia"),
    Uf("CE", "Ceará"),
    Uf("DF", "Distrito Federal"),
    Uf("ES", "Espírito Santo"),
    Uf("GO", "Goiás"),
    Uf("MA", "Maranhão"),
    Uf("MT", "Mato Grosso"),
    Uf("MS", "Mato Grosso do Sul"),
    Uf("MG", "Minas Gerais"),
    Uf("PA", "Pará"),
    Uf("PB", "Paraíba"),
    Uf("PR", "Paraná"),
    Uf("PE", "Pernambuco"),
    Uf("PI", "Piauí"),
    Uf("RJ", "Rio de Janeiro"),
    Uf("RN", "Rio Grande do Norte"),
    Uf("RS", "Rio Grande do Sul"),
    Uf("RO", "Rondônia"),
    Uf("RR", "Roraima"),
    Uf("SC", "Santa Catarina"),
    Uf("SP", "São Paulo"),
    Uf("SE", "Sergipe"),
    Uf("TO", "Tocantins"),
  ];
}
