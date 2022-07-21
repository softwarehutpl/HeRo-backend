using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class ErrorMessageHelper
    {
        //Auth
        public const string UserExists                      = "User already exists";
        public const string ConfirmationFailed              = "Confirmation failed";
        public const string UserAndGuidDifferentOwner       = "User and Guid don't have the same owner";
        public static string AccountDoesntExist(string email)
        {
            return $"Account:{email} doesn't exist";
        }

        //Candidate
        public const string InvalidCandidateParameters      = "Error creating candidate (one or more invalid parameters)";
        public const string InvalidCandidateId              = "Error creating candidate (invalid recruitmentId)";
        public const string ErrorSavingToDatabase           = "Error saving object to database";
        public const string ErrorAddingHRNoteToCandidate    = "Error adding HR note to candidate";
        public const string ErrorAddingTechNoteToCandidate  = "Error adding tech note to candidate";
        public const string ErrorGettingCandidate           = "Error getting candidate (bad parameters or candidate doesn't exist)";
        public const string ErrorUpdatingCandidate          = "Error updating candidate";
        public const string ErrorDeletingCandidate          = "Error deleting candidate";
        public const string ErrorAllocatingRecruiters       = "Error updating candidate when allocating recruiters";
        public const string ErrorAssigningInterviewDate     = "Error assigning interview date";
        public const string ErrorAssigningTechInterviewDate = "Error assigning tech interview date";
    }
}
