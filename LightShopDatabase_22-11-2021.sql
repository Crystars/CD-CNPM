﻿USE [master]
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

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Coupon' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Coupon](
		Coupon_Id VARCHAR(20) NOT NULL,
		Detail NVARCHAR(MAX) NULL,
		Calculator float NOT NULL DEFAULT 0,
		NumberForUsed int NOT NULL DEFAULT 1,
		CONSTRAINT PK_Coupon PRIMARY KEY(Coupon_Id),
		-- Ràng buộc CHECK:
		CONSTRAINT CHK_Coupon_NumberForUsed CHECK (NumberForUsed >=0)
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Cart' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Cart](
		Cart_Id VARCHAR(20) NOT NULL,
		User_Id int NOT NULL,
		CONSTRAINT PK_Cart PRIMARY KEY(Cart_Id),
		CONSTRAINT FK_Cart_User FOREIGN KEY(User_Id) REFERENCES [dbo].[UserTable](User_Id),
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
		Coupon_Id VARCHAR(20) NOT NULL,
		paymentMethod VARCHAR(50) DEFAULT N'Tiền mặt',
		Status VARCHAR(50) DEFAULT N'Đang xử lý',
		User_Id int NOT NULL,
		CONSTRAINT PK_Order PRIMARY KEY(Order_Id), /* Primary Key mà gọi hết 3 cái thì bảng OderDetail sẽ bị lỗi liền */
		CONSTRAINT FK_Order_Coupon FOREIGN KEY(Coupon_Id) REFERENCES [dbo].[Coupon](Coupon_Id),
		CONSTRAINT FK_Order_User FOREIGN KEY(User_Id) REFERENCES [dbo].[UserTable](User_Id),
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
		Cart_Id VARCHAR(20) NOT NULL,
		CONSTRAINT PK_OrderDetail PRIMARY KEY(Order_Id, Product_Id, Cart_Id),
		CONSTRAINT FK_Order_OrderDetail FOREIGN KEY(Order_Id) REFERENCES [dbo].[Order](Order_Id),
		CONSTRAINT FK_Product_OrderDetail FOREIGN KEY(Product_Id) REFERENCES [dbo].[Product](Product_Id),
		CONSTRAINT FK_Cart_OrderDetail FOREIGN KEY(Cart_Id) REFERENCES [dbo].[Cart](Cart_Id),
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
INSERT INTO [dbo].[Category](Category_Name, url, Picture1) VALUES 
	(N'Đèn LED thường', N'led', N'https://blight.com.vn/wp-content/uploads/2018/12/nhan-biet-den-led-gia-2.jpg'),
	(N'Đèn LED cảm ứng', N'cam-ung', N'https://www.thietbidiendgp.vn/media/products/800/DLS108-6W-T-V.jpg')

INSERT INTO [dbo].[Category](Category_Name, parentId, isHidden, url, Picture1) VALUES 
	(N'Đèn LED âm trần', '1', 0,  N'den-led-am-tran', 'https://rangdong.com.vn/uploads/product/LED/LED_Downlight/D-AT10L-90-9W/D-AT10L-90-Vien-Vang-1.jpg'),
	(N'Đèn LED dây', '1', 0, N'den-led-day', 'https://images.kingled.vn/data/Product/038E189B-7144-4131-AEA2-70B6CAD3AB64/led-day-5050-kingled.jpg'),
	(N'Đèn LED dưới nước', '1', 0, N'den-led-duoi-nuoc', 'https://file.hstatic.net/1000299129/file/den-am-duoi-nuoc-18w-doi-mau-ip68_4385ed44d3fe42638afac3003659f75f_grande.jpg'),
	(N'Đèn cảm ứng cầu thang', '2', 0, N'cau-thang-cam-ung', 'https://nvc-lighting.com.vn/wp-content/uploads/2018/09/category-den-am-bac-cau-thang-chieu-sang-bang-led-day-min-700x264.jpg'),
	(N'Đèn ngủ cảm ứng', '2', 0, N'ngu-cam-ung', 'https://i.imgur.com/Y5jeAjCl.jpg')

	-- PRODUCT
INSERT INTO [dbo].[Product](Product_Name, Price, Discount, Size, Color, Description, Brand, url, Picture1, Warrant) VALUES
	(N'Đèn led âm trần MPE Series RPL 6w, 9w, 12w, 15w, 18w, 24w', 497900, 477900, N'12 x 14mm', N'3 màu ngẫu nhiên',
	N'Đèn led âm trần mpe là một trong những sản phẩm chiếu sáng của những ngôi nhà hiện nay. Nắm bắt được xu thế của công nghệ chiếu sáng mới SKYLED kinh doanh sản phẩm đèn Led âm trần hiện đại. Đèn dowlightng âm trần chuyên sử dụng cho trần thạch cao, gỗ, nhựa. Với thiết kế gọn nhẹ đã đáp ứng được nhu cầu chiếu sáng và trang trí cho ngôi nhà của bạn.', 'MPE', N'1', 
	N'den-downlight-am-tran-rpl-6st-led-6w-1.jpg', N'Không bảo hành'),
	(N'Đèn led âm trần Hufa 12W AT-69', 72000, 65000, N'Ø170 x H15', N'Ánh sáng trắng và ánh sáng vàng',
	N'Đèn led âm trần HUFA chất lượng cao.', 'HUFA', N'2', 
	N'den-downlight-am-tran-at-69-led-12w-1.jpg', N'24 tháng'),
	(N'Đèn led âm trần 12w Rạng Đông D PT04L 135/12W', 112450, 100500, N'LØ170 x H15', N'Ánh sáng trắng',
	N'Đèn led âm trần Rạng Đông chất lượng cao.', N'Rạng Đông', N'3',
	N'den-led-am-tran-9w-rang-dong-d-pt04l-110-9w.jpg', N'2 năm')

INSERT INTO [dbo].[Product](Product_Name, Price, Discount, Size, Color, Description, Brand, url, Picture1, Warrant) VALUES
	(N'Đèn led dây 9W/m NST120R NST120G NST120B Nanoco', 2230000, 1561000, N'18 x 8mm', N'Ánh sáng màu đỏ, lục, xanh dương', 
	N'Đèn Led Nanoco chất lượng cao, giá tốt, siêu bền.', N'Nanoco', N'4',
	'den-led-day-9w-m-nst120r-nst120g-nst120b-nanoco-4.jpg', N'2 năm')

INSERT INTO [dbo].[Product](Product_Name, Price, Discount, Size, Color, Description, Brand, url, Picture1, Warrant) VALUES
	(N'Đèn pha led dưới nước HBA 12W Vàng', 4040000, 1696800, N'Ø180 x H112 – AC 24V', N'Ánh sáng vàng', 
	N'Đèn Led dưới nước của hãng HBA uy tín chất lượng cao, đang giảm giá sốc.', N'HBA', N'5',
	'den-pha-led-duoi-nuoc-hba-12w-vang.jpg', N'1 - 3 năm, vui lòng liên hệ')

INSERT INTO [dbo].[Product](Product_Name, Price, Discount, Size, Color, Description, Brand, url, Picture1, Warrant) VALUES
	(N'Đèn pha led dưới nước HB 12W – Đổi 3 màu', 1920000, 1344000, N'Ø160 x H160 – AC 24V', N'Ánh sáng đỏ, tím, xanh lục', 
	N'Đèn Led dưới nước của hãng HB đang giảm giá.', N'HB', N'6',
	'den-pha-led-duoi-nuoc-hb-12w-doi-3-mau.jpg', N'3 năm')
	-- ORDER

/* Bảng tham chiếu thì Insert sau */
	-- CATEGORY_PRODUCT
INSERT [dbo].[Category_Product] ([Product_Id], [Category_Id], [Position]) VALUES (1, 3, 1)
INSERT [dbo].[Category_Product] ([Product_Id], [Category_Id], [Position]) VALUES (2, 3, 2)
INSERT [dbo].[Category_Product] ([Product_Id], [Category_Id], [Position]) VALUES (3, 3, 3)
INSERT [dbo].[Category_Product] ([Product_Id], [Category_Id], [Position]) VALUES (4, 4, 1)
INSERT [dbo].[Category_Product] ([Product_Id], [Category_Id], [Position]) VALUES (5, 5, 1)
INSERT [dbo].[Category_Product] ([Product_Id], [Category_Id], [Position]) VALUES (6, 5, 2)
GO
	-- ORDER DETAIL


-- Xóa TABLE (nên dùng lệnh để bớt lỗi)
/*
USE [master]
GO
IF OBJECT_ID('dbo.Cart', 'U')
IS NOT NULL  DROP TABLE [dbo].[Cart];
GO
GO
IF OBJECT_ID('dbo.Coupon', 'U')
IS NOT NULL  DROP TABLE [dbo].[Coupon];
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

CREATE PROCEDURE Product_GetDetailByURL
	@Product_URL varchar(255)
AS
BEGIN
	SELECT *
	from [LightShopOnline].dbo.Product
	WHERE url = @Product_URL
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

/* Hàm hỗ trợ chạy CategoryController */
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

USE [master]
GO