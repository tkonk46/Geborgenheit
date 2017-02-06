CREATE TABLE [dbo].[Products] (
    [Name]        NVARCHAR (200) NOT NULL,
    [Quantity]    INT            NULL,
    [Type]        NVARCHAR (200) NULL,
    [Price]       DECIMAL (18)   NULL,
    [ImageUrl]    NVARCHAR (250) NULL,
    [Description] NTEXT          NULL,
    [DateAdded]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

