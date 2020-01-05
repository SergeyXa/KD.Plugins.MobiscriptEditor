using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KD.Plugins.MobiscriptEditor
{
    public class TooltipManager
    {
        TextEditor _textEditor;
        Dictionary<string /*keyword*/, string /*description*/> _keywords;
        ToolTip _tooltip;

        public TooltipManager(TextEditor textEditor, Dictionary<string /*keyword*/, string /*description*/> keywords)
        {
            _textEditor = textEditor;
            _keywords = keywords;

            _textEditor.MouseHover += TextEditor_MouseHover; ;
            _textEditor.MouseHoverStopped += TextEditor_MouseHoverStopped;

            _tooltip = new ToolTip();
            _tooltip.PlacementTarget = _textEditor;
        }

        private void TextEditor_MouseHoverStopped(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _tooltip.IsOpen = false;
        }

        private void TextEditor_MouseHover(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var pos = _textEditor.GetPositionFromPoint(e.GetPosition(_textEditor));
            if (pos != null)
            {
                var offset = _textEditor.Document.GetOffset(
                    ((TextViewPosition)pos).Location);

                ShowTooltip(offset);
            }
        }
        private void ShowTooltip(int offset)
        {
            var currentTextSegment = 
                CompletionManager.GetCurrentTextSegment(_textEditor, new TextSegment()
                {
                    StartOffset = offset,
                    Length = 0,
                });

            if (currentTextSegment.Length > 0)
            {
                string currentText = _textEditor.Document.GetText(currentTextSegment);
                string currentDescription;

                if (_keywords.TryGetValue(currentText, out currentDescription))
                {
                    _tooltip.Content = currentDescription;
                    _tooltip.IsOpen = true;
                }
            }
            else
                _tooltip.IsOpen = false;
        }
    }
}
