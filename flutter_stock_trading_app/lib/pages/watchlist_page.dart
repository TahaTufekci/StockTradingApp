import 'package:flutter/material.dart';
import 'package:flutter_stock_trading_app/api_handler.dart';
import 'package:flutter_stock_trading_app/models/watchlist.dart';
import 'package:flutter_stock_trading_app/pages/buy_page.dart';
import 'package:flutter_stock_trading_app/pages/welcome_page.dart';
import 'package:provider/provider.dart';

class WatchlistPage extends StatefulWidget {
  const WatchlistPage({Key? key}) : super(key: key);

  @override
  State<WatchlistPage> createState() => _WatchlistPageState();
}

class _WatchlistPageState extends State<WatchlistPage> {
  ApiHandler apiHandler = ApiHandler();
  Watchlist watchlist = const Watchlist.empty();
  late int userId;

  void getWatchlistData(int userId) async {
    watchlist = await apiHandler.getWatchlistData(userId: userId);
    setState(() {});
  }

  void deleteStockFromWatchlist(int userId, int stockId) async {
    await apiHandler.deleteStockFromWatchlist(userId: userId, stockId: stockId);
    getWatchlistData(userId);
    setState(() {});
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    userId = Provider.of<UserProvider>(context).currentUser!.userId;
    getWatchlistData(userId);
  }

  @override
  Widget build(BuildContext context) {
    // Access the UserProvider to retrieve the userId
    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'Watchlist',
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
        child: Column(
          children: [
            const SizedBox(height: 20),
            Expanded(
              child: ListView.builder(
                itemCount: watchlist.stocks.length,
                itemBuilder: (context, index) {
                  final stock = watchlist.stocks[index];
                  return Card(
                    elevation: 3,
                    margin:
                        const EdgeInsets.symmetric(vertical: 5, horizontal: 10),
                    child: ListTile(
                      leading: Text(
                        "${stock.stockId}",
                        style: const TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                      title: Text(stock.stockName),
                      subtitle: Text(
                          'Price: \$${stock.currentPrice.toStringAsFixed(2)}'),
                      trailing: Row(
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          IconButton(
                            icon: Icon(Icons.remove_circle),
                            onPressed: () {
                              deleteStockFromWatchlist(userId, stock.stockId);
                            },
                          ),
                          IconButton(
                            icon: Icon(Icons.shopping_cart),
                            onPressed: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                  builder: (context) => BuyPage(stock: stock),
                                ),
                              );
                            },
                          ),
                        ],
                      ),
                    ),
                  );
                },
              ),
            ),
          ],
        ),
      ),
    );
  }
}
