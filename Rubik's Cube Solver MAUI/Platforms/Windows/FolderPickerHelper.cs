/*using Windows.Storage.Pickers;
using Windows.Storage;
using System.Runtime.InteropServices;

namespace Rubik_s_Cube_Solver_MAUI.Platforms.Windows;

public static class FolderPickerHelper
{
    public static async Task<string?> PickFolderAsync()
    {
        var picker = new FolderPicker();
        picker.SuggestedStartLocation = PickerLocationId.Desktop;
        picker.FileTypeFilter.Add("*");

        // Get window handle manually for interop
        IntPtr hwnd = GetActiveWindowHandle();
        InitializeWithWindow(picker, hwnd);

        var folder = await picker.PickSingleFolderAsync();
        return folder?.Path;
    }

    // Interop to get the window handle
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    private static IntPtr GetActiveWindowHandle()
    {
        return GetActiveWindow();
    }

    private static void InitializeWithWindow(FolderPicker picker, IntPtr hwnd)
    {
        var windowNative = picker as IInitializeWithWindow;
        windowNative?.Initialize(hwnd);
    }

    [ComImport]
    [Guid("3E68D4BD-7135-4D10-801A-5B3690A792C3")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IInitializeWithWindow
    {
        void Initialize(IntPtr hwnd);
    }
}
*/