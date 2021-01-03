using System;

namespace Domain.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }


        public void AlterarIdentificador(Guid id)
        {
            Id = id;
        }
    }
}