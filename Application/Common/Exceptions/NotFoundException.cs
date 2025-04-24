using System;

namespace NovaExpediente.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity {name} value {key} was not found.")
        {
        }
    }
}