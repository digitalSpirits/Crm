﻿using Crm.Application.Configuration.Validation;
using Microsoft.AspNetCore.Http;

namespace Crm.Api.SeedWork
{
    public class InvalidCommandProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public InvalidCommandProblemDetails(InvalidCommandException exception)
        {
            Title = exception.Message;
            Status = StatusCodes.Status400BadRequest;
            Detail = exception.Details;
            Type = "https://somedomain/validation-error";
        }
    }
}