DELIMITER //

CREATE PROCEDURE CreateOrder (
    IN p_ProductId INT,
    IN p_Quantity INT,
    IN p_OrderId INT,
    IN p_UserId INT,
    IN p_CountryId INT
)
BEGIN
	DECLARE v_Price DECIMAL(10, 2) DEFAULT 0;
    DECLARE v_TotalPrice DECIMAL(10, 2) DEFAULT 0;
    DECLARE v_StockQuantity INT DEFAULT 0;
    DECLARE v_ProductName VARCHAR(250);
     DECLARE v_CurrencyCode VARCHAR(3);
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        RESIGNAL;
    END;
    
    SELECT ProductName, StockQuantity
    INTO v_ProductName, v_StockQuantity
    FROM Products 
    WHERE Id = p_ProductId;
    
    IF v_StockQuantity < p_Quantity THEN
        SET @errorMsg = CONCAT(v_ProductName, ' stoğu yetersiz!');
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = @errorMsg;
	END IF;
    
    SELECT Price
    INTO v_Price
    FROM ProductPrices
    WHERE ProductId = p_ProductId AND CountryId = p_CountryId;
    
	IF v_Price <=0 THEN
        SET @errorMsg = CONCAT(v_ProductName, ' ürününe ait fiyat bulunamadı!');
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = @errorMsg;
	END IF;
    
    SELECT CurrencyCode
    INTO v_CurrencyCode
    FROM Countries
    WHERE Id = p_CountryId;
	
    SET v_TotalPrice =  v_Price * p_Quantity;

	INSERT INTO OrderDetails (`OrderId`,`ProductId`,`Quantity`,`PriceAtPurchase`,  `CurrencyCodeAtPurchase`,  `CreatedDateTime`,`CreatedUserId`)
	VALUES (p_OrderId, p_ProductId, p_Quantity, v_TotalPrice, v_CurrencyCode, CURRENT_TIMESTAMP, p_UserId);
	
    UPDATE Products SET StockQuantity = StockQuantity - p_Quantity WHERE Id = p_ProductId;
	
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE SaveOrUpdateProduct
(
IN p_Id INT,
IN p_ProductName VARCHAR(255),
IN p_Description VARCHAR(250),
IN p_StockQuantity INT,
IN p_CreatedDateTime TIMESTAMP,
IN p_UpdatedDateTime TIMESTAMP,
IN p_CreatedUserId INT,
IN p_UpdatedUserId INT,
OUT p_LastInsertID INT
)
BEGIN

	IF (p_Id <= 0) THEN
		INSERT INTO Products (ProductName, Description, StockQuantity, CreatedDateTime, CreatedUserId)
		VALUES (p_ProductName, p_Description, p_StockQuantity, p_CreatedDateTime, p_CreatedUserId);
		SET p_LastInsertID = LAST_INSERT_ID();
	ELSE
		UPDATE Products SET 
        ProductName=p_ProductName, 
        Description=p_Description, 
        StockQuantity=p_StockQuantity, 
        UpdatedDateTime=p_UpdatedDateTime, 
        UpdatedUserId=p_UpdatedUserId
        WHERE Id = p_Id;
	END IF;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE SaveOrUpdateProductPrice
(
IN p_Id INT,
IN p_ProductId INT,
IN p_CountryId INT,
IN p_Price DECIMAL(10,2),
IN p_CreatedDateTime TIMESTAMP,
IN p_UpdatedDateTime TIMESTAMP,
IN p_CreatedUserId INT,
IN p_UpdatedUserId INT,
OUT p_LastInsertID INT
)
BEGIN

	IF (p_Id <= 0) THEN
		INSERT INTO ProductPrices (ProductId, CountryId, Price, CreatedDateTime, CreatedUserId)
		VALUES (p_ProductId, p_CountryId, p_Price, p_CreatedDateTime, p_CreatedUserId);
		SET p_LastInsertID = LAST_INSERT_ID();
	ELSE
		UPDATE ProductPrices SET 
        Price=p_Price,
        CountryId=p_CountryId,
        UpdatedDateTime=p_UpdatedDateTime, 
        UpdatedUserId=p_UpdatedUserId
        WHERE Id = p_Id;
	END IF;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE InsertOrder
(
IN p_Status TINYINT,
IN p_CountryId INT,
IN p_CreatedUserId INT,
OUT p_LastInsertID INT
)
BEGIN
        INSERT INTO Orders (Status, CountryId, CreatedDateTime, CreatedUserId)
        VALUES (p_Status, p_CountryId, CURRENT_TIMESTAMP, p_CreatedUserId);
        SET p_LastInsertID = LAST_INSERT_ID();
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE GetProductById
(
IN p_Id INT
)
BEGIN
	SELECT * FROM Products WHERE Id = p_Id;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE DeleteProductById
(
IN p_Id INT
)
BEGIN
	DELETE FROM ProductPrices Where ProductId = p_Id;
	DELETE FROM Products WHERE Id = p_Id;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE GetProductPriceById
(
IN p_Id INT
)
BEGIN
	SELECT * FROM ProductPrices WHERE Id = p_Id;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE DeleteProductPriceById
(
IN p_Id INT
)
BEGIN
	DELETE FROM ProductPrices WHERE Id = p_Id;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE GetOrderById
(
IN p_Id INT
)
BEGIN
	SELECT * FROM Orders WHERE Id = p_Id;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE GetOrderDetailsByOrderId
(
IN p_Id INT
)
BEGIN
	SELECT * FROM OrderDetails WHERE OrderId = p_Id;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE GetPaymentRulesByProductIds
(
IN p_Ids LONGTEXT,
IN p_CountryId INT
)
BEGIN
	SELECT * FROM PaymentRules WHERE FIND_IN_SET(ProductId, p_Ids) AND CountryId = p_CountryId;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE UpdateOrderStatusById
(
IN p_Id INT,
IN p_Status tinyint
)
BEGIN
	UPDATE Orders SET Status = p_Status WHERE Id = p_Id;
END //

DELIMITER ;

