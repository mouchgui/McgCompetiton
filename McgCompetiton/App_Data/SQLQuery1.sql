use master
go
create database McgCompetiton
on primary
(
name='McgCompetiton.DATA',
FILENAME='D:\DB\McgCompetiton.MDF',
SIZE=1MB,
FILEGrOWTH=1MB
)
log on
(name='McgCompetiton.LOG',
FILENAME='D:\DB\McgCompetiton.LDF',
SIZE=1MB,
FILEGrOWTH=1MB)
go
use McgCompetiton
GO

create table Users(
UserId int identity(1000,1) primary key not null,
UseName varchar(50),
Pwd varchar(50),
Roles varchar(10),
)
go
insert into Users(UseName,Pwd,Roles) values('管理员','1','管理员')
go
create table Categorys(
CategoryId int identity(1,1) primary key not null,
CategoryName varchar(50)
)
go
create table Competitions
(
CompetitionId int identity(1,1) primary key not null,
Comdate varchar(50),
ComAddress varchar(300),
CompetitionName varchar(300),
Comdateil text,
ComStartus varchar(6),
CategoryId int not null,
UserId int not null
)
go
create table Registrations
(
RegistrationId int identity(1,1) primary key not null,
CompetitionId int not null,
UserId int not null
)
go