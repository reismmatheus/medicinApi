
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Utils
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class CpfAttribute : ValidationAttribute
    {
        public CpfAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            string valor = value.ToString().Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return new ValidationResult(ErrorMessage);

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return new ValidationResult(ErrorMessage);

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return new ValidationResult(ErrorMessage);
            }
            else if (numeros[9] != 11 - resultado)
                return new ValidationResult(ErrorMessage);

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return new ValidationResult(ErrorMessage);
            }
            else if (numeros[10] != 11 - resultado)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
