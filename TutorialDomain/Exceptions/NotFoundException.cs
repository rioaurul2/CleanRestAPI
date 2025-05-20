namespace TutorialDomain.Exceptions;

public class NotFoundException(string resourceType, string resourceIdentifier) 
    : Exception($"{resourceType} with id {resourceIdentifier} doesn't exist")
{
    public string ResourceType { get; } = resourceType;
    public string ResourceIdentifier { get; } = resourceIdentifier;
}
