using Common.Listing;
using Data.DTOs.Recruitment;
using Data.Entities;
using Services.Listing;

namespace Tests.RecruitmentTests
{
    public class GetRecruitmentsTests : BaseRecruitmentTests
    {
        [Theory]
        [InlineData(null, null, null, null, null, null, true, 4)]
        [InlineData(null, null, null, null, null, null, false, 3)]
        [InlineData("Pra", null, null, null, null, null, true, 3)]
        [InlineData("Pra", null, null, true, null, null, true, 1)]
        [InlineData("Pra", null, true, null, null, null, true, 2)]
        [InlineData("Pra", "Front", null, null, null, null, true, 2)]
        [InlineData("Pra", "Front", null, null, null, null, true, 1)]
        public void GetRecruitments_ShoudWork(string? name, string? description, bool showOpen,
            bool showClosed, DateTime? beginningDate, DateTime? endingDate, bool showPrivate, int expectedCount)
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

            RecruitmentFiltringDTO recruitmentFiltringDTO = new RecruitmentFiltringDTO
            (
                name, description, beginningDate, endingDate, showPrivate, showOpen, showClosed
            );

            List<Recruitment> tempRecruitments = new List<Recruitment>
            {
                new Recruitment
                {
                    BeginningDate = new DateTime(2022,07,15),
                    EndingDate = new DateTime(2022,08,15),
                    Name = "Praktyki .NET",
                    Description = "Backend",
                    IsPublic = true,
                    Candidates = new List<Candidate>
                    {
                        new Candidate(),
                        new Candidate(),
                    }
                },
                new Recruitment
                {
                    BeginningDate = new DateTime(2022,06,15),
                    EndingDate = new DateTime(2022,07,15),
                    Name = "JavaScript Developer",
                    Description = "Frontend",
                    IsPublic = true,
                    Candidates = new List<Candidate>
                    {
                        new Candidate(),
                        new Candidate(),
                    }
                },
                new Recruitment
                {
                    BeginningDate = new DateTime(2022, 7, 20),
                    EndingDate = new DateTime(2022, 8, 25),
                    Name = "Praktyki JavaScript",
                    Description = "Frontend",
                    IsPublic = false,
                    Candidates = new List<Candidate>
                    {
                        new Candidate(),
                        new Candidate(),
                    }
                },
                new Recruitment
                {
                    BeginningDate = new DateTime(2022,06,10),
                    EndingDate = new DateTime(2022,07,15),
                    Name = "Praktyki JavaScript",
                    Description = "Frontend",
                    IsPublic = true,
                    Candidates = new List<Candidate>
                    {
                        new Candidate(),
                        new Candidate(),
                    }
                },
            };

            // Act
            IQueryable<Recruitment> recruitments = tempRecruitments.AsQueryable();
            _recruitmentRepo.Setup(i => i.GetAll()).Returns(recruitments);
            RecruitmentListing recruitmentListing = _recruitmentService.GetRecruitments(paging, sortOrder, recruitmentFiltringDTO);

            //Assert
            Assert.Equal(expectedCount, recruitmentListing.TotalCount);
        }
    }
}