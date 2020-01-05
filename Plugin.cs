using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using KD.SDK2;

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
            _appli.RemoveMenuItem(_mobiscriptMenuItem);

            return true;
        }

        public bool ShowEditor(int unused)
        {
            if (!_appli.Scene.IsLoaded || _appli.Scene.ActiveObject == null)
                return false;

            var mainWindow = new MainWindow() { Text = _appli.Scene.ActiveObject.Script };

            var wih = new WindowInteropHelper(mainWindow);
            wih.Owner = _appli.GetCallParamsInfoDirect(unused).WindowHandle;

            if (mainWindow.ShowDialog() == true)
            {
                _appli.Scene.ActiveObject.Script = mainWindow.Text;
                return true;
            };

            return false;
        }

        public bool ShowEditorInMobiscript(int unused)
        {
            var catalog = _appli.Catalog;

            catalog.UseMobiscriptCatalog();

            if(catalog.ActiveTable.Type != Catalog.TableType.Blocks)
            {
                MessageBox.Show(Properties.Resources.WrongTableMessageText);
                return false;
            }

            var mainWindow = new MainWindow() { Text = catalog.ActiveRow.Cells[1] };

            var wih = new WindowInteropHelper(mainWindow);
            wih.Owner = _appli.GetCallParamsInfoDirect(unused).WindowHandle;

            if (mainWindow.ShowDialog() == true)
            {
                catalog.ActiveRow.Cells[1] = mainWindow.Text;
                return true;
            };

            return false;
        }
    }
}
