using System;
using FinancialSummaryApi.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace FinancialSummaryApi.Tests.V1.Domain
{
    [TestFixture]
    public class AssetSummaryTests
    {
        [Test]
        public void EntitiesHaveAnId()
        {
            var entity = new AssetSummary();
            entity.Id.Should().Be(Guid.Empty);
        }
    }
}
