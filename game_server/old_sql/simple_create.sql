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

CREATE TABLE [Games].[GameSession] (
        [GameSessionId] uniqueidentifier NOT NULL,
        [GameInfoId] uniqueidentifier NOT NULL,
        [GameSessionState] nvarchar(max) NULL,
        [GameStates] nvarchar(max) NULL,
        CONSTRAINT [PK_GameSession] PRIMARY KEY ([GameSessionId]),
        CONSTRAINT [FK_GameSession_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE NO ACTION,     
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

CREATE TABLE [Games].[UserGameSession] (
          [UserGameSessionId] uniqueidentifier NOT NULL,
          [GameSessionId] uniqueidentifier NOT NULL,
          [GameInfoId] uniqueidentifier NOT NULL,
          [UserId] uniqueidentifier NOT NULL,
          CONSTRAINT [PK_UserGameSession] PRIMARY KEY ([UserGameSessionId]),
          CONSTRAINT [FK_UserGameSession_GameInfo_GameInfoId] FOREIGN KEY ([GameInfoId]) REFERENCES [Games].[GameInfo] ([GameInfoId]) ON DELETE CASCADE,
          CONSTRAINT [FK_UserGameSession_GameSession_GameSessionId] FOREIGN KEY ([GameSessionId]) REFERENCES [Games].[GameSession] ([GameSessionId]) ON DELETE NO ACTION,
          CONSTRAINT [FK_UserGameSession_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Games].[Users] ([UserId]) ON DELETE NO ACTION
      );

IF EXISTS 
(
    SELECT * FROM [sys].[identity_columns] 
        WHERE [name] IN (N'GameInfoId', N'GameName') AND [object_id] = OBJECT_ID(N'[Games].[GameInfo]')
)   
    SET IDENTITY_INSERT [Games].[GameInfo] ON;
INSERT INTO [Games].[GameInfo] 
(
[GameInfoId], 
[GameName]
)
VALUES 
('9f18c790-e0fe-42be-97db-66074fe89a26', N'Escoba'),
('b48e5657-7869-4536-ab55-c989ab02484c', N'Pusoy Dos');

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'GameInfoId', N'GameName') AND [object_id] = OBJECT_ID(N'[Games].[GameInfo]'))   
          SET IDENTITY_INSERT [Games].[GameInfo] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'EmailAddress', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Games].[Users]'))
          SET IDENTITY_INSERT [Games].[Users] ON;
      INSERT INTO [Games].[Users] ([UserId], [EmailAddress], [FirstName], [LastName])
      VALUES ('b826f918-cd3e-495a-9aaa-cb5abb84057e', N'jdoe@acme.com', N'John', N'Doe'),
      ('880e4c42-7ed2-4c28-9379-d57b998ff59e', N'ai@escoba.com', N'Hal', N'9000');
      IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'EmailAddress', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Games].[Users]'))
          SET IDENTITY_INSERT [Games].[Users] OFF;

CREATE INDEX [IX_GameSession_GameInfoId] ON [Games].[GameSession] ([GameInfoId]);

CREATE INDEX [IX_GameStatistics_GameInfoId] ON [Games].[GameStatistics] ([GameInfoId]);

CREATE INDEX [IX_Rules_GameInfoId] ON [Games].[Rules] ([GameInfoId]);

CREATE INDEX [IX_UserGameSession_GameInfoId] ON [Games].[UserGameSession] ([GameInfoId]);

CREATE INDEX [IX_UserGameSession_GameSessionId] ON [Games].[UserGameSession] ([GameSessionId]);

CREATE INDEX [IX_UserGameSession_UserId] ON [Games].[UserGameSession] ([UserId]);

CREATE INDEX [IX_UserStatistics_GameInfoId] ON [Games].[UserStatistics] ([GameInfoId]);

CREATE INDEX [IX_UserStatistics_UserId] ON [Games].[UserStatistics] ([UserId]);

