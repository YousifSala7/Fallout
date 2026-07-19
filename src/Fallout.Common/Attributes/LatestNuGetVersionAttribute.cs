using System;
using System.Linq;
using System.Reflection;
using NuGet.Versioning;
using Fallout.Common.Utilities;
using Fallout.Common.ValueInjection;

namespace Fallout.Common.Tooling;

public class LatestNuGetVersionAttribute : ValueInjectionAttributeBase
{
    private readonly string packageId;

    public LatestNuGetVersionAttribute(string packageId)
    {
        this.packageId = packageId;
    }

    public bool IncludePrerelease { get; set; }
    public bool IncludeUnlisted { get; set; }

    public override object GetValue(MemberInfo member, object instance)
    {
        var version = NuGetVersionResolver.GetLatestVersion(packageId, IncludePrerelease, IncludeUnlisted).GetAwaiter().GetResult();
        return member.GetMemberType() == typeof(string)
            ? version
            : NuGetVersion.Parse(version);
    }
}
