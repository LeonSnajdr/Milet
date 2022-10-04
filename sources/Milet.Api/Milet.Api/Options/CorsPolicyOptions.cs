using Samhammer.Options.Abstractions;

namespace Milet.Api.Options;

[Option]
public class CorsPolicyOptions
{
    public List<string> DomainUrls { get; set; }
}