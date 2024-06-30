import 'package:flutter/material.dart';
import 'package:flutter_stock_trading_app/api_handler.dart';
import 'package:flutter_stock_trading_app/models/user.dart';
import 'package:flutter_stock_trading_app/pages/main_page.dart'; // Import the main page
import 'package:provider/provider.dart';

class WelcomePage extends StatefulWidget {
  const WelcomePage({Key? key}) : super(key: key);

  @override
  _WelcomePageState createState() => _WelcomePageState();
}

class _WelcomePageState extends State<WelcomePage> {
  TextEditingController _emailController = TextEditingController();
  TextEditingController _passwordController = TextEditingController();
  ApiHandler apiHandler = ApiHandler();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'Stock Trading App',
          style: TextStyle(
            fontWeight: FontWeight.bold,
          ),
        ),
        centerTitle: true,
        backgroundColor: Colors.teal,
        foregroundColor: Colors.white,
      ),
      body: Container(
        decoration: BoxDecoration(
          gradient: LinearGradient(
            begin: Alignment.topCenter,
            end: Alignment.bottomCenter,
            colors: [Colors.teal.shade50, Colors.teal.shade100],
          ),
        ),
        child: Padding(
          padding: EdgeInsets.all(20.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              // Icon placed in the middle of the screen
              Icon(
                Icons.account_circle,
                size: 100,
                color: Colors.teal,
              ),
              SizedBox(height: 20),
              TextField(
                controller: _emailController,
                decoration: InputDecoration(
                  labelText: 'Email',
                ),
              ),
              SizedBox(height: 20),
              TextField(
                controller: _passwordController,
                obscureText: true,
                decoration: InputDecoration(
                  labelText: 'Password',
                ),
              ),
              SizedBox(height: 20),
              ElevatedButton(
                onPressed: () async {
                  String email = _emailController.text.trim();
                  String password = _passwordController.text.trim();

                  try {
                    await Provider.of<UserProvider>(context, listen: false)
                        .fetchUserData(email, password);
                    Navigator.push(
                      context,
                      MaterialPageRoute(builder: (context) => MainPage()),
                    );
                  } catch (e) {
                    showDialog(
                      context: context,
                      builder: (context) => AlertDialog(
                        title: Text('Error'),
                        content: Text('Invalid email or password.'),
                        actions: [
                          TextButton(
                            onPressed: () => Navigator.pop(context),
                            child: Text('OK'),
                          ),
                        ],
                      ),
                    );
                  }
                },
                style: ButtonStyle(
                  backgroundColor: MaterialStateProperty.all<Color>(
                      Colors.teal), // Set background color here
                  padding: MaterialStateProperty.all<EdgeInsetsGeometry>(
                      const EdgeInsets.symmetric(vertical: 15, horizontal: 30)),
                  textStyle: MaterialStateProperty.all<TextStyle>(
                    const TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                      color: Colors.white,
                    ),
                  ),
                ),
                child: Text(
                  'Enter',
                  style: TextStyle(color: Colors.white),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class UserProvider extends ChangeNotifier {
  late User? currentUser;
  final ApiHandler _apiHandler = ApiHandler();

  // Method to fetch user data from the API
  Future<void> fetchUserData(String email, String password) async {
    // Changed parameter name from email to password
    try {
      final List<User> users = await _apiHandler.getUserData();
      final defaultUser = User.empty();
      final User? user = users.firstWhere(
        (user) =>
            user.email == email &&
            user.password == password, // Changed from email to password
        orElse: () => defaultUser,
      );

      if (user != defaultUser) {
        currentUser = user;
        notifyListeners();
      } else {
        throw Exception('User not found');
      }
    } catch (e) {
      print('Error fetching user data: $e');
      throw Exception('Failed to fetch user data');
    }
  }
}
