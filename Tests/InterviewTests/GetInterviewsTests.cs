using Common.Listing;
using Data.DTOs.Interview;
using Data.Entities;
using Services.Listing;

namespace Tests.InterviewTests
{
    public class GetInterviewsTests : BaseTest
    {
        [Theory]
        [InlineData(null, null, null, null, null, 5)]
        [InlineData(null, null, null, null, "HR", 4)]
        [InlineData(null, null, 4, null, null, 2)]
        [InlineData(null, null, null, 2, "HR", 2)]
        [InlineData(null, null, 4, 1, "HR", 1)]
        public void GetInterviews_ShoudWork(DateTime fromDate, DateTime? toDate, int? candidateId,
            int? workerId, string? type, int expectedCount)
        {
            //Arrange
            Paging paging = new Paging
            {
                PageNumber = 1,
                PageSize = 10,
            };

            SortOrder? sortOrder = new SortOrder
            {
                Sort = new List<KeyValuePair<string, string>>
                {
                    //new KeyValuePair<string, string>("Id","Desc")
                }
            };

            InterviewFiltringDTO interviewFiltringDTO = new InterviewFiltringDTO
            {
                FromDate = fromDate,
                ToDate = toDate,
                CandidateId = candidateId,
                WorkerId = workerId,
                Type = type
            };

            // Act
            IQueryable<Interview> interviews = Interviews.AsQueryable();
            InterviewRepository.Setup(i => i.GetAll()).Returns(interviews);
            InterviewListing interviewListingActual = InterviewService.GetInterviews(paging, sortOrder, interviewFiltringDTO);

            //Assert
            Assert.Equal(expectedCount, interviewListingActual.TotalCount);
        }
    }
}