using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Cave_Explorer.Helpers
{
    public static class ConsoleHook
    {
        public static void DisableAllResizingControl()
        {
            var window = GetConsoleWindow();
            var systemMenu = GetSystemMenu(window, false);
            DeleteMenu(systemMenu, ScClose, MfByCommand);
            DeleteMenu(systemMenu, ScMinimize, MfByCommand);
            DeleteMenu(systemMenu, ScMaximize, MfByCommand);
            DeleteMenu(systemMenu, ScSize, MfByCommand);
        }

        public static void SetConsoleFont(string fontName, short size)
        {
            unsafe
            {
                var hnd = GetStdHandle(StdHandle.OutputHandle);
                if (hnd != InvalidHandleValue)
                {
                    var info = new CONSOLE_FONT_INFO_EX();
                    info.cbSize = (uint)Marshal.SizeOf(info);

                    // Set console font to Lucida Console.
                    var newInfo = new CONSOLE_FONT_INFO_EX();
                    newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
                    newInfo.FontFamily = TmpfTrueType;
                    IntPtr ptr = new IntPtr(newInfo.FaceName);
                    Marshal.Copy(fontName.ToCharArray(), 0, ptr, fontName.Length);

                    // Get some settings from current font.
                    newInfo.dwFontSize = new COORD(info.dwFontSize.X, size);
                    SetCurrentConsoleFontEx(hnd, false, ref newInfo);
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private unsafe struct CONSOLE_FONT_INFO_EX
        {
            internal uint cbSize;
            internal int FontIndex;
            internal COORD dwFontSize;
            internal int FontFamily;
            internal int FontWeight;
            internal fixed char FaceName[LfFaceSize];
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            internal short X;
            internal short Y;
            internal COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        private enum StdHandle
        {
            OutputHandle = -11
        }

        private const int TmpfTrueType = 4;
        private const int LfFaceSize = 32;
        private const int MfByCommand = 0x0;
        private const int ScClose = 0xF060;
        private const int ScMinimize = 0xF020;
        private const int ScMaximize = 0xF030;
        private const int ScSize = 0xF000;
        private static IntPtr InvalidHandleValue = new IntPtr(-1);

        [DllImport("kernel32")]
        private static extern IntPtr GetStdHandle(StdHandle index);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32")]
        private static extern IntPtr GetSystemMenu(IntPtr handle, bool revert);

        [DllImport("user32")]
        private static extern IntPtr DeleteMenu(IntPtr handle, int position, int flags);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(
            IntPtr consoleOutput,
            bool maximumWindow,
            ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);
    }
}
