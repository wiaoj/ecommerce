namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public sealed record FullName {
    public String FirstName { get; private set; }
    public List<String> MiddleNames { get; private set; } = [];
    public String LastName { get; private set; }

    private FullName() { }
    internal FullName(String firstName, IEnumerable<String>? middleNames, String lastName) {
        this.FirstName = ValidateName(firstName, nameof(this.FirstName));
        this.LastName = ValidateName(lastName, nameof(this.LastName));

        if(middleNames != null) {
            foreach(String middleName in middleNames) {
                this.MiddleNames.Add(ValidateName(middleName, "MiddleName", false));
            }
        }
    }

    public static FullName Create(String firstName, String lastName) {
        return new(firstName, null, lastName);
    }

    private static String ValidateName(String name, String propertyName) {
        return ValidateName(name, propertyName, true);
    }

    private static String ValidateName(String name, String propertyName, Boolean required) {
        if(required)
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return name.Any(Char.IsDigit)
            ? throw new ArgumentException($"{propertyName} cannot contain numbers.", propertyName)
            : name?.Trim() ?? String.Empty;
    }

    public sealed override String ToString() {
        return ToWesternFormat();
    }

    public String ToWesternFormat() {
        return this.MiddleNames.Count is 0
            ? ConcatenateFirstAndLastName()
            : $"{this.FirstName} {String.Join(" ", this.MiddleNames)} {this.LastName}";
    }

    public String ToEasternFormat() {
        return this.MiddleNames.Count is 0
            ? ConcatenateFirstAndLastName()
            : $"{this.LastName} {this.FirstName} {String.Join(" ", this.MiddleNames)}";
    }

    private String ConcatenateFirstAndLastName() {
        return $"{this.FirstName} {this.LastName}";
    }
}