﻿using AutoMapper;
using Data.DTOs.Candidate;
using HeRoBackEnd.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Services.Services;

namespace Tests.CandidateTests
{
    public class GetCandidateTests
    {
        private Mock<CandidateService> _candidateService = new Mock<CandidateService>();
        private Mock<IMapper> _mapper = new Mock<IMapper>();
        private Mock<ILogger<CandidateController>> _logger = new Mock<ILogger<CandidateController>>();

        [Fact]
        public void CallsGetCandidateProfileByIdFromCandidateServiceOnce()
        {
            CandidateController con = new CandidateController(
                _candidateService.Object,
                _logger.Object,
                _mapper.Object
                );

            var dto = new CandidateProfileDTO(1, "fullName", "email", "phone", DateTime.Now, 1, "", "");
            con.Get(dto.Id);

            _candidateService.Verify(g => g.GetCandidateProfileById(dto.Id), Times.Once);
        }
    }
}