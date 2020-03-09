using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using KD.SDK2;

namespace KD.Plugins.MobiscriptEditor
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CompletionManager _completionManager;
        TooltipManager _tooltipManager;
        public MainWindow()
        {
            InitializeComponent();

            textEditor.SyntaxHighlighting = new MobiscriptHighlightingDefinition();

            _completionManager = new CompletionManager(textEditor, MobiscriptHighlightingDefinition.Keywords);
            _tooltipManager = new TooltipManager(textEditor, MobiscriptHighlightingDefinition.Keywords);
        }


        public string Text { get => textEditor.Text; set => textEditor.Text = value; }

        public double EditorFontSize { get => textEditor.FontSize; set => textEditor.FontSize = value; }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void mainWindow_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                EditorFontSize += Math.Sign(e.Delta);
                e.Handled = true;
            }
        }

        private void mainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void mainWindow_LocationChanged(object sender, EventArgs e)
        {

        }
    }
}
