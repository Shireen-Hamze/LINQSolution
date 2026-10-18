using LINQ.DataSources;
using LINQ.Models;
using System.Linq;
using LINQ.Helpers;

namespace LINQ
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var products = Source.ProductList;
			var customers = Source.CustomerList;

			#region Assignment 1

			//Q1
			var SeafoodProducts = products
				.Where(p => p.Category == "Seafood");

			foreach (var p in SeafoodProducts)
			{
				Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q2
			var productsNames = products
				.Select(p => p.ProductName);

			foreach (var name in productsNames)
			{
				Console.WriteLine(name);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q3
			var SortedproductsbyPrice = products
				.OrderBy(p => p.UnitPrice);

			foreach (var p in SortedproductsbyPrice)
			{
				Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q4
			var productspricewithin10_30 = products
				.Where(p => p.UnitPrice >= 10 &&
				p.UnitPrice <= 30);

			foreach (var p in productspricewithin10_30)
			{
				Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q5
			var productsInStockInCondiments = products
				.Where(p => p.UnitsInStock > 0 &&
				p.Category == "Condiments");

			foreach (var p in productsInStockInCondiments)
			{
				Console.WriteLine(p);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q6
			var productsWithStockInfo = products
			.Select(p => new
			{
				Name = p.ProductName,
				Price = p.UnitPrice,
				StockStatus = p.UnitsInStock > 0
					? "Available"
					: "Out of Stock"
			});

			foreach (var p in productsWithStockInfo)
			{
				Console.WriteLine(
					$"Product: {p.Name} -    Price: {p.Price} -   Stock Status: {p.StockStatus}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q7
			var Product = products
				.Select((p, index) =>
				$"{index + 1}. {p.ProductName}");

			foreach (var item in Product)
			{
				Console.WriteLine(item);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q8
			var productsbyCategoryAndPrice = products
				.OrderBy(p => p.Category)
				.ThenByDescending(p => p.UnitPrice);

			foreach (var p in productsbyCategoryAndPrice)
			{
				Console.WriteLine(
					$"{p.Category} - {p.ProductName} - {p.UnitPrice}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q9
			var availableBeverageproduct = products
				.Where(p => p.Category == "Beverages")
				.OrderByDescending(p => p.UnitsInStock);

			foreach (var p in availableBeverageproduct)
			{
				Console.WriteLine(
					$"{p.ProductName} - Stock: {p.UnitsInStock}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q10
			var CustomerInfo =
				from c in customers
				from o in c.Orders
				where o.OrderDate.Year >= 1997
				select new
				{
					c.CustomerID,
					o.OrderDate
				};

			foreach (var item in CustomerInfo)
			{
				Console.WriteLine(
					$"{item.CustomerID} - {item.OrderDate:d}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q11
			var ProductName = products
				.Select((p, index) => new
				{
					Position = index + 1,
					ProductName = p.ProductName
				});

			foreach (var item in ProductName)
			{
				Console.WriteLine(
					$"{item.Position} - {item.ProductName}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q12
			string[] Arr =
				{
					"aPPLE",
					"AbAcUs",
					"bRaNcH",
					"BlUeBeRrY",
					"ClOvEr",
					"cHeRry"
				};

			var Sortedlist = Arr
				.OrderBy(s => s.Length)
				.ThenBy(s => s, StringComparer.OrdinalIgnoreCase);

			foreach (var word in Sortedlist)
			{
				Console.WriteLine(word);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q13
			var reversedList = Arr
				.Where(s => s.Length >= 2 && 
						char.ToLowerInvariant(s[1]) == 'l')
				.ToList();

			foreach (var word in reversedList)
			{
				Console.WriteLine(word);
			}

			Console.WriteLine("------------------------------------------------------");

			#endregion

			#region Assignment 2
			/*
			
			//Q1
			var productsWithHighPrices = products.OrderByDescending(p => p.UnitPrice).Take(3);

			foreach (var product in productsWithHighPrices)
			{
				Console.WriteLine(product);
			}
			Console.WriteLine("------------------------------------------------------");
		
			//Q2
			int pageSize = 5;
			int pageNumber = 2;

			var productsOnPage = products
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize);

			foreach (var product in productsOnPage)
			{
				Console.WriteLine(product);
			}
			Console.WriteLine("------------------------------------------------------");

			//Q3
			var productsUnder25 = products
				.OrderBy(p => p.UnitPrice)
				.TakeWhile(p => p.UnitPrice < 25);

			foreach (var product in productsUnder25)
			{
				Console.WriteLine(product);
			}
			Console.WriteLine("------------------------------------------------------");

			//Q4
			bool productsInStock = products
			.Where(p => p.Category == "Seafood")
			.All(p => p.UnitsInStock > 0);

			Console.WriteLine(productsInStock);

			Console.WriteLine("------------------------------------------------------");

			//Q5
			int[] IDs = { 3, 9, 13, 18 };

			bool result01 = IDs.Contains(9);

			Console.WriteLine(result01);
			Console.WriteLine("------------------------------------------------------");

			//Q6
			var CategorieswithCounts = products
			.GroupBy(p => p.Category)
			.Select(g => new
			{
				Category = g.Key,
				Count = g.Count()
			});

			foreach (var group in CategorieswithCounts)
			{
				Console.WriteLine(
					$"{group.Category}: {group.Count}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q7
			var productNamesByCategory = products
			.GroupBy(p => p.Category)
			.Select(g => new
			{
				Category = g.Key,
				ProductNames = g.Select(p => p.ProductName)
			});

			foreach (var group in productNamesByCategory)
			{
				Console.WriteLine($"\n Category: {group.Category}\n");

				foreach (var name in group.ProductNames)
				{
					Console.WriteLine($"  {name}");
				}
			}
			Console.WriteLine("------------------------------------------------------");

			//Q8
			var productsByCategory = products
			.GroupBy(p => p.Category)
			.Where(g => g.Count() > 3)
			.Select(g => new
			{
				Category = g.Key,
				Count = g.Count()
			});

			foreach (var category in productsByCategory)
			{
				Console.WriteLine(
					$"{category.Category}: {category.Count}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q9
			var Customerinfo =
			from c in customers
			group c by c.Country into countryGroup
			select new
			{
				Country = countryGroup.Key,
				Count = countryGroup.Count(),
				TotalOrderValue = countryGroup
					.SelectMany(c => c.Orders)
					.Sum(o => o.Total)
			};

			foreach (var item in Customerinfo)
			{
				Console.WriteLine(
					$"{item.Country} | Customers: {item.Count} | " +
					$"Total Orders: {item.TotalOrderValue:C2}");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q10
			int totalProductsInStock = products.Sum(p => p.UnitsInStock);

			Console.WriteLine(totalProductsInStock);

			Console.WriteLine("------------------------------------------------------");

			//Q11
			var cheapestPrice =
				products.Min(p => p.UnitPrice);

			var mostExpensivePrice =
				products.Max(p => p.UnitPrice);

			Console.WriteLine($"Cheapest: ${cheapestPrice}");
			Console.WriteLine($"Most Expensive: ${mostExpensivePrice}");

			Console.WriteLine("------------------------------------------------------");

			//Q12
			var distinctproducts = products
				.Select(p => p.Category)
				.Distinct();

			foreach (var category in distinctproducts)
			{
				Console.WriteLine(category);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q13
			int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
			int[] setB = { 3, 6, 9, 12, 15, 13 };

			var inAnotInB= setA.Except(setB);

			foreach (var id in inAnotInB)
			{
				Console.WriteLine(id);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q14
			string[] list1 =
				{ "Germany", "France", "UK", "Spain" };

			string[] list2 =
				{ "france", "SPAIN", "Italy" };

			var inList1notinList2= list1.Except(
				list2,
				StringComparer.OrdinalIgnoreCase);

			foreach (var country in inList1notinList2)
			{
				Console.WriteLine(country);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q15
			Dictionary<int, Product> productDictionary =
				products.ToDictionary(p => p.ProductID);

			if (productDictionary.TryGetValue(18, out Product? Product))
			{
				Console.WriteLine(Product);
			}
			else
			{
				Console.WriteLine("Product not found.");
			}

			Console.WriteLine("------------------------------------------------------");

			//Q16
			var firstProductgreater50 = products
				.First(p => p.UnitPrice > 50);

			Console.WriteLine(firstProductgreater50);

			Console.WriteLine("------------------------------------------------------");

			//Q17
			var firstProductgreater50OrNull = products
				.FirstOrDefault(p => p.UnitPrice > 500);
			
			Console.WriteLine(firstProductgreater50OrNull);
			
			Console.WriteLine("------------------------------------------------------");

			//Q18
			var multiplicationTableRowFor7 = Enumerable.Range(1, 10)
				.Select(n => $"7 x {n} = {7 * n}");

			foreach (var row in multiplicationTableRowFor7)
			{
				Console.WriteLine(row);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q19
			var Evenbetween1_30 = Enumerable.Range(1, 30)
				.Where(n => n % 2 == 0);

			foreach (var number in Evenbetween1_30)
			{
				Console.WriteLine(number);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q20
			var result = products
				.Take(3)
				.Select(p => p.ProductName)
				.Concat(
					customers
						.Take(3)
						.Select(c => c.CompanyName)
				);

			foreach (var name in result)
			{
				Console.WriteLine(name);
			}

			Console.WriteLine("------------------------------------------------------");

			//Q21
			var productCustomerPairs = products
				.Zip(
					customers,
					(product, customer) =>
						$"{product.ProductName} sold to {customer.CompanyName}"
				);

			foreach (var item in productCustomerPairs)
			{
				Console.WriteLine(item);
			}

			Console.WriteLine("------------------------------------------------------");
			
			*/
			#endregion
		}
	}
}
