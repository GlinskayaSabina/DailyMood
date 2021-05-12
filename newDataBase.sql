drop table Users
drop table [Statistics]
 
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
[Birthday] date not null,
[Phone] nvarchar(20) not null,
[UserId] int foreign key references Users (Id)
)