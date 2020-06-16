using System;
using System.Net.Http;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Integration.Tests.Fixtures.Base;
using Anjoz.Identity.Utils.Tests.Utils.Http;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Anjoz.Identity.Integration.Tests.Extensions
{
    public static class BaseWebApiExtensions
    {
        public static async Task ListarTodosRegistrosAsync<TEntity, TResponseDto>(this BaseIntegrationTestFixture baseIntegrationTestFixture, string url,
            Action criarListaRegistros, Func<Task<IPagedList<TEntity>>> obterTodosRegistros)
            where TEntity : class
            where TResponseDto : EntidadeDto
        {
            criarListaRegistros();
            var resultadoResponse = await EnviarAsync<PagedListDto<TResponseDto>>(url, baseIntegrationTestFixture.Client.GetAsync, StatusCodes.Status200OK);
            var resultadoEsperado = await ObterResultadoEsperado<TResponseDto, TEntity>(baseIntegrationTestFixture.Mapper, obterTodosRegistros);

            resultadoEsperado.Should().BeEquivalentTo(resultadoResponse);
        }

        public static async Task ListarTodosRegistrosComFiltroAsync<TEntity, TFilterDto, TResponseDto>(this BaseIntegrationTestFixture baseIntegrationTestFixture, string url,
            TFilterDto filtroDto, Action criarListaRegistros, Func<Task<IPagedList<TEntity>>> obterTodosRegistros)
            where TFilterDto : FiltroDto
            where TEntity : class
            where TResponseDto : EntidadeDto
        {
            criarListaRegistros();

            var queryParams = GerarQueryParams(filtroDto);
            var resultadoResponse = (await EnviarAsync<PagedListDto<TResponseDto>>($"{url}?{queryParams}", baseIntegrationTestFixture.Client.GetAsync, StatusCodes.Status200OK));
            var resultadoEsperado = await ObterResultadoEsperado<TResponseDto, TEntity>(baseIntegrationTestFixture.Mapper, obterTodosRegistros);

            resultadoEsperado.Should().BeEquivalentTo(resultadoResponse);
        }

        public static async Task ObterRegistroAsync<TEntity, TResponseDto>(this BaseIntegrationTestFixture baseIntegrationTestFixture,
            Action criarRegistro, Func<string> obterUrlConsultaRegistro, Func<TResponseDto, Task<TEntity>> obterRegistro)
            where TEntity : class
        {
            criarRegistro();

            var urlConsultaRegistro = obterUrlConsultaRegistro();
            var resultadoResponse = await EnviarAsync<TResponseDto>(urlConsultaRegistro, baseIntegrationTestFixture.Client.GetAsync, StatusCodes.Status200OK);
            var resultadoEsperado = await ObterResultadoEsperado(baseIntegrationTestFixture.Mapper, resultadoResponse, obterRegistro);

            resultadoEsperado.Should().BeEquivalentTo(resultadoResponse);
        }

        public static async Task CriarAsync<TEntity, TCreateDto, TResponseDto>(this BaseIntegrationTestFixture baseIntegrationTestFixture,
            string url, TCreateDto dtoCriar, Func<TResponseDto, Task<TEntity>> obterRegistro)
            where TEntity : class
            where TCreateDto : class
        {
            var resultadoResponse = await EnviarAsync<TResponseDto, TCreateDto>(url, dtoCriar, baseIntegrationTestFixture.Client.PostAsync, 201);
            var resultadoEsperado = await ObterResultadoEsperado(baseIntegrationTestFixture.Mapper, resultadoResponse, obterRegistro);

            resultadoEsperado.Should().BeEquivalentTo(resultadoResponse);
        }

        public static async Task AtualizarAsync<TEntity, TUpdateDto, TResponseDto>(this BaseIntegrationTestFixture baseIntegrationTestFixture, string url,
            Action criarRegistro, Func<TUpdateDto> obterRegistroParaAtualizar, Func<TResponseDto, Task<TEntity>> obterRegistro)
            where TEntity : class
            where TUpdateDto : class
        {
            criarRegistro();
            var dtoAtualizar = obterRegistroParaAtualizar();

            var resultadoResponse = await EnviarAsync<TResponseDto, TUpdateDto>(url, dtoAtualizar, baseIntegrationTestFixture.Client.PutAsync, StatusCodes.Status200OK);
            var resultadoEsperado = await ObterResultadoEsperado(baseIntegrationTestFixture.Mapper, resultadoResponse, obterRegistro);

            resultadoEsperado.Should().BeEquivalentTo(resultadoResponse);
        }

        public static async Task ExcluirAsync<TEntity, TResponseDto>(this BaseIntegrationTestFixture baseIntegrationTestFixture,
            Action criarRegistro, Func<string> obterUrlExclusao, Func<TResponseDto, Task<TEntity>> obterRegistro)
            where TEntity : class
        {
            criarRegistro();

            var url = obterUrlExclusao();
            var resultadoResponse = await EnviarAsync<TResponseDto>(url, baseIntegrationTestFixture.Client.DeleteAsync, StatusCodes.Status200OK);
            var resultadoEsperado = await ObterResultadoEsperado(baseIntegrationTestFixture.Mapper, resultadoResponse, obterRegistro);

            resultadoEsperado.Should().BeEquivalentTo(0);
        }

        public static async Task<TResponseDto> EnviarAsync<TResponseDto>(string url,
            Func<string, Task<HttpResponseMessage>> acao, int statusCode)
        {
            var response = await acao(url);
            return await ValidarEObterResultadoHttpResponseMessage<TResponseDto>(response, statusCode);
        }

        public static async Task<TResponseDto> EnviarAsync<TResponseDto, TDtoContent>(string url, TDtoContent dtoContent,
            Func<string, StringContent, Task<HttpResponseMessage>> acao, int statusCode)
        {
            var stringContent = HttpUtils.ConvertObjectToStringContent(dtoContent);
            var response = await acao(url, stringContent);
            return await ValidarEObterResultadoHttpResponseMessage<TResponseDto>(response, statusCode);
        }

        private static async Task<TResponseDto> ValidarEObterResultadoHttpResponseMessage<TResponseDto>(HttpResponseMessage response, int statusCode)
        {
            response.StatusCode.Should().BeEquivalentTo(statusCode, response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            return await HttpUtils.ConvertHttpResponseToAsync<TResponseDto>(response);
        }

        private static async Task<TResponseDto> ObterResultadoEsperado<TResponseDto, TEntity>(IMapper mapper, TResponseDto resultadoResponseDto,
            Func<TResponseDto, Task<TEntity>> obterRegistro)
            where TEntity : class
        {
            var resultadoAplicacao = await obterRegistro(resultadoResponseDto);
            return mapper.Map<TEntity, TResponseDto>(resultadoAplicacao);
        }

        private static async Task<PagedListDto<TResponseDto>> ObterResultadoEsperado<TResponseDto, TEntity>(IMapper mapper, Func<Task<IPagedList<TEntity>>> obterRegistro)
            where TEntity : class
            where TResponseDto : EntidadeDto
        {
            var resultadoAplicacao = await obterRegistro();
            return mapper.Map<IPagedList<TEntity>, PagedListDto<TResponseDto>>(resultadoAplicacao);
        }

        private static string GerarQueryParams(object obj)
        {
            return obj.ToQueryString();
        }
    }
}