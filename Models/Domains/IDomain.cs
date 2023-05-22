using System.ComponentModel.DataAnnotations;

namespace infrastracture_api.Models.Domains;

public interface IDomain
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; }

    public string Name { get; set; }
}