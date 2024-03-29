﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class MessageHelper
    {
        //Auth
        public const string UserCreated = "User created successfully";

        public const string AccountConfirmed = "Account confirmed. Your password has been changed";
        public const string RecoveryEmailSent = "Recovery e-mail sent";
        public const string PasswordChanged = "Password Changed";

        //Email
        public const string RegistrationSubject = "Registration";
        public const string SMTPAccountAdded = "SMTP Account Added";
        public const string IMAPAccountAdded = "IMAP Account Added";

        public static string RegistrationBody(string url)
        {
            return $"Registration successful. Click confirmation link to complete the process. \n {url}";
        }

        public const string PasswordRecoverySubject = "Password Recovery";

        public static string PasswordRecoveryBody(string url)
        {
            return $"Password recovery link: {url}";
        }

        public const string EmailSent = "Email sent successfully";

        //Candidates
        public const string CandidateCreatedSuccessfully = "Candidate created successfully";

        public const string CandidateUpdatedSuccessfully = "Candidate updated successfully";
        public const string CandidateDeletedSuccessfully = "Candidate deleted successfully";
        public const string InterviewNoteAdded = "Interview note added successfully";
        public const string TechInterviewNoteAdded = "Tech interview note added correctly";
        public const string EmployeesAssigned = "Employees assigned correctly";
        public const string InterviewDateSet = "Interview date set correctly";
        public const string TechInterviewDateSet = "Tech interview date set correctly";

        //Interviews
        public const string InterviewCreatedSuccessfully = "Interview created successfully";

        public const string InterviewEditedSuccessfully = "Interview edited successfully";
        public const string InterviewDeletedSuccessfully = "Interview deleted successfully";

        //Recruitments
        public const string RecruitmentCreatedSuccessfully = "Recruitment created successfully";

        public const string RecruitmentDeletedSuccessfully = "Recruitment deleted successfully";
        public const string RecruitmentEndedSuccessfully = "Recruitment ended successfully";
        public const string RecruitmentEditedSuccessfully = "Recruitment edited successfully";

        //Skill
        public const string SkillAdded = "Skill added successfully";

        public const string SkillUpdated = "Skill updated successfully";
        public const string SkillDeleted = "Skill deleted successfully";

        //User
        public static string UserEditSuccess = "Editing was successful";

        public static string UserDeleteSuccess = "User deleted successfully";

        //Language
        public const string LanguageChangeSuccess = "Language changed successfully";
    }
}