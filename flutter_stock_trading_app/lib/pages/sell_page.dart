import 'package:flutter/material.dart';
import 'package:flutter_stock_trading_app/api_handler.dart';
import 'package:flutter_stock_trading_app/models/userstock.dart';

class SellPage extends StatefulWidget {
  final UserStock userStock;
  final int userId;
  final int quantity;

  const SellPage({
    Key? key,
    required this.userStock,
    required this.userId,
    required this.quantity,
  }) : super(key: key);

  @override
  _SellPageState createState() => _SellPageState();
}

class _SellPageState extends State<SellPage> {
  ApiHandler apiHandler = ApiHandler();
  late int quantity;
  late double totalCost;

  @override
  void initState() {
    super.initState();
    quantity = widget.quantity;
    totalCost = quantity * widget.userStock.currentPrice;
  }

  void updateTotalCost() {
    setState(() {
      totalCost = quantity * widget.userStock.currentPrice;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(
          'Sell ${widget.userStock.stockName}',
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
                'Current Price: \$${widget.userStock.currentPrice.toStringAsFixed(2)}',
                style: TextStyle(fontSize: 18),
              ),
              SizedBox(height: 20),
              Text(
                'Enter the quantity you want to sell:',
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
                onPressed: () async {
                  if (quantity > widget.quantity) {
                    showDialog(
                      context: context,
                      builder: (context) => AlertDialog(
                        title: Text('Warning'),
                        content: Text(
                          'You are trying to sell more shares than you have. Are you sure you want to proceed?',
                        ),
                        actions: [
                          TextButton(
                            onPressed: () {
                              Navigator.of(context).pop();
                            },
                            child: Text('Cancel'),
                          ),
                          TextButton(
                            onPressed: () {
                              Navigator.of(context).pop();
                              sellStock();
                            },
                            child: Text('Proceed'),
                          ),
                        ],
                      ),
                    );
                  } else {
                    sellStock();
                  }
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
                  'Sell',
                  style: TextStyle(color: Colors.white),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  void sellStock() {
    apiHandler
        .sellStock(
      userId: widget.userId,
      stockId: widget.userStock.stockId,
      quantity: quantity,
    )
        .then((success) {
      if (success.statusCode == 200) {
        showDialog(
          context: context,
          builder: (context) => AlertDialog(
            title: Text('Sell Successful'),
            content: Text(
              'You have successfully sold $quantity shares of ${widget.userStock.stockName}.',
            ),
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
      } else {
        showDialog(
          context: context,
          builder: (context) => AlertDialog(
            title: Text('Sell Failed'),
            content: Text(
              'Failed to sell ${widget.userStock.stockName}. Please try again later.',
            ),
            actions: [
              TextButton(
                onPressed: () {
                  Navigator.of(context).pop();
                },
                child: Text('OK'),
              ),
            ],
          ),
        );
      }
    });
  }
}
