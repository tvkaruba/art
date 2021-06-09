using System;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }

        ICommentRepository CommentRepository { get; }

        IExamParticipantAnswerRepository ExamParticipantAnswerRepository { get; }

        IExamParticipantRepository ExamParticipantRepository { get; }

        IExamRepository ExamRepository { get; }

        IImageRepository ImageRepository { get; }

        INamespaceRepository NamespaceRepository { get; }

        IQuestionCommentLinkRepository QuestionCommentLinkRepository { get; }

        IQuestionHistoryRepository QuestionHistoryRepository { get; }

        IQuestionImageLinkRepository QuestionImageLinkRepository { get; }

        IQuestionQuestionConflictRepository QuestionQuestionConflictRepository { get; }

        IQuestionRepository QuestionRepository { get; }

        IQuestionTagLinkRepository QuestionTagLinkRepository { get; }

        IQuestionTypeRepository QuestionTypeRepository { get; }

        ITagRepository TagRepository { get; }

        ITestCommentLinkRepository TestCommentLinkRepository { get; }

        ITestQuestionLinkRepository TestQuestionLinkRepository { get; }

        ITestRepository TestRepository { get; }

        ITestTagLinkRepository TestTagLinkRepository { get; }

        void Commit();
    }
}