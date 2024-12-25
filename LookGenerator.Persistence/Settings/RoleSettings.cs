namespace LookGenerator.Persistence.Settings ;

    public class RoleSettings
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }