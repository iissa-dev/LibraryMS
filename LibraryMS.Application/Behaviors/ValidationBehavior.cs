namespace LibraryMS.Application.Behaviors;

// Behavior into MedaitR work with any request and any response in General execute before the handler
public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next(cancellationToken);
        
        var context = new ValidationContext<TRequest>(request);
        
        var result = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        
        var failures = result.SelectMany(r => r.Errors).Where(e => e is not null).ToList();
        
        if (failures.Count != 0)
            throw new ValidationException(failures);
        
        
        return await next(cancellationToken);
    }
}