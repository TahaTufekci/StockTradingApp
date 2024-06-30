import 'package:flutter/material.dart';
import 'package:flutter_stock_trading_app/api_handler.dart';
import 'package:flutter_stock_trading_app/models/stock.dart';
import 'package:flutter_stock_trading_app/pages/welcome_page.dart';
import 'package:provider/provider.dart';

class BuyPage extends StatefulWidget {
  final Stock stock;

  const BuyPage({Key? key, required this.stock}) : super(key: key);

  @override
  _BuyPageState createState() => _BuyPageState();
}

class _BuyPageState extends State<BuyPage> {
  int quantity = 1;
  ApiHandler apiHandler = ApiHandler();
  late int userId;
  late double totalCost;

  @override
  void initState() {
    super.initState();
    userId =
        Provider.of<UserProvider>(context, listen: false).currentUser!.userId;
    totalCost = quantity * widget.stock.currentPrice;
  }

  void updateTotalCost() {
    setState(() {
      totalCost = quantity * widget.stock.currentPrice;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(
          'Buy ${widget.stock.stockName}',
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
          padding: const EdgeInsets.all(20.0),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text(
                'Current Price: \$${widget.stock.currentPrice.toStringAsFixed(2)}',
                style: TextStyle(fontSize: 18),
              ),
              SizedBox(height: 20),
              Text(
                'Enter the quantity you want to buy:',
                style: TextStyle(fontSize: 18),
              ),
              SizedBox(height: 20),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  IconButton(
                    icon: Icon(Icons.remove),
                    onPressed: () {
                      setState(() {
                        if (quantity > 1) {
                          quantity--;
                          updateTotalCost();
                        }
                      });
                    },
                  ),
                  Text(
                    quantity.toString(),
                    style: TextStyle(fontSize: 20),
                  ),
                  IconButton(
                    icon: Icon(Icons.add),
                    onPressed: () {
                      setState(() {
                        quantity++;
                        updateTotalCost();
                      });
                    },
                  ),
                ],
              ),
              SizedBox(height: 20),
              Text(
                'Total Cost: \$${totalCost.toStringAsFixed(2)}',
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
              SizedBox(height: 20),
              ElevatedButton(
                onPressed: () {
                  buyStock(userId, widget.stock.stockId, quantity);
                },
                style: ButtonStyle(
                  backgroundColor:
                      MaterialStateProperty.all<Color>(Colors.teal),
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
                  'Buy',
                  style: TextStyle(color: Colors.white),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  void buyStock(int userId, int stockId, int quantity) async {
    await apiHandler.buyStock(
        userId: userId, stockId: stockId, quantity: quantity);
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: Text('Purchase Successful'),
        content: Text(
            'You have successfully purchased $quantity shares of ${widget.stock.stockName}.'),
        actions: [
          TextButton(
            onPressed: () {
              Navigator.of(context).pop();
              Navigator.of(context).pop();
            },
            child: Text('OK'),
          ),
        ],
      ),
    );
  }
}
