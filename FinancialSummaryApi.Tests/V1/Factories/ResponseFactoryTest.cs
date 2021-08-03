using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Factories
{
    public class ResponseFactoryTest
    {
        [Fact]
        public void CanMapAnAssetSummaryDomainObjectToResponse()
        {
            var domain = new AssetSummary()
            {
                Id = new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"),
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Estate,
                SubmitDate = new DateTime(2021, 7, 1),
                TotalDwellingRent = 178,
                TotalNonDwellingRent = 67,
                TotalRentalServiceCharge = 120,
                TotalServiceCharges = 213,
                TotalIncome = 111,
                TotalExpenditure = 123,
                AssetName = "Estate 1"
            };

            var response = domain.ToResponse();

            response.Id.Should().Be(new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"));
            response.TargetId.Should().Be(new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"));
            response.TargetType.Should().Be(TargetType.Estate);
            response.SubmitDate.Should().Be(new DateTime(2021, 7, 1));
            response.TotalDwellingRent.Should().Be(178);
            response.TotalNonDwellingRent.Should().Be(67);
            response.TotalRentalServiceCharge.Should().Be(120);
            response.TotalServiceCharges.Should().Be(213);
            response.TotalIncome.Should().Be(111);
            response.TotalExpenditure.Should().Be(123);
            response.AssetName.Should().Be("Estate 1");
        }

        [Fact]
        public void CanMapARentGroupSummaryDomainObjectToResponse()
        {
            var domain = new RentGroupSummary()
            {
                Id = new Guid("98d1a25e-4a88-4117-b789-c8b7a1e8e758"),
                TargetType = TargetType.RentGroup,
                SubmitDate = new DateTime(2021, 7, 1),
                ArrearsYTD = 150,
                ChargedYTD = 113,
                PaidYTD = 263,
                TargetDescription = "Description",
                TotalBalance = 100,
                TotalCharged = 263,
                TotalPaid = 363,
                RentGroupName = "RentGroupName"
            };

            var response = domain.ToResponse();

            response.Id.Should().Be(new Guid("98d1a25e-4a88-4117-b789-c8b7a1e8e758"));
            response.TargetType.Should().Be(TargetType.RentGroup);
            response.SubmitDate.Should().Be(new DateTime(2021, 7, 1));
            response.ArrearsYTD.Should().Be(150);
            response.ChargedYTD.Should().Be(113);
            response.PaidYTD.Should().Be(263);
            response.TargetDescription.Should().Be("Description");
            response.TotalBalance.Should().Be(100);
            response.TotalCharged.Should().Be(263);
            response.TotalPaid.Should().Be(363);
            response.RentGroupName.Should().Be("RentGroupName");
        }

        [Fact]
        public void CanMapListOfAssetSummaryDomainObjectsToResponse()
        {
            var firstDomain = new AssetSummary()
            {
                Id = new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"),
                TargetId = new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"),
                TargetType = TargetType.Estate,
                SubmitDate = new DateTime(2021, 7, 1),
                TotalDwellingRent = 178,
                TotalNonDwellingRent = 67,
                TotalRentalServiceCharge = 120,
                TotalServiceCharges = 213,
                TotalIncome = 123,
                TotalExpenditure = 111,
                AssetName = "Estate 1"
            };

            var secondDomain = new AssetSummary()
            {
                Id = new Guid("ac17ff09-4a3d-4202-ae50-45b6c12110df"),
                TargetId = new Guid("83b3d3db-0afc-4c4c-b180-0a9ceaad7918"),
                TargetType = TargetType.Estate,
                SubmitDate = new DateTime(2021, 7, 2),
                TotalDwellingRent = 156,
                TotalNonDwellingRent = 53,
                TotalRentalServiceCharge = 178,
                TotalServiceCharges = 196,
                TotalIncome = 124,
                TotalExpenditure = 122,
                AssetName = "Estate 2"
            };

            var listOfDomains = new List<AssetSummary>()
            {
                firstDomain,
                secondDomain
            };

            var response = listOfDomains.ToResponse();

            response[0].Id.Should().Be(new Guid("9b014c26-88be-466e-a589-0f402c6b94c1"));
            response[0].TargetId.Should().Be(new Guid("58daf21a-e2d5-475f-87f4-1c0c7f1ffb10"));
            response[0].TargetType.Should().Be(TargetType.Estate);
            response[0].SubmitDate.Should().Be(new DateTime(2021, 7, 1));
            response[0].TotalDwellingRent.Should().Be(178);
            response[0].TotalNonDwellingRent.Should().Be(67);
            response[0].TotalRentalServiceCharge.Should().Be(120);
            response[0].TotalServiceCharges.Should().Be(213);
            response[0].TotalIncome.Should().Be(123);
            response[0].TotalExpenditure.Should().Be(111);
            response[0].AssetName.Should().Be("Estate 1");

            response[1].Id.Should().Be(new Guid("ac17ff09-4a3d-4202-ae50-45b6c12110df"));
            response[1].TargetId.Should().Be(new Guid("83b3d3db-0afc-4c4c-b180-0a9ceaad7918"));
            response[1].TargetType.Should().Be(TargetType.Estate);
            response[1].SubmitDate.Should().Be(new DateTime(2021, 7, 2));
            response[1].TotalDwellingRent.Should().Be(156);
            response[1].TotalNonDwellingRent.Should().Be(53);
            response[1].TotalRentalServiceCharge.Should().Be(178);
            response[1].TotalServiceCharges.Should().Be(196);
            response[1].TotalIncome.Should().Be(124);
            response[1].TotalExpenditure.Should().Be(122);
            response[1].AssetName.Should().Be("Estate 2");
        }

        [Fact]
        public void CanMapListOfRentGroupSummaryDomainObjectsToResponse()
        {
            var firstDomain = new RentGroupSummary()
            {
                Id = new Guid("98d1a25e-4a88-4117-b789-c8b7a1e8e758"),
                TargetType = TargetType.RentGroup,
                SubmitDate = new DateTime(2021, 7, 1),
                ArrearsYTD = 150,
                ChargedYTD = 113,
                PaidYTD = 263,
                TargetDescription = "Description1",
                TotalBalance = 100,
                TotalCharged = 263,
                TotalPaid = 363,
                RentGroupName = "RentGroupName1"
            };

            var secondDomain = new RentGroupSummary()
            {
                Id = new Guid("814ec575-ed34-4f06-a7cc-3cddb985bb00"),
                TargetType = TargetType.RentGroup,
                SubmitDate = new DateTime(2021, 7, 2),
                ArrearsYTD = 145,
                ChargedYTD = 100,
                PaidYTD = 245,
                TargetDescription = "Description2",
                TotalBalance = 255,
                TotalCharged = 245,
                TotalPaid = 500,
                RentGroupName = "RentGroupName2"
            };

            var listOfDomains = new List<RentGroupSummary>()
            {
                firstDomain,
                secondDomain
            };

            var response = listOfDomains.ToResponse();

            response[0].Id.Should().Be(new Guid("98d1a25e-4a88-4117-b789-c8b7a1e8e758"));
            response[0].TargetType.Should().Be(TargetType.RentGroup);
            response[0].SubmitDate.Should().Be(new DateTime(2021, 7, 1));
            response[0].ArrearsYTD.Should().Be(150);
            response[0].ChargedYTD.Should().Be(113);
            response[0].PaidYTD.Should().Be(263);
            response[0].TargetDescription.Should().Be("Description1");
            response[0].TotalBalance.Should().Be(100);
            response[0].TotalCharged.Should().Be(263);
            response[0].TotalPaid.Should().Be(363);
            response[0].RentGroupName.Should().Be("RentGroupName1");

            response[1].Id.Should().Be(new Guid("814ec575-ed34-4f06-a7cc-3cddb985bb00"));
            response[1].TargetType.Should().Be(TargetType.RentGroup);
            response[1].SubmitDate.Should().Be(new DateTime(2021, 7, 2));
            response[1].ArrearsYTD.Should().Be(145);
            response[1].ChargedYTD.Should().Be(100);
            response[1].PaidYTD.Should().Be(245);
            response[1].TargetDescription.Should().Be("Description2");
            response[1].TotalBalance.Should().Be(255);
            response[1].TotalCharged.Should().Be(245);
            response[1].TotalPaid.Should().Be(500);
            response[1].RentGroupName.Should().Be("RentGroupName2");
        }
    }
}
