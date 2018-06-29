create database UsersDB on primary
(name = UsersDB,
filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.STEP_SQL_SERVER\MSSQL\DATA\UsersDB.mdf',
size = 2MB,
maxsize = 6MB,
filegrowth = 1MB)
LOG ON
(name = UsersDB_log,
filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.STEP_SQL_SERVER\MSSQL\DATA\UsersDB_log.ldf',
size = 2MB,
maxsize = 6MB,
filegrowth = 1MB)
collate Ukrainian_CI_AS;
go
use UsersDB;
create table Users
(ID_USER int primary key identity(1, 1),
[login] nvarchar(30) not null,
[password] nvarchar(30) not null,
[adress] nvarchar(100) not null,
[phone] nvarchar(15) not null,
[admin] bit);

go  
use UsersDB;
insert into Users values
 ('Kostya', '111', 'Kryvyi Rih', '0930012345', 1);
 
