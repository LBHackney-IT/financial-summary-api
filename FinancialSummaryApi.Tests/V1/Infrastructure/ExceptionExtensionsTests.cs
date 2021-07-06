using FinancialSummaryApi.V1.Infrastructure;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace FinancialSummaryApi.Tests.V1.Infrastructure
{
    public class ExceptionExtensionsTests
    {
        [Test]
        public void GetFullMessage_WithInnerException_ReturnsFullMessage()
        {
            var exception = new Exception("Test exception", new Exception("Inner Exception 1", new Exception("Inner Exception 2")));

            var expectedResult = "Test exception; Inner Exception 1; Inner Exception 2; ";

            var result = exception.GetFullMessage();

            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetFullMessage_WithoutInnerException_ReturnsMessageFromException()
        {
            var exception = new Exception("Test exception");

            var expectedResult = "Test exception; ";

            var result = exception.GetFullMessage();

            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void GetFullMessage_ExceptionNull_ReturnsStringWithSemicolon()
        {
            var exception = (Exception) null;

            var expectedResult = "Exception message is empty";

            var result = exception.GetFullMessage();

            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
