USE [Database_ResearchRepository]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvestigationProjects] (
    [Id]          INT         NOT NULL,
    [Name]        NCHAR (30)  NULL,
    [StartDate]   DATETIME    NULL,
    [EndDate]     DATETIME    NULL,
    [InvestigationGroupID] INT NULL,
    [Description] VARCHAR(8000) NULL
);

INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (1, N'Water Purification', N'2021-08-12 00:00:00', N'2021-09-12 00:00:00',1,N'Water Putification based on filter specialized techniques')
INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (2, N'Artificial Intelligence', N'2000-09-12 00:00:00', N'2021-08-12 00:00:00',2,N'Simulation of human intelligence processes by machines to recognize human faces')
INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (3, N'Natural Language Proccesing', N'2001-03-07 00:00:00', N'2021-08-12 00:00:00',3,N'Youtube natural language proccesing to create subtitles for videos')
INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (4, N'Forest Net', N'2002-03-07 00:00:00', N'2021-08-12 00:00:00',4,N'Deforestation driver classification using satellite imagery.')
INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (5, N'Solar Forecasting', N'2004-03-07 00:00:00', N'2021-08-12 00:00:00',5,N'Calibrated probabilistic solar irradiance forecasting.')
INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (6, N'Oxygen Analysis', N'2006-03-07 00:00:00', N'2021-08-12 00:00:00',6,N'World locations classified by their oxygen quality')
INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (7, N'COVID Vaccinate', N'2007-03-07 00:00:00', N'2021-08-12 00:00:00',7,N'Analysis of the effectiveness of the vaccine in different people')
INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (8, N'ECG Arrhythmia', N'2008-03-07 00:00:00', N'2021-08-12 00:00:00',8,N'Cardiologist-level arrythmia detection from ECG signals.')
INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (9, N'Education', N'2006-07-07 00:00:00', N'2021-08-12 00:00:00',9,N'Designing natural language models to detect writing errors and provide feedback.')
INSERT INTO [dbo].[InvestigationProjects] ([Id], [Name], [StartDate], [EndDate],[InvestigationGroupID],[Description]) VALUES (10, N'Palliative Care', N'2002-03-08 00:00:00', N'2021-08-12 00:00:00',10,N'Using Electronic Health Record Data to direct palliative care resources.')



