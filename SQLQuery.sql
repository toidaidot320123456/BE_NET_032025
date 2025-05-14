USE BE_NET_032025
GO

CREATE TABLE Products (
    ID int primary key identity,
    ProductName nvarchar(255),
	Price float,
	ImagePath varchar(255)
)
GO