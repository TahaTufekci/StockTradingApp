class Stock {
  final int stockId;
  final String stockName;
  final double currentPrice;

  const Stock({
    required this.stockId,
    required this.stockName,
    required this.currentPrice,
  });

  factory Stock.fromJson(Map<String, dynamic> json) {
    return Stock(
      stockId: json['stockID'],
      stockName: json['stockName'],
      currentPrice: json['currentPrice'],
    );
  }

  Map<String, dynamic> toJson() => {
        'stockId': stockId,
        'stockName': stockName,
        'currentPrice': currentPrice,
      };
}
