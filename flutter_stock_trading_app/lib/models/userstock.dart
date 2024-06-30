class UserStock {
  final String stockName;
  final int quantity;
  final double purchasePrice;
  final double currentPrice;
  final double profit;
  final double profitPercentage;
  final int stockId;

  const UserStock({
    required this.stockId,
    required this.stockName,
    required this.quantity,
    required this.purchasePrice,
    required this.currentPrice,
    required this.profit,
    required this.profitPercentage,
  });

  factory UserStock.fromJson(Map<String, dynamic> json) {
    return UserStock(
      stockId: json['stockId'],
      stockName: json['stockName'],
      quantity: json['quantity'],
      purchasePrice: json['purchasePrice'],
      currentPrice: json['currentPrice'],
      profit: json['profit'],
      profitPercentage: json['profitPercentage'],
    );
  }

  Map<String, dynamic> toJson() => {
        'stockId': stockId,
        'stockName': stockName,
        'quantity': quantity,
        'purchasePrice': purchasePrice,
        'currentPrice': currentPrice,
        'profit': profit,
        'profitPercentage': profitPercentage,
      };
}
