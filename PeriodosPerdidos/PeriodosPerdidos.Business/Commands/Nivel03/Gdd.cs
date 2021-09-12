﻿using FluentValidation;
using System;
using System.Text.Json.Serialization;

namespace PeriodosPerdidos.Business.Commands.Nivel03
{
    public class Gdd : MediatR.IRequest<GddResponse>
    {
        public int Id { get; set; } //los datos que pide el comando
        public string FechaCreacion { get; set; }
        public string FechaFin { get; set; }
        public string[] Fechas { get; set; }
    }

    public class GddValidator : AbstractValidator<Gdd> //validamos los datos del comando
    {
        public GddValidator()
        {
            RuleFor(request => request.Id)
                .NotNull().NotEmpty().WithMessage("Id no encontrado")
                .Must(ValidateInt).WithMessage("Ingrese un numero valido");

            RuleFor(request => request.FechaCreacion)
                .NotNull().NotEmpty().WithMessage("FechaCreacion no encontrado")
                .Must(ValidateDate).WithMessage("Ingrese una FechaCreacion valida");

            RuleFor(request => request.FechaFin)
                .NotNull().NotEmpty().WithMessage("fechaFin no encontrado")
                .Must(ValidateDate).WithMessage("Ingrese una fechaFin valida");

            RuleForEach(request => request.Fechas)
                .NotNull().NotEmpty().WithMessage("Fechas no encontrada")
                .Must(ValidateDate).WithMessage("Ingrese una Fecha valida");
        }

        public static bool ValidateInt(int value)
        {
            if (value > 0) return true;

            return false;
        }

        public static bool ValidateDate(string value)
        {
            if (DateTime.TryParse(value, out _))
                return true;

            return false;
        }
    }

    public class GddResponse
    {
        [JsonIgnore]
        public bool Success { get; set; }
        [JsonIgnore]
        public string ReasonPhrase { get; set; }

        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime[] Fechas { get; set; }
        public DateTime[] FechasFaltantes { get; set; }
    }
}