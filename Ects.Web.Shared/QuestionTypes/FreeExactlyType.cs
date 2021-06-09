using System;
using Ects.Web.Shared.QuestionTypes.Abstractions;

namespace Ects.Web.Shared.QuestionTypes
{
    public class FreeExactlyType : IQuestionType
    {
        public const string QuestionTypeName = "free-exactly";

        public string GetTypeName()
        {
            return QuestionTypeName;
        }

        public Type GetAnswersType()
        {
            return typeof(string);
        }

        public Type GetChoicesType()
        {
            return typeof(string);
        }

        public Type GetRightsType()
        {
            return typeof(string);
        }

        public object GetAnswers(string answers)
        {
            return answers;
        }

        public string SetAnswers(object answers)
        {
            return (string) answers;
        }

        public object GetRights(string rights)
        {
            return rights;
        }

        public string SetRights(object rights)
        {
            return (string) rights;
        }

        public object GetChoices(string choices)
        {
            return choices;
        }

        public string SetChoices(object choices)
        {
            return (string) choices;
        }

        public double Test(object rights, object choices)
        {
            return (string) rights == (string) choices ? 1.0 : .0;
        }
    }
}