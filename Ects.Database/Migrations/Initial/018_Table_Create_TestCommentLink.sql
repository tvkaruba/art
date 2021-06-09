create table TestCommentLink
(
    Id bigint not null
        identity(1, 1)
  , TestId bigint not null
  , CommentId bigint not null
  , constraint PK_TestCommentLink primary key clustered (Id)
  , constraint FK_TestCommentLink_Test_TestId foreign key (TestId) references Test (Id)
  , constraint FK_TestCommentLink_Comment_CommentId foreign key (CommentId) references Comment (Id)
)
