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
GO

CREATE TABLE [AspNetRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    [Discriminator] nvarchar(34) NOT NULL,
    [Id] uniqueidentifier NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Doctor] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Specialty] int NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [DateCreated] datetime2 NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DateModified] datetime2 NULL,
    [ModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Doctor] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Doctor_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Patient] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [DateOfBirth] date NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [DateCreated] datetime2 NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DateModified] datetime2 NULL,
    [ModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Patient_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Schedule] (
    [Id] uniqueidentifier NOT NULL,
    [DoctorId] uniqueidentifier NOT NULL,
    [DayOfWeek] int NOT NULL,
    [StartTime] time NOT NULL,
    [EndTime] time NOT NULL,
    [DateCreated] datetime2 NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DateModified] datetime2 NULL,
    [ModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Schedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Schedule_Doctor_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Doctor] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Appointment] (
    [Id] uniqueidentifier NOT NULL,
    [DoctorId] uniqueidentifier NOT NULL,
    [PatientId] uniqueidentifier NOT NULL,
    [AppointmentDate] datetime2 NOT NULL,
    [ReasonForVisit] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [DateCreated] datetime2 NULL,
    [CreatedBy] nvarchar(max) NULL,
    [DateModified] datetime2 NULL,
    [ModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Appointment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Appointment_Doctor_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Doctor] ([Id]),
    CONSTRAINT [FK_Appointment_Patient_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patient] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] ON;
INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
VALUES ('0e2d2d97-14d4-49fa-8616-d9711e4bab9d', NULL, N'Admin', N'ADMIN'),
('57838d8b-8b16-4b01-8018-a6b316785d8e', NULL, N'Patient', N'PATIENT'),
('6a41b122-c556-4f2e-8b87-948f115f274c', NULL, N'Doctor', N'DOCTOR');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
    SET IDENTITY_INSERT [AspNetRoles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'FirstName', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] ON;
INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [FirstName], [LastName], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES ('32173892-2e8f-4f7b-a059-8d88eb21482b', 0, N'abe722fa-012b-42ac-955b-87839bca6401', N'admin@localhost.com', CAST(1 AS bit), N'System', N'Admin', CAST(0 AS bit), NULL, N'ADMIN@LOCALHOST.COM', N'ADMIN@LOCALHOST.COM', N'AQAAAAIAAYagAAAAEAmaOMRM1vSIPMxKYIPmzC9RLK80GYwlW2s6ziOfrA6CySvDrqEJmxX5BB0nEJvNfw==', NULL, CAST(0 AS bit), NULL, CAST(0 AS bit), N'admin@localhost.com'),
('a9fce60c-1947-4313-b124-9300d1b4a127', 0, N'5f6a0db2-88a1-49e2-b7d7-41a1dcea3b6a', N'patient@localhost.com', CAST(1 AS bit), N'System', N'patient', CAST(0 AS bit), NULL, N'PATIENT@LOCALHOST.COM', N'PATIENT@LOCALHOST.COM', N'AQAAAAIAAYagAAAAELT8BULH7iBPziS3QsykEBtdcPDIE12tBpvzd7Nd+HTidZAtqDUWHx7PSPXnUyQX7Q==', NULL, CAST(0 AS bit), NULL, CAST(0 AS bit), N'patient@localhost.com'),
('e19eb100-f7b6-4081-b6ab-412958bb700e', 0, N'8f10031a-9d8a-4077-85f3-bd2f80dc9b5b', N'doctor@localhost.com', CAST(1 AS bit), N'System', N'doctor', CAST(0 AS bit), NULL, N'DOCTOR@LOCALHOST.COM', N'DOCTOR@LOCALHOST.COM', N'AQAAAAIAAYagAAAAEMLBqzLKPOJfVzgckuhriaHi0w9bgHVFtwEzqo9SA02kC1D0kkg5rmORKD5Xy5HoFw==', NULL, CAST(0 AS bit), NULL, CAST(0 AS bit), N'doctor@localhost.com');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'FirstName', N'LastName', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
    SET IDENTITY_INSERT [AspNetUsers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId', N'Discriminator', N'Id') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] ON;
INSERT INTO [AspNetUserRoles] ([RoleId], [UserId], [Discriminator], [Id])
VALUES ('0e2d2d97-14d4-49fa-8616-d9711e4bab9d', '32173892-2e8f-4f7b-a059-8d88eb21482b', N'UserRoles', '00000000-0000-0000-0000-000000000000'),
('57838d8b-8b16-4b01-8018-a6b316785d8e', 'a9fce60c-1947-4313-b124-9300d1b4a127', N'UserRoles', '00000000-0000-0000-0000-000000000000'),
('6a41b122-c556-4f2e-8b87-948f115f274c', 'e19eb100-f7b6-4081-b6ab-412958bb700e', N'UserRoles', '00000000-0000-0000-0000-000000000000');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId', N'Discriminator', N'Id') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
    SET IDENTITY_INSERT [AspNetUserRoles] OFF;
GO

CREATE INDEX [IX_Appointment_DoctorId] ON [Appointment] ([DoctorId]);
GO

CREATE INDEX [IX_Appointment_PatientId] ON [Appointment] ([PatientId]);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Doctor_UserId] ON [Doctor] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_Patient_UserId] ON [Patient] ([UserId]);
GO

CREATE INDEX [IX_Schedule_DoctorId] ON [Schedule] ([DoctorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241126220551_Initial', N'8.0.4');
GO

COMMIT;
GO

