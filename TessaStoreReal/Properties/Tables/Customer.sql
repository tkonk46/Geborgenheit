CREATE TABLE [dbo].[Customer] (
    [CustomerID]       INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]        NVARCHAR (150) NULL,
    [LastName]         NVARCHAR (150) NULL,
    [BillingAddress]   NVARCHAR (400) NULL,
    [ShippingAddress]  NVARCHAR (400) NULL,
    [CreditCardNumber] NVARCHAR (150) NULL,
    [CreditCardName]   NVARCHAR (150) NULL,
    [CVV]              NVARCHAR (4)   NULL,
    [CreditCardExp]    DATETIME       NULL,
    [EmailAddress]     NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([CustomerID] ASC)
);



