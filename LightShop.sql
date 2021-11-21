USE [master]
GO

--- Tạo DATABASE nếu như chưa có
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'LightShopOnline')
BEGIN
	CREATE DATABASE LightShopOnline
END
GO

USE [LightShopOnline]
GO

-- Tạo TABLE cho DATABASE
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserTable' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[UserTable](
		/* Sử dụng VARCHAR do nó được nhập vào bao nhiêu thì sẽ sài bấy nhiêu
		, còn CHAR thì luôn cố định theo khai báo */
		User_Id int IDENTITY(1,1) NOT NULL,
		Username VARCHAR(50) NULL,
		Password VARCHAR(50) NULL,
		ggAuthId VARCHAR(20) NULL,
		Role VARCHAR(20) NULL DEFAULT 'admin',
		Gmail VARCHAR(50) NULL,
		CONSTRAINT PK_UserTable PRIMARY KEY(User_Id) 
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Product' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Product](
		/* NCHAR hay NVARCHAR đều sẽ lưu trữ biến dưới dạng 2 chiều
		, phù hợp cho mở rộng nhiều ngôn ngữ chứ không chỉ tiếng Việt không thôi */
		Product_Id int IDENTITY(1,1) NOT NULL,
		Product_Name NVARCHAR(510) NULL,
		url VARCHAR(255) NULL,
		Price bigint NULL DEFAULT 0,
		Warrant NVARCHAR(50) NULL,
		Size NVARCHAR(50) NULL,
		Color NVARCHAR(50) NULL,
		Description NVARCHAR(MAX) NULL,
		Brand NVARCHAR(50) NULL,
		Discount float NULL DEFAULT 0,
		isHidden int NOT NULL DEFAULT 0,
		Picture1 VARCHAR(255) NULL,
		Picture2 VARCHAR(255) NULL,
		Picture3 VARCHAR(255) NULL,
		Picture4 VARCHAR(255) NULL,
		CONSTRAINT PK_Product PRIMARY KEY(Product_Id),
		-- Ràng buộc CHECK:
		CONSTRAINT CHK_Product_Price CHECK (Price >=0),
		CONSTRAINT CHK_Product_Discount CHECK (Discount >=0),
		CONSTRAINT CHK_Product_isHidden CHECK (isHidden = 0 OR isHidden = 1)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Category' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Category](
		Category_Id int IDENTITY(1,1) NOT NULL,
		Category_Name NVARCHAR(255) NULL,
		url VARCHAR(50) NULL,
		parentId VARCHAR(20) NULL,
		isHidden int NOT NULL DEFAULT 0,
		Picture1 VARCHAR(255) NULL,
		CONSTRAINT PK_Category PRIMARY KEY(Category_Id),
		-- Ràng buộc CHECK:
		CONSTRAINT CHK_Category_isHidden CHECK (isHidden = 0 OR isHidden = 1)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Category_Product' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Category_Product](
		Product_Id int NOT NULL,
		Category_Id int NOT NULL,
		Position int NOT NULL,
		CONSTRAINT PK_Category_Product PRIMARY KEY(Category_Id, Product_Id, Position),
		CONSTRAINT FK_Product_Category_Product FOREIGN KEY(Product_Id) REFERENCES [dbo].[Product](Product_Id),
		CONSTRAINT FK_Category_Category_Product FOREIGN KEY(Category_Id) REFERENCES [dbo].[Category](Category_Id)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Order' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Order](
		Order_Id VARCHAR(20) NOT NULL,
		Guest_Name NVARCHAR(255) NULL,
		Guest_Phone VARCHAR(255) NULL,
		dateCreate datetime NULL,
		Address NVARCHAR(510) NULL,
		Price bigint NULL DEFAULT 0,
		CONSTRAINT PK_Order PRIMARY KEY(Order_Id),
		-- Ràng buộc CHECK:
		CONSTRAINT CHK_Order_Price CHECK (Price >=0)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OrderDetail' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[OrderDetail](
		Order_Id VARCHAR(20) NOT NULL,
		Product_Id int NOT NULL,
		Quantity int NOT NULL DEFAULT 1,
		CONSTRAINT PK_OrderDetail PRIMARY KEY(Order_Id, Product_Id),
		CONSTRAINT FK_Order_OrderDetail FOREIGN KEY(Order_Id) REFERENCES [dbo].[Order](Order_Id),
		CONSTRAINT FK_Product_OrderDetail FOREIGN KEY(Product_Id) REFERENCES [dbo].[Product](Product_Id),
		-- Ràng buộc CHECK:
		CONSTRAINT CHK_OrderDetail_Quantity CHECK (Quantity >=0)
	)
END
GO

-- Nhập dữ liệu cho các TABLE trong DATABASE
/* Insert bảng chính trước */
	-- USER TABLE
INSERT INTO [dbo].[UserTable](Username, Password) VALUES
	('admin', '123456'),
	('manager', '123456')

	-- CATEGORY
INSERT INTO [dbo].[Category](Category_Name, isHidden, Picture1) VALUES 
	(N'Đèn LED thường', 0, N'https://blight.com.vn/wp-content/uploads/2018/12/nhan-biet-den-led-gia-2.jpg'),
	(N'Đèn LED cảm ứng', 0, N'https://www.thietbidiendgp.vn/media/products/800/DLS108-6W-T-V.jpg')

INSERT INTO [dbo].[Category](Category_Name, parentId, isHidden, Picture1) VALUES 
	(N'Đèn LED âm trần', '1', 0, 'https://rangdong.com.vn/uploads/product/LED/LED_Downlight/D-AT10L-90-9W/D-AT10L-90-Vien-Vang-1.jpg'),
	(N'Đèn LED dây', '1', 0, 'https://images.kingled.vn/data/Product/038E189B-7144-4131-AEA2-70B6CAD3AB64/led-day-5050-kingled.jpg'),
	(N'Đèn LED dưới nước', '1', 0, 'https://file.hstatic.net/1000299129/file/den-am-duoi-nuoc-18w-doi-mau-ip68_4385ed44d3fe42638afac3003659f75f_grande.jpg'),
	(N'Đèn cảm ứng cầu thang', '2', 0, 'https://nvc-lighting.com.vn/wp-content/uploads/2018/09/category-den-am-bac-cau-thang-chieu-sang-bang-led-day-min-700x264.jpg'),
	(N'Đèn ngủ cảm ứng', '2', 0, 'https://i.imgur.com/Y5jeAjCl.jpg')

	-- PRODUCT
INSERT INTO [dbo].[Product](Product_Name, Price, Size, Color, Description, Brand, Picture1) VALUES
	(N'Đèn led âm trần MPE Series RPL 6w, 9w, 12w, 15w, 18w, 24w', 497900, N'3 màu', N'12w, 15w, 18w, 24w', 
	N'Đèn led âm trần mpe là một trong những sản phẩm chiếu sáng của những ngôi nhà hiện nay. Nắm bắt được xu thế của công nghệ chiếu sáng mới SKYLED kinh doanh sản phẩm đèn Led âm trần hiện đại. Đèn dowlightng âm trần chuyên sử dụng cho trần thạch cao, gỗ, nhựa. Với thiết kế gọn nhẹ đã đáp ứng được nhu cầu chiếu sáng và trang trí cho ngôi nhà của bạn.', 
	'MPE', 'https://skyled.com.vn/wp-content/uploads/2020/11/den-downlight-am-tran-rpl-6st-led-6w-1.jpg'),
	(N'Đèn led âm trần Hufa 12W AT-69', 72000, N'Ánh sáng trắng, ánh sáng vàng', N'Kich Thước: Ø170 x H15', 
	N'Bảo Hành : 24 tháng', 
	'HUFA', 'https://skyled.com.vn/wp-content/uploads/2020/08/den-downlight-am-tran-at-69-led-12w-1.jpg'),
	(N'Đèn led âm trần 12w Rạng Đông D PT04L 135/12W', 112450, N'Ánh sáng trắng', N'Liên hệ', 
	N'Bảo Hành : 2 năm', 
	N'Rạng Đông', 'https://skyled.com.vn/wp-content/uploads/2020/07/den-led-am-tran-9w-rang-dong-d-pt04l-110-9w.jpg')

	
	-- ORDER
/* Bảng tham chiếu thì Insert sau */
	-- CATEGORY_PRODUCT
	-- ORDER DETAIL

-- Xóa TABLE (nên dùng lệnh để bớt lỗi)
/*
USE [master]
GO

IF OBJECT_ID('dbo.OrderDetail', 'U')
IS NOT NULL  DROP TABLE [dbo].[OrderDetail];
GO
IF OBJECT_ID('dbo.Category_Product', 'U')
IS NOT NULL  DROP TABLE [dbo].[Category_Product];
GO

IF OBJECT_ID('dbo.UserTable', 'U')
IS NOT NULL  DROP TABLE [dbo].[UserTable];
GO
IF OBJECT_ID('dbo.Category', 'U')
IS NOT NULL  DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID('dbo.Product', 'U')
IS NOT NULL  DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID('dbo.Order', 'U')
IS NOT NULL  DROP TABLE [dbo].[Order];
GO
*/

-- Tạo PROCEDURE
GO
/* READ của Product */
CREATE PROCEDURE Product_GetAll 
AS
BEGIN
	SELECT *
	from [LightShopOnline].dbo.Product
END
GO

CREATE PROCEDURE Product_GetSpecific
	@Product_Id int
AS
BEGIN
	SELECT *
	from [LightShopOnline].dbo.Product
	WHERE Product_Id = @Product_Id
END
GO

CREATE PROCEDURE Product_GetID
	@Product_Id int
AS
BEGIN
	SELECT Product_Id
	from [LightShopOnline].dbo.Product
	WHERE Product_Id = @Product_Id
END
GO

/* READ của Category */
CREATE PROCEDURE Category_GetAll 
AS
BEGIN
	SELECT *
	from [LightShopOnline].dbo.Category
END
GO

CREATE PROCEDURE Category_GetSpecific
	@Category_Id int
AS
BEGIN
	SELECT *
	from [LightShopOnline].dbo.Category
	WHERE Category_Id = @Category_Id
END
GO

CREATE PROCEDURE GetProductInCategoryByCategoryURLAndPage
	@Category_URL varchar(50),
	@BeginRow int,
	@NumberOfNextRow int
AS
BEGIN
	SELECT Product.Product_Name, Product.url, Product.Price, Product.Discount , Product.Picture1
	FROM Category, Category_Product , Product
	WHERE Category.url = @Category_URL
	AND Category.Category_Id = Category_Product.Category_Id
	AND Category_Product.Product_Id = Product.Product_Id
	AND Product.isHidden = 0
	ORDER BY Category_Product.Position DESC
	OFFSET @BeginRow ROWS
	FETCH NEXT @NumberOfNextRow ROWS ONLY;
END
GO

CREATE PROCEDURE CountProductInCategory
	@Category_URL varchar(50)
AS
BEGIN
	SELECT Count(Product_Id) as SumProduct
	FROM Category, Category_Product
	WHERE Category.url = @Category_URL
	AND Category.Category_Id = Category_Product.Category_Id
END
GO
