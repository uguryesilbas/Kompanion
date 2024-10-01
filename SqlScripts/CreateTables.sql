CREATE DATABASE ECommerce;

CREATE TABLE Countries (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CountryName VARCHAR(100) NOT NULL,
    CurrencyCode VARCHAR(3) NOT NULL,
    CreatedDateTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDateTime TIMESTAMP,
    CreatedUserId INT NOT NULL,
    UpdatedUserId INT
);

CREATE TABLE Products (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProductName VARCHAR(250) NOT NULL,
    Description VARCHAR(250),
    StockQuantity INT NOT NULL,
	CreatedDateTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDateTime TIMESTAMP,
    CreatedUserId INT NOT NULL,
    UpdatedUserId INT
);

CREATE TABLE ProductPrices (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    ProductId INT NOT NULL,
    CountryId INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    CreatedDateTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDateTime TIMESTAMP,
    CreatedUserId INT NOT NULL,
    UpdatedUserId INT,
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    FOREIGN KEY (CountryId) REFERENCES Countries(Id)
);

CREATE TABLE Orders (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    Status TINYINT NOT NULL,
	CreatedDateTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDateTime TIMESTAMP,
    CreatedUserId INT NOT NULL,
    UpdatedUserId INT
);

CREATE TABLE OrderDetails (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    PriceAtPurchase DECIMAL(10, 2) NOT NULL,
	CreatedDateTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDateTime TIMESTAMP,
    CreatedUserId INT NOT NULL,
    UpdatedUserId INT,
    FOREIGN KEY (OrderID) REFERENCES Orders(Id),
    FOREIGN KEY (ProductID) REFERENCES Products(Id)
);

CREATE TABLE Banks (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    BankName VARCHAR(255) NOT NULL,
    CreatedDateTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDateTime TIMESTAMP,
    CreatedUserId INT NOT NULL,
    UpdatedUserId INT
);

CREATE TABLE PaymentRules (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    MinAmount DECIMAL(10, 2),
    MaxAmount DECIMAL(10, 2),
    StartDateTime TIMESTAMP,
    EndDateTime TIMESTAMP,
    ProductId INT,
    BankId INT,
	CreatedDateTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
    UpdatedDateTime TIMESTAMP,
    CreatedUserId INT NOT NULL,
    UpdatedUserId INT,
    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    FOREIGN KEY (BankId) REFERENCES Banks(Id)
);

