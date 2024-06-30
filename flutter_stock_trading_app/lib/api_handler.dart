import 'dart:convert';

import 'package:flutter_stock_trading_app/models/portfolio.dart';
import 'package:flutter_stock_trading_app/models/stock.dart';
import 'package:flutter_stock_trading_app/models/user.dart';
import 'package:flutter_stock_trading_app/models/userstock.dart';
import 'package:flutter_stock_trading_app/models/watchlist.dart';
import 'package:http/http.dart' as http;

class ApiHandler {
  final String userUri = "https://localhost:7034/api/user";
  final String stocklistUri = "https://localhost:7034/api/Stocklist";
  final String watchlistUri = "https://localhost:7034/api/Watchlist";
  final String buySellUri = "https://localhost:7034/api/BuySell";
  final String dashboardUri = "https://localhost:7034/api/Dashboard";

  Future<Portfolio> getPortfolioData({required int userId}) async {
    Portfolio? portfolio;
    final uri = Uri.parse("$dashboardUri/portfolio/$userId");

    try {
      final response = await http.get(uri, headers: {
        "Accept": "application/json",
        "Access-Control_Allow_Origin": "*",
        "Access-Control-Allow-Headers": "Access-Control-Allow-Origin, Accept"
      });

      if (response.statusCode >= 200 && response.statusCode <= 299) {
        final Map<String, dynamic> jsonData = json.decode(response.body);
        final List<dynamic> stocksJson = jsonData['stocks'];
        final List<UserStock> stocks = stocksJson
            .map((userStockJson) => UserStock.fromJson(userStockJson))
            .toList();

        portfolio = Portfolio(
          totalProfit: jsonData['totalProfit'],
          totalProfitPercentage: jsonData['totalProfitPercentage'],
          stocks: stocks,
        );
      } else {
        print('Request failed with status: ${response.statusCode}');
        print('Response body: ${response.body}');
      }
    } catch (e) {
      print('Error: ' + e.toString());
    }

    // If portfolio is null, return an empty portfolio
    return portfolio ?? const Portfolio.empty();
  }

  Future<http.Response> sellStock(
      {required int userId,
      required int stockId,
      required int quantity}) async {
    final uri = Uri.parse("$buySellUri/sellFromPortfolio");
    final Map<String, dynamic> requestBody = {
      'userId': userId,
      'stockId': stockId,
      'quantity': quantity,
    };
    try {
      final response = await http.post(
        uri,
        headers: {
          "Accept": "application/json",
          "Content-Type": "application/json", // Set content type to JSON
          "Access-Control_Allow_Origin": "*",
          "Access-Control-Allow-Headers": "Access-Control-Allow-Origin, Accept"
        },
        body: json.encode(requestBody), // Encode the request body as JSON
      );

      if (response.statusCode >= 200 && response.statusCode <= 299) {
        return response;
      } else {
        print('Request failed with status: ${response.statusCode}');
        print('Response body: ${response.body}');
        return response;
      }
    } catch (e) {
      return http.Response('Error', 500);
    }
  }

  Future<http.Response> buyStock(
      {required int userId,
      required int stockId,
      required int quantity}) async {
    final uri = Uri.parse("$buySellUri/buyFromWatchlist");
    final Map<String, dynamic> requestBody = {
      'userId': userId,
      'stockId': stockId,
      'quantity': quantity,
    };
    try {
      final response = await http.post(
        uri,
        headers: {
          "Accept": "application/json",
          "Content-Type": "application/json", // Set content type to JSON
          "Access-Control_Allow_Origin": "*",
          "Access-Control-Allow-Headers": "Access-Control-Allow-Origin, Accept"
        },
        body: json.encode(requestBody), // Encode the request body as JSON
      );

      if (response.statusCode >= 200 && response.statusCode <= 299) {
        return response;
      } else {
        print('Request failed with status: ${response.statusCode}');
        print('Response body: ${response.body}');
        return response;
      }
    } catch (e) {
      return http.Response('Error', 500);
    }
  }

  Future<http.Response> addStockToWatchlist(
      {required int userId, required int stockId}) async {
    final uri = Uri.parse("$watchlistUri/add?userId=$userId&stockId=$stockId");

    try {
      final response = await http.post(uri, headers: {
        "Accept": "application/json",
        "Access-Control_Allow_Origin": "*",
        "Access-Control-Allow-Headers": "Access-Control-Allow-Origin, Accept"
      });

      if (response.statusCode >= 200 && response.statusCode <= 299) {
        return response;
      } else {
        print('Request failed with status: ${response.statusCode}');
        print('Response body: ${response.body}');
        return response;
      }
    } catch (e) {
      return http.Response('Error', 500);
    }
  }

  Future<http.Response> deleteStockFromWatchlist(
      {required int userId, required int stockId}) async {
    final uri =
        Uri.parse("$watchlistUri/remove?userId=$userId&stockId=$stockId");

    try {
      final response = await http.delete(uri, headers: {
        "Accept": "application/json",
        "Access-Control_Allow_Origin": "*",
        "Access-Control-Allow-Headers": "Access-Control-Allow-Origin, Accept"
      });

      if (response.statusCode >= 200 && response.statusCode <= 299) {
        return response;
      } else {
        print('Request failed with status: ${response.statusCode}');
        print('Response body: ${response.body}');
        return response;
      }
    } catch (e) {
      return http.Response('Error', 500);
    }
  }

  Future<List<Stock>> getStocklistData() async {
    List<Stock> data = [];

    final uri = Uri.parse(stocklistUri);

    try {
      final response = await http.get(uri, headers: {
        "Accept": "application/json",
        "Access-Control_Allow_Origin": "*",
        "Access-Control-Allow-Headers": "Access-Control-Allow-Origin, Accept"
      });

      if (response.statusCode >= 200 && response.statusCode <= 299) {
        final List<dynamic> jsonData = json.decode(response.body);
        data = jsonData.map((json) => Stock.fromJson(json)).toList();
      } else {
        print('Request failed with status: ${response.statusCode}');
        print('Response body: ${response.body}');
      }
    } catch (e) {
      return data;
    }
    return data;
  }

  Future<Watchlist> getWatchlistData({required int userId}) async {
    Watchlist? watchlist;

    final uri = Uri.parse("$watchlistUri?userId=$userId");

    try {
      final response = await http.get(uri, headers: {
        "Accept": "application/json",
        "Access-Control_Allow_Origin": "*",
        "Access-Control-Allow-Headers": "Access-Control-Allow-Origin, Accept"
      });

      if (response.statusCode >= 200 && response.statusCode <= 299) {
        final Map<String, dynamic> jsonData = json.decode(response.body);
        final List<dynamic> stocksJson = jsonData['stocks'];
        final List<Stock> stocks =
            stocksJson.map((stockJson) => Stock.fromJson(stockJson)).toList();

        watchlist = Watchlist(
          watchlistId: jsonData['watchlistID'],
          userId: jsonData['userID'],
          stocks: stocks,
        );
      } else {
        print('Request failed with status: ${response.statusCode}');
        print('Response body: ${response.body}');
      }
    } catch (e) {
      print('Error: ' + e.toString());
    }

    // If watchlist is null, return an empty watchlist
    return watchlist ?? const Watchlist.empty();
  }

  Future<List<User>> getUserData() async {
    List<User> data = [];

    final uri = Uri.parse(userUri);

    try {
      final response = await http.get(uri, headers: {
        "Accept": "application/json",
        "Access-Control_Allow_Origin": "*",
        "Access-Control-Allow-Headers": "Access-Control-Allow-Origin, Accept"
      });

      if (response.statusCode >= 200 && response.statusCode <= 299) {
        final List<dynamic> jsonData = json.decode(response.body);
        data = jsonData.map((json) => User.fromJson(json)).toList();
      } else {
        print('Request failed with status: ${response.statusCode}');
        print('Response body: ${response.body}');
      }
    } catch (e) {
      return data;
    }
    return data;
  }
}
