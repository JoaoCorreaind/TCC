import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:get/get_core/src/get_main.dart';
import 'package:groupy_app/application/rest_client/rest_client.dart';
import 'package:groupy_app/models/grupo.model.dart';
import 'package:groupy_app/models/user/user.model.dart';

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
  Future<Grupo?> getOne({required String id}) async {
    try {
      var response = await RestClient().dio.get('/grupo/' + id);
      var Grupo = response.data;
      return Grupo.fromJson(Grupo);
    } on DioError catch (err) {
      Get.snackbar(
          'Erro', 'Ocorreu um erro interno no servidor, tente mais tarde',
          backgroundColor: Colors.red);
    }
  }

  @override
  Future<void> update({required Grupo grupo}) async {
    try {
      var response = await RestClient()
          .dio
          .put('user/' + grupo.id.toString(), data: grupo);
      if (response.statusCode == 200 || response.statusCode == 200) {
        Get.snackbar('Editar Usuário', 'Usuário editado com sucesso',
            backgroundColor: Colors.green);
        Get.toNamed('/login');
      } else {
        Get.snackbar('Editar Usuário',
            'Não foi possível editar usuário, tente novamente',
            backgroundColor: Colors.orange);
      }
    } on DioError catch (err) {
      Get.snackbar(
          'Erro', 'Ocorreu um erro interno no servidor, tente mais tarde',
          backgroundColor: Colors.red);
    }
  }

  @override
  Future<List<Grupo>> getGroupsByUser({required String id}) async {
    try {
      var response = await RestClient().dio.get('/grupo/' + id + '/user');
      var list = response.data as List;
      List<Grupo> listGrupo = [];
      for (var json in list) {
        final grupo = Grupo.fromJson(json);
        listGrupo.add(grupo);
      }
      return listGrupo;
    } on DioError catch (err) {
      Get.snackbar(
          'Erro', 'Ocorreu um erro interno no servidor, tente mais tarde',
          backgroundColor: Colors.red);
      return [];
    }
  }

  @override
  Future<List<Grupo>> getGroupsByleader({required String id}) async {
    try {
      var response = await RestClient().dio.get('/grupo/' + id + '/leader');
      var list = response.data as List;
      List<Grupo> listGrupo = [];
      for (var json in list) {
        final grupo = Grupo.fromJson(json);
        listGrupo.add(grupo);
      }
      return listGrupo;
    } on DioError catch (err) {
      Get.snackbar(
          'Erro', 'Ocorreu um erro interno no servidor, tente mais tarde',
          backgroundColor: Colors.red);
      return [];
    }
  }

  @override
  Future<bool> addMember(
      {required String idUsuario, required String idGrupo}) async {
    var response = await RestClient().dio.post('/grupo/addMember',
        data: {'GrupoId': idGrupo, 'UserId': idUsuario});
    if (response.statusCode == 200) {
      return true;
    } else {
      return false;
    }
  }

  @override
  Future<bool> removeMember(
      {required String idUsuario, required String idGrupo}) async {
    var response = await RestClient().dio.post('/grupo/removeMember',
        data: {'GrupoId': idGrupo, 'UserId': idUsuario});
    if (response.statusCode == 200) {
      return true;
    } else {
      return false;
    }
  }

  @override
  Future<List<User>> getMembers({required String idGrupo}) async {
    var response = await RestClient().dio.get('/grupo/getMembers/' + idGrupo);
    var usersResponse = response.data as List;
    List<User> listUsers = [];
    for (var user in usersResponse) {
      listUsers.add(User.fromJson(user));
    }
    return listUsers;
  }
}
