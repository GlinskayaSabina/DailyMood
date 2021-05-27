
drop table Accounts
drop table [Statistics]
drop table Users
drop table Tests
 
create table Users
(
[Id] int identity(1,1) primary key,
[Login] nvarchar(50) not null,
[HashedPassword] nvarchar(100) not null,
[Role] nvarchar(20) default 'user' 
)

create table [Statistics]
(
[Id] int identity(1,1) primary key,
[History] nvarchar(500) not null,
[Emoji] int not null,
[UserId] int foreign key references Users (Id),
[Date] nvarchar(12) not null
) 

create table Accounts
(
[Id] int identity(1,1) primary key,
[Name] nvarchar(100) not null,
[Years] int not null,
[Telegram] nvarchar(100) not null,
[Role] nvarchar(10) not null,
[UserId] int foreign key references Users (Id)
)

create table Tests
(
[Id] int identity(1,1) primary key,
[Value] nvarchar(200) not null
)