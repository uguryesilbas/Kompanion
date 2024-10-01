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
	DELETE FROM Products WHERE Id = p_Id;
END //

DELIMITER ;


