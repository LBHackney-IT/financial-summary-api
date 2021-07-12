using FinancialSummaryApi.V1.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using Xunit;

namespace FinancialSummaryApi.Tests.V1.Controllers
{
    public class BaseControllerTests
    {
        private readonly BaseController _sut;
        private readonly ControllerContext _controllerContext;
        private readonly HttpContext _stubHttpContext;

        public BaseControllerTests()
        {
            _stubHttpContext = new DefaultHttpContext();
            _controllerContext = new ControllerContext(new ActionContext(_stubHttpContext, new RouteData(), new ControllerActionDescriptor()));
            _sut = new BaseController
            {
                ControllerContext = _controllerContext
            };
        }

        [Fact]
        public void GetCorrelationShouldThrowExceptionIfCorrelationHeaderUnavailable()
        {
            // Arrange + Act + Assert
            _sut.Invoking(x => x.GetCorrelationId())
                .Should().Throw<KeyNotFoundException>()
                .WithMessage("Request is missing a correlationId");
        }

        [Fact]
        public void GetCorrelationShouldReturnCorrelationIdWhenExists()
        {
            // Arrange
            _stubHttpContext.Request.Headers.Add(Constants.CorrelationId, "123");

            // Act
            var result = _sut.GetCorrelationId();

            // Assert
            result.Should().BeEquivalentTo("123");
        }
    }
}
