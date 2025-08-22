using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class NotEqualAttribute : ValidationAttribute
{
    private readonly string _otherProperty;

    public NotEqualAttribute(string otherProperty)
    {
        _otherProperty = otherProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherValue = validationContext.ObjectType
            .GetProperty(_otherProperty)
            .GetValue(validationContext.ObjectInstance, null);

        if (value != null && value.Equals(otherValue))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}
