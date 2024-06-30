import 'package:flutter_stock_trading_app/models/userstock.dart';

class Portfolio {
  final double totalProfit;
  final double totalProfitPercentage;
  final List<UserStock> stocks;

  const Portfolio({
    required this.totalProfit,
    required this.totalProfitPercentage,
    required this.stocks,
  });

  const Portfolio.empty()
      : totalProfit = 0,
        totalProfitPercentage = 0,
        stocks = const [];

  factory Portfolio.fromJson(Map<String, dynamic> json) {
    final List<dynamic> stocksJson = json['stocks'];
    final List<UserStock> stocks =
        stocksJson.map((stockJson) => UserStock.fromJson(stockJson)).toList();

    return Portfolio(
      totalProfit: json['totalProfit'],
      totalProfitPercentage: json['totalProfitPercentage'],
      stocks: stocks,
    );
  }

  Map<String, dynamic> toJson() => {
        'totalProfit': totalProfit,
        'totalProfitPercentage': totalProfitPercentage,
        'stocks': stocks.map((stock) => stock.toJson()).toList(),
      };
}
