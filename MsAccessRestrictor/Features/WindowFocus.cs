using System;
using MsAccessRestrictor.Interfaces;
using MsAccessRestrictor.Utils;

namespace MsAccessRestrictor.Features {
    class WindowFocus : IFeature {
        private readonly IntPtr _windowHandle;

        enum WindowSetting {
            Maximize = 3,
            Restore = 9
        }

        public WindowFocus() {
            _windowHandle = WinApi.GetMsAccessWindowHandle();
        }

        public void Run() {
            MakeTopMost(_windowHandle);
        }

        public void Clear() {
            MakeNormal(_windowHandle);
        }

        public static void MakeTopMost(IntPtr window) {
            RestoreAndMaximize(window);
            WinApi.SetWindowTopMost(window, true);
        }

        private static void RestoreAndMaximize(IntPtr window) {
            // If the window is minimized then it won't become "always on top" so it needs restoring.
            if (WinApi.IsIconic(window)) {
                WinApi.ShowWindow(window, (int)WindowSetting.Restore);
            }

            WinApi.ShowWindow(window, (int)WindowSetting.Maximize);
        }

        public static void MakeNormal(IntPtr window) {
            WinApi.SetWindowTopMost(window, false);
        }
    }
}