CREATE TABLE [dbo].[Orders] (
    [QuantityOrdered] INT            NULL,
    [ProductID]       INT            NULL,
	[CustomerID]	  INT			 NULL,
    [Total]           DECIMAL (18)   NULL,
    [DatePlaced]      DATETIME       DEFAULT (getdate()) NULL,
    [OrderNumber]     INT            IDENTITY (100, 1) NOT NULL,
    [Name]            NVARCHAR (100) NULL,
    [ShippingStreet1] NVARCHAR(200) NULL, 
    [ShippingStreet2] NVARCHAR(200) NULL, 
    [ShippingCity] NVARCHAR(50) NULL, 
    [ShippingState] NVARCHAR(50) NULL, 
    [ShippingZip] NVARCHAR(20) NULL, 
    [ShippingRecipient] NVARCHAR(150) NULL, 
    PRIMARY KEY CLUSTERED ([OrderNumber] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ID]), 
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

