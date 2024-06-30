import 'package:flutter/material.dart';
import 'package:flutter_stock_trading_app/pages/dashboard_page.dart'; // Import the first page
import 'package:flutter_stock_trading_app/pages/stocklist_page.dart'; // Import the second page
import 'package:flutter_stock_trading_app/pages/watchlist_page.dart'; // Import the third page

class MainPage extends StatelessWidget {
  const MainPage({Key? key}) : super(key: key);

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
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Icon(
                Icons.trending_up,
                size: 100,
                color: Colors.teal,
              ),
              const SizedBox(
                  height: 30), // Add space between the icon and buttons
              ElevatedButton.icon(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => const DashboardPage(),
                    ), // Navigate to the first page
                  );
                },
                style: ButtonStyle(
                  minimumSize: MaterialStateProperty.all<Size>(
                      const Size(300, 50)), // Set the button size
                  backgroundColor: MaterialStateProperty.all<Color>(
                      Colors.teal), // Set background color here
                  padding: MaterialStateProperty.all<EdgeInsetsGeometry>(
                      const EdgeInsets.symmetric(vertical: 15, horizontal: 30)),
                  textStyle: MaterialStateProperty.all<TextStyle>(
                    const TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                icon: Icon(
                  Icons.arrow_forward,
                  color: Colors.white,
                ),
                label: const Text(
                  'Dashboard Screen',
                  style:
                      TextStyle(color: Colors.white), // Set text color to white
                ),
              ),
              const SizedBox(height: 30), // Add space between the buttons
              ElevatedButton.icon(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => const WatchlistPage(),
                    ), // Navigate to the second page
                  );
                },
                style: ButtonStyle(
                  minimumSize: MaterialStateProperty.all<Size>(
                      const Size(300, 50)), // Set the button size
                  backgroundColor: MaterialStateProperty.all<Color>(
                      Colors.teal), // Set background color here
                  padding: MaterialStateProperty.all<EdgeInsetsGeometry>(
                      const EdgeInsets.symmetric(vertical: 15, horizontal: 30)),
                  textStyle: MaterialStateProperty.all<TextStyle>(
                    const TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                icon: Icon(
                  Icons.remove_red_eye,
                  color: Colors.white,
                ),
                label: const Text(
                  'Watchlist Screen',
                  style:
                      TextStyle(color: Colors.white), // Set text color to white
                ),
              ),
              const SizedBox(height: 30), // Add space between the buttons
              ElevatedButton.icon(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => const StocklistPage(),
                    ), // Navigate to the third page
                  );
                },
                style: ButtonStyle(
                  minimumSize: MaterialStateProperty.all<Size>(
                      const Size(300, 50)), // Set the button size
                  backgroundColor: MaterialStateProperty.all<Color>(
                      Colors.teal), // Set background color here
                  padding: MaterialStateProperty.all<EdgeInsetsGeometry>(
                      const EdgeInsets.symmetric(vertical: 15, horizontal: 30)),
                  textStyle: MaterialStateProperty.all<TextStyle>(
                    const TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
                icon: Icon(
                  Icons.attach_file,
                  color: Colors.white,
                ),
                label: const Text(
                  'Stocklist Screen',
                  style:
                      TextStyle(color: Colors.white), // Set text color to white
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
