using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ADASOIdentityServer.AuthServer.Models
{
    public class MustBeTrueAttribute : ValidationAttribute, IClientModelValidator
    {
        public MustBeTrueAttribute()
        {
            ErrorMessage = "Kullanıcı sözleşmesini kabul etmelisiniz.";
        }

        public override bool IsValid(object value)
        {
            return value is bool b && b;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-mustbetrue", ErrorMessage);
        }
    }


}
