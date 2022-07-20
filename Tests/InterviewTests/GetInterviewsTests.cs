using Common.Listing;
using Data.DTOs.Interview;
using Data.Entities;
using PagedList;
using Services.Listing;

namespace Tests.InterviewTests
{
    public class GetInterviewsTests : BaseTest
    {
        [Fact]
        public void GetInterviews_ShoudWork()
        {
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
                FromDate = new DateTime(2000, 1, 1),
                ToDate = new DateTime(2030, 1, 1)
            };

            //Arrange
            int ExpectedCount = 2;

            // Act
            IQueryable<Interview> interviews = Interviews.AsQueryable();
            InterviewRepository.Setup(i => i.GetAll()).Returns(interviews);
            InterviewListing interviewListingActual = InterviewService.GetInterviews(paging, sortOrder, interviewFiltringDTO);

            //Assert
            Assert.True(ExpectedCount == interviewListingActual.InterviewDTOs.Count());
        }
    }
}