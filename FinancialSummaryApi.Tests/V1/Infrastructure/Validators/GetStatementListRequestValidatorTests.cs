using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Infrastructure.Validators;
using FluentValidation.TestHelper;
using System;
using Xunit;
using System.Text.RegularExpressions;
using FluentAssertions;

namespace FinancialSummaryApi.Tests.V1.Infrastructure.Validators
{
    public class GetStatementListRequestValidatorTests
    {
        private readonly GetStatementListRequestValidator _validator = new GetStatementListRequestValidator();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenInvalidPageSize_ShouldHaveValidationError(int pageSize)
        {
            var request = new GetStatementListRequest()
            {
                PageSize = pageSize,
                PaginationToken = string.Empty,
                StartDate = new DateTime(2021, 10, 10),
                EndDate = new DateTime(2021, 11, 11)
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.PageSize)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(request.PageSize))}' must be greater than or equal to '1'.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("1223333")]
        public void GivenInvalidPageNumber_ShouldHaveValidationError(string pageNumber)
        {
            var request = new GetStatementListRequest()
            {
                PageSize = 10,
                PaginationToken = pageNumber,
                StartDate = new DateTime(2021, 10, 10),
                EndDate = new DateTime(2021, 11, 11)
            };

            var result = _validator.TestValidate(request);

            result.Errors.Should().HaveCount(0);
        }

        [Fact]
        public void GivenMinValueForStartDate_ShouldBeOk()
        {
            var request = new GetStatementListRequest()
            {
                PageSize = 10,
                PaginationToken = string.Empty,
                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2021, 11, 11)
            };

            var result = _validator.TestValidate(request);

            result.IsValid.Should().BeTrue();
            result.ShouldNotHaveValidationErrorFor(x => x.StartDate);
        }

        [Fact]
        public void GivenMinValueForEndDate_ShouldbeOk()
        {
            var request = new GetStatementListRequest()
            {
                PageSize = 10,
                PaginationToken = string.Empty,
                StartDate = new DateTime(2021, 11, 11),
                EndDate = DateTime.MinValue
            };

            var result = _validator.TestValidate(request);

            result.IsValid.Should().BeTrue();
            result.ShouldNotHaveValidationErrorFor(x => x.EndDate);
        }

        public static string InsertSpaceBetweenWords(string input)
        {
            return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }
}
