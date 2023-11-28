using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KD.Plugins.MobiscriptEditor
{
    public class ApplicatRuleScriptHighlightingDefinition: IHighlightingDefinition
    {
        public static Dictionary<string /*keyword*/, string /*desc*/> Keywords;

        static ApplicatRuleScriptHighlightingDefinition()
        {
            Keywords = new Dictionary<string, string>();

            foreach (var textLine in MobiscriptEditor.Properties.Resources.JavaScriptKeywords
                .Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Split('\t'))
                )
            {
                Keywords[textLine[0]] = textLine.Length > 1 ? textLine[1] : "";
            }
        }

        public ApplicatRuleScriptHighlightingDefinition()
        {
            var keywordRules = Keywords.Keys.Select(keyword => new HighlightingRule()
            {
                Regex = new Regex(keyword.StartsWith("@") ? $@"\B{keyword}=?\b" : $@"\b{keyword}=?\b",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled),
                Color = GetNamedColor("Keyword"),
            });

            var doubleQuotedStringSpan = new HighlightingSpan()
            {
                StartExpression = new Regex("\"", RegexOptions.Compiled),
                EndExpression = new Regex("\"", RegexOptions.Compiled),
                SpanColor = GetNamedColor("String"),
                SpanColorIncludesEnd = true,
                SpanColorIncludesStart = true,
            };

            var singleQuotedStringSpan = new HighlightingSpan()
            {
                StartExpression = new Regex("'", RegexOptions.Compiled),
                EndExpression = new Regex("'", RegexOptions.Compiled),
                SpanColor = GetNamedColor("String"),
                SpanColorIncludesEnd = true,
                SpanColorIncludesStart = true,
            };

            MainRuleSet = new HighlightingRuleSet()
            {
                Spans =
                {
                    singleQuotedStringSpan,
                    doubleQuotedStringSpan,
                },
                Rules =
                {
                    new HighlightingRule()
                    {
                        Regex = new Regex(@"\d+[\.\d+]", RegexOptions.Compiled),
                        Color = GetNamedColor("Number"),
                    },
                    new HighlightingRule()
                    {
                        Regex = new Regex(@"\$\w+", RegexOptions.Compiled),
                        Color = GetNamedColor("Placeholder"),
                    },
                }
            };
            foreach (var rule in keywordRules)
                MainRuleSet.Rules.Add(rule);
        }

        public string Name => "Applicat Rule Script";

        public HighlightingRuleSet MainRuleSet { get; }
            = new HighlightingRuleSet();

        public IEnumerable<HighlightingColor> NamedHighlightingColors { get; }
            = new List<HighlightingColor>()
            {
                    new HighlightingColor() { Name = "Keyword", Foreground = new SimpleHighlightingBrush(Colors.DarkViolet), },
                    new HighlightingColor() { Name = "String", Foreground = new SimpleHighlightingBrush(Colors.DarkRed), Background = new SimpleHighlightingBrush(Colors.Transparent) },
                    new HighlightingColor() { Name = "Number", Foreground = new SimpleHighlightingBrush(Colors.DarkBlue), },
                    new HighlightingColor() { Name = "Placeholder", Foreground = new SimpleHighlightingBrush(Colors.DarkCyan), },
            };

        public IDictionary<string, string> Properties { get; }
            = new Dictionary<string, string>();

        public HighlightingColor GetNamedColor(string name)
        {
            return NamedHighlightingColors.FirstOrDefault(hc => hc.Name == name);
        }

        public HighlightingRuleSet GetNamedRuleSet(string name)
        {
            return null;
        }

    }
}
