using uBeac.Repositories;

namespace API;

public class AccountsController : AccountsControllerBase<User>
{
    private readonly IHistoryRepository<LoginRequest> _historyRepository;

    public AccountsController(IUserService<User> userService, IHistoryRepository<LoginRequest> historyRepository) : base(userService)
    {
        _historyRepository = historyRepository;
    }

    public override async Task<IResult<SignInResult<Guid>>> Login(LoginRequest model, CancellationToken cancellationToken = default)
    {
        await _historyRepository.AddToHistory(model, nameof(Login), cancellationToken);
        return await base.Login(model, cancellationToken);
    }
}