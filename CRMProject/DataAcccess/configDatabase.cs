namespace DataAcccess
{
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

//CREATE TABLE Functions (
//    FunctionID int identity primary key,
//    FunctionName nvarchar(255),
//    FunctionCode varchar(255)
//);

//CREATE TABLE Permissions (
//    PermissionID int identity primary key,
//    FunctionID int,
//    UserID int,
//    IsInsert bit,
//    IsUpdate bit,
//    IsDelete bit,
//    IsView bit,
//   FOREIGN KEY (FunctionID) REFERENCES Functions(FunctionID),
//   FOREIGN KEY (UserID) REFERENCES Users(ID)
//);



//github

//git add .
//git commit -m "Noi dung thay doi"
//git push
