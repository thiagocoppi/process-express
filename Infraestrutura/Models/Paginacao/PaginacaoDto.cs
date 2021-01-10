using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestrutura.Models.Paginacao
{
    public sealed class PaginacaoDto
    {
        public int TamanhoPagina { get; set; }
        public int PularRegistros { get; set; }
    }
}
