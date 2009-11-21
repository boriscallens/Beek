using System;
using NHaml;
using NHaml.Compilers;
using NHaml.Rules;
using NHaml.TemplateResolution;
using System.IO;

namespace Boris.NHaml.Rules
{
    public class BuildupPlaceHolderRule : MarkupRule
    {
        private const string NoPartialName = "No placeholder name specified";

        public override string Signifier
        {
            get { return "?"; }
        }

        public override bool PerformCloseActions { get { return false; } }

        public override BlockClosingAction Render(ViewSourceReader viewSourceReader, TemplateOptions options, TemplateClassBuilder builder)
        {
            var partialName = viewSourceReader.CurrentInputLine.NormalizedText.Trim();

            if (string.IsNullOrEmpty(partialName))
            {
                if (viewSourceReader.ViewSourceQueue.Count == 0)
                {
                    throw new InvalidOperationException(NoPartialName);
                }
                var templatePath = viewSourceReader.ViewSourceQueue.Dequeue();
                viewSourceReader.MergeTemplate(templatePath, true);
            }
            else
            {
                partialName = partialName.Insert(partialName.LastIndexOf(@"\", StringComparison.OrdinalIgnoreCase) + 1, "_");
                IViewSource viewSource;
                try
                {
                    viewSource = options.TemplateContentProvider.GetViewSource(partialName, viewSourceReader.ViewSources);
                }
                catch (FileNotFoundException)
                {
                    //ToDo: render content behind the ? tag
                    viewSource = null;
                }
                viewSourceReader.ViewSourceModifiedChecks.Add(() => viewSource.IsModified);
                viewSourceReader.MergeTemplate(viewSource, true);
            }

            return EmptyClosingAction;
        }
    }
}
