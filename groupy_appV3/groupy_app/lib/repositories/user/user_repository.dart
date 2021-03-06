import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:groupy_app/models/user/user.model.dart';

import '../../application/rest_client/rest_client.dart';
import 'iuser_repository.dart';

class UserRepository implements IUserRepository {
  @override
  Future<void> create(User user) async {
    try {
      var response = await RestClient().dio.post('user', data: user);
      if (response.statusCode == 200 || response.statusCode == 200) {
        Get.snackbar('Cadastrar Usuário', 'Usuário cadastrado com sucesso',
            backgroundColor: Colors.green);
        Get.toNamed('/login');
      } else {
        Get.snackbar('Cadastrar Usuário',
            'Não foi possível cadastrar usuário, tente novamente',
            backgroundColor: Colors.orange);
      }
    } on DioError catch (err) {
      Get.snackbar('Erro ao Usuário', err.error.toString(),
          backgroundColor: Colors.red);
    }
  }

  @override
  Future<User?> getOne(String id) async {
    try {
      var response = await RestClient().dio.get('user/$id');
      if (response.statusCode == 200) {
        var user = response.data;
        return User.fromJson(user);
      } else {
        return null;
      }
    } on DioError catch (err) {
      Get.snackbar('Erro', err.message);
    }
  }

  @override
  Future<void> update(User user) async {
    try {
      var response =
          await RestClient().dio.put('user/' + user.id.toString(), data: user);
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
      Get.snackbar('Erro ao Usuário', err.error.toString(),
          backgroundColor: Colors.red);
    }
  }
}
