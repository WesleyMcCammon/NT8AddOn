using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using NinjaTrader.Cbi;
using NinjaTrader.Code;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Tools;
using NinjaTrader.NinjaScript;

namespace NinjaTrader.Gui.NinjaScript 
{
    public class $itemName$ : AddOnBase
    {
        private NTMenuItem _myMenuItem;
        private NTMenuItem _existingMenu;

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = "";
                Name = $itemNameText$;
        }
        }

        protected override void OnWindowCreated(Window window)
        {
            var controlCenterWindow = window as ControlCenter;
            if (controlCenterWindow == null)
                return;

            _existingMenu = controlCenterWindow.FindFirst($nt8targetmenu$) as NTMenuItem;

            if (_existingMenu == null)
                return;

            _myMenuItem = new NTMenuItem
            {
                Header = $nt8menuText$,
                Style = Application.Current.TryFindResource($mainMenuItemName$) as Style
            };

            _existingMenu.Items.Add(_myMenuItem);
            _myMenuItem.Click += _myMenuItem_Click;
        }

        protected override void OnWindowDestroyed(Window window)
        {
            if (_myMenuItem != null && window is ControlCenter)
            {
                if (_existingMenu != null && _existingMenu.Items.Contains(_myMenuItem))
                {
                    _existingMenu.Items.Remove(_myMenuItem);
                }

                _myMenuItem.Click -= _myMenuItem_Click;
                _myMenuItem = null;
            }
        }

        private void _myMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Core.Globals.RandomDispatcher.BeginInvoke(new Action(() => new $nt8WindowClass$().Show()));
        }
    }

    public class $nt8WindowClass$ : NTWindow 
    {
        public $nt8WindowClass$ 
        {
            Caption = $nt8menuText$;
            Width = 1000;
            Height = 400;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var tabControl = new TabControl();
            TabControlManager.SetIsMovable(tabControl, false);
            TabControlManager.SetCanAddTabs(tabControl, false);
            TabControlManager.SetCanRemoveTabs(tabControl, false);
            tabControl.AddNTTabPage(new $nt8TabClassName$());

            TabControlManager.SetFactory(tabControl, new $nt8TabFactoryName$());
            Content = tabControl;
        }
    }

    public class $nt8TabFactoryName$ : INTTabFactory
    {
        public NTWindow CreateParentWindow()
        {
            return new $nt8WindowClass$();
        }

        public NTTabPage CreateTabPage(string typeName, bool isNewWindow = false)
        {
            return new $nt8TabClassName$();
        }
    }

    public class $nt8TabClassName$ : NTTabPage
    {
        public $nt8TabClassName$() 
        {
            Content = LoadXAML();
        }

        private DependencyObject LoadXAML()
        {
            try
            {
                using (var stream = GetManifestResourceStream($xamlFileName$))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        var page = System.Windows.Markup.XamlReader.Load(streamReader.BaseStream) as Page;
                        DependencyObject pageContent = null;

                        if (page != null)
                        {
                            pageContent = page.Content as DependencyObject;
                            
                            // get controls here
                        }

                        return pageContent;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        protected override string GetHeaderPart(string variable)
        {
            return $itemNameText$;
}

        protected override void Restore(XElement element)
        {
            throw new NotImplementedException();
        }

        protected override void Save(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}