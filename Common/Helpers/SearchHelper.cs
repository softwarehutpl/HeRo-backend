using Data.DTOs.Report;
using System.Data.Common;

namespace Common.Helpers
{
    public static class SearchHelper
    {
        public static IQueryable<ReportRequestedSkillDTO> GetRequestedSkills(DbDataReader reader)
        {
            List<ReportRequestedSkillDTO> reportRequestedSkillDTOs = new List<ReportRequestedSkillDTO>();

            while (reader.Read())
            {
                ReportRequestedSkillDTO reportRequestedSkill = new ReportRequestedSkillDTO();
                reportRequestedSkill.SkillId = int.Parse(reader["SkillId"].ToString());
                reportRequestedSkill.SkillName = reader["SkillName"].ToString();
                reportRequestedSkill.Quantity = int.Parse(reader["Quantity"].ToString());

                reportRequestedSkillDTOs.Add(reportRequestedSkill);
            }

            return reportRequestedSkillDTOs.AsQueryable();
        }

        public static IQueryable<ReportRecruitmentDTO> GetPopularRecruitments(DbDataReader reader)
        {
            List<ReportRecruitmentDTO> reportRecruitmentDTOs = new List<ReportRecruitmentDTO>();

            while (reader.Read())
            {
                ReportRecruitmentDTO reportRecruitment = new ReportRecruitmentDTO();
                reportRecruitment.RecruitmentId = int.Parse(reader["RecruitmentId"].ToString());
                reportRecruitment.RecruitmentName = reader["RecruitmentName"].ToString();
                reportRecruitment.NumberOfCandidate = int.Parse(reader["NumberOfCandidate"].ToString());

                reportRecruitmentDTOs.Add(reportRecruitment);
            }

            return reportRecruitmentDTOs.AsQueryable();
        }

        public static IQueryable<ReportDailyRecruitmentDTO> CountNewCandidates(DbDataReader reader)
        {
            List<ReportDailyRecruitmentDTO> reportDailyRecruitmentDTOs = new List<ReportDailyRecruitmentDTO>();

            while (reader.Read())
            {
                ReportDailyRecruitmentDTO reportDailyRecruitment = new ReportDailyRecruitmentDTO();
                reportDailyRecruitment.Date = DateTime.Parse(reader["Date"].ToString());
                reportDailyRecruitment.RecruitmentId = int.Parse(reader["RecruitmentId"].ToString());
                reportDailyRecruitment.RecruitmentName = reader["RecruitmentName"].ToString();
                reportDailyRecruitment.NumberOfCandidate = int.Parse(reader["NumberOfCandidate"].ToString());

                reportDailyRecruitmentDTOs.Add(reportDailyRecruitment);
            }

            return reportDailyRecruitmentDTOs.AsQueryable();
        }
    }
}