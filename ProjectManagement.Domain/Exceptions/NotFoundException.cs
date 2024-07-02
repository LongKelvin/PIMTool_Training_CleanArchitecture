namespace ProjectManagement.Domain.Exceptions
{
    public class NotFoundException(string entityType, object key) :
        Exception($"Entity of type '{entityType}' with key '{key}' not found.")
    {
        public string EntityType { get; private set; } = entityType;
        public object Key { get; private set; } = key;
    }
}