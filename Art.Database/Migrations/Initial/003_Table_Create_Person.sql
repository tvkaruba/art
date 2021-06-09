create table Person (
    Id bigint not null identity(1, 1)
  , PersonRoleId int not null constraint DF_Person_PersonRoleId default (2)
  , Email nvarchar(255) not null
  , [Password] nvarchar(255) not null
  , FirstName nvarchar(255) null
  , SecondName nvarchar(255) null
  , CreatedAtUtc datetime not null constraint DF_PersonCreatedAtUtc default getutcdate()
  , ChangedAtUtc datetime null
  , constraint PK_Person primary key clustered (Id)
  , constraint FK_Person_PersonRole_PersonRoleId foreign key (PersonRoleId) references PersonRole (Id)
  , constraint UQ_Person_Email unique (Email)
)