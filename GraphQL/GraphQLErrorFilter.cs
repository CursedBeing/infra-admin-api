namespace infrastracture_api.GraphQL;

public class GraphQLErrorFilter: IErrorFilter
{
    public IError OnError(IError error)
    {
        return error
            .WithMessage(error.Exception.TargetSite.Name + ": " +error.Exception.Message);
    }
}