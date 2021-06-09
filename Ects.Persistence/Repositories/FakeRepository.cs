using System.Collections.Generic;
using Ects.Persistence.Models;

namespace Ects.Persistence.Repositories
{
    public static class FakeRepository
    {
        public static List<Account> Accounts { get; set; } = new();

        public static List<Comment> Comments { get; set; } = new();

        public static List<Exam> Exams { get; set; } = new();

        public static List<ExamParticipant> ExamParticipants { get; set; } = new();

        public static List<ExamParticipantAnswer> ExamParticipantAnswer { get; set; } = new();

        public static List<Image> Images { get; set; } = new();

        public static List<Namespace> Namespaces { get; set; } = new();

        public static List<Question> Questions { get; set; } = new();

        public static List<QuestionCommentLink> QuestionCommentLinks { get; set; } = new();

        public static List<QuestionHistory> QuestionHistorys { get; set; } = new();

        public static List<QuestionImageLink> QuestionImageLinks { get; set; } = new();

        public static List<QuestionQuestionConflict> QuestionQuestionConflicts { get; set; } = new();

        public static List<QuestionTagLink> QuestionTagLinks { get; set; } = new();

        public static List<QuestionType> QuestionTypes { get; set; } = new();

        public static List<Tag> Tags { get; set; } = new();

        public static List<Test> Tests { get; set; } = new();

        public static List<TestCommentLink> TestCommentLinks { get; set; } = new();

        public static List<TestQuestionLink> TestQuestionLinks { get; set; } = new();

        public static List<TestTagLink> TestTagLinks { get; set; } = new();
    }
}