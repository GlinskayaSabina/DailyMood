create database DAILYMOOD
drop database DAILYMOOD

drop table [User]
drop table Statistic
drop table Roles
drop table UserRoles

create table [User]
(
[id]	int	identity(1,1) primary key not null,
[email] nvarchar(100) unique not null,
[login] nvarchar(50) unique not null,
[password] nvarchar(60) not null
)

create table Statistic
(	
[id]	int foreign key references [User](id) not null,
[mood]  smallint not null,
[date]  date not null

)

create table Roles
(
[id]			int primary key	IDENTITY (1, 1)		not null,
[name]		    nvarchar(40)						not null
)

create table UserRoles
(
[id]			int primary key	IDENTITY (1, 1)				not null,
[user_id]		int foreign key references [User] (id)	not null,
[role_id]		int foreign key references Roles (id)		not null
)

insert into ROLES values 
('User'),
('Administrator')