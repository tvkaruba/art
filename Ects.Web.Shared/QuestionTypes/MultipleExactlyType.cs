using Ects.Web.Shared.QuestionTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ects.Web.Shared.QuestionTypes
{
    public class MultipleExactlyType : IQuestionType
    {
        public const string QuestionTypeName = "multiple-exactly";

        public string GetTypeName()
        {
            return QuestionTypeName;
        }

        public Type GetAnswersType()
        {
            return typeof(IEnumerable<string>);
        }

        public Type GetChoicesType()
        {
            return typeof(IEnumerable<string>);
        }

        public Type GetRightsType()
        {
            return typeof(IEnumerable<string>);
        }

        public object GetAnswers(string answers)
        {
            return answers.Split('\t');
        }

        public string SetAnswers(object answers)
        {
            return string.Join('\t', (IEnumerable<string>)answers);
        }

        public object GetChoices(string choices)
        {
            return choices.Split('\t');
        }

        public string SetChoices(object choices)
        {
            return string.Join('\t', (IEnumerable<string>)choices);
        }

        public object GetRights(string rights)
        {
            return rights.Split('\t');
        }

        public string SetRights(object rights)
        {
            return string.Join('\t', (IEnumerable<string>)rights);
        }

        public double Test(object rights, object choices)
        {
            var rightAnswers = (IEnumerable<string>)rights;
            var studentAnswers = (IEnumerable<string>)choices;

            var isRight = true;
            foreach (var studentAnswer in studentAnswers)
                isRight &= rightAnswers.Contains(studentAnswer);

            return isRight ? 1.0 : 0.0;
        }
    }
}
