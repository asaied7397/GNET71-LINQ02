using System;
using System.Linq;
using System.Collections.Generic;
using LINQ.DataSources;
using LINQ.Models;

// ============================================================
// Question 1
// Get top 3 most expensive products.
// ============================================================

Console.WriteLine("\n========== Question 1 ==========");

var top3ExpensiveProducts = Source.ProductList
    .OrderByDescending(p => p.UnitPrice)
    .Take(3);

foreach (var product in top3ExpensiveProducts)
{
    Console.WriteLine($"{product.ProductName} - {product.UnitPrice:F2}");
}


// ============================================================
// Question 2
// Show page 2 of products, with page size = 5.
// ============================================================

Console.WriteLine("\n========== Question 2 ==========");

int pageNumber = 2;
int pageSize = 5;

var secondPageProducts = Source.ProductList
    .OrderBy(p => p.ProductID)
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize);

foreach (var product in secondPageProducts)
{
    Console.WriteLine($"{product.ProductID}. {product.ProductName}");
}


// ============================================================
// Question 3
// Take products as long as UnitPrice is less than 25.
// The list must be ordered by price.
// ============================================================

Console.WriteLine("\n========== Question 3 ==========");

var productsUnder25 = Source.ProductList
    .OrderBy(p => p.UnitPrice)
    .TakeWhile(p => p.UnitPrice < 25);

foreach (var product in productsUnder25)
{
    Console.WriteLine($"{product.ProductName} - {product.UnitPrice:F2}");
}


// ============================================================
// Question 4
// Check if ALL Seafood products are in stock.
// ============================================================

Console.WriteLine("\n========== Question 4 ==========");

bool allSeafoodInStock = Source.ProductList
    .Where(p => p.Category == "Seafood")
    .All(p => p.UnitsInStock > 0);

Console.WriteLine($"All Seafood products in stock: {allSeafoodInStock}");


// ============================================================
// Question 5
// Check if the ID list contains 9.
// ============================================================

Console.WriteLine("\n========== Question 5 ==========");

int[] ids = { 3, 9, 13, 18 };

bool containsNine = ids.Contains(9);

Console.WriteLine($"Contains ID 9: {containsNine}");


// ============================================================
// Question 6
// Group products by Category and print product count.
// ============================================================

Console.WriteLine("\n========== Question 6 ==========");

var productsGroupedByCategory = Source.ProductList
    .GroupBy(p => p.Category);

foreach (var group in productsGroupedByCategory)
{
    Console.WriteLine(
        $"Category: {group.Key} - Count: {group.Count()}"
    );
}


// ============================================================
// Question 7
// Group products by Category and project product names.
// ============================================================

Console.WriteLine("\n========== Question 7 ==========");

var groupedProductNames = Source.ProductList
    .GroupBy(p => p.Category)
    .Select(g => new
    {
        Category = g.Key,
        ProductNames = g.Select(p => p.ProductName)
    });

foreach (var group in groupedProductNames)
{
    Console.WriteLine($"\nCategory: {group.Category}");

    foreach (var name in group.ProductNames)
    {
        Console.WriteLine($"- {name}");
    }
}


// ============================================================
// Question 8
// Find categories having MORE THAN 3 products.
// ============================================================

Console.WriteLine("\n========== Question 8 ==========");

var categoriesWithMoreThan3Products = Source.ProductList
    .GroupBy(p => p.Category)
    .Where(g => g.Count() > 3)
    .Select(g => new
    {
        Category = g.Key,
        ProductCount = g.Count()
    });

foreach (var category in categoriesWithMoreThan3Products)
{
    Console.WriteLine(
        $"{category.Category} - {category.ProductCount} products"
    );
}


// ============================================================
// Question 9
// QUERY SYNTAX
// Group customers by Country.
// Select Country, Count, TotalOrderValue.
// ============================================================

Console.WriteLine("\n========== Question 9 ==========");

var customersGroupedByCountry =
    from customer in Source.CustomerList
    group customer by customer.Country into countryGroup
    select new
    {
        Country = countryGroup.Key,
        Count = countryGroup.Count(),
        TotalOrderValue = countryGroup.Sum(
            c => c.Orders.Sum(o => o.Total)
        )
    };

foreach (var country in customersGroupedByCountry)
{
    Console.WriteLine(
        $"Country: {country.Country} | " +
        $"Customers: {country.Count} | " +
        $"Total Order Value: {country.TotalOrderValue:F2}"
    );
}


// ============================================================
// Question 10
// Calculate total units in stock across all products.
// ============================================================

Console.WriteLine("\n========== Question 10 ==========");

int totalUnitsInStock = Source.ProductList
    .Sum(p => p.UnitsInStock);

Console.WriteLine($"Total Units in Stock: {totalUnitsInStock}");


// ============================================================
// Question 11
// Find cheapest and most expensive product prices.
// ============================================================

Console.WriteLine("\n========== Question 11 ==========");

decimal cheapestPrice = Source.ProductList
    .Min(p => p.UnitPrice);

decimal mostExpensivePrice = Source.ProductList
    .Max(p => p.UnitPrice);

Console.WriteLine($"Cheapest Price: {cheapestPrice:F2}");
Console.WriteLine($"Most Expensive Price: {mostExpensivePrice:F2}");


// ============================================================
// Question 12
// Get distinct product categories.
// ============================================================

Console.WriteLine("\n========== Question 12 ==========");

var distinctCategories = Source.ProductList
    .Select(p => p.Category)
    .Distinct();

foreach (var category in distinctCategories)
{
    Console.WriteLine(category);
}


// ============================================================
// Question 13
// Find IDs in setA but NOT in setB.
// ============================================================

Console.WriteLine("\n========== Question 13 ==========");

int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
int[] setB = { 3, 6, 9, 12, 15, 13 };

var idsOnlyInSetA = setA.Except(setB);

foreach (var id in idsOnlyInSetA)
{
    Console.WriteLine(id);
}


// ============================================================
// Question 14
// Find countries in list1 but NOT in list2.
// Case-insensitive comparison.
// ============================================================

Console.WriteLine("\n========== Question 14 ==========");

string[] list1 = { "Germany", "France", "UK", "Spain" };
string[] list2 = { "france", "SPAIN", "Italy" };

var countriesOnlyInList1 = list1
    .Except(list2, StringComparer.OrdinalIgnoreCase);

foreach (var country in countriesOnlyInList1)
{
    Console.WriteLine(country);
}


// ============================================================
// Question 15
// Build Dictionary<int, Product> using ProductID.
// Retrieve product with ID = 18.
// ============================================================

Console.WriteLine("\n========== Question 15 ==========");

Dictionary<int, Product> productDictionary = Source.ProductList
    .ToDictionary(p => p.ProductID);

if (productDictionary.TryGetValue(18, out Product? product18))
{
    Console.WriteLine($"ID: {product18.ProductID}");
    Console.WriteLine($"Name: {product18.ProductName}");
    Console.WriteLine($"Category: {product18.Category}");
    Console.WriteLine($"Price: {product18.UnitPrice:F2}");
    Console.WriteLine($"Stock: {product18.UnitsInStock}");
}
else
{
    Console.WriteLine("Product with ID 18 was not found.");
}


// ============================================================
// Question 16
// Get first product with price greater than 50.
// ============================================================

Console.WriteLine("\n========== Question 16 ==========");

var firstProductOver50 = Source.ProductList
    .First(p => p.UnitPrice > 50);

Console.WriteLine(
    $"{firstProductOver50.ProductName} - {firstProductOver50.UnitPrice:F2}"
);


// ============================================================
// Question 17
// Try to get first product with price > 500.
// Return null if no matching product exists.
// ============================================================

Console.WriteLine("\n========== Question 17 ==========");

Product? firstProductOver500 = Source.ProductList
    .FirstOrDefault(p => p.UnitPrice > 500);

if (firstProductOver500 != null)
{
    Console.WriteLine(
        $"{firstProductOver500.ProductName} - {firstProductOver500.UnitPrice:F2}"
    );
}
else
{
    Console.WriteLine("No product found with price greater than 500.");
}


// ============================================================
// Question 18
// Generate multiplication table row for 7.
// ============================================================

Console.WriteLine("\n========== Question 18 ==========");

var multiplicationTable = Enumerable.Range(1, 12)
    .Select(n => new
    {
        Number = n,
        Result = 7 * n
    });

foreach (var item in multiplicationTable)
{
    Console.WriteLine($"7 x {item.Number} = {item.Result}");
}


// ============================================================
// Question 19
// Generate even numbers between 1 and 30.
// ============================================================

Console.WriteLine("\n========== Question 19 ==========");

var evenNumbers = Enumerable.Range(1, 30)
    .Where(n => n % 2 == 0);

foreach (var number in evenNumbers)
{
    Console.WriteLine(number);
}


// ============================================================
// Question 20
// Concatenate first 3 product names and first 3
// customer company names into a single sequence.
// ============================================================

Console.WriteLine("\n========== Question 20 ==========");

var first3ProductNames = Source.ProductList
    .Take(3)
    .Select(p => p.ProductName);

var first3CustomerNames = Source.CustomerList
    .Take(3)
    .Select(c => c.CustomerName);

var combinedNames = first3ProductNames
    .Concat(first3CustomerNames);

foreach (var name in combinedNames)
{
    Console.WriteLine(name);
}


// ============================================================
// Question 21
// Pair each product with a customer by position.
// Produce "ProductName sold to CompanyName".
// ============================================================

Console.WriteLine("\n========== Question 21 ==========");

var productCustomerPairs = Source.ProductList
    .Zip(
        Source.CustomerList,
        (product, customer) =>
            $"{product.ProductName} sold to {customer.CustomerName}"
    );

foreach (var pair in productCustomerPairs)
{
    Console.WriteLine(pair);
}