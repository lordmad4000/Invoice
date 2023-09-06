using MediatR;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Application.Users.Exceptions;
using System.Threading;
using System.Threading.Tasks;

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
            if (await _userRepository.SaveChangesAsync(cancellationToken) == 0)
                throw new UserRemovingException($"Error removing the User.");

            _logger.Debug(@$"User with id {request.Id} removed.");

            return true;
        }

    }
}