namespace Rubik_s_Cube_Solver_MAUI.Components;

public class Preview : ContentView
{
    public static readonly BindableProperty SourceProperty =
        BindableProperty.Create(nameof(Source), typeof(string), typeof(Preview), default(string));

    public string Source
    {
        get => (string)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(Preview), default(string));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty PlacementProperty =
        BindableProperty.Create(nameof(Placement), typeof(string), typeof(Preview), default(string), propertyChanged: OnPlacementChanged);

    public string Placement
    {
        get => (string)GetValue(PlacementProperty);
        set => SetValue(PlacementProperty, value);
    }

    public Preview()
    {
        StackLayout stackLayout = new()
        {
            Spacing = 10,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Padding = new(40)
        };

        
        Image image = new()
        {
            Aspect = Aspect.AspectFit,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        image.SetBinding(Image.SourceProperty, new Binding(nameof(Source), source: this));

        Frame thumbnail = new()
        {
            CornerRadius = 10,
            BorderColor = Colors.Transparent,
            HasShadow = false,
            Padding = 0,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            Content = image
        };

        Label label = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.End,
            TextColor = Colors.Black
        };
        label.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));

        stackLayout.Children.Add(thumbnail);
        stackLayout.Children.Add(label);

        Content = stackLayout;
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();
        ApplyPlacement();
    }

    private static void OnPlacementChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var preview = (Preview)bindable;
        preview.ApplyPlacement();
    }

    private void ApplyPlacement()
    {
        if (Parent is Grid grid)
        {
            List<string> coordinates = Placement.Split(',').Select(coord => coord.Trim()).ToList();
            if (coordinates.Count == 2 &&
                int.TryParse(coordinates[0], out var row) &&
                int.TryParse(coordinates[1], out var column))
            {
                Grid.SetRow(this, row);
                Grid.SetColumn(this, column);
            }
        }
    }
}
