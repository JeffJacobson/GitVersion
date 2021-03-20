using System;
using Cake.Common.Tools.DotNetCore;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.DotNetCoreFormat
{
    public sealed class DotNetCoreFormatToolRunner : DotNetCoreTool<DotNetCoreFormatSettings>
    {
        public DotNetCoreFormatToolRunner(
            IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
        }

        public void Run(DotNetCoreFormatSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            Run(settings, GetArguments(settings));
        }

        private static ProcessArgumentBuilder GetArguments(DotNetCoreFormatSettings settings)
        {
            ProcessArgumentBuilder arguments = new();
            arguments.Append("format");

            arguments.Append(settings.Workspace != null ? settings.Workspace.ToString() : settings.WorkingDirectory.ToString());

            if (settings.Folder)
            {
                arguments.Append("--folder");
            }
            if (settings.FixWhitespaces)
            {
                arguments.Append("--fix-whitespace");
            }
            if (settings.FixStyle != null)
            {
                arguments.AppendSwitch("--fix-style", " ", settings.FixStyle);
            }
            if (settings.FixAnalyzers != null)
            {
                arguments.AppendSwitch("--fix-analyzers", " ", settings.FixAnalyzers);
            }
            if (settings.Diagnostics != null)
            {
                arguments.AppendSwitch("--diagnostics", " ", string.Join(" ", settings.Diagnostics));
            }
            if (settings.Include != null)
            {
                arguments.AppendSwitch("--include", " ", string.Join(" ", settings.Include));
            }
            if (settings.Exclude != null)
            {
                arguments.AppendSwitch("--exclude", " ", string.Join(" ", settings.Exclude));
            }
            if (settings.Check)
            {
                arguments.Append("--check");
            }
            return arguments;
        }
    }
}