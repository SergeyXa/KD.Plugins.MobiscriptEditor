using KD.SDK2;
using System;
using System.Windows;
using System.Windows.Interop;

namespace KD.Plugins.MobiscriptEditor
{
    public class Plugin
    {
        Appli _appli = new Appli();
        Appli.MenuItem _menuItem;
        Appli.MobiscriptMenuItem _mobiscriptMenuItem;

        public bool OnAppReady(int unused)
        {
            return OnPluginLoad(unused);
        }

        public bool OnAppQuitAfter(int unused)
        {
            return OnPluginUnload(unused);
        }

        public bool OnPluginLoad(int unused)
        {
            _mobiscriptMenuItem = _appli.InsertMobiscriptMenuItem(new Appli.MenuItemInsertionInfo()
                {
                    ClassName = nameof(Plugin),
                    DllFilenameWithoutPath = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    KeyCode = Appli.MenuKeyCode.E,
                    KeyModifiers = Appli.MenuKeyModifiers.Control,
                    MethodName = nameof(ShowEditorInMobiscript),
                    Text = $"{Properties.Resources.MenuItemText}\tCtrl+E",
                },
                Appli.MobiscriptMenuItem.StandardId.Edit_SelectAll,
                Appli.MenuItemInsertionPosition.After);

            if(_menuItem == null)
                _menuItem = _appli.InsertMenuItem(new Appli.MenuItemInsertionInfo()
                    {
                        ClassName = nameof(Plugin),
                        DllFilenameWithoutPath = System.IO.Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                        KeyCode = Appli.MenuKeyCode.E,
                        KeyModifiers = Appli.MenuKeyModifiers.Control | Appli.MenuKeyModifiers.Menu | Appli.MenuKeyModifiers.Shift,
                        MethodName = nameof(ShowEditor),
                        Text = $"{Properties.Resources.MenuItemText}\tCtrl+Alt+Shift+E",
                    },
                    Appli.MenuItem.StandardId.Help_SendLogFiles,
                    Appli.MenuItemInsertionPosition.Before);

            return true;
        }

        public bool OnPluginUnload(int unused)
        {
            try
            {
                if (_mobiscriptMenuItem != null)
                {
                    _appli.RemoveMenuItem(_mobiscriptMenuItem);
                    _mobiscriptMenuItem = null;
                }
                if (_menuItem != null)
                {
                    _appli.RemoveMenuItem(_menuItem);
                    _menuItem = null;
                }
            }
            catch
            {
            }

            return true;
        }

        public bool ShowEditor(int unused)
        {
            if (!_appli.Scene.IsLoaded || _appli.Scene.ActiveObject == null)
                return false;

            var mainWindow = new MainWindow {
                SyntaxMode = SyntaxMode.BlockScript,
                Text = _appli.Scene.ActiveObject.Script 
            };

            var wih = new WindowInteropHelper(mainWindow);
            wih.Owner = _appli.GetCallParamsInfoDirect(unused).WindowHandle;

            RestoreWindowSettings(mainWindow);

            if (mainWindow.ShowDialog() == true)
            {
                _appli.Scene.UndoStack.AddState(Scene.UndoStateType.Attributes);
                _appli.Scene.ActiveObject.Script = mainWindow.Text;
                return true;
            };

            return false;
        }

        public bool ShowEditorInMobiscript(int unused)
        {
            var catalog = _appli.Catalog;

            catalog.UseMobiscriptCatalog();

            var mainWindow = new MainWindow();

            RestoreWindowSettings(mainWindow);

            var wih = new WindowInteropHelper(mainWindow);
            wih.Owner = _appli.GetCallParamsInfoDirect(unused).WindowHandle;

            Action<string> applyTextAction = null;

            if (catalog.ActiveTable.Type == Catalog.TableType.Blocks)
            {
                mainWindow.SyntaxMode = SyntaxMode.BlockScript;
                mainWindow.Text = catalog.ActiveRow.Cells[1];
                applyTextAction = (text) => catalog.ActiveRow.Cells[1] = text;
            }
            else if (catalog.ActiveTable.Type == Catalog.TableType.ApplicatRules)
            {
                mainWindow.SyntaxMode = SyntaxMode.AppliCatRuleScript;
                mainWindow.Text = catalog.ActiveRow.Cells[3];
                applyTextAction = (text) => catalog.ActiveRow.Cells[3] = text;
            }
            else
            {
                MessageBox.Show(Properties.Resources.WrongTableMessageText);
                return false;
            }

            bool dialogResult = mainWindow.ShowDialog() ?? false;

            if (dialogResult == true)
                applyTextAction(mainWindow.Text);

            SaveWindowSettings(mainWindow);

            return dialogResult;
        }

        private void SaveWindowSettings(MainWindow mainWindow)
        {
            _appli.WriteIniValue(
                "space.ini", typeof(Plugin).Namespace, "FontSize", mainWindow.EditorFontSize);

            _appli.WriteIniValue(
                "space.ini", typeof(Plugin).Namespace, "WindowWidth", mainWindow.Width);

            _appli.WriteIniValue(
                "space.ini", typeof(Plugin).Namespace, "WindowHeight", mainWindow.Height);
        }

        private void RestoreWindowSettings(MainWindow mainWindow)
        {
            double editorFontSize;
            if (!_appli.TryReadIniValue("space.ini", typeof(Plugin).Namespace, "FontSize", out editorFontSize))
            {
                if (_appli.TryReadIniValue("space.ini", "InSitu", "MobiscriptFontSizeDelta", out editorFontSize))
                    editorFontSize += 8;
                else
                    editorFontSize = 12.0;
            }
            mainWindow.EditorFontSize = editorFontSize;

            double windowWidth;
            if (_appli.TryReadIniValue("space.ini", typeof(Plugin).Namespace, "WindowWidth", out windowWidth))
                mainWindow.Width = windowWidth;

            double windowHeight;
            if (_appli.TryReadIniValue("space.ini", typeof(Plugin).Namespace, "WindowHeight", out windowHeight))
                mainWindow.Height = windowHeight;
        }
    }
}
