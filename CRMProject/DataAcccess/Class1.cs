using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcccess
{
    internal class Class1
    {
    }
    //dotnet ef dbcontext scaffold "Data Source=DESKTOP-CVF87RA\MSSQLSERVER2019;Initial Catalog=BE_NET_032025;Integrated Security=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -o DBContext --force
}

//CREATE TABLE Customers  (
//    CustomerID INT PRIMARY KEY identity,
//    CustomerName NVARCHAR(255),
//    ContactNumber VARCHAR(50),
//    Email VARCHAR(255),
//    Address NVARCHAR(255)
//);


//CREATE TABLE Products   (
//    ProductID INT PRIMARY KEY identity,
//    ProductName NVARCHAR(255),
//    UnitPrice float,
//    StockQuantity float
//);


//CREATE TABLE Orders    (
//    OrderID INT PRIMARY KEY identity,
//    CustomerID INT,
//    OrderDate DATETIME,
//    TotalAmount float,
//    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
//);

//CREATE TABLE OrdersDetail(
//    OrderDetailID INT PRIMARY KEY identity,
//    OrderID INT,
//    ProductID INT,
//    UnitPrice float,
//    Quantity float,
//    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
//    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
//);