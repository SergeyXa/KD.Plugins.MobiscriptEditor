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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace KD.Plugins.MobiscriptEditor
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public SyntaxMode SyntaxMode {
            get => _syntaxMode_private;
            set
            {
                _syntaxMode_private = value;
                if(value == SyntaxMode.AppliCatRuleScript)
                {
                    textEditor.SyntaxHighlighting =
                        new ApplicatRuleScriptHighlightingDefinition();
                }
                else
                {
                    textEditor.SyntaxHighlighting = 
                        new BlockScriptHighlightingDefinition();
                }
            }
        }
        private SyntaxMode _syntaxMode_private;

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
