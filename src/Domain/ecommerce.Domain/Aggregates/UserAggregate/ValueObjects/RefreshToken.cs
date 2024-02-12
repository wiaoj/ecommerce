namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects;
public record RefreshToken {
    public String Token { get; private set; }
    public DateTime Expires { get; private set; }
    public DateTime Created { get; private set; }
    public String CreatedByIp { get; private set; }
    public Boolean IsExpired => DateTime.UtcNow >= this.Expires;

    private RefreshToken() { }
    internal RefreshToken(String token, DateTime created, DateTime expires, String createdByIp) {
        this.Token = token;
        this.Created = created;
        this.Expires = expires;
        this.CreatedByIp = createdByIp;
    }
}