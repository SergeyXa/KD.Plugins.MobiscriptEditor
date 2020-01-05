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


        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
