using Data.Entities;
using Moq;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.CandidateTests
{
    public class ChangeStatusTests : BaseCandidateTests
    {
        public static CandidateService candidateService = new CandidateService(
           mapper,
           candidateRepositoryMock.Object,
           loggerMock.Object,
           recruitmentRepositoryMock.Object
           );

        [Fact]
        public void GetNullUserWhenChangingStatusTest()
        {
            // candidateRepositoryMock.Verify(g => g.AddAndSaveChanges(candidate), Times.Once);
            candidateRepositoryMock.Setup(x => x.GetById(1)).Equals(null);
           //candidateRepositoryMock.Verify(x => x.GetById(1), Times.Once);

            candidateService.ChangeStatus(1, "text");

           
            //Candidate? candidate = candidateRepositoryMock.Object.GetById(1);

        }
    }
}
