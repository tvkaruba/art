using System.Collections.Generic;
using System.Linq;
using Ects.Persistence.Repositories;
using Ects.Web.Shared.QuestionTypes.Abstractions;

namespace Ects.Web.Checker
{
    public class TestingSystem
    {
        private readonly List<IQuestionType> _questionTypes;

        public TestingSystem(IEnumerable<IQuestionType> questionTypes)
        {
            _questionTypes = questionTypes.ToList();
        }

        public void CheckAnswers(IEnumerable<long> participantAnswerIds)
        {
            var total = .0;
            var totalMax = .0;
            foreach (var examParticipantAnswer in FakeRepository.ExamParticipantAnswer.Where(_ =>
                participantAnswerIds.Contains(_.Id)))
            {
                var question = FakeRepository.Questions[(int) examParticipantAnswer.QuestionId];
                var questionTypeName = FakeRepository.QuestionTypes[(int) question.QuestionTypeId].Type;

                IQuestionType questionType = null;
                foreach (var member in _questionTypes)
                    if (member.GetTypeName() == questionTypeName)
                        questionType = member;

                examParticipantAnswer.Value = questionType!.Test(questionType.GetRights(question.Rights),
                    questionType.GetChoices(examParticipantAnswer.Answer));

                total += examParticipantAnswer.Value;
                totalMax += FakeRepository.TestQuestionLinks.Single(_ => _.QuestionId == question.Id).Value;
            }

            var participant = FakeRepository.ExamParticipants[
                (int) FakeRepository.ExamParticipantAnswer[(int) participantAnswerIds.ElementAt(0)].ExamParticipantId];
            participant.Result = total;
            participant.MaxResult = totalMax;
        }
    }
}