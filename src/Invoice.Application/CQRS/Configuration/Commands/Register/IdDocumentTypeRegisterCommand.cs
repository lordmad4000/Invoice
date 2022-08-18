using MediatR;
using Invoice.Application.Common.Dto;

namespace Invoice.Application.CQRS.Configuration.Commands.Register
{

    public class IdDocumentTypeRegisterCommand : IRequest<IdDocumentTypeDto>
    {
        public string Name { get; set; }

        public IdDocumentTypeRegisterCommand(string name)
        {
            Name = name;
        }

    }
}