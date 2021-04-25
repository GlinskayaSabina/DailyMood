 create table Users
 (
 [Id] int identity(1,1) primary key,
 [Login] nvarchar(50) not null,
 [HashedPassword] nvarchar(100) not null,
 )