using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Validators;
using FluentValidation.TestHelper;
using System;
using System.Linq;
using Xunit;
using System.Text.RegularExpressions;

namespace FinancialSummaryApi.Tests.V1.Infrastructure.Validators
{
    public class AddStatementRequestValidatorTests
    {
        private readonly AddStatementRequestValidator _validator = new AddStatementRequestValidator();

        [Fact]
        public void GivenAValidModel_ShouldNotHaveValidationErrors()
        {
            var statement = new AddStatementRequest()
            {
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Block,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = "123456789",
                Address = "12 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            var result = _validator.TestValidate(statement);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void GivenAnInvalidTargetId_ShouldHaveValidationError()
        {
            var statement = new AddStatementRequest()
            {
                TargetId = Guid.Empty,
                TargetType = TargetType.Block,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = "123456789",
                Address = "12 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            var result = _validator.TestValidate(statement);

            result.ShouldHaveValidationErrorFor(x => x.TargetId)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(statement.TargetId))}' must not be empty.");
        }

        [Fact]
        public void GivenNoTargetId_ShouldHaveValidationError()
        {
            var statement = new AddStatementRequest()
            {
                TargetType = TargetType.Block,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = "123456789",
                Address = "12 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            var result = _validator.TestValidate(statement);

            result.ShouldHaveValidationErrorFor(x => x.TargetId)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(statement.TargetId))}' must not be empty.");
        }

        [Fact]
        public void GivenOutOfRangeTargetType_ShouldHaveValidationError()
        {
            var invalidTargetType = Enum.GetValues(typeof(TargetType)).Cast<TargetType>().Last() + 1;
            var statement = new AddStatementRequest()
            {
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = invalidTargetType,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = "123456789",
                Address = "12 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            var result = _validator.TestValidate(statement);

            result.ShouldHaveValidationErrorFor(x => x.TargetType)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(statement.TargetType))}' has a range of values which does not include '{invalidTargetType}'.");
        }

        [Fact]
        public void GivenMinValueForStatementPeriodEndDate_ShouldHaveValidationError()
        {
            var statement = new AddStatementRequest()
            {
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Block,
                StatementPeriodEndDate = DateTime.MinValue,
                RentAccountNumber = "123456789",
                Address = "12 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            var result = _validator.TestValidate(statement);

            result.ShouldHaveValidationErrorFor(x => x.StatementPeriodEndDate)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(statement.StatementPeriodEndDate))}' must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenInvalidRentAccountNumber_ShouldHaveValidationError(string rentAccounNumber)
        {
            var statement = new AddStatementRequest()
            {
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Block,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = rentAccounNumber,
                Address = "12 Macron Court, E8 1ND",
                StatementType = StatementType.Tenant,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            var result = _validator.TestValidate(statement);

            result.ShouldHaveValidationErrorFor(x => x.RentAccountNumber)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(nameof(statement.RentAccountNumber))}' must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenInvalidAddress_ShouldHaveValidationError(string address)
        {
            var statement = new AddStatementRequest()
            {
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Block,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = "123456789",
                Address = address,
                StatementType = StatementType.Tenant,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            var result = _validator.TestValidate(statement);

            result.ShouldHaveValidationErrorFor(x => x.Address)
                  .WithErrorMessage($"'{nameof(statement.Address)}' must not be empty.");
        }

        [Fact]
        public void GivenStatementTypeOutOfRange_ShouldHaveValidationError()
        {
            var invalidStatementType = Enum.GetValues(typeof(StatementType)).Cast<StatementType>().Last() + 1;
            var statement = new AddStatementRequest()
            {
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Block,
                StatementPeriodEndDate = new DateTime(2021, 7, 1),
                RentAccountNumber = "123456789",
                Address = "12 Macron Court, E8 1ND",
                StatementType = invalidStatementType,
                ChargedAmount = 200,
                PaidAmount = 500,
                HousingBenefitAmount = 300,
                StartBalance = 1000,
                FinishBalance = 400
            };

            var result = _validator.TestValidate(statement);

            result.ShouldHaveValidationErrorFor(x => x.StatementType)
                  .WithErrorMessage($"'{InsertSpaceBetweenWords(typeof(StatementType).Name)}' has a range of values which does not include '{invalidStatementType}'.");
        }

        public static string InsertSpaceBetweenWords(string input)
        {
            return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }
    }
}
