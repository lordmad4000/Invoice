using AutoMapper;
using MediatR;
using SimplexInvoice.Application.Common.Dto;
using SimplexInvoice.Application.Common.Exceptions;
using SimplexInvoice.Application.Common.Interfaces.Persistance;
using System.Threading;
using System.Threading.Tasks;

namespace SimplexInvoice.Application.Users.Queries;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICustomLogger _logger;

    public GetUserByIdQueryHandler(IUserRepository userRepository,
                                   IMapper mapper,
                                   ICustomLogger logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(c => c.Id == request.Id, cancellationToken, true, $"Id=={request.Id}");
        if (user is null)
            throw new NotFoundException($"User with id {request.Id} was not found");

        _logger.Debug($"GetUserById with data: {user.ToString()}");

        return _mapper.Map<UserDto>(user);
    }
}