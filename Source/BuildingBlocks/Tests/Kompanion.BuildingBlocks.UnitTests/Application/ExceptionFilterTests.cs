using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using Kompanion.Application.Exceptions;
using Kompanion.Application.Exceptions.Filters;
using Kompanion.Application.Wrappers;
using Kompanion.Domain.Exceptions;
using Kompanion.Domain.Interfaces;

namespace Kompanion.BuildingBlocks.UnitTests.Application;

public class ExceptionFilterTests
{
    private readonly ActionContext _actionContext;

    public ExceptionFilterTests()
    {
        _actionContext = new ActionContext
        {
            HttpContext = new DefaultHttpContext(),
            RouteData = new RouteData(),
            ActionDescriptor = new ActionDescriptor()
        };
    }

    private static ApiResponse ExceptionResultToApiResponse(ExceptionContext exceptionContext, int expectedStatusCode)
    {
        exceptionContext.Result
            .Should().NotBeNull()
            .And.BeOfType<ObjectResult>();

        exceptionContext.ExceptionHandled
            .Should().BeTrue();

        ObjectResult objectResult = exceptionContext.Result as ObjectResult;

        objectResult.StatusCode
            .Should().Be(expectedStatusCode);

        ApiResponse apiResponse = objectResult.Value as ApiResponse;

        apiResponse
            .Should().NotBeNull();

        apiResponse.HttpStatusCode
            .Should().Be(expectedStatusCode);

        apiResponse.Errors
            .Should().NotBeNull()
            .And.HaveCount(1);

        return apiResponse;
    }

    [Fact]
    public void OnException_ValidationException_ReturnBadRequest()
    {
        const int ExpectedStatusCode = StatusCodes.Status400BadRequest;

        ExceptionContext exceptionContext = new(_actionContext, new List<IFilterMetadata>());

        exceptionContext.Exception = new ValidationException("error message", new List<ValidationFailure>
        {
            new ValidationFailure("error-prop-name","error-message")
        });

        new ExceptionFilter().OnException(exceptionContext);

        ExceptionResultToApiResponse(exceptionContext, ExpectedStatusCode);
    }

    [Fact]
    public void OnException_NotFoundException_ReturnNotFound()
    {
        const int ExpectedStatusCode = StatusCodes.Status404NotFound;

        const string ErrorMessage = "not-found-exception-message";

        ExceptionContext exceptionContext = new(_actionContext, new List<IFilterMetadata>());

        exceptionContext.Exception = new NotFoundException(ErrorMessage);

        new ExceptionFilter().OnException(exceptionContext);

        ApiResponse response = ExceptionResultToApiResponse(exceptionContext, ExpectedStatusCode);

        response.Errors
            .Select(x => x.Message)
            .Contains(ErrorMessage);
    }

    [Fact]
    public void OnException_BusinessRuleException_ReturnBadRequest()
    {
        const int ExpectedStatusCode = StatusCodes.Status400BadRequest;

        const string ErrorMessage = "business-rule-message";

        Mock<IBusinessRule> mockBusinessRule = new();

        mockBusinessRule.Setup(x => x.IsBroken(It.IsAny<CancellationToken>())).Returns(true);

        mockBusinessRule.Setup(x => x.Message).Returns(ErrorMessage);

        ExceptionContext exceptionContext = new(_actionContext, new List<IFilterMetadata>());

        exceptionContext.Exception = new BusinessRuleValidationException(mockBusinessRule.Object);

        new ExceptionFilter().OnException(exceptionContext);

        ApiResponse response = ExceptionResultToApiResponse(exceptionContext, ExpectedStatusCode);

        response.Errors
            .Select(x => x.Message)
            .Contains(ErrorMessage);
    }

    [Fact]
    public void OnException_Exception_ReturnInternalServerError()
    {
        ExceptionContext exceptionContext = new(_actionContext, new List<IFilterMetadata>());

        exceptionContext.Exception = new Exception();

        new ExceptionFilter().OnException(exceptionContext);

        exceptionContext
            .ExceptionHandled
            .Should().Be(true);

        exceptionContext.Result
            .Should()
            .NotBeNull()
            .And
            .BeOfType<ObjectResult>();

        ObjectResult response = exceptionContext.Result as ObjectResult;

        response
            .StatusCode
            .Should()
            .Be(StatusCodes.Status500InternalServerError);
    }

    [Theory]
    [InlineData(StatusCodes.Status404NotFound)]
    [InlineData(StatusCodes.Status400BadRequest)]
    [InlineData(StatusCodes.Status500InternalServerError)]
    public void OnException_HandledException_ReturnInlineDataStatusCode(int statusCode)
    {
        const string ErrorMessage = "handled-exception-message";

        ExceptionContext exceptionContext = new(_actionContext, new List<IFilterMetadata>());

        exceptionContext.Exception = new HandledException(statusCode, ErrorMessage);

        new ExceptionFilter().OnException(exceptionContext);

        ApiResponse response = ExceptionResultToApiResponse(exceptionContext, statusCode);

        response.Errors
            .Select(x => x.Message)
            .Contains(ErrorMessage);
    }
}

