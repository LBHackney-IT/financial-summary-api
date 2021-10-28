using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Infrastructure.Validators;
using FluentValidation.TestHelper;
using System;
using Xunit;
using System.Text.RegularExpressions;

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
                PageNumber = 1,
                StartDate = new DateTime(2021, 10, 10),
                EndDate = new DateTime(2021, 11, 11)
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.PageSize)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(request.PageSize))}' must be greater than or equal to '1'.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenInvalidPageNumber_ShouldHaveValidationError(int pageNumber)
        {
            var request = new GetStatementListRequest()
            {
                PageSize = 10,
                PageNumber = pageNumber,
                StartDate = new DateTime(2021, 10, 10),
                EndDate = new DateTime(2021, 11, 11)
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.PageNumber)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(request.PageNumber))}' must be greater than or equal to '1'.");
        }

        [Fact]
        public void GivenMinValueForStartDate_ShouldHaveValidationError()
        {
            var request = new GetStatementListRequest()
            {
                PageSize = 10,
                PageNumber = 1,
                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2021, 11, 11)
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.StartDate)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(request.StartDate))}' must not be empty.");
        }

        [Fact]
        public void GivenMinValueForEndDate_ShouldHaveValidationError()
        {
            var request = new GetStatementListRequest()
            {
                PageSize = 10,
                PageNumber = 1,
                StartDate = new DateTime(2021, 11, 11),
                EndDate = DateTime.MinValue
            };

            var result = _validator.TestValidate(request);

            result.ShouldHaveValidationErrorFor(x => x.EndDate)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(request.EndDate))}' must not be empty.");
        }

        public static string InsertSpaceBetweenWords(string input)
        {
            return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }
}
