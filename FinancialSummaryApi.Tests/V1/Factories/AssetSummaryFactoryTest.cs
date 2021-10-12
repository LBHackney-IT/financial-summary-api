using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Factories
{
    public class AssetSummaryFactoryTest
    {
        [Fact]
        public void CanMapADatabaseEntityToADomainObject()
        {
            var databaseEntity = new AssetSummaryDbEntity
            {
                Id = new Guid("4983a920-dcac-48e5-90f6-31195a2dcb69"),
                TargetId = new Guid("4e1fe95c-50f0-4d7a-83eb-c7734339aaf0"),
                TargetType = TargetType.Estate,
                SubmitDate = new DateTime(2021, 7, 2),
                TargetName = "Estate 1",
                TotalDwellingRent = 100,
                TotalNonDwellingRent = 50,
                TotalRentalServiceCharge = 76,
                TotalServiceCharges = 120,
                TotalIncome = 123,
                TotalExpenditure = 99
            };

            var entity = databaseEntity.ToDomain();

            databaseEntity.Id.Should().Be(entity.Id);
            databaseEntity.SummaryType = SummaryType.AssetSummary;
            databaseEntity.TargetId.Should().Be(entity.TargetId);
            databaseEntity.TargetType.Should().Be(entity.TargetType);
            databaseEntity.SubmitDate.Should().Be(entity.SubmitDate);
            databaseEntity.TargetName.Should().Be(entity.AssetName);
            databaseEntity.TotalDwellingRent.Should().Be(entity.TotalDwellingRent);
            databaseEntity.TotalNonDwellingRent.Should().Be(entity.TotalNonDwellingRent);
            databaseEntity.TotalRentalServiceCharge.Should().Be(entity.TotalRentalServiceCharge);
            databaseEntity.TotalServiceCharges.Should().Be(entity.TotalServiceCharges);
            databaseEntity.TotalIncome.Should().Be(entity.TotalIncome);
            databaseEntity.TotalExpenditure.Should().Be(entity.TotalExpenditure);
        }

        [Fact]
        public void CanMapADomainEntityToADatabaseObject()
        {
            var entity = new AssetSummary
            {
                Id = new Guid("4a5b61f6-dd03-4803-9dc2-80fa3b18a7ab"),
                TargetId = new Guid("3ffe26ad-be50-4789-b509-9379972f07bb"),
                TargetType = TargetType.Estate,
                SubmitDate = new DateTime(2021, 7, 2),
                AssetName = "Estate 1",
                TotalDwellingRent = 100,
                TotalNonDwellingRent = 70,
                TotalRentalServiceCharge = 40,
                TotalServiceCharges = 150,
                TotalIncome = 111,
                TotalExpenditure = 99
            };

            var databaseEntity = entity.ToDatabase();

            entity.Id.Should().Be(databaseEntity.Id);
            entity.TargetId.Should().Be(databaseEntity.TargetId);
            entity.TargetType.Should().Be(databaseEntity.TargetType);
            entity.SubmitDate.Should().Be(databaseEntity.SubmitDate);
            entity.AssetName.Should().Be(databaseEntity.TargetName);
            entity.TotalDwellingRent.Should().Be(databaseEntity.TotalDwellingRent);
            entity.TotalNonDwellingRent.Should().Be(databaseEntity.TotalNonDwellingRent);
            entity.TotalRentalServiceCharge.Should().Be(databaseEntity.TotalRentalServiceCharge);
            entity.TotalServiceCharges.Should().Be(databaseEntity.TotalServiceCharges);
            entity.TotalIncome.Should().Be(databaseEntity.TotalIncome);
            entity.TotalExpenditure.Should().Be(databaseEntity.TotalExpenditure);
        }
    }
}
