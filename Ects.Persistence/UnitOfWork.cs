using System;
using System.Data;
using System.Data.SqlClient;
using Ects.Persistence.Abstractions;
using Ects.Persistence.Repositories;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence
{
    public class UnitOfWork
        : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private bool _disposed;

        private IAccountRepository _accountRepository;
        private ICommentRepository _commentRepository;
        private IExamParticipantAnswerRepository _examParticipantAnswerRepository;
        private IExamParticipantRepository _examParticipantRepository;
        private IExamRepository _examRepository;
        private IImageRepository _imageRepository;
        private INamespaceRepository _namespaceRepository;
        private IQuestionCommentLinkRepository _questionCommentLinkRepository;
        private IQuestionHistoryRepository _questionHistoryRepository;
        private IQuestionImageLinkRepository _questionImageLinkRepository;
        private IQuestionQuestionConflictRepository _questionQuestionConflictRepository;
        private IQuestionRepository _questionRepository;
        private IQuestionTagLinkRepository _questionTagLinkRepository;
        private IQuestionTypeRepository _questionTypeRepository;
        private ITagRepository _tagRepository;
        private ITestCommentLinkRepository _testCommentLinkRepository;
        private ITestQuestionLinkRepository _testQuestionLinkRepository;
        private ITestRepository _testRepository;
        private ITestTagLinkRepository _testTagLinkRepository;

        public IAccountRepository AccountRepository =>
            _accountRepository ??= new AccountRepository(_transaction);

        public ICommentRepository CommentRepository =>
            _commentRepository ??= new CommentRepository(_transaction);

        public IExamParticipantAnswerRepository ExamParticipantAnswerRepository =>
            _examParticipantAnswerRepository ??= new ExamParticipantAnswerRepository(_transaction);

        public IExamParticipantRepository ExamParticipantRepository =>
            _examParticipantRepository ??= new ExamParticipantRepository(_transaction);

        public IExamRepository ExamRepository =>
            _examRepository ??= new ExamRepository(_transaction);

        public IImageRepository ImageRepository =>
            _imageRepository ??= new ImageRepository(_transaction);

        public INamespaceRepository NamespaceRepository =>
            _namespaceRepository ??= new NamespaceRepository(_transaction);

        public IQuestionCommentLinkRepository QuestionCommentLinkRepository =>
            _questionCommentLinkRepository ??= new QuestionCommentLinkRepository(_transaction);

        public IQuestionHistoryRepository QuestionHistoryRepository =>
            _questionHistoryRepository ??= new QuestionHistoryRepository(_transaction);

        public IQuestionImageLinkRepository QuestionImageLinkRepository =>
            _questionImageLinkRepository ??= new QuestionImageLinkRepository(_transaction);

        public IQuestionQuestionConflictRepository QuestionQuestionConflictRepository =>
            _questionQuestionConflictRepository ??= new QuestionQuestionConflictRepository(_transaction);

        public IQuestionRepository QuestionRepository =>
            _questionRepository ??= new QuestionRepository(_transaction);

        public IQuestionTagLinkRepository QuestionTagLinkRepository =>
            _questionTagLinkRepository ??= new QuestionTagLinkRepository(_transaction);

        public IQuestionTypeRepository QuestionTypeRepository =>
            _questionTypeRepository ??= new QuestionTypeRepository(_transaction);

        public ITagRepository TagRepository =>
            _tagRepository ??= new TagRepository(_transaction);

        public ITestCommentLinkRepository TestCommentLinkRepository =>
            _testCommentLinkRepository ??= new TestCommentLinkRepository(_transaction);

        public ITestQuestionLinkRepository TestQuestionLinkRepository =>
            _testQuestionLinkRepository ??= new TestQuestionLinkRepository(_transaction);

        public ITestRepository TestRepository =>
            _testRepository ??= new TestRepository(_transaction);

        public ITestTagLinkRepository TestTagLinkRepository =>
            _testTagLinkRepository ??= new TestTagLinkRepository(_transaction);

        public UnitOfWork(IUnitOfWorkConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentException(nameof(configuration));

            _connection = new SqlConnection(configuration.ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _transaction?.Dispose();
            _transaction = null;
            _connection?.Dispose();
            _connection = null;

            _disposed = true;
        }

        public void Commit()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(UnitOfWork));

            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }
        }
    }
}