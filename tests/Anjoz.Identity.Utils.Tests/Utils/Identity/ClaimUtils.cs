using System.Collections.Generic;
using Anjoz.Identity.Application.Dtos.Identity.Claim;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Utils.Tests.Utils.Identity
{
    public sealed class ClaimUtils
    {
        public static IEnumerable<Claim> Claims => new List<Claim>
        {
            new Claim {Id = 1, Valor = "Value1", ValorNormalizado = "VALUE1", Descricao = "Descricao1", DescricaNormalizada = "DESCRICAO1"},
            new Claim {Id = 2, Valor = "Value2", ValorNormalizado = "VALUE2", Descricao = "Descricao2", DescricaNormalizada = "DESCRICAO2"},
            new Claim {Id = 3, Valor = "Value3", ValorNormalizado = "VALUE3", Descricao = "Descricao3", DescricaNormalizada = "DESCRICAO3"},
            new Claim {Id = 4, Valor = "Value4", ValorNormalizado = "VALUE4", Descricao = "Descricao4", DescricaNormalizada = "DESCRICAO4"},
            new Claim {Id = 5, Valor = "Value5", ValorNormalizado = "VALUE5", Descricao = "Descricao5", DescricaNormalizada = "DESCRICAO5"},
            new Claim {Id = 6, Valor = "Value6", ValorNormalizado = "VALUE6", Descricao = "Descricao6", DescricaNormalizada = "DESCRICAO6"},
            new Claim {Id = 7, Valor = "Value7", ValorNormalizado = "VALUE7", Descricao = "Descricao7", DescricaNormalizada = "DESCRICAO7"},
            new Claim {Id = 8, Valor = "Value8", ValorNormalizado = "VALUE8", Descricao = "Descricao8", DescricaNormalizada = "DESCRICAO8"},
            new Claim {Id = 9, Valor = "Value9", ValorNormalizado = "VALUE9", Descricao = "Descricao9", DescricaNormalizada = "DESCRICAO9"},
            new Claim {Id = 10, Valor = "Value10", ValorNormalizado = "VALUE10", Descricao = "Descricao10", DescricaNormalizada = "DESCRICAO10"}
        };
        
        public static IEnumerable<ClaimDto> ClaimsDto => new List<ClaimDto>
        {
            new ClaimDto {Id = 1, Valor = "Value1", Descricao = "Descricao1"},
            new ClaimDto {Id = 2, Valor = "Value2", Descricao = "Descricao2"},
            new ClaimDto {Id = 3, Valor = "Value3", Descricao = "Descricao3"},
            new ClaimDto {Id = 4, Valor = "Value4", Descricao = "Descricao4"},
            new ClaimDto {Id = 5, Valor = "Value5", Descricao = "Descricao5"},
            new ClaimDto {Id = 6, Valor = "Value6", Descricao = "Descricao6"},
            new ClaimDto {Id = 7, Valor = "Value7", Descricao = "Descricao7"},
            new ClaimDto {Id = 8, Valor = "Value8", Descricao = "Descricao8"},
            new ClaimDto {Id = 9, Valor = "Value9", Descricao = "Descricao9"},
            new ClaimDto {Id = 10, Valor = "Value10", Descricao = "Descricao10"}
        };
    }
}