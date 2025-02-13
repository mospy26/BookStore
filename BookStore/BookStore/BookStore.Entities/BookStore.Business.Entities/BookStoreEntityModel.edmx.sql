
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/28/2020 02:17:09
-- Generated from EDMX file: C:\Users\Mustafa Fulwala\Documents\BookStore\BookStore\BookStore\BookStore.Entities\BookStore.Business.Entities\BookStoreEntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BookStore];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DeliveryOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_DeliveryOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderOrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderOrderItem];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_CustomerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerLoginCredential]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_CustomerLoginCredential];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderItemMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderItemMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_MediaStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Media] DROP CONSTRAINT [FK_MediaStock];
GO
IF OBJECT_ID(N'[dbo].[FK_PurchaseUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Purchases] DROP CONSTRAINT [FK_PurchaseUser];
GO
IF OBJECT_ID(N'[dbo].[FK_PurchaseMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Purchases] DROP CONSTRAINT [FK_PurchaseMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_RatingMedia]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ratings] DROP CONSTRAINT [FK_RatingMedia];
GO
IF OBJECT_ID(N'[dbo].[FK_RatingUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ratings] DROP CONSTRAINT [FK_RatingUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Deliveries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Deliveries];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[OrderItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderItems];
GO
IF OBJECT_ID(N'[dbo].[Stocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stocks];
GO
IF OBJECT_ID(N'[dbo].[LoginCredentials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginCredentials];
GO
IF OBJECT_ID(N'[dbo].[Media]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Media];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Purchases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Purchases];
GO
IF OBJECT_ID(N'[dbo].[Ratings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ratings];
GO
IF OBJECT_ID(N'[dbo].[UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRole];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Revision] timestamp  NOT NULL,
    [LoginCredential_Id] int  NOT NULL
);
GO

-- Creating table 'Deliveries'
CREATE TABLE [dbo].[Deliveries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DeliveryDate] datetime  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [Warehouse] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Total] decimal(18,0)  NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [Status] int  NOT NULL,
    [DeliveryOrder_Order_Id] int  NULL,
    [Customer_Id] int  NOT NULL
);
GO

-- Creating table 'OrderItems'
CREATE TABLE [dbo].[OrderItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [OrderOrderItem_OrderItem_Id] int  NOT NULL,
    [Media_Id] int  NOT NULL
);
GO

-- Creating table 'Stocks'
CREATE TABLE [dbo].[Stocks] (
    [Id] uniqueidentifier  NOT NULL,
    [Warehouse] nvarchar(max)  NOT NULL,
    [Quantity] int  NULL
);
GO

-- Creating table 'LoginCredentials'
CREATE TABLE [dbo].[LoginCredentials] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(30)  NOT NULL,
    [EncryptedPassword] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Media'
CREATE TABLE [dbo].[Media] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Genre] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [UserId] int  NOT NULL,
    [Stocks_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [RatingId] int  NOT NULL
);
GO

-- Creating table 'Purchases'
CREATE TABLE [dbo].[Purchases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User_Id] int  NOT NULL,
    [Media_Id] int  NOT NULL
);
GO

-- Creating table 'Ratings'
CREATE TABLE [dbo].[Ratings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Like] bit  NOT NULL,
    [Media_Id] int  NOT NULL,
    [User_Id] int  NOT NULL
);
GO

-- Creating table 'UserRole'
CREATE TABLE [dbo].[UserRole] (
    [User_Id] int  NOT NULL,
    [Roles_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Deliveries'
ALTER TABLE [dbo].[Deliveries]
ADD CONSTRAINT [PK_Deliveries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [PK_OrderItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Stocks'
ALTER TABLE [dbo].[Stocks]
ADD CONSTRAINT [PK_Stocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoginCredentials'
ALTER TABLE [dbo].[LoginCredentials]
ADD CONSTRAINT [PK_LoginCredentials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Media'
ALTER TABLE [dbo].[Media]
ADD CONSTRAINT [PK_Media]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [PK_Purchases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [PK_Ratings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [User_Id], [Roles_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY CLUSTERED ([User_Id], [Roles_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DeliveryOrder_Order_Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_DeliveryOrder]
    FOREIGN KEY ([DeliveryOrder_Order_Id])
    REFERENCES [dbo].[Deliveries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryOrder'
CREATE INDEX [IX_FK_DeliveryOrder]
ON [dbo].[Orders]
    ([DeliveryOrder_Order_Id]);
GO

-- Creating foreign key on [OrderOrderItem_OrderItem_Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderOrderItem]
    FOREIGN KEY ([OrderOrderItem_OrderItem_Id])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderItem'
CREATE INDEX [IX_FK_OrderOrderItem]
ON [dbo].[OrderItems]
    ([OrderOrderItem_OrderItem_Id]);
GO

-- Creating foreign key on [Customer_Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_CustomerOrder]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrder'
CREATE INDEX [IX_FK_CustomerOrder]
ON [dbo].[Orders]
    ([Customer_Id]);
GO

-- Creating foreign key on [LoginCredential_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_CustomerLoginCredential]
    FOREIGN KEY ([LoginCredential_Id])
    REFERENCES [dbo].[LoginCredentials]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerLoginCredential'
CREATE INDEX [IX_FK_CustomerLoginCredential]
ON [dbo].[Users]
    ([LoginCredential_Id]);
GO

-- Creating foreign key on [Media_Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderItemMedia]
    FOREIGN KEY ([Media_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderItemMedia'
CREATE INDEX [IX_FK_OrderItemMedia]
ON [dbo].[OrderItems]
    ([Media_Id]);
GO

-- Creating foreign key on [User_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_User]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Roles_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_Role]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole_Role'
CREATE INDEX [IX_FK_UserRole_Role]
ON [dbo].[UserRole]
    ([Roles_Id]);
GO

-- Creating foreign key on [Stocks_Id] in table 'Media'
ALTER TABLE [dbo].[Media]
ADD CONSTRAINT [FK_MediaStock]
    FOREIGN KEY ([Stocks_Id])
    REFERENCES [dbo].[Stocks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MediaStock'
CREATE INDEX [IX_FK_MediaStock]
ON [dbo].[Media]
    ([Stocks_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_PurchaseUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseUser'
CREATE INDEX [IX_FK_PurchaseUser]
ON [dbo].[Purchases]
    ([User_Id]);
GO

-- Creating foreign key on [Media_Id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [FK_PurchaseMedia]
    FOREIGN KEY ([Media_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PurchaseMedia'
CREATE INDEX [IX_FK_PurchaseMedia]
ON [dbo].[Purchases]
    ([Media_Id]);
GO

-- Creating foreign key on [Media_Id] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [FK_RatingMedia]
    FOREIGN KEY ([Media_Id])
    REFERENCES [dbo].[Media]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RatingMedia'
CREATE INDEX [IX_FK_RatingMedia]
ON [dbo].[Ratings]
    ([Media_Id]);
GO

-- Creating foreign key on [User_Id] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [FK_RatingUser]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RatingUser'
CREATE INDEX [IX_FK_RatingUser]
ON [dbo].[Ratings]
    ([User_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------