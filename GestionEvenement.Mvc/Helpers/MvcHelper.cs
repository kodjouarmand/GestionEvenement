using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace GestionEvenement.Mvc.Helpers
{
    public class MvcHelper
    {
        public static string GetErrorMessages(ModelStateDictionary modelState)
        {
            return string.Join("\n", modelState.Values
                                            .SelectMany(v => v.Errors)
                                            .Select(e => $"- {e.ErrorMessage}"));
        }
    }
}
