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
    [GameInfoId1] uniqueidentifier NULL,
    [GameSessionState] nvarchar(max) NULL,
    [GameStates] nvarchar(max) NULL,
    CONSTRAINT [PK_GameSessions] PRIMARY KEY ([GameSessionId]),
    CONSTRAINT [FK_GameSessions_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_GameSessions_GameInfo_GameInfoId1] FOREIGN KEY ([GameInfoId1]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE NO ACTION
    );

CREATE TABLE [Games].[GameStatistics] (
          [GameStatisticId] uniqueidentifier NOT NULL,
          [GameInfoId] uniqueidentifier NOT NULL,
          [TimesPlayed] int NOT NULL,
          [HumanWins] int NOT NULL,
          [AiWins] int NOT NULL,
          [HumanLosses] int NOT NULL,
          [AiLosses] int NOT NULL,
          [HumanDraws] int NOT NULL,
          [AiDraws] int NOT NULL,
          CONSTRAINT [PK_GameStatistics] PRIMARY KEY ([GameStatisticId]),
          CONSTRAINT [FK_GameStatistics_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE CASCADE
      );

CREATE TABLE [Games].[Rules] (
          [RuleId] uniqueidentifier NOT NULL,
          [GameInfoId] uniqueidentifier NOT NULL,
          [RuleName] nvarchar(max) NULL,
          [RuleText] nvarchar(max) NULL,
          CONSTRAINT [PK_Rules] PRIMARY KEY ([RuleId]),
          CONSTRAINT [FK_Rules_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE CASCADE
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

CREATE TABLE [Games].[UserGameSessions] (
          [UserGameSessionId] uniqueidentifier NOT NULL,
          [GameSessionId] uniqueidentifier NOT NULL,
          [GameInfoId] uniqueidentifier NOT NULL,
          [UserId] uniqueidentifier NOT NULL,
          CONSTRAINT [PK_UserGameSessions] PRIMARY KEY ([UserGameSessionId]),
          CONSTRAINT [FK_UserGameSessions_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE CASCADE,
          CONSTRAINT [FK_UserGameSessions_GameSessions_GameSessionId] FOREIGN KEY ([GameSessionId]) REFERENCES [Games].[GameSessions] ([GameSessionId]) ON DELETE 
NO ACTION,
          CONSTRAINT [FK_UserGameSessions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Games].[Users] ([UserId]) ON DELETE NO ACTION
      );


IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GameInfoId', N'GameName') AND [object_id] = OBJECT_ID(N'[Games].[GameInfo]'))
          SET IDENTITY_INSERT [Games].[GameInfo] ON;
      INSERT INTO [Games].[GameInfo] ([GameInfoId], [GameName])
      VALUES ('b6848618-5dad-4e21-bbca-a5f592c188ef', N'Escoba'),
      ('02730ad8-87b0-44ab-866b-a333107f64c5', N'Pusoy Dos');
      IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GameInfoId', N'GameName') AND [object_id] = OBJECT_ID(N'[Games].[GameInfo]'))
          SET IDENTITY_INSERT [Games].[GameInfo] OFF;

 IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'EmailAddress', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Games].[Users]'))
          SET IDENTITY_INSERT [Games].[Users] ON;
      INSERT INTO [Games].[Users] ([UserId], [EmailAddress], [FirstName], [LastName])
      VALUES ('4d1612a8-01bb-472c-85d5-3933434478d5', N'jdoe@acme.com', N'John', N'Doe'),
      ('06926505-7940-461f-9688-7ee6cf3a2e17', N'ai@escoba.com', N'Hal', N'9000');
      IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'EmailAddress', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Games].[Users]'))
          SET IDENTITY_INSERT [Games].[Users] OFF;

CREATE INDEX [IX_GameSessions_GameInfoId] ON [Games].[GameSessions] ([GameInfoId]);
CREATE INDEX [IX_GameSessions_GameInfoId1] ON [Games].[GameSessions] ([GameInfoId1]);
CREATE INDEX [IX_GameStatistics_GameInfoId] ON [Games].[GameStatistics] ([GameInfoId]);
CREATE INDEX [IX_Rules_GameInfoId] ON [Games].[Rules] ([GameInfoId]);
CREATE INDEX [IX_UserGameSessions_GameInfoId] ON [Games].[UserGameSessions] ([GameInfoId]);
CREATE INDEX [IX_UserGameSessions_GameSessionId] ON [Games].[UserGameSessions] ([GameSessionId]);
CREATE INDEX [IX_UserStatistics_GameInfoId] ON [Games].[UserStatistics] ([GameInfoId]);
CREATE INDEX [IX_UserStatistics_UserId] ON [Games].[UserStatistics] ([UserId]);

dotnet ef dbcontext scaffold "Server=(localdb)\\mssqllocaldb;Data Source=SORFACE;Initial Catalog=Game_Server_DB;Trusted_Connection=True;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Scaffolds