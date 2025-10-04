using Microsoft.Maui;
using Rubik_s_Cube_Solver_MAUI.Components;
using System.Diagnostics;

namespace Rubik_s_Cube_Solver_MAUI;

public partial class MainPage : ContentPage
{
    private double windowWidth, windowHeight, baseWidth = 1366, baseHeight = 711;
    bool gte136;

    public MainPage()
    {
        InitializeComponent();

        DeviceDisplay.Current.MainDisplayInfoChanged += OnDisplayInfoChanged;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;

        windowWidth = displayInfo.Width / displayInfo.Density;
        windowHeight = displayInfo.Height / displayInfo.Density - 57;

        AbsoluteLayout absoluteLayout = BuildDOM(windowWidth, windowHeight);
        Content = absoluteLayout;
    }

    private AbsoluteLayout BuildDOM(double windowWidth, double windowHeight)
    {
        double menuHeight, window1Width, window1Height, window2Width, window2Height, window3Width, window3Height;
        bool gte136 = (windowWidth / windowHeight >= 1.36);

        if (windowWidth / windowHeight >= 1.36)
        {
            menuHeight = 25 * windowHeight / baseHeight;
            window1Width = 693 * windowHeight / baseHeight;
            window1Height = 545 * windowHeight / baseHeight;
            window2Width = window1Width;
            window2Height = windowHeight - window1Height;
            window3Width = windowWidth - window1Width;
            window3Height = windowHeight;
            gte136 = true;
        }
        else
        {
            menuHeight = 0;
            window1Width = 0;
            window1Height = 0;
            window2Width = 0;
            window2Height = 0;
            window3Width = 0;
            window3Height = 0;
            gte136 = false;
        }

        AbsoluteLayout absoluteLayout = new()
        {
            BackgroundColor = Color.FromArgb("#352F44")
        };


        Menu menu = new();
        menu.hHeight = menuHeight;
        AbsoluteLayout.SetLayoutBounds(menu, new(0, 0, windowWidth, menuHeight));
        absoluteLayout.Children.Add(menu);


        Components.Window window1 = new()
        {
            WidthRequest = window1Width,
            HeightRequest = window1Height,
            Title = "Model"
        };
        AbsoluteLayout.SetLayoutBounds(window1, new(0, menuHeight, window1Width, window1Height));
        Wrapper wrapper1 = new()
        {
            windowHeight = windowHeight,
            gte136 = gte136
        };
        wrapper1.Children.Add(new CubeModelFlat());
        window1.Children.Add(wrapper1);
        absoluteLayout.Children.Add(window1);


        Components.Window window2 = new()
        {
            WidthRequest = window2Width,
            HeightRequest = window2Height,
            Title = "Controls"
        };
        AbsoluteLayout.SetLayoutBounds(window2, new(0, menuHeight + window1Height + 1, window2Width, window2Height));
        Wrapper wrapper2 = new()
        {
            windowHeight = windowHeight,
            gte136 = gte136
        };
        wrapper2.Children.Add(new Controls());
        window2.Children.Add(wrapper2);
        absoluteLayout.Children.Add(window2);


        Components.Window window3 = new()
        {
            WidthRequest = window3Width,
            HeightRequest = window3Height,
            Title = "Solution"
        };
        AbsoluteLayout.SetLayoutBounds(window3, new(window1Width + 1, menuHeight, window3Width, window3Height));
        Wrapper wrapper3 = new()
        {
            windowHeight = windowHeight,
            windowWidth = window3Width,
            gte136 = gte136
        };
        wrapper3.Children.Add(new Moveset());
        window3.Children.Add(wrapper3);
        absoluteLayout.Children.Add(window3);


        return absoluteLayout;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Window != null)
        {
            Window.Title = "Rubik's Cube Solver";
        }
    }

    private void OnDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        var displayInfo = e.DisplayInfo;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            windowWidth = displayInfo.Width / displayInfo.Density;
            windowHeight = displayInfo.Height / displayInfo.Density;

            Debug.WriteLine($"{windowWidth} {windowHeight}");

            BuildDOM(windowWidth, windowHeight);
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        DeviceDisplay.Current.MainDisplayInfoChanged -= OnDisplayInfoChanged;
    }
}
