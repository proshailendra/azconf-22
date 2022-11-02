USE [ePizzaHub]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON
GO
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (1, N'Pizza', N'Pizza')
GO
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (2, N'Dessert', N'Dessert')
GO
INSERT [dbo].[Categories] ([Id], [Name], [Description]) VALUES (3, N'Beverages', N'Beverages')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemTypes] ON
GO
INSERT [dbo].[ItemTypes] ([Id], [Name]) VALUES (1, N'Veg')
GO
INSERT [dbo].[ItemTypes] ([Id], [Name]) VALUES (2, N'NonVeg')
GO
SET IDENTITY_INSERT [dbo].[ItemTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (1, N'Farm House', N'Delightful combination of onion, capsicum, tomato & grilled mushroom', CAST(299.00 AS Decimal(18, 2)), N'/images/farmhouse_pizza.webp', 1, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (2, N'Pepe Paneer', N'Flavorful trio of juicy paneer, crisp capsicum with spicy red paprika', CAST(399.00 AS Decimal(18, 2)), N'/images/paneer_pizza.webp', 1, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (3, N'Veggie Paradise', N'The awesome foursome! Golden corn, black olives, capsicum, red paprika', CAST(499.00 AS Decimal(18, 2)), N'/images/veggie_pizza.webp', 1, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (4, N'Cheese & Corn', N'A delectable combination of sweet & juicy golden corn', CAST(399.00 AS Decimal(18, 2)), N'/images/cheese_n_corn.webp', 1, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (5, N'Non Veg Delight', N'Chicken sausage, pepper barbecue chicken & peri-peri chicken in a fresh pan crust', CAST(499.00 AS Decimal(18, 2)), N'/images/noveg_delight.webp', 1, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (6, N'Non Veg Supreme', N'Loaded with a delicious creamy tomato pasta topping, pepper chicken, capsicum', CAST(599.00 AS Decimal(18, 2)), N'/images/nonveg_supreme.webp', 1, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (7, N'Choco Lava Cake', N'Chocolate lovers delight! Indulgent, gooey molten lava inside chocolate cake', CAST(99.00 AS Decimal(18, 2)), N'/images/choco_lava.webp', 2, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (8, N'Butterscotch Cake', N'Sweet temptation! Butterscotch flavored mousse', CAST(99.00 AS Decimal(18, 2)), N'/images/butter_scotch.webp', 2, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (9, N'Red Lava Cake', N'NULLA truly indulgent experience with sweet and rich red cake', CAST(99.00 AS Decimal(18, 2)), N'/images/red_cake.webp', 2, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (10, N'Pepsi', N'Sparkling and Refreshing Beverage', CAST(99.00 AS Decimal(18, 2)), N'/images/pepsi.webp', 3, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (11, N'Mirinda', N'Delicious Orange Flavoured beverage', CAST(99.00 AS Decimal(18, 2)), N'/images/mirinda.webp', 3, 1)
GO
INSERT [dbo].[Items] ([Id], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [ItemTypeId]) VALUES (12, N'7 Up', N'Refreshing clear drink with lemon flavor', CAST(99.00 AS Decimal(18, 2)), N'/images/7up.webp', 3, 1)
GO
SET IDENTITY_INSERT [dbo].[Items] OFF
GO

CREATE TABLE [Logs] (

   [Id] int IDENTITY(1,1) NOT NULL,
   [Message] nvarchar(max) NULL,
   [MessageTemplate] nvarchar(max) NULL,
   [Level] nvarchar(128) NULL,
   [TimeStamp] datetime NOT NULL,
   [Exception] nvarchar(max) NULL,
   [Properties] nvarchar(max) NULL

   CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC) 
);