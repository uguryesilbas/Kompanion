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



