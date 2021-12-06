using Amazon.DynamoDBv2.Model;
using AutoMapper;
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Infrastructure.Entities;
using Hackney.Core.DynamoDb;
using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StatementDbEntity, Statement>();
            CreateMap<Statement, StatementDbEntity>()
                .ForMember(s => s.SummaryType, opt => opt.MapFrom(x => SummaryType.Statement));

            CreateMap<AddStatementRequest, Statement>();

            CreateMap<QueryResponse, List<Statement>>()
                .ConvertUsing(qr => (qr == null) ? null : qr.ToStatement());

            CreateMap<Statement, StatementResponse>();
            CreateMap<PagedResult<Statement>, PagedResult<StatementResponse>>();
        }
    }
}
