using System.IO;
using System.Threading.Tasks;
using Fallout.Common.CI.GitHubActions.Configuration;
using Fallout.Common.Utilities;
using VerifyXunit;
using Xunit;

namespace Fallout.Common.Specs.CI;

public class GitHubActionsArtifactStepSpecs
{
    private static Task Verify(GitHubActionsArtifactStep step)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream, leaveOpen: true);
        step.Write(new CustomFileWriter(writer, indentationFactor: 2, commentPrefix: "#"));
        writer.Flush();

        stream.Seek(offset: 0, SeekOrigin.Begin);
        return Verifier.Verify(new StreamReader(stream).ReadToEnd());
    }

    [Fact]
    public Task Name_with_an_apostrophe_is_yaml_escaped()
        => Verify(new GitHubActionsArtifactStep { Name = "Yousif's Build", Path = "out", Condition = "" });
}
