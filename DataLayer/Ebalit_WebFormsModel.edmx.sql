
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/15/2013 18:09:03
-- Generated from EDMX file: C:\Users\elias\Source\Repos\ebalit\DataLayer\Ebalit_WebFormsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE ebalit;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BlogEntry_ToBlogCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogEntries] DROP CONSTRAINT [FK_BlogEntry_ToBlogCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogCategory_BlogTopic]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogCategories] DROP CONSTRAINT [FK_BlogCategory_BlogTopic];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogComment_ToBlogEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogComments] DROP CONSTRAINT [FK_BlogComment_ToBlogEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_Task_ToTaskCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_Task_ToTaskCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskComment_ToTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TaskComments] DROP CONSTRAINT [FK_TaskComment_ToTask];
GO
IF OBJECT_ID(N'[dbo].[FK_WineToWineAttribute_ToWine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WineToWineAttributes] DROP CONSTRAINT [FK_WineToWineAttribute_ToWine];
GO
IF OBJECT_ID(N'[dbo].[FK_WineToWineAttribute_ToWineAttribute]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WineToWineAttributes] DROP CONSTRAINT [FK_WineToWineAttribute_ToWineAttribute];
GO
IF OBJECT_ID(N'[dbo].[FK_WineConsumation_ToWine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WineConsumations] DROP CONSTRAINT [FK_WineConsumation_ToWine];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectUserAssignment_aspnet_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectUserAssignments] DROP CONSTRAINT [FK_ProjectUserAssignment_aspnet_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectResources_ProjectProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectResources] DROP CONSTRAINT [FK_ProjectResources_ProjectProject];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectTask_ProjectProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectTasks] DROP CONSTRAINT [FK_ProjectTask_ProjectProject];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectResourceTaskAssignment_ProjectResources]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectResourceTaskAssignments] DROP CONSTRAINT [FK_ProjectResourceTaskAssignment_ProjectResources];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectUserAssignment_ProjectResources]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectUserAssignments] DROP CONSTRAINT [FK_ProjectUserAssignment_ProjectResources];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectResourceTaskAssignment_ProjectTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectResourceTaskAssignments] DROP CONSTRAINT [FK_ProjectResourceTaskAssignment_ProjectTask];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectWorkingReport_ProjectProject]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectWorkingReports] DROP CONSTRAINT [FK_ProjectWorkingReport_ProjectProject];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectWorkingReport_ProjectResources]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectWorkingReports] DROP CONSTRAINT [FK_ProjectWorkingReport_ProjectResources];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectWorkingReport_ProjectTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectWorkingReports] DROP CONSTRAINT [FK_ProjectWorkingReport_ProjectTask];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[BlogCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogCategories];
GO
IF OBJECT_ID(N'[dbo].[BlogEntries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogEntries];
GO
IF OBJECT_ID(N'[dbo].[BlogTopics]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogTopics];
GO
IF OBJECT_ID(N'[dbo].[BlogComments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogComments];
GO
IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO
IF OBJECT_ID(N'[dbo].[TaskCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskCategories];
GO
IF OBJECT_ID(N'[dbo].[TaskComments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaskComments];
GO
IF OBJECT_ID(N'[dbo].[aspnet_Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[aspnet_Users];
GO
IF OBJECT_ID(N'[dbo].[Wines]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Wines];
GO
IF OBJECT_ID(N'[dbo].[WineAttributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WineAttributes];
GO
IF OBJECT_ID(N'[dbo].[WineToWineAttributes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WineToWineAttributes];
GO
IF OBJECT_ID(N'[dbo].[WineConsumations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WineConsumations];
GO
IF OBJECT_ID(N'[dbo].[ProjectResources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectResources];
GO
IF OBJECT_ID(N'[dbo].[ProjectProjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectProjects];
GO
IF OBJECT_ID(N'[dbo].[ProjectResourceTaskAssignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectResourceTaskAssignments];
GO
IF OBJECT_ID(N'[dbo].[ProjectTasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectTasks];
GO
IF OBJECT_ID(N'[dbo].[ProjectUserAssignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectUserAssignments];
GO
IF OBJECT_ID(N'[dbo].[ProjectWorkingReports]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectWorkingReports];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(255)  NOT NULL,
    [Password] nvarchar(128)  NOT NULL,
    [LastActivityDate] datetime  NULL,
    [CreationDate] datetime  NULL,
    [LastLoginDate] datetime  NULL,
    [LastPasswordChangedDate] datetime  NULL,
    [ApplicationName] nvarchar(255)  NOT NULL,
    [Email] nvarchar(255)  NOT NULL,
    [Comment] nvarchar(255)  NULL,
    [PasswordQuestion] nvarchar(255)  NULL,
    [PasswordAnswer] nvarchar(255)  NULL,
    [IsApproved] bit  NULL,
    [IsOnLine] bit  NULL,
    [IsLockedOut] bit  NULL,
    [LastLockedOutDate] datetime  NULL,
    [FailedPasswordAttemptCount] int  NULL,
    [FailedPasswordAttemptWindowStart_] datetime  NULL,
    [FailedPasswordAnswerAttemptCount_] int  NULL,
    [FailedPasswordAnswerAttemptWindowStart_] datetime  NULL
);
GO

-- Creating table 'BlogCategories'
CREATE TABLE [dbo].[BlogCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Category] nvarchar(50)  NOT NULL,
    [FK_Topic] int  NOT NULL
);
GO

-- Creating table 'BlogEntries'
CREATE TABLE [dbo].[BlogEntries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Subject] nvarchar(50)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [PublishedOn] datetime  NOT NULL,
    [Category] int  NOT NULL
);
GO

-- Creating table 'BlogTopics'
CREATE TABLE [dbo].[BlogTopics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Topic] nvarchar(50)  NOT NULL,
    [PageName] nvarchar(50)  NULL
);
GO

-- Creating table 'BlogComments'
CREATE TABLE [dbo].[BlogComments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_BlogEntry] int  NOT NULL,
    [Subject] nvarchar(50)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(50)  NOT NULL,
    [eMail] nvarchar(50)  NOT NULL,
    [PostedOn] datetime  NOT NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Subject] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NULL,
    [DueDate] datetime  NULL,
    [FK_TaskCategory] int  NULL,
    [State] varchar(50)  NULL,
    [Priority] varchar(50)  NULL,
    [ClosingType] varchar(50)  NULL,
    [CreatedOn] datetime  NULL,
    [CreatedBy] varchar(50)  NULL,
    [ChangedOn] datetime  NULL,
    [ChangedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'TaskCategories'
CREATE TABLE [dbo].[TaskCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TaskCategory1] varchar(255)  NULL,
    [Description] varchar(max)  NULL
);
GO

-- Creating table 'TaskComments'
CREATE TABLE [dbo].[TaskComments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_Task] int  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [CreatedOn] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ChangedOn] datetime  NULL,
    [ChangedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'aspnet_Users'
CREATE TABLE [dbo].[aspnet_Users] (
    [ApplicationId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [LoweredUserName] nvarchar(256)  NOT NULL,
    [MobileAlias] nvarchar(16)  NULL,
    [IsAnonymous] bit  NOT NULL,
    [LastActivityDate] datetime  NOT NULL
);
GO

-- Creating table 'Wines'
CREATE TABLE [dbo].[Wines] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Label] nvarchar(255)  NOT NULL,
    [Year] int  NOT NULL,
    [BoughtOn] datetime  NULL,
    [Magazine] nvarchar(255)  NULL,
    [CreatedOn] datetime  NOT NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [ChangedOn] datetime  NULL,
    [ChangedBy] nvarchar(50)  NULL,
    [Remarks] nvarchar(max)  NULL,
    [Grape] nvarchar(255)  NULL,
    [Origin] nvarchar(255)  NULL
);
GO

-- Creating table 'WineAttributes'
CREATE TABLE [dbo].[WineAttributes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Attribute] nvarchar(255)  NOT NULL,
    [Remarks] nvarchar(max)  NULL
);
GO

-- Creating table 'WineToWineAttributes'
CREATE TABLE [dbo].[WineToWineAttributes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FK_Wine] int  NOT NULL,
    [FK_WineAttribute] int  NOT NULL
);
GO

-- Creating table 'WineConsumations'
CREATE TABLE [dbo].[WineConsumations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Taster] nvarchar(50)  NOT NULL,
    [TastedOn] datetime  NULL,
    [WeatherConditions] nvarchar(max)  NULL,
    [FK_Wine] int  NOT NULL,
    [Meal] nvarchar(max)  NULL,
    [Rating] int  NULL,
    [Remarks] nvarchar(max)  NULL,
    [CreatedOn] datetime  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [ChangedOn] datetime  NULL,
    [ChangedBy] nvarchar(50)  NULL
);
GO

-- Creating table 'ProjectResources'
CREATE TABLE [dbo].[ProjectResources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ProjectId] int  NULL,
    [Guid] uniqueidentifier  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'ProjectProjects'
CREATE TABLE [dbo].[ProjectProjects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Guid] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ProjectResourceTaskAssignments'
CREATE TABLE [dbo].[ProjectResourceTaskAssignments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TaskId] int  NULL,
    [ResourceId] int  NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'ProjectTasks'
CREATE TABLE [dbo].[ProjectTasks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Guid] uniqueidentifier  NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Parent] uniqueidentifier  NULL,
    [ProjectId] int  NULL,
    [ActualWork] float  NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'ProjectUserAssignments'
CREATE TABLE [dbo].[ProjectUserAssignments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [ResourceId] int  NULL
);
GO

-- Creating table 'ProjectWorkingReports'
CREATE TABLE [dbo].[ProjectWorkingReports] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ProjectId] int  NULL,
    [ResourceId] int  NULL,
    [TaskId] int  NULL,
    [From] datetime  NULL,
    [To] datetime  NULL,
    [Notes] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BlogCategories'
ALTER TABLE [dbo].[BlogCategories]
ADD CONSTRAINT [PK_BlogCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BlogEntries'
ALTER TABLE [dbo].[BlogEntries]
ADD CONSTRAINT [PK_BlogEntries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BlogTopics'
ALTER TABLE [dbo].[BlogTopics]
ADD CONSTRAINT [PK_BlogTopics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BlogComments'
ALTER TABLE [dbo].[BlogComments]
ADD CONSTRAINT [PK_BlogComments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TaskCategories'
ALTER TABLE [dbo].[TaskCategories]
ADD CONSTRAINT [PK_TaskCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TaskComments'
ALTER TABLE [dbo].[TaskComments]
ADD CONSTRAINT [PK_TaskComments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'aspnet_Users'
ALTER TABLE [dbo].[aspnet_Users]
ADD CONSTRAINT [PK_aspnet_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Id] in table 'Wines'
ALTER TABLE [dbo].[Wines]
ADD CONSTRAINT [PK_Wines]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WineAttributes'
ALTER TABLE [dbo].[WineAttributes]
ADD CONSTRAINT [PK_WineAttributes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WineToWineAttributes'
ALTER TABLE [dbo].[WineToWineAttributes]
ADD CONSTRAINT [PK_WineToWineAttributes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WineConsumations'
ALTER TABLE [dbo].[WineConsumations]
ADD CONSTRAINT [PK_WineConsumations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectResources'
ALTER TABLE [dbo].[ProjectResources]
ADD CONSTRAINT [PK_ProjectResources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectProjects'
ALTER TABLE [dbo].[ProjectProjects]
ADD CONSTRAINT [PK_ProjectProjects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectResourceTaskAssignments'
ALTER TABLE [dbo].[ProjectResourceTaskAssignments]
ADD CONSTRAINT [PK_ProjectResourceTaskAssignments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectTasks'
ALTER TABLE [dbo].[ProjectTasks]
ADD CONSTRAINT [PK_ProjectTasks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectUserAssignments'
ALTER TABLE [dbo].[ProjectUserAssignments]
ADD CONSTRAINT [PK_ProjectUserAssignments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectWorkingReports'
ALTER TABLE [dbo].[ProjectWorkingReports]
ADD CONSTRAINT [PK_ProjectWorkingReports]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Category] in table 'BlogEntries'
ALTER TABLE [dbo].[BlogEntries]
ADD CONSTRAINT [FK_BlogEntry_ToBlogCategory]
    FOREIGN KEY ([Category])
    REFERENCES [dbo].[BlogCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogEntry_ToBlogCategory'
CREATE INDEX [IX_FK_BlogEntry_ToBlogCategory]
ON [dbo].[BlogEntries]
    ([Category]);
GO

-- Creating foreign key on [FK_Topic] in table 'BlogCategories'
ALTER TABLE [dbo].[BlogCategories]
ADD CONSTRAINT [FK_BlogCategory_BlogTopic]
    FOREIGN KEY ([FK_Topic])
    REFERENCES [dbo].[BlogTopics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogCategory_BlogTopic'
CREATE INDEX [IX_FK_BlogCategory_BlogTopic]
ON [dbo].[BlogCategories]
    ([FK_Topic]);
GO

-- Creating foreign key on [FK_BlogEntry] in table 'BlogComments'
ALTER TABLE [dbo].[BlogComments]
ADD CONSTRAINT [FK_BlogComment_ToBlogEntry]
    FOREIGN KEY ([FK_BlogEntry])
    REFERENCES [dbo].[BlogEntries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogComment_ToBlogEntry'
CREATE INDEX [IX_FK_BlogComment_ToBlogEntry]
ON [dbo].[BlogComments]
    ([FK_BlogEntry]);
GO

-- Creating foreign key on [FK_TaskCategory] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_Task_ToTaskCategory]
    FOREIGN KEY ([FK_TaskCategory])
    REFERENCES [dbo].[TaskCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Task_ToTaskCategory'
CREATE INDEX [IX_FK_Task_ToTaskCategory]
ON [dbo].[Tasks]
    ([FK_TaskCategory]);
GO

-- Creating foreign key on [FK_Task] in table 'TaskComments'
ALTER TABLE [dbo].[TaskComments]
ADD CONSTRAINT [FK_TaskComment_ToTask]
    FOREIGN KEY ([FK_Task])
    REFERENCES [dbo].[Tasks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskComment_ToTask'
CREATE INDEX [IX_FK_TaskComment_ToTask]
ON [dbo].[TaskComments]
    ([FK_Task]);
GO

-- Creating foreign key on [FK_Wine] in table 'WineToWineAttributes'
ALTER TABLE [dbo].[WineToWineAttributes]
ADD CONSTRAINT [FK_WineToWineAttribute_ToWine]
    FOREIGN KEY ([FK_Wine])
    REFERENCES [dbo].[Wines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WineToWineAttribute_ToWine'
CREATE INDEX [IX_FK_WineToWineAttribute_ToWine]
ON [dbo].[WineToWineAttributes]
    ([FK_Wine]);
GO

-- Creating foreign key on [FK_WineAttribute] in table 'WineToWineAttributes'
ALTER TABLE [dbo].[WineToWineAttributes]
ADD CONSTRAINT [FK_WineToWineAttribute_ToWineAttribute]
    FOREIGN KEY ([FK_WineAttribute])
    REFERENCES [dbo].[WineAttributes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WineToWineAttribute_ToWineAttribute'
CREATE INDEX [IX_FK_WineToWineAttribute_ToWineAttribute]
ON [dbo].[WineToWineAttributes]
    ([FK_WineAttribute]);
GO

-- Creating foreign key on [FK_Wine] in table 'WineConsumations'
ALTER TABLE [dbo].[WineConsumations]
ADD CONSTRAINT [FK_WineConsumation_ToWine]
    FOREIGN KEY ([FK_Wine])
    REFERENCES [dbo].[Wines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WineConsumation_ToWine'
CREATE INDEX [IX_FK_WineConsumation_ToWine]
ON [dbo].[WineConsumations]
    ([FK_Wine]);
GO

-- Creating foreign key on [UserId] in table 'ProjectUserAssignments'
ALTER TABLE [dbo].[ProjectUserAssignments]
ADD CONSTRAINT [FK_ProjectUserAssignment_aspnet_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[aspnet_Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectUserAssignment_aspnet_Users'
CREATE INDEX [IX_FK_ProjectUserAssignment_aspnet_Users]
ON [dbo].[ProjectUserAssignments]
    ([UserId]);
GO

-- Creating foreign key on [ProjectId] in table 'ProjectResources'
ALTER TABLE [dbo].[ProjectResources]
ADD CONSTRAINT [FK_ProjectResources_ProjectProject]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[ProjectProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectResources_ProjectProject'
CREATE INDEX [IX_FK_ProjectResources_ProjectProject]
ON [dbo].[ProjectResources]
    ([ProjectId]);
GO

-- Creating foreign key on [ProjectId] in table 'ProjectTasks'
ALTER TABLE [dbo].[ProjectTasks]
ADD CONSTRAINT [FK_ProjectTask_ProjectProject]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[ProjectProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectTask_ProjectProject'
CREATE INDEX [IX_FK_ProjectTask_ProjectProject]
ON [dbo].[ProjectTasks]
    ([ProjectId]);
GO

-- Creating foreign key on [ResourceId] in table 'ProjectResourceTaskAssignments'
ALTER TABLE [dbo].[ProjectResourceTaskAssignments]
ADD CONSTRAINT [FK_ProjectResourceTaskAssignment_ProjectResources]
    FOREIGN KEY ([ResourceId])
    REFERENCES [dbo].[ProjectResources]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectResourceTaskAssignment_ProjectResources'
CREATE INDEX [IX_FK_ProjectResourceTaskAssignment_ProjectResources]
ON [dbo].[ProjectResourceTaskAssignments]
    ([ResourceId]);
GO

-- Creating foreign key on [ResourceId] in table 'ProjectUserAssignments'
ALTER TABLE [dbo].[ProjectUserAssignments]
ADD CONSTRAINT [FK_ProjectUserAssignment_ProjectResources]
    FOREIGN KEY ([ResourceId])
    REFERENCES [dbo].[ProjectResources]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectUserAssignment_ProjectResources'
CREATE INDEX [IX_FK_ProjectUserAssignment_ProjectResources]
ON [dbo].[ProjectUserAssignments]
    ([ResourceId]);
GO

-- Creating foreign key on [TaskId] in table 'ProjectResourceTaskAssignments'
ALTER TABLE [dbo].[ProjectResourceTaskAssignments]
ADD CONSTRAINT [FK_ProjectResourceTaskAssignment_ProjectTask]
    FOREIGN KEY ([TaskId])
    REFERENCES [dbo].[ProjectTasks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectResourceTaskAssignment_ProjectTask'
CREATE INDEX [IX_FK_ProjectResourceTaskAssignment_ProjectTask]
ON [dbo].[ProjectResourceTaskAssignments]
    ([TaskId]);
GO

-- Creating foreign key on [ProjectId] in table 'ProjectWorkingReports'
ALTER TABLE [dbo].[ProjectWorkingReports]
ADD CONSTRAINT [FK_ProjectWorkingReport_ProjectProject]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[ProjectProjects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectWorkingReport_ProjectProject'
CREATE INDEX [IX_FK_ProjectWorkingReport_ProjectProject]
ON [dbo].[ProjectWorkingReports]
    ([ProjectId]);
GO

-- Creating foreign key on [ResourceId] in table 'ProjectWorkingReports'
ALTER TABLE [dbo].[ProjectWorkingReports]
ADD CONSTRAINT [FK_ProjectWorkingReport_ProjectResources]
    FOREIGN KEY ([ResourceId])
    REFERENCES [dbo].[ProjectResources]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectWorkingReport_ProjectResources'
CREATE INDEX [IX_FK_ProjectWorkingReport_ProjectResources]
ON [dbo].[ProjectWorkingReports]
    ([ResourceId]);
GO

-- Creating foreign key on [TaskId] in table 'ProjectWorkingReports'
ALTER TABLE [dbo].[ProjectWorkingReports]
ADD CONSTRAINT [FK_ProjectWorkingReport_ProjectTask]
    FOREIGN KEY ([TaskId])
    REFERENCES [dbo].[ProjectTasks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectWorkingReport_ProjectTask'
CREATE INDEX [IX_FK_ProjectWorkingReport_ProjectTask]
ON [dbo].[ProjectWorkingReports]
    ([TaskId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------