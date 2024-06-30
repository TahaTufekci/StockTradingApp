class User {
  final int userId;
  final String email;
  final String password;

  const User({
    required this.userId,
    required this.password,
    required this.email,
  });
  const User.empty()
      : userId = 0,
        email = '',
        password = '';

  factory User.fromJson(Map<String, dynamic> json) => User(
        userId: json['userID'] ??
            1, // Provide a default value (0 in this case) if userId is null
        email: json['email'] ??
            '', // Provide a default value for other properties if needed
        password: json['password'] ?? '',
      );

  Map<String, dynamic> toJson() => {
        'userId': userId,
        'email': email,
        'password': password,
      };
}
