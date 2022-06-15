import 'package:flutter/material.dart';

class CustomButtonWidget extends StatelessWidget {
  final String title;
  final VoidCallback onPressed;
  const CustomButtonWidget(
      {required this.title, required this.onPressed, Key? key})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ElevatedButton(
      onPressed: onPressed,
      child: Text(title),
      style: ButtonStyle(
        fixedSize: MaterialStateProperty.all(const Size(300, 60)),
        backgroundColor: MaterialStateProperty.all(Colors.blue),
        padding: MaterialStateProperty.all(const EdgeInsets.all(20)),
        textStyle: MaterialStateProperty.all(const TextStyle(fontSize: 16)),
        shape: MaterialStateProperty.all<RoundedRectangleBorder>(
          RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(25),
            side: const BorderSide(
              color: Colors.transparent,
            ),
          ),
        ),
      ),
    );
  }
}
