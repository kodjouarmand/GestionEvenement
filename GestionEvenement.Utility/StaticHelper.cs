using System;

namespace GestionEvenement.Utility
{
    public static class StaticHelper
    {
        #region Error Messages

        public const string NameRequiredErrorMessage = "The name is required;";
        public const string NameMaxLengthErrorMessage = "The name is required;";
        public const string DescriptionRequiredErrorMessage = "The description is required;";
        public const string StartDateAndTimeRequiredErrorMessage = "The start date is required;";
        public const string EndDateAndTimeRequiredErrorMessage = "The end date is required;";
        public const string StartEndDateComparisonErrorMessage = "The end date should be greater or equal tahn the start date;";
        public const string StartDateComparisonErrorMessage = "The start date should be greater or equal than today date;";

        #endregion Error Messages
    }
}
