--dotnet ef dbcontext scaffold "Server=(localdb)\\mssqllocaldb;Data Source=SORFACE;Initial Catalog=Game_Server_DB;Trusted_Connection=True;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Scaffolds


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
    [UserId] uniqueidentifier NOT NULL,
    [FinalScore] nvarchar(max) NULL,
    [HumanWin] bit NULL,
    [AiWin] bit NULL,
    [Draw] bit NULL,
    [GameComplete] bit NOT NULL,
    [GameStart] datetime2 NOT NULL,
    [GameEnd] datetime2 NULL,
    CONSTRAINT [PK_GameStatistics] PRIMARY KEY ([GameStatisticId]),
    CONSTRAINT [FK_GameStatistics_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GameStatistics_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Games].[Users] ([UserId]) ON DELETE CASCADE,
    CONSTRAINT [FK_GameStatistics_UserStatistics_UserStatisticId] FOREIGN KEY ([UserStatisticId]) REFERENCES [Games].[UserStatistics] ([UserStatisticId]) ON DELETE NO ACTION
);


CREATE TABLE [Games].[GameSessions] (
    [GameSessionId] uniqueidentifier NOT NULL,
    [GameInfoId] uniqueidentifier NOT NULL,
    [GameStatisticId] uniqueidentifier NOT NULL,
    [GameSessionState] nvarchar(max) NULL,
    [GameStates] nvarchar(max) NULL,
    CONSTRAINT [PK_GameSessions] PRIMARY KEY ([GameSessionId]),
    CONSTRAINT [FK_GameSessions_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_GameSessions_GameStatistics_GameStatisticId] FOREIGN KEY ([GameStatisticId]) REFERENCES [Games].[GameStatistics] ([GameStatisticId]) ON DELETE 
CASCADE
);


CREATE TABLE [Games].[UserGameSessions] (
    [UserGameSessionId] uniqueidentifier NOT NULL,
    [GameSessionId] uniqueidentifier NOT NULL,
    [GameStatisticId] uniqueidentifier NOT NULL,
    [GameInfoId] uniqueidentifier NOT NULL,
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
VALUES ('95aff857-682e-494a-97e9-21996b5a753d', N'Escoba'),
('7bdbcf13-dce5-4424-92cd-f7be50f3259a', N'Pusoy Dos');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GameInfoId', N'GameName') AND [object_id] = OBJECT_ID(N'[Games].[GameInfo]'))
    SET IDENTITY_INSERT [Games].[GameInfo] OFF;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'EmailAddress', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Games].[Users]'))
    SET IDENTITY_INSERT [Games].[Users] ON;
INSERT INTO [Games].[Users] ([UserId], [EmailAddress], [FirstName], [LastName])
VALUES ('29d76e39-e05e-4b84-a13d-7d98924ab18c', N'jdoe@acme.com', N'John', N'Doe'),
('7611b043-b630-49a4-8fcc-c6e783d72719', N'ai@escoba.com', N'Hal', N'9000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'EmailAddress', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Games].[Users]'))
    SET IDENTITY_INSERT [Games].[Users] OFF;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserStatisticId', N'Draws', N'GameInfoId', N'Losses', N'NumberOfPlays', N'UserId', N'Wins') AND [object_id] = OBJECT_ID(N'[Games].[UserStatistics]'))
    SET IDENTITY_INSERT [Games].[UserStatistics] ON;
INSERT INTO [Games].[UserStatistics] ([UserStatisticId], [Draws], [GameInfoId], [Losses], [NumberOfPlays], [UserId], [Wins])
VALUES ('da8d378f-8896-4c21-94b6-6d438ace6bb8', 0, '95aff857-682e-494a-97e9-21996b5a753d', 0, 0, '29d76e39-e05e-4b84-a13d-7d98924ab18c', 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserStatisticId', N'Draws', N'GameInfoId', N'Losses', N'NumberOfPlays', N'UserId', N'Wins') AND [object_id] = OBJECT_ID(N'[Games].[UserStatistics]'))
    SET IDENTITY_INSERT [Games].[UserStatistics] OFF;


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserStatisticId', N'Draws', N'GameInfoId', N'Losses', N'NumberOfPlays', N'UserId', N'Wins') AND [object_id] = OBJECT_ID(N'[Games].[UserStatistics]'))
    SET IDENTITY_INSERT [Games].[UserStatistics] ON;
INSERT INTO [Games].[UserStatistics] ([UserStatisticId], [Draws], [GameInfoId], [Losses], [NumberOfPlays], [UserId], [Wins])
VALUES ('accd8c26-496e-4f96-97e2-4fde22d1dea9', 0, '95aff857-682e-494a-97e9-21996b5a753d', 0, 0, '7611b043-b630-49a4-8fcc-c6e783d72719', 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserStatisticId', N'Draws', N'GameInfoId', N'Losses', N'NumberOfPlays', N'UserId', N'Wins') AND [object_id] = OBJECT_ID(N'[Games].[UserStatistics]'))
    SET IDENTITY_INSERT [Games].[UserStatistics] OFF;


CREATE INDEX [IX_GameSessions_GameInfoId] ON [Games].[GameSessions] ([GameInfoId]);


CREATE UNIQUE INDEX [IX_GameSessions_GameStatisticId] ON [Games].[GameSessions] ([GameStatisticId]);


CREATE INDEX [IX_GameStatistics_GameInfoId] ON [Games].[GameStatistics] ([GameInfoId]);


CREATE UNIQUE INDEX [IX_GameStatistics_GameSessionId] ON [Games].[GameStatistics] ([GameSessionId]);


CREATE INDEX [IX_GameStatistics_UserId] ON [Games].[GameStatistics] ([UserId]);


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
VALUES (N'20191208033256_InitalCreate', N'3.0.1');
