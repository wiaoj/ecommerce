using System.Diagnostics;

namespace ecommerce.Domain.Common.Models;
[DebuggerDisplay("{Value}")]
public abstract record EntityId<TId> {
    public TId Value { get; }

    protected EntityId() { }
    protected EntityId(TId value) {
        this.Value = value;
    }

    public override String ToString() {
        return $"{this.Value}";
    }
}