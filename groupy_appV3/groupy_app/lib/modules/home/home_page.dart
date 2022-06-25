import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '../../models/grupo.model.dart';
import '../grupo/grupoDetalhes/grupo_detalhes_page.dart';
import 'home_controller.dart';

class HomePage extends GetView<HomeController> {
  final _homeController = Get.put(HomeController());
  static final URL_IMAGES = "http://10.0.2.2:5000/";
  HomePage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Grupos'),
        actions: [
          IconButton(
            onPressed: () => Get.toNamed('grupoCadastro'),
            icon: const Icon(Icons.add),
            iconSize: 26,
          ),
          IconButton(
              onPressed: () {
                showSearch(
                    context: context,
                    delegate: CustomSarchDelegate(_homeController));
              },
              icon: const Icon(Icons.search))
        ],
      ),
      body: Column(
        children: [
          Expanded(
              child: Obx(
            () => ListView.builder(
              itemCount: _homeController.grupos.value.length,
              itemBuilder: (BuildContext context, int grupo) {
                return Card(
                  elevation: 1,
                  child: ListTile(
                    onTap: () {
                      Get.to(() => GrupoDetalhesPage(
                          grupo: _homeController.grupos.value[grupo]));
                    },
                    tileColor: const Color(0xff0F1828),
                    //textColor: const Color(0xff585554),
                    leading: CircleAvatar(
                        radius: 25,
                        backgroundImage: _homeController
                                    .grupos.value[grupo].grupoMainImage !=
                                null
                            ? NetworkImage(URL_IMAGES +
                                _homeController
                                    .grupos.value[grupo].grupoMainImage
                                    .toString())
                            : const AssetImage('assets/no_image.png')
                                as ImageProvider),
                    title: Text(
                      _homeController.grupos.value[grupo].title.toString(),
                      style: const TextStyle(
                          fontSize: 17,
                          fontWeight: FontWeight.w500,
                          color: Color(0xffF7F7FC)),
                    ),
                    subtitle: Text(
                      _homeController.grupos.value[grupo].descricao!.length > 15
                          ? _homeController.grupos.value[grupo].descricao
                                  .toString()
                                  .substring(0, 15) +
                              '...'
                          : _homeController.grupos.value[grupo].descricao!,
                      style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w500,
                          color: Color(0xffADB5BD)),
                    ),
                    trailing: Column(
                      children: [
                        const Text(
                          'Participantes',
                          style: TextStyle(color: Colors.white),
                        ),
                        Text(
                            '${_homeController.grupos.value[grupo].participantes?.length}/${_homeController.grupos.value[grupo].maximoUsuarios}',
                            style: const TextStyle(color: Colors.white))
                      ],
                    ),
                  ),
                );
              },
              padding: const EdgeInsets.all(10),
            ),
          )),
        ],
      ),
      bottomNavigationBar: Container(
        color: const Color(0xff0F1828),
        child: Row(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            IconButton(
              onPressed: () {
                Get.toNamed('/grupoCadastro');
              },
              icon: const Icon(Icons.group_add_outlined, color: Colors.white),
              iconSize: 50,
            ),
            IconButton(
              onPressed: () {
                showSearch(
                    context: context,
                    delegate: CustomSarchDelegate(_homeController));
              },
              icon: const Icon(Icons.search, color: Colors.white),
              iconSize: 50,
            ),
            IconButton(
              onPressed: () async {
                Get.toNamed('/user');
              },
              icon: const Icon(Icons.more_horiz, color: Colors.white),
              iconSize: 50,
            ),
          ],
        ),
      ),
    );
  }
}

class CustomSarchDelegate extends SearchDelegate {
  //var _homeController = HomeController();
  var controller;
  List<Grupo> searchTerms = [];

  CustomSarchDelegate(this.controller) {
    searchTerms = controller.grupos.value;
  }

  @override
  List<Widget>? buildActions(BuildContext context) {
    return [
      IconButton(
          onPressed: () {
            query = '';
          },
          icon: const Icon(Icons.clear))
    ];
  }

  @override
  Widget? buildLeading(BuildContext context) {
    return IconButton(
        onPressed: () {
          close(context, null);
        },
        icon: const Icon(Icons.arrow_back));
  }

  @override
  Widget buildResults(BuildContext context) {
    List<Grupo> machQuery = [];
    for (var term in searchTerms) {
      if (term.title.toString().toLowerCase().contains(query.toLowerCase())) {
        machQuery.add(term);
      }
    }
    return ListView.builder(
        itemCount: machQuery.length,
        itemBuilder: (context, index) {
          var result = machQuery[index];
          return Card(
            elevation: 3,
            color: Colors.white,
            shadowColor: Colors.blueGrey[50],
            child: ListTile(
                tileColor: const Color(0xff263238),
                title: Text(result.title.toString())),
          );
        });
  }

  @override
  Widget buildSuggestions(BuildContext context) {
    List<Grupo> machQuery = [];
    for (var i = 0; i < searchTerms.length; i++) {
      if (searchTerms[i].title != null &&
          searchTerms[i]
              .title
              .toString()
              .toLowerCase()
              .contains(query.toLowerCase())) {
        machQuery.add(searchTerms[i]);
      }
    }
    // for (var term in searchTerms) {
    //   if (term.title.toString().contains(query.toLowerCase())) {
    //     machQuery.add(term);
    //   }
    // }
    return ListView.builder(
        itemCount: machQuery.length,
        itemBuilder: (context, index) {
          var result = machQuery[index];
          return ListTile(
            title: Text(
              result.title.toString(),
            ),
            onTap: () {
              Get.to(() => GrupoDetalhesPage(grupo: machQuery[index]));
            },
          );
        });
  }
}
