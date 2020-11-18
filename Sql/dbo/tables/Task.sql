CREATE TABLE [dbo].[Task]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Subject] VARCHAR(255) NOT NULL, 
    [IsComplete] BIT NOT NULL, 
    [AssignedMemberId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT FK_Task_AssignedMemberId FOREIGN KEY ([AssignedMemberId]) REFERENCES [dbo].[Member] ([Id])
)
