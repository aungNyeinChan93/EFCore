IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508025700_initial'
)
BEGIN
    CREATE TABLE [Leagues] (
        [LeagueId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Leagues] PRIMARY KEY ([LeagueId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508025700_initial'
)
BEGIN
    CREATE TABLE [Teams] (
        [TeamId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [LeagueId] int NOT NULL,
        CONSTRAINT [PK_Teams] PRIMARY KEY ([TeamId]),
        CONSTRAINT [FK_Teams_Leagues_LeagueId] FOREIGN KEY ([LeagueId]) REFERENCES [Leagues] ([LeagueId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508025700_initial'
)
BEGIN
    CREATE INDEX [IX_Teams_LeagueId] ON [Teams] ([LeagueId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508025700_initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260508025700_initial', N'10.0.7');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508094035_add_player'
)
BEGIN
    CREATE TABLE [Player] (
        [PalyerId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Age] int NOT NULL,
        [Country] nvarchar(max) NOT NULL,
        [AssistScore] int NOT NULL,
        [GoalScore] int NOT NULL,
        [TeamId] int NOT NULL,
        CONSTRAINT [PK_Player] PRIMARY KEY ([PalyerId]),
        CONSTRAINT [FK_Player_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([TeamId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508094035_add_player'
)
BEGIN
    CREATE INDEX [IX_Player_TeamId] ON [Player] ([TeamId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508094035_add_player'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260508094035_add_player', N'10.0.7');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508095053_addDbsetPlayers'
)
BEGIN
    ALTER TABLE [Player] DROP CONSTRAINT [FK_Player_Teams_TeamId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508095053_addDbsetPlayers'
)
BEGIN
    ALTER TABLE [Player] DROP CONSTRAINT [PK_Player];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508095053_addDbsetPlayers'
)
BEGIN
    EXEC sp_rename N'[Player]', N'Players', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508095053_addDbsetPlayers'
)
BEGIN
    EXEC sp_rename N'[Players].[IX_Player_TeamId]', N'IX_Players_TeamId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508095053_addDbsetPlayers'
)
BEGIN
    ALTER TABLE [Players] ADD CONSTRAINT [PK_Players] PRIMARY KEY ([PalyerId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508095053_addDbsetPlayers'
)
BEGIN
    ALTER TABLE [Players] ADD CONSTRAINT [FK_Players_Teams_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [Teams] ([TeamId]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260508095053_addDbsetPlayers'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260508095053_addDbsetPlayers', N'10.0.7');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509011958_oneToOne'
)
BEGIN
    CREATE TABLE [Users] (
        [UserId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509011958_oneToOne'
)
BEGIN
    CREATE TABLE [UserDetails] (
        [UserDetailId] int NOT NULL IDENTITY,
        [Age] int NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_UserDetails] PRIMARY KEY ([UserDetailId]),
        CONSTRAINT [FK_UserDetails_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509011958_oneToOne'
)
BEGIN
    CREATE UNIQUE INDEX [IX_UserDetails_UserId] ON [UserDetails] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509011958_oneToOne'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260509011958_oneToOne', N'10.0.7');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509014820_add_school'
)
BEGIN
    ALTER TABLE [Users] ADD [SchoolId] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509014820_add_school'
)
BEGIN
    CREATE TABLE [Schools] (
        [SchoolId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Schools] PRIMARY KEY ([SchoolId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509014820_add_school'
)
BEGIN
    CREATE INDEX [IX_Users_SchoolId] ON [Users] ([SchoolId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509014820_add_school'
)
BEGIN
    ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([SchoolId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509014820_add_school'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260509014820_add_school', N'10.0.7');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509015014_change_column_user'
)
BEGIN
    ALTER TABLE [Users] DROP CONSTRAINT [FK_Users_Schools_SchoolId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509015014_change_column_user'
)
BEGIN
    DROP INDEX [IX_Users_SchoolId] ON [Users];
    DECLARE @var nvarchar(max);
    SELECT @var = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'SchoolId');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT ' + @var + ';');
    EXEC(N'UPDATE [Users] SET [SchoolId] = 0 WHERE [SchoolId] IS NULL');
    ALTER TABLE [Users] ALTER COLUMN [SchoolId] int NOT NULL;
    ALTER TABLE [Users] ADD DEFAULT 0 FOR [SchoolId];
    CREATE INDEX [IX_Users_SchoolId] ON [Users] ([SchoolId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509015014_change_column_user'
)
BEGIN
    ALTER TABLE [Users] ADD CONSTRAINT [FK_Users_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([SchoolId]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509015014_change_column_user'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260509015014_change_column_user', N'10.0.7');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509023018_add_teacherUser'
)
BEGIN
    CREATE TABLE [Teachers] (
        [TeacherId] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Teachers] PRIMARY KEY ([TeacherId])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509023018_add_teacherUser'
)
BEGIN
    CREATE TABLE [TeacherUsers] (
        [TeacherId] int NOT NULL,
        [UserId] int NOT NULL,
        CONSTRAINT [PK_TeacherUsers] PRIMARY KEY ([TeacherId], [UserId]),
        CONSTRAINT [FK_TeacherUsers_Teachers_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [Teachers] ([TeacherId]) ON DELETE CASCADE,
        CONSTRAINT [FK_TeacherUsers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509023018_add_teacherUser'
)
BEGIN
    CREATE INDEX [IX_TeacherUsers_UserId] ON [TeacherUsers] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260509023018_add_teacherUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260509023018_add_teacherUser', N'10.0.7');
END;

COMMIT;
GO