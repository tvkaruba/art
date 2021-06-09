create table QuestionCommentLink
(
    Id bigint not null
        identity(1, 1)
  , QuestionId bigint not null
  , CommentId bigint not null
  , constraint PK_QuestionCommentLink primary key clustered (Id)
  , constraint FK_QuestionCommentLink_Question_QuestionId foreign key (QuestionId) references Question (Id)
  , constraint FK_QuestionCommentLink_Comment_CommentId foreign key (CommentId) references Comment (Id)
)
