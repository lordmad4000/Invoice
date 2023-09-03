using AutoMapper;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Interfaces;
using SimplexInvoice.Domain.Exceptions;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using SimplexInvoice.Domain.Users;
using SimplexInvoice.Domain.Users.Validations;

namespace SimplexInvoice.Application.Users.Commands
{

    public class UserRemoveHandler : IRequestHandler<UserRemoveCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomLogger _logger;

        public UserRemoveHandler(IUserRepository userRepository,
                                 ICustomLogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(UserRemoveCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(request.Id, cancellationToken);
            var result = await _userRepository.SaveChangesAsync(cancellationToken);
            if (result > 0)
                _logger.Debug(@$"User with id {request.Id} removed.");

            return true;
        }

    }
}