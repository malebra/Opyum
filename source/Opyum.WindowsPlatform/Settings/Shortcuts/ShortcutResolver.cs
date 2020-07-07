using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Opyum.WindowsPlatform.Settings
{
    public static class ShortcutResolver
    {
        static bool _keysPressedTimerSet = false;
        static readonly int timerDuration = 750;
        static System.Timers.Timer _keysPressedTimer = new System.Timers.Timer(timerDuration);
        static List<ShortcutKeyBinding> _shortcutCompareList = null;

        static List<Keys> KeysPressed { get; set; } = new List<Keys>();

        /// <summary>
        /// Runs the method assigned to the shortcut
        /// </summary>
        /// <param name="sender">The obcjet whos event triggered the Mmethod.</param>
        /// <param name="e">The args of the event.</param>
        public static void ResolveShortcut(object sender, KeyEventArgs e)
        {
            var value = e.KeyData & ~(Keys.Modifiers | Keys.ControlKey | Keys.Menu | Keys.RButton | Keys.LButton);
            if (value == 0)
            {
                return;
            }
            KeysPressed.Add(e.KeyData);
            compareShortcutSequence(sender);
        }


        //After a while clears KeysPressed
        static void runKeysPressedTimer()
        {
            if (_keysPressedTimerSet)
            {
                _keysPressedTimer.Stop();
                _keysPressedTimer.Elapsed -= keysPressedTimerEventCall;
                _keysPressedTimer.Dispose();
                _keysPressedTimer = new System.Timers.Timer(timerDuration);
            }

            _keysPressedTimer.Elapsed += keysPressedTimerEventCall;
            _keysPressedTimer.Start();
            _keysPressedTimerSet = true;
        }

        

        static void keysPressedTimerEventCall(object s, EventArgs e)
        {
            if (KeysPressed.Count > 0 && _shortcutCompareList.Count > 0)
            {
                try
                {
                    var tt = _shortcutCompareList.Where(x => (x.ShortcutKeys.Count == KeysPressed.Count) && (KeysPressed.Where(t => x.ShortcutKeys.Contains(t)).Count() == KeysPressed.Count))?.First();
                    //MainWindow.Window.Invoke((Delegate)tt.Function);
                    if (((Delegate)tt.Function).Target.GetType().IsAssignableFrom(typeof(System.Windows.Forms.Control)))
                    {
                        ((Control)((Delegate)tt.Function).Target).Invoke((Delegate)tt.Function);
                    }
                }
                catch (InvalidOperationException)
                {
                    
                };
            }
            _keysPressedTimer.Stop();
            _keysPressedTimer.Dispose();
            _keysPressedTimer = new System.Timers.Timer(timerDuration);
            KeysPressed.Clear();
            _shortcutCompareList = null;
            _keysPressedTimerSet = false;
        }

        static void compareShortcutSequence(object caller)
        {
            //check if there are already some detected keys pressed
            if (_shortcutCompareList == null)
            {
                _shortcutCompareList = SettingsManager.GlobalSettings?.Shortcuts?.Where(i => i.ShortcutKeys.First() == KeysPressed.First()).ToList();
            }
            //if there are already some keys pressed, shorten the number of potential methods to summon.
            else
            {
                _shortcutCompareList = _shortcutCompareList?.Where(i => KeysPressed.Where(x => i.ShortcutKeys.Contains(x)).Count() == KeysPressed.Count).ToList();
            }
            //if there are none potentila KeyBindings
            if (_shortcutCompareList == null || _shortcutCompareList.Count <= 0)
            {
                KeysPressed.Clear();
                _shortcutCompareList = null;
                return;
            }
            else if (_shortcutCompareList.Count() == 1 && KeysPressed.Count == _shortcutCompareList.First().ShortcutKeys.Count)
            {
                _shortcutCompareList?.First().Run(caller);
            }
            else
            {
                runKeysPressedTimer();
                return;
            }
            KeysPressed.Clear();
            _shortcutCompareList = null;
            return;
        }
    }
}
