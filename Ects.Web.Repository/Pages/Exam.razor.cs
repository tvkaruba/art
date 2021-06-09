using System.Collections.Generic;
using Ects.Persistence.Models;
using Microsoft.AspNetCore.Components;

namespace Ects.Web.Repository.Pages
{
    public class ExamBase : ComponentBase
    {
        public static List<ExamParticipantAnswer> StudentAnswers { get; set; } = new();
    }
}