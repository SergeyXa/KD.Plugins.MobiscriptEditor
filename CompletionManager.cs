using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace KD.Plugins.MobiscriptEditor
{
    class CompletionManager
    {
        TextEditor _textEditor;
        IEnumerable<CompletionData> _completionDatas;
        CompletionWindow _completionWindow;

        internal static Regex _currentSegmentRegex = 
            new Regex(@"^(?:(?:@[A-Z0-9]*)|(?:[A-Z0-9]+))$", 
                RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

        public CompletionManager(TextEditor textEditor, 
            IEnumerable<KeyValuePair<string /*keyword*/, string /*description*/>> keywords)
        {
            _textEditor = textEditor;
            
            _textEditor.TextArea.KeyDown += TextArea_KeyDown;

            _completionDatas =
                keywords
                .Select(kvp => new CompletionData()
                {
                    Text = kvp.Key,
                    Description = kvp.Value
                })
                .OrderBy(cd => cd.Text)
                .ToList();
        }

        private void TextArea_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Space 
                && Keyboard.Modifiers == ModifierKeys.Control)
            {
                var currentTextSegment = GetCurrentTextSegment(_textEditor);

                if(currentTextSegment.Length > 0)
                    ShowCompletionWindow(currentTextSegment);

                e.Handled = true;
            }
        }

        static internal ISegment GetCurrentTextSegment(TextEditor textEditor, ISegment sourceSegment = null)
        {
            int textStart = sourceSegment?.Offset ?? textEditor.CaretOffset;
            int textEnd = (sourceSegment?.EndOffset ?? textEditor.CaretOffset);

            while (textStart > 0 
                    && _currentSegmentRegex.IsMatch(
                        textEditor.Text.Substring(textStart - 1, textEnd - (textStart - 1))))
                textStart--;

            while (textEnd + 1 < textEditor.Text.Length
                    && _currentSegmentRegex.IsMatch(
                        textEditor.Text.Substring(textStart, textEnd + 1 - textStart)))
                textEnd++;

            return new TextSegment()
            {
                StartOffset = textStart,
                EndOffset = textEnd,
            };
        }
        private void ShowCompletionWindow(ISegment targetSegment)
        {
            _completionWindow = new CompletionWindow(_textEditor.TextArea)
            {
                StartOffset = targetSegment.Offset,
                EndOffset = targetSegment.Offset,
            };
            _completionWindow.Closed += delegate
            {
                _completionWindow = null;
            };

            foreach (var completionData in _completionDatas)
                _completionWindow.CompletionList.CompletionData.Add(completionData);

            string targetText = _textEditor.Document.GetText(targetSegment);

            _completionWindow.CompletionList.IsFiltering = false;
            _completionWindow.CompletionList.SelectItem(targetText);

            _completionWindow.Show();
        }

        public class CompletionData : ICompletionData
        {
            public ImageSource Image { get => null; }

            public string Text { get; set; }

            public object Content => Text;

            public object Description { get; set; }
            public double Priority => 0.0;

            public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
            {
                var segmentBeingReplaced = new TextSegment()
                {
                    StartOffset = completionSegment.Offset,
                    EndOffset = completionSegment.EndOffset,
                };

                while (segmentBeingReplaced.EndOffset + 1 < textArea.Document.TextLength
                     && _currentSegmentRegex.IsMatch(textArea.Document.GetText(segmentBeingReplaced.StartOffset, segmentBeingReplaced.Length + 1)))
                {
                    segmentBeingReplaced.EndOffset++;
                }

                textArea.Document.Replace(segmentBeingReplaced, this.Text);
            }
        }
    }
}
