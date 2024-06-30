import 'package:flutter/material.dart';
import 'package:flutter_stock_trading_app/api_handler.dart';
import 'package:flutter_stock_trading_app/models/stock.dart';
import 'package:flutter_stock_trading_app/models/watchlist.dart';
import 'package:flutter_stock_trading_app/pages/welcome_page.dart';
import 'package:provider/provider.dart';

class StocklistPage extends StatefulWidget {
  const StocklistPage({Key? key}) : super(key: key);

  @override
  State<StocklistPage> createState() => _StocklistPageState();
}

class _StocklistPageState extends State<StocklistPage> {
  ApiHandler apiHandler = ApiHandler();
  late List<Stock> data = [];
  late int userId;
  late Watchlist watchlistStocks;

  void getStocklistData() async {
    data = await apiHandler.getStocklistData();
    setState(() {});
  }

  Future<void> getWatchlistData() async {
    watchlistStocks = await apiHandler.getWatchlistData(userId: userId);
  }

  Future<void> addStockToWatchlist(int userId, int stockId) async {
    // Check if the stock is already in the user's watchlist
    bool isInWatchlist =
        watchlistStocks.stocks.any((stock) => stock.stockId == stockId);
    if (isInWatchlist) {
      // If the stock is already in the watchlist, show a warning
      showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: Text('Warning'),
          content: Text('This stock is already in your watchlist.'),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: Text('OK'),
            ),
          ],
        ),
      );
    } else {
      // If the stock is not in the watchlist, add it
      await apiHandler.addStockToWatchlist(userId: userId, stockId: stockId);
      // Find the stock name from the stock list
      String stockName =
          data.firstWhere((stock) => stock.stockId == stockId).stockName;
      // Show a dialog to indicate the stock was added to the watchlist
      showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: Text('Success'),
          content: Text('$stockName added to your watchlist.'),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.pop(context);
                setState(() {});
              },
              child: Text('OK'),
            ),
          ],
        ),
      );
    }
  }

  @override
  void initState() {
    super.initState();
    getStocklistData();
    userId =
        Provider.of<UserProvider>(context, listen: false).currentUser!.userId;
    getWatchlistData();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'Stocklist',
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
        child: ListView.builder(
          itemCount: data.length,
          itemBuilder: (BuildContext context, int index) {
            final stock = data[index];
            return Card(
              elevation: 3,
              margin: const EdgeInsets.symmetric(vertical: 5, horizontal: 10),
              child: ListTile(
                leading: Text(
                  "${stock.stockId}",
                  style: const TextStyle(
                    fontSize: 18,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                title: Text(stock.stockName),
                subtitle: Text("\$${stock.currentPrice.toStringAsFixed(2)}"),
                trailing: IconButton(
                  icon: const Icon(
                    Icons.add_circle_outline_sharp,
                  ),
                  onPressed: () async {
                    await getWatchlistData();
                    await addStockToWatchlist(userId, stock.stockId);
                  },
                ),
              ),
            );
          },
        ),
      ),
    );
  }
}
