create table QuestionImageLink
(
    Id int not null
        identity(1, 1)
  , QuestionId bigint not null
  , ImageId bigint not null
  , constraint PK_QuestionImageLink primary key clustered (Id)
  , constraint FK_QuestionImageLink_Question_QuestionId foreign key (QuestionId) references Question (Id)
  , constraint FK_QuestionImageLink_Image_ImageId foreign key (ImageId) references [Image] (Id)
)
