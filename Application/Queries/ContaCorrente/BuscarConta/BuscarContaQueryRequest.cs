using MediatR;
using System;

namespace Application.Queries.ContaCorrente.BuscarConta
{
    public sealed class BuscarContaQueryRequest : IRequest<BuscarContaQueryResult>
    {
        public Guid Id { get; set; }
    }
}