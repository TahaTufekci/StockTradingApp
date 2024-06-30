import 'package:flutter_stock_trading_app/models/stock.dart';

class Watchlist {
  final int watchlistId;
  final int userId;
  final List<Stock> stocks;

  const Watchlist({
    required this.watchlistId,
    required this.userId,
    required this.stocks,
  });

  const Watchlist.empty()
      : watchlistId = 0,
        userId = 0,
        stocks = const [];

  factory Watchlist.fromJson(Map<String, dynamic> json) {
    final List<dynamic> stocksJson = json['stocks'];
    final List<Stock> stocks =
        stocksJson.map((stockJson) => Stock.fromJson(stockJson)).toList();

    return Watchlist(
      watchlistId: json['watchlistId'],
      userId: json['userId'],
      stocks: stocks,
    );
  }

  Map<String, dynamic> toJson() => {
        'watchlistId': watchlistId,
        'userId': userId,
        'stocks': stocks.map((stock) => stock.toJson()).toList(),
      };
}
