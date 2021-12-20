using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.Infrastructure;
using FinancialSummaryApi.V1.UseCase;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.UseCase
{
    public class AddStatementListUseCaseTests
    {
        private readonly Mock<IFinanceSummaryGateway> _mockFinanceGateway;
        private readonly AddStatementListUseCase _useCase;
        private readonly IMapper _mapper;
        public AddStatementListUseCaseTests()
        {
            _mockFinanceGateway = new Mock<IFinanceSummaryGateway>();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                    mc.AddProfile(new MappingProfile()));
                _mapper = mappingConfig.CreateMapper();
            }
            _useCase = new AddStatementListUseCase(_mockFinanceGateway.Object, _mapper);
        }

        [Fact]
        public async Task Add_NullModel_ThrowsArgumentNullException()
        {
            List<AddStatementRequest> statementModel = null;
            _mockFinanceGateway.Setup(_ => _.AddRangeAsync(It.IsAny<List<Statement>>()))
                .Returns(Task.CompletedTask);

            try
            {
                await _useCase.ExecuteAsync(statementModel).ConfigureAwait(false);
                Assert.True(false, "ArgumentNullException should be thrown!");
            }
            catch (Exception ex)
            {
                ex.Should().NotBeNull();
                ex.Should().BeOfType<ArgumentNullException>();
                ex.Message.Should().Be("Value cannot be null. (Parameter 'statements')");
            }
        }

        [Fact]
        public async Task Add_ValidModel_CallsGateway()
        {
            List<AddStatementRequest> statementModel = new List<AddStatementRequest>();

            _mockFinanceGateway.Setup(_ => _.AddRangeAsync(It.IsAny<List<Statement>>()))
               .Returns(Task.CompletedTask);

            await _useCase.ExecuteAsync(statementModel).ConfigureAwait(false);

            _mockFinanceGateway.Verify(_ => _.AddRangeAsync(It.IsAny<List<Statement>>()), Times.Once);
        }
    }
}
