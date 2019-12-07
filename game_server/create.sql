USE [Test8]
GO

/****** Object:  Table [Games].[GameInfo]    Script Date: 12/6/2019 0:07:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Games].[GameInfo](
	[GameInfoId] [uniqueidentifier] NOT NULL,
	[GameName] [nvarchar](max) NULL,
 CONSTRAINT [PK_GameInfo] PRIMARY KEY CLUSTERED 
(
	[GameInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

USE [Test8]
GO

/****** Object:  Table [Games].[GameSession]    Script Date: 12/6/2019 0:08:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Games].[GameSession](
	[GameSessionId] [uniqueidentifier] NOT NULL,
	[GameInfoId] [uniqueidentifier] NOT NULL,
	[GameInfoId1] [uniqueidentifier] NULL,
	[GameSessionState] [nvarchar](max) NULL,
	[GameStates] [nvarchar](max) NULL,
 CONSTRAINT [PK_GameSession] PRIMARY KEY CLUSTERED 
(
	[GameSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Games].[GameSession]  WITH CHECK ADD  CONSTRAINT [FK_GameSession_GameInfo_GameInfoId] FOREIGN KEY([GameInfoId])
REFERENCES [Games].[GameInfo] ([GameInfoId])
GO

ALTER TABLE [Games].[GameSession] CHECK CONSTRAINT [FK_GameSession_GameInfo_GameInfoId]
GO

ALTER TABLE [Games].[GameSession]  WITH CHECK ADD  CONSTRAINT [FK_GameSession_GameInfo_GameInfoId1] FOREIGN KEY([GameInfoId1])
REFERENCES [Games].[GameInfo] ([GameInfoId])
GO

ALTER TABLE [Games].[GameSession] CHECK CONSTRAINT [FK_GameSession_GameInfo_GameInfoId1]
GO


USE [Test8]
GO

/****** Object:  Table [Games].[GameStatistics]    Script Date: 12/6/2019 0:08:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Games].[GameStatistics](
	[GameStatisticId] [uniqueidentifier] NOT NULL,
	[GameInfoId] [uniqueidentifier] NOT NULL,
	[TimesPlayed] [int] NOT NULL,
	[HumanWins] [int] NOT NULL,
	[AiWins] [int] NOT NULL,
	[HumanLosses] [int] NOT NULL,
	[AiLosses] [int] NOT NULL,
	[HumanDraws] [int] NOT NULL,
	[AiDraws] [int] NOT NULL,
 CONSTRAINT [PK_GameStatistics] PRIMARY KEY CLUSTERED 
(
	[GameStatisticId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Games].[GameStatistics]  WITH CHECK ADD  CONSTRAINT [FK_GameStatistics_GameInfo_GameInfoId] FOREIGN KEY([GameInfoId])
REFERENCES [Games].[GameInfo] ([GameInfoId])
ON DELETE CASCADE
GO

ALTER TABLE [Games].[GameStatistics] CHECK CONSTRAINT [FK_GameStatistics_GameInfo_GameInfoId]
GO


USE [Test8]
GO

/****** Object:  Table [Games].[Rules]    Script Date: 12/6/2019 0:08:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Games].[Rules](
	[RuleId] [uniqueidentifier] NOT NULL,
	[GameInfoId] [uniqueidentifier] NOT NULL,
	[RuleName] [nvarchar](max) NULL,
	[RuleText] [nvarchar](max) NULL,
 CONSTRAINT [PK_Rules] PRIMARY KEY CLUSTERED 
(
	[RuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [Games].[Rules]  WITH CHECK ADD  CONSTRAINT [FK_Rules_GameInfo_GameInfoId] FOREIGN KEY([GameInfoId])
REFERENCES [Games].[GameInfo] ([GameInfoId])
ON DELETE CASCADE
GO

ALTER TABLE [Games].[Rules] CHECK CONSTRAINT [FK_Rules_GameInfo_GameInfoId]
GO


USE [Test8]
GO

/****** Object:  Table [Games].[UserGameSession]    Script Date: 12/6/2019 0:09:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Games].[UserGameSession](
	[UserGameSessionId] [uniqueidentifier] NOT NULL,
	[GameSessionId] [uniqueidentifier] NOT NULL,
	[GameInfoId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserGameSession] PRIMARY KEY CLUSTERED 
(
	[UserGameSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Games].[UserGameSession]  WITH CHECK ADD  CONSTRAINT [FK_UserGameSession_GameInfo_GameInfoId] FOREIGN KEY([GameInfoId])
REFERENCES [Games].[GameInfo] ([GameInfoId])
ON DELETE CASCADE
GO

ALTER TABLE [Games].[UserGameSession] CHECK CONSTRAINT [FK_UserGameSession_GameInfo_GameInfoId]
GO

ALTER TABLE [Games].[UserGameSession]  WITH CHECK ADD  CONSTRAINT [FK_UserGameSession_GameSession_GameSessionId] FOREIGN KEY([GameSessionId])
REFERENCES [Games].[GameSession] ([GameSessionId])
GO

ALTER TABLE [Games].[UserGameSession] CHECK CONSTRAINT [FK_UserGameSession_GameSession_GameSessionId]
GO

ALTER TABLE [Games].[UserGameSession]  WITH CHECK ADD  CONSTRAINT [FK_UserGameSession_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Games].[Users] ([UserId])
GO

ALTER TABLE [Games].[UserGameSession] CHECK CONSTRAINT [FK_UserGameSession_Users_UserId]
GO


USE [Test8]
GO

/****** Object:  Table [Games].[Users]    Script Date: 12/6/2019 0:09:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Games].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[EmailAddress] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [Test8]
GO

/****** Object:  Table [Games].[UserStatistics]    Script Date: 12/6/2019 0:10:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Games].[UserStatistics](
	[UserStatisticId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[GameInfoId] [uniqueidentifier] NOT NULL,
	[NumberOfPlays] [int] NOT NULL,
	[Wins] [int] NOT NULL,
	[Losses] [int] NOT NULL,
	[Draws] [int] NOT NULL,
 CONSTRAINT [PK_UserStatistics] PRIMARY KEY CLUSTERED 
(
	[UserStatisticId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Games].[UserStatistics]  WITH CHECK ADD  CONSTRAINT [FK_UserStatistics_GameInfo_GameInfoId] FOREIGN KEY([GameInfoId])
REFERENCES [Games].[GameInfo] ([GameInfoId])
ON DELETE CASCADE
GO

ALTER TABLE [Games].[UserStatistics] CHECK CONSTRAINT [FK_UserStatistics_GameInfo_GameInfoId]
GO

ALTER TABLE [Games].[UserStatistics]  WITH CHECK ADD  CONSTRAINT [FK_UserStatistics_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [Games].[Users] ([UserId])
ON DELETE CASCADE
GO

ALTER TABLE [Games].[UserStatistics] CHECK CONSTRAINT [FK_UserStatistics_Users_UserId]
GO



