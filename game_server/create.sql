--dotnet ef dbcontext scaffold "Server=(localdb)\\mssqllocaldb;Data Source=SORFACE;Initial Catalog=Game_Server_DB;Trusted_Connection=True;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Scaffolds

-- The EF Core tools version '3.0.0' is older than that of the runtime '3.0.1'. Update the tools for the latest features and bug fixes.

SELECT OBJECT_ID(N'[__EFMigrationsHistory]');

SELECT OBJECT_ID(N'[__EFMigrationsHistory]');

SELECT [MigrationId], [ProductVersion]
FROM [__EFMigrationsHistory]
ORDER BY [MigrationId];

IF SCHEMA_ID(N'Games') IS NULL EXEC(N'CREATE SCHEMA [Games];');

CREATE TABLE [Games].[GameInfo] (
    [GameInfoId] uniqueidentifier NOT NULL,
    [GameName] nvarchar(max) NULL,
    CONSTRAINT [PK_GameInfo] PRIMARY KEY ([GameInfoId])
);

CREATE TABLE [Games].[Users] (
    [UserId] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [EmailAddress] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
);

CREATE TABLE [Games].[GameSessions] (
    [GameSessionId] uniqueidentifier NOT NULL,
    [GameInfoId] uniqueidentifier NOT NULL,
    [GameStatisticId] uniqueidentifier NOT NULL,
    [GameSessionState] nvarchar(max) NULL,
    [GameStates] nvarchar(max) NULL,
    CONSTRAINT [PK_GameSessions] PRIMARY KEY ([GameSessionId]),
    CONSTRAINT [FK_GameSessions_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE NO ACTION
);

CREATE TABLE [Games].[Rules] (
    [RuleId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [GameInfoId] uniqueidentifier NOT NULL,
    [RuleName] nvarchar(max) NULL,
    [RuleText] nvarchar(max) NULL,
    CONSTRAINT [PK_Rules] PRIMARY KEY ([RuleId]),
    CONSTRAINT [FK_Rules_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Rules_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Games].[Users] ([UserId]) ON DELETE NO ACTION
);

CREATE TABLE [Games].[UserStatistics] (
    [UserStatisticId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [GameInfoId] uniqueidentifier NOT NULL,
    [NumberOfPlays] int NOT NULL,
    [Wins] int NOT NULL,
    [Losses] int NOT NULL,
    [Draws] int NOT NULL,
    CONSTRAINT [PK_UserStatistics] PRIMARY KEY ([UserStatisticId]),
    CONSTRAINT [FK_UserStatistics_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserStatistics_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Games].[Users] ([UserId]) ON DELETE CASCADE
);

CREATE TABLE [Games].[GameStatistics] (
    [GameStatisticId] uniqueidentifier NOT NULL,
    [GameInfoId] uniqueidentifier NOT NULL,
    [GameSessionId] uniqueidentifier NOT NULL,
    [UserStatisticId] uniqueidentifier NOT NULL,
    [FinalScore] nvarchar(max) NULL,
    [HumanWin] bit NULL,
    [AiWin] bit NULL,
    [Draw] bit NULL,
    [GameComplete] bit NOT NULL,
    [GameStart] datetime2 NOT NULL,
    [GameEnd] datetime2 NULL,
    CONSTRAINT [PK_GameStatistics] PRIMARY KEY ([GameStatisticId]),
    CONSTRAINT [FK_GameStatistics_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GameStatistics_GameSessions_GameSessionId] FOREIGN KEY ([GameSessionId]) REFERENCES [Games].[GameSessions] ([GameSessionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GameStatistics_UserStatistics_UserStatisticId] FOREIGN KEY ([UserStatisticId]) REFERENCES [Games].[UserStatistics] ([UserStatisticId]) ON DELETE NO ACTION
);

CREATE TABLE [Games].[UserGameSessions] (
    [UserGameSessionId] uniqueidentifier NOT NULL,
    [GameSessionId] uniqueidentifier NOT NULL,
    [GameInfoId] uniqueidentifier NOT NULL,
    [GameStatisticId] uniqueidentifier NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserGameSessions] PRIMARY KEY ([UserGameSessionId]),
    CONSTRAINT [FK_UserGameSessions_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserGameSessions_GameSessions_GameSessionId] FOREIGN KEY ([GameSessionId]) REFERENCES [Games].[GameSessions] ([GameSessionId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserGameSessions_GameStatistics_GameStatisticId] FOREIGN KEY ([GameStatisticId]) REFERENCES [Games].[GameStatistics] ([GameStatisticId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_UserGameSessions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Games].[Users] ([UserId]) ON DELETE NO ACTION
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GameInfoId', N'GameName') AND [object_id] = OBJECT_ID(N'[Games].[GameInfo]'))
    SET IDENTITY_INSERT [Games].[GameInfo] ON;
INSERT INTO [Games].[GameInfo] ([GameInfoId], [GameName])
VALUES ('c2ebe68a-6059-4351-8443-4491bc3bab48', N'Escoba'),
('31616261-d554-4c90-9e8d-03bd49d8f56f', N'Pusoy Dos');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GameInfoId', N'GameName') AND [object_id] = OBJECT_ID(N'[Games].[GameInfo]'))
    SET IDENTITY_INSERT [Games].[GameInfo] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'EmailAddress', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Games].[Users]'))
    SET IDENTITY_INSERT [Games].[Users] ON;
INSERT INTO [Games].[Users] ([UserId], [EmailAddress], [FirstName], [LastName])
VALUES ('3e74ec77-32a3-4a1e-ade6-9ab9afc9d37f', N'jdoe@acme.com', N'John', N'Doe'),
('bd4953f8-6a6c-455c-8078-e8d07596008b', N'ai@escoba.com', N'Hal', N'9000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'EmailAddress', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Games].[Users]'))
    SET IDENTITY_INSERT [Games].[Users] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserStatisticId', N'Draws', N'GameInfoId', N'Losses', N'NumberOfPlays', N'UserId', N'Wins') AND [object_id] = OBJECT_ID(N'[Games].[UserStatistics]'))
    SET IDENTITY_INSERT [Games].[UserStatistics] ON;
INSERT INTO [Games].[UserStatistics] ([UserStatisticId], [Draws], [GameInfoId], [Losses], [NumberOfPlays], [UserId], [Wins])
VALUES ('e03841ba-0c15-4844-82f3-3563b441a352', 0, 'c2ebe68a-6059-4351-8443-4491bc3bab48', 0, 0, '3e74ec77-32a3-4a1e-ade6-9ab9afc9d37f', 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserStatisticId', N'Draws', N'GameInfoId', N'Losses', N'NumberOfPlays', N'UserId', N'Wins') AND [object_id] = OBJECT_ID(N'[Games].[UserStatistics]'))
    SET IDENTITY_INSERT [Games].[UserStatistics] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserStatisticId', N'Draws', N'GameInfoId', N'Losses', N'NumberOfPlays', N'UserId', N'Wins') AND [object_id] = OBJECT_ID(N'[Games].[UserStatistics]'))
    SET IDENTITY_INSERT [Games].[UserStatistics] ON;
INSERT INTO [Games].[UserStatistics] ([UserStatisticId], [Draws], [GameInfoId], [Losses], [NumberOfPlays], [UserId], [Wins])
VALUES ('fb08cb85-86c2-4beb-bd29-11c010befb5a', 0, 'c2ebe68a-6059-4351-8443-4491bc3bab48', 0, 0, 'bd4953f8-6a6c-455c-8078-e8d07596008b', 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserStatisticId', N'Draws', N'GameInfoId', N'Losses', N'NumberOfPlays', N'UserId', N'Wins') AND [object_id] = OBJECT_ID(N'[Games].[UserStatistics]'))
    SET IDENTITY_INSERT [Games].[UserStatistics] OFF;

CREATE INDEX [IX_GameSessions_GameInfoId] ON [Games].[GameSessions] ([GameInfoId]);

CREATE INDEX [IX_GameSessions_GameStatisticId] ON [Games].[GameSessions] ([GameStatisticId]);

CREATE INDEX [IX_GameStatistics_GameInfoId] ON [Games].[GameStatistics] ([GameInfoId]);

CREATE UNIQUE INDEX [IX_GameStatistics_GameSessionId] ON [Games].[GameStatistics] ([GameSessionId]);

CREATE INDEX [IX_GameStatistics_UserStatisticId] ON [Games].[GameStatistics] ([UserStatisticId]);

CREATE INDEX [IX_Rules_GameInfoId] ON [Games].[Rules] ([GameInfoId]);

CREATE INDEX [IX_Rules_UserId] ON [Games].[Rules] ([UserId]);

CREATE INDEX [IX_UserGameSessions_GameInfoId] ON [Games].[UserGameSessions] ([GameInfoId]);

CREATE INDEX [IX_UserGameSessions_GameSessionId] ON [Games].[UserGameSessions] ([GameSessionId]);

CREATE INDEX [IX_UserGameSessions_GameStatisticId] ON [Games].[UserGameSessions] ([GameStatisticId]);

CREATE INDEX [IX_UserGameSessions_UserId] ON [Games].[UserGameSessions] ([UserId]);

CREATE INDEX [IX_UserStatistics_GameInfoId] ON [Games].[UserStatistics] ([GameInfoId]);

CREATE INDEX [IX_UserStatistics_UserId] ON [Games].[UserStatistics] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191215215647_InitialCreate', N'3.0.1');