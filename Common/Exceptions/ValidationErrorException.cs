﻿using FluentValidation.Results;

namespace AJFIlfov.Common.Exceptions;

public class ValidationErrorException : Exception
{
    public readonly object Model;
    public readonly ValidationResult ValidationResult;

    public ValidationErrorException(ValidationResult result, object model)
    {
        ValidationResult = result;
        Model = model;
    }
}