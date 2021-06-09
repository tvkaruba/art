using Ects.Web.Shared.QuestionTypes.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ects.Web.Shared.QuestionTypes
{
    public class SingleType : IQuestionType
    {
        public const string QuestionTypeName = "single";

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
            return typeof(string);
        }

        public Type GetRightsType()
        {
            return typeof(string);
        }

        public object GetAnswers(string answers)
        {
            return answers.Split('\t');
        }

        public string SetAnswers(object answers)
        {
            return string.Join('\t', (IEnumerable<string>)answers);
        }

        public object GetRights(string rights)
        {
            return rights;
        }

        public string SetRights(object rights)
        {
            return (string)rights;
        }

        public object GetChoices(string choices)
        {
            return choices;
        }

        public string SetChoices(object choices)
        {
            return (string)choices;
        }

        public double Test(object rights, object choices)
        {
            return (string)rights == (string)choices ? 1.0 : 0;
        }
    }
}
