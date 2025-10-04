namespace Rubik_s_Cube_Solver_MAUI.Components;

public class Separator : BoxView
{
    public Separator()
    {
        WidthRequest = 0.5;
        HeightRequest = 0.5;
        HorizontalOptions = LayoutOptions.Fill;
        VerticalOptions = LayoutOptions.Center;
        Color = Color.FromArgb("#352F44");
    }
}