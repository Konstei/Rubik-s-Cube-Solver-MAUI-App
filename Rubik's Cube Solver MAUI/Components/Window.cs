namespace Rubik_s_Cube_Solver_MAUI.Components;

public partial class Window : StackLayout
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(Window),
        string.Empty
    );

    public string Title
    {
        get => (string)GetValue(TitleProperty) ?? "";
        set => SetValue(TitleProperty, value);
    }


    protected override void OnParentSet()
    {
        base.OnParentSet();

        BackgroundColor = Color.FromArgb("#B9B4C7");

        Title windowHandle = new()
        {
            Padding = new Thickness(6, 3),
            HorizontalTextAlignment = TextAlignment.Start,
            BackgroundColor = Color.FromArgb("#5C5470"),
            TextColor = Color.FromArgb("#FAF0E6"),
            FontSize = 12
        };
        windowHandle.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));

        Children.Insert(0, windowHandle);
    }

    protected override void OnChildAdded(Element child)
    {
        base.OnChildAdded(child);

        if (child is View view && !Children.Contains(view) && Children[1] is Grid content)
        {
            int index = Children.Count > 2 ? 1 : Children.Count;
            content.Children.Insert(index, view);
        }
    }
}