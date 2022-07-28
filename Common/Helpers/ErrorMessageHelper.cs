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
        public const string UserExists = "User already exists";

        public const string ConfirmationFailed = "Confirmation failed";
        public const string UserAndGuidDifferentOwner = "User and Guid don't have the same owner";
        public const string WrongCredentials = "Wrong credentials!";
        public const string AccountDoesntExist = "Account doesn't exist";

        //Candidate
        public const string InvalidCandidateParameters = "Error creating candidate (one or more invalid parameters)";

        public const string InvalidCandidateId = "Error creating candidate (invalid recruitmentId)";
        public const string ErrorSavingToDatabase = "Error saving object to database";
        public const string ErrorAddingHRNoteToCandidate = "Error adding HR note to candidate";
        public const string ErrorAddingTechNoteToCandidate = "Error adding tech note to candidate";
        public const string ErrorGettingCandidate = "Error getting candidate (bad parameters or candidate doesn't exist)";
        public const string ErrorUpdatingCandidate = "Error updating candidate";
        public const string ErrorDeletingCandidate = "Error deleting candidate";
        public const string ErrorAllocatingRecruiters = "Error updating candidate when allocating recruiters";
        public const string ErrorAssigningInterviewDate = "Error assigning interview date";
        public const string ErrorAssigningTechInterviewDate = "Error assigning tech interview date";

        //Interview
        public const string NoInterview = "No Interview with this Id";

        public const string ErrorCreatingInterview = "Error creating interview";
        public const string ErrorEditingInterview = "Error editing interview";
        public const string ErrorDeletingInterview = "Error deleting interview";

        //Recruitment
        public const string NoRecruitment = "There is no recruitment with such Id";

        public const string ErrorDeletingRecruitment = "Error deleting recruitment";
        public const string ErrorEndingRecruitment = "Error ending recruitment";
        public const string ErrorUpdatingRecruitment = "Error updating recruitment";
        public const string ErrorListRecruitment = "Error getting list of recruitments";
        public const string WrongData = "Wrong data!";

        //Skills
        public const string SkillExists = "Skill with that name already exists!";

        public const string NoSkill = "There is no such skill!";
        public const string ErrorDeletingSkill = "Error while deleting skill!";
        public const string SkillIsUsed = "This skill is currently used in one of the recruitments. You can't delete it.";

        //Users
        public const string NotFound = "Not found";

        public const string NoUser = "No user with given Id found";
        public const string ErrorDeletingUser = "Error while deleting the user";
        public const string ErrorUpdatingUser = "Error while updating the user";
        public const string ForbiddenSymbol = "Name and Surname must only contain letters";


        //Email
        public const string EmailFailed = "Email delivery failed";
        public const string MailNotFound = "Mail not found";
        public const string EmailDataBaseEmpty = "Mail database is empty";
        public const string AccountInDatabase = "Account already in Database";

        //Language
        public const string NoLanguage = "No language specified";

    }
}