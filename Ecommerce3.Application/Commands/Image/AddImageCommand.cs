namespace Ecommerce3.Application.Commands.Image;

public record AddImageCommand
{
    public Type ImageType { get; init; }
    public int ParentId { get; init; }
}