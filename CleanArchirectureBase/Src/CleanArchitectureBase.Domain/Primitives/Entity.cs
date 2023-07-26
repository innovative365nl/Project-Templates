using System.Globalization;
using System.Text;

namespace CleanArchitectureBase.Domain.Primitives;

public class Entity
{
    public override string ToString()
    {
        return GetType().GetProperties()
            .Select(selector: info => (info.Name, Value: info.GetValue(obj: this, index: null) ?? "(null)"))
            .Aggregate(
                seed: new StringBuilder(),
                func: (sb, pair) => sb.AppendLine(provider: CultureInfo.InvariantCulture, handler: $"{pair.Name}: {pair.Value}"),
                resultSelector: sb => sb.ToString());
    }
}