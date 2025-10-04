using Microsoft.Maui.LifecycleEvents;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace Rubik_s_Cube_Solver_MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(events =>
                {
#if WINDOWS
                    events.AddWindows(windows =>
                        windows.OnWindowCreated(window =>
                        {
                            window.Title = "Rubik's Cube Solver";

                            /*window.Closed += async (sender, args) =>
                            {
                                args.Handled = true;

                                SaveConfirmationPopup savePopup = new (
                                    "Save Changes?",
                                    "Do you want to save your changes before closing?"
                                );

                                if (Application.Current?.Windows[0] is Window mainWindow)
                                {
                                    var result = await mainWindow.ShowPopupAsync(savePopup);

                                    switch (result)
                                    {
                                        case SaveConfirmationPopup.PopupResult.Save:
                                            SaveProject(null, EventArgs.Empty);
                                            break;
                                        case SaveConfirmationPopup.PopupResult.Cancel:
                                        case null:
                                            return;
                                    }
                                    //Application.Current.Quit();
                                }
                            };*/
                        }));

                    events.AddWindows(wndLifeCycleBuilder =>
                    {
                        wndLifeCycleBuilder.OnWindowCreated(window =>
                        {
                            IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                            WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                            AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                            if(winuiAppWindow.Presenter is OverlappedPresenter p)
                                p.Maximize();
                            else
                            {
                                const int width = 1920;
                                const int height = 1080;
                                winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
                            }
                        });
                    });
#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
