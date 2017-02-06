CREATE TABLE [dbo].[OrderProducts] (
    [ProductsID]        INT            NOT NULL,
    [OrdersOrderNumber] INT            NOT NULL,
    [Quantity]          INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Quantity] ASC, [ProductsID] ASC, [OrdersOrderNumber] ASC),
    FOREIGN KEY ([OrdersOrderNumber]) REFERENCES [dbo].[Orders] ([OrderNumber]),
    FOREIGN KEY ([ProductsID]) REFERENCES [dbo].[Products] ([ID])
);

