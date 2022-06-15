import 'package:flutter/material.dart';

class CustomTextFormField extends StatelessWidget {
  final String label;
  final TextEditingController? controller;
  final FormFieldValidator<String>? validator;
  final bool obscureText;
  final IconButton? suffixIcon;
  final ValueNotifier<bool> _obscureTextVN;
  final String? value;
  final String? keyboardType;
  CustomTextFormField(
      {Key? key,
      required this.label,
      this.controller,
      this.validator,
      this.obscureText = false,
      this.suffixIcon,
      this.value,
      this.keyboardType})
      : _obscureTextVN = ValueNotifier<bool>(obscureText),
        assert(obscureText == true ? suffixIcon == null : true,
            'ObscureText não pode ser adicionado junto com o suffixIcon'),
        super(key: key);

  @override
  Widget build(BuildContext context) {
    return ValueListenableBuilder<bool>(
      valueListenable: _obscureTextVN,
      builder: (_, obscureTextValue, child) {
        return TextFormField(
          controller: controller,
          initialValue: value,
          validator: validator,
          obscureText: obscureTextValue,
          style: const TextStyle(
            color: Colors.white,
          ),
          decoration: InputDecoration(
            filled: true,
            fillColor: const Color(0xff152033),
            suffixIcon: obscureText
                ? IconButton(
                    onPressed: () {
                      _obscureTextVN.value = !obscureTextValue;
                    },
                    icon: Icon(
                      obscureTextValue ? Icons.lock_outlined : Icons.lock_open,
                      color: Colors.white,
                    ),
                  )
                : suffixIcon,
            hintText: label,
            hintStyle: const TextStyle(
              color: Color.fromRGBO(189, 189, 189, 1),
            ),
          ),
        );
      },
    );
  }
}
