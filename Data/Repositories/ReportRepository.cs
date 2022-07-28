using Common.Helpers;
using Common.ServiceRegistrationAttributes;
using Data.DTOs.Report;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace Data.Repositories
{
    [ScopedRegistration]
    public class ReportRepository
    {
        public IConfiguration Configuration { get; }

        private readonly SqlConnection dbConnection;

        public ReportRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            string myCompanyDBcs = Configuration.GetConnectionString("DefaultConnection");
            dbConnection = new SqlConnection(myCompanyDBcs);
        }

        public IQueryable<ReportRequestedSkillDTO> GetRequestedSkills()
        {
            DbCommand command = new SqlCommand("sp_RequestedSkills", dbConnection);
            command.CommandType = CommandType.StoredProcedure;

            DateTime now = DateTime.UtcNow;
            SqlParameter nowTime_SqlParam = new SqlParameter("@now", SqlDbType.DateTime2);
            nowTime_SqlParam.Value = now;
            command.Parameters.Add(nowTime_SqlParam);

            try
            {
                command.Connection.Open();

                DbDataReader? reader = command.ExecuteReader();

                return SearchHelper.GetRequestedSkills(reader);
            }
            finally
            {
                command.Connection.Close();
            }
        }

        public IQueryable<ReportDailyRecruitmentDTO> CountNewCandidates(ReportCountDTO reportDTO)
        {
            DbCommand command = new SqlCommand("sp_CountNewCandidates", dbConnection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter ids_SqlParamz = new SqlParameter("@recruitmentsId", SqlDbType.VarChar);
            string tempIds = String.Join(",", reportDTO.Ids.ToArray());
            ids_SqlParamz.Value = tempIds;
            command.Parameters.Add(ids_SqlParamz);

            SqlParameter nowTime_SqlParam = new SqlParameter("@now", SqlDbType.DateTime2);
            DateTime now = DateTime.UtcNow;
            nowTime_SqlParam.Value = now;
            command.Parameters.Add(nowTime_SqlParam);

            SqlParameter fromDate_SqlParam = new SqlParameter("@fromDate", SqlDbType.DateTime2);
            fromDate_SqlParam.Value = reportDTO.FromDate;
            command.Parameters.Add(fromDate_SqlParam);

            SqlParameter toDate_SqlParam = new SqlParameter("@toDate", SqlDbType.DateTime2);
            toDate_SqlParam.Value = reportDTO.ToDate;
            command.Parameters.Add(toDate_SqlParam);

            try
            {
                command.Connection.Open();

                DbDataReader? reader = command.ExecuteReader();

                return SearchHelper.CountNewCandidates(reader);
            }
            finally
            {
                command.Connection.Close();
            }
            throw new NotImplementedException();
        }

        public IQueryable<ReportRecruitmentDTO> GetPopularRecruitments()
        {
            DbCommand command = new SqlCommand("sp_GetPopularRecruitments", dbConnection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter nowTime_SqlParam = new SqlParameter("@now", SqlDbType.DateTime2);
            DateTime now = DateTime.UtcNow;
            nowTime_SqlParam.Value = now;
            command.Parameters.Add(nowTime_SqlParam);

            try
            {
                command.Connection.Open();

                DbDataReader? reader = command.ExecuteReader();

                return SearchHelper.GetPopularRecruitments(reader);
            }
            finally
            {
                command.Connection.Close();
            }
        }
    }
}