using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace KD.Plugins.MobiscriptEditor
{
    public partial class MainWindow
    {
        public class BlockScriptHighlightingDefinition : IHighlightingDefinition
        {
            public static Dictionary<string /*keyword*/, string /*desc*/> Keywords;

            static BlockScriptHighlightingDefinition()
            {
                Keywords = new Dictionary<string, string>();

                foreach (var textLine in MobiscriptEditor.Properties.Resources.BlockScriptKeywords
                    .Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(l => l.Contains('\t'))
                    .Select(l => l.Split('\t'))
                    )
                {
                    Keywords[textLine[0]] = textLine[1];
                }
            }
            public BlockScriptHighlightingDefinition()
            {
                var keywordRules = Keywords.Keys.Select(keyword => new HighlightingRule()
                {
                    Regex = new Regex(keyword.StartsWith("@") ? $@"\B{keyword}=?\b" : $@"\b{keyword}=?\b", 
                        RegexOptions.IgnoreCase | RegexOptions.Compiled),
                    Color = GetNamedColor("Keyword"),
                });

                var stringSpan = new HighlightingSpan()
                {
                    StartExpression = new Regex("\"", RegexOptions.Compiled),
                    EndExpression = new Regex("\"", RegexOptions.Compiled),
                    SpanColor = GetNamedColor("String"),
                    SpanColorIncludesEnd = true,
                    SpanColorIncludesStart = true,
                };

                var expressionRuleSet = new HighlightingRuleSet()
                {
                    Spans =
                    {
                        stringSpan,
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
                            Regex = new Regex(@"C\d+", RegexOptions.Compiled),
                            Color = GetNamedColor("Constant"),
                        },
                    },
                };
                
                var blockParametersRuleSet = new HighlightingRuleSet()
                {
                    Spans = 
                    {
                        new HighlightingSpan()
                        {
                            StartExpression = new Regex("=", RegexOptions.Compiled),
                            EndExpression = new Regex(@"\s*(?=,|\))", RegexOptions.Compiled),
                            StartColor = GetNamedColor("Keyword"),
                            SpanColor = GetNamedColor("Expression"),
                            RuleSet = expressionRuleSet,
                        },
                        stringSpan,
                    },
                    Rules = 
                    {
                    }
                };
                foreach (var rule in keywordRules)
                    blockParametersRuleSet.Rules.Add(rule);

                MainRuleSet = new HighlightingRuleSet()
                {
                    Spans =
                    {
                        new HighlightingSpan()
                        {
                            StartExpression = new Regex(@"@COMMENT\(", RegexOptions.IgnoreCase | RegexOptions.Compiled),
                            EndExpression = new Regex(@"\)", RegexOptions.Compiled),
                            SpanColor = GetNamedColor("Comment"),
                            SpanColorIncludesEnd = true,
                            SpanColorIncludesStart = true,
                        },
                        new HighlightingSpan()
                        {
                            StartExpression = new Regex(@"\(", RegexOptions.Compiled),
                            EndExpression = new Regex(@"\)", RegexOptions.Compiled),
                            SpanColor = GetNamedColor("BlockParameters"),
                            RuleSet = blockParametersRuleSet,
                        },
                    },
                };
                foreach (var rule in keywordRules)
                    MainRuleSet.Rules.Add(rule);
            }

            public string Name => "Mobiscript";

            public HighlightingRuleSet MainRuleSet { get; }
                = new HighlightingRuleSet();

            public IEnumerable<HighlightingColor> NamedHighlightingColors { get; }
                = new List<HighlightingColor>()
                {
                    new HighlightingColor() { Name = "Comment", Foreground = new SimpleHighlightingBrush(Colors.Green)},
                    new HighlightingColor() { Name = "BlockParameters", /*Background = new SimpleHighlightingBrush(Colors.WhiteSmoke),*/ },
                    new HighlightingColor() { Name = "Expression", Background = new SimpleHighlightingBrush(Colors.WhiteSmoke), },
                    new HighlightingColor() { Name = "Keyword", Foreground = new SimpleHighlightingBrush(Colors.DarkViolet), },
                    new HighlightingColor() { Name = "String", Foreground = new SimpleHighlightingBrush(Colors.DarkRed), Background = new SimpleHighlightingBrush(Colors.Transparent) },
                    new HighlightingColor() { Name = "Number", Foreground = new SimpleHighlightingBrush(Colors.DarkBlue), },
                    new HighlightingColor() { Name = "Constant", Foreground = new SimpleHighlightingBrush(Colors.DarkCyan), },
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
}
