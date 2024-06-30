import 'package:flutter/material.dart';
import 'package:flutter_stock_trading_app/api_handler.dart';
import 'package:flutter_stock_trading_app/models/portfolio.dart';
import 'package:flutter_stock_trading_app/models/userstock.dart';
import 'package:flutter_stock_trading_app/pages/sell_page.dart';
import 'package:flutter_stock_trading_app/pages/welcome_page.dart';
import 'package:provider/provider.dart';

class DashboardPage extends StatefulWidget {
  const DashboardPage({Key? key}) : super(key: key);

  @override
  State<DashboardPage> createState() => _DashboardPageState();
}

class _DashboardPageState extends State<DashboardPage> {
  ApiHandler apiHandler = ApiHandler();
  Portfolio portfolio = const Portfolio.empty();
  late int userId;

  @override
  void initState() {
    super.initState();
    // Get the portfolio data when the page initializes
    userId =
        Provider.of<UserProvider>(context, listen: false).currentUser!.userId;
    getPortfolioData();
  }

  void getPortfolioData() async {
    // Fetch the user's portfolio data from the API
    portfolio = await apiHandler.getPortfolioData(userId: userId);

    // Filter out stocks with quantity <= 0
    portfolio.stocks.removeWhere((stock) => stock.quantity <= 0);

    setState(() {});
  }

  @override
  Widget build(BuildContext context) {
    double totalProfit = portfolio.totalProfit;
    double totalProfitPercentage = portfolio.totalProfitPercentage;

    return Scaffold(
      appBar: AppBar(
        title: const Text(
          'Dashboard',
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
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'My Portfolio',
                style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
              ),
              SizedBox(height: 8),
              Text(
                'Total Profit: \$${totalProfit.toStringAsFixed(2)}',
                style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                  color: totalProfit >= 0 ? Colors.green : Colors.red,
                ),
              ),
              SizedBox(height: 8),
              Text(
                'Total Profit Percentage: ${totalProfitPercentage.toStringAsFixed(2)}%',
                style: TextStyle(
                  fontSize: 16,
                  color: totalProfitPercentage >= 0 ? Colors.green : Colors.red,
                ),
              ),
              SizedBox(height: 16),
              Expanded(
                child: ListView.builder(
                  itemCount: portfolio.stocks.length,
                  itemBuilder: (context, index) {
                    final UserStock userStock = portfolio.stocks[index];
                    double profitPercentage = userStock.profitPercentage;
                    return Card(
                      elevation: 3,
                      margin: const EdgeInsets.symmetric(vertical: 8),
                      child: ListTile(
                        title: Text(
                          portfolio.stocks[index].stockName,
                          style: TextStyle(
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                        subtitle: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(
                                "Stock ID: ${portfolio.stocks[index].stockId}"),
                            Text('Quantity: ${userStock.quantity}'),
                            Text(
                                'Purchase Price: \$${userStock.purchasePrice.toStringAsFixed(2)}'),
                            Text(
                              'Profit Percentage: ${profitPercentage.toStringAsFixed(2)}%',
                              style: TextStyle(
                                color: profitPercentage >= 0
                                    ? Colors.green
                                    : Colors.red,
                              ),
                            ),
                            Text(
                                'Current Price: \$${userStock.currentPrice.toStringAsFixed(2)}'),
                          ],
                        ),
                        trailing: IconButton(
                          icon: Icon(Icons.remove_circle),
                          onPressed: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => SellPage(
                                    userStock: userStock,
                                    userId: userId,
                                    quantity: userStock.quantity),
                              ),
                            ).then((_) {
                              // Refresh portfolio data regardless of the sell operation result
                              getPortfolioData();
                            });
                          },
                        ),
                      ),
                    );
                  },
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
