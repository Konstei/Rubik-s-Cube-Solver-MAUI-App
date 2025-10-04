//namespace Rubik_s_Cube_Solver_MAUI.Components;
//public partial class Thumbnail : ContentView
//{
//    public static readonly BindableProperty TitleProperty =
//        BindableProperty.Create(nameof(Title), typeof(string), typeof(Thumbnail), default(string));

//    public string Title
//    {
//        get => (string)GetValue(TitleProperty);
//        set => SetValue(TitleProperty, value);
//    }
    
//    public static readonly BindableProperty SourceProperty =
//        BindableProperty.Create(nameof(Source), typeof(string), typeof(Thumbnail), default(string));

//    public string Source
//    {
//        get => (string)GetValue(SourceProperty);
//        set => SetValue(SourceProperty, value);
//    }

//    public static readonly BindableProperty PlacementProperty =
//        BindableProperty.Create(nameof(Placement), typeof(string), typeof(Thumbnail), default(string));

//    public string Placement
//    {
//        get => (string)GetValue(PlacementProperty);
//        set
//        {
//            SetValue(PlacementProperty, value);
//            _placement = (
//                Convert.ToInt32(Placement.Split(",")[0].Trim()),
//                Convert.ToInt32(Placement.Split(",")[1].Trim())
//            );
//        }
//    }

//    private (int, int) _placement = (-1, -1);

//    public Thumbnail()
//    {
//        Padding = new(40);
//        BackgroundColor = Colors.Transparent;
//        Grid.SetRow(this, _placement.Item1);
//        Grid.SetColumn(this, _placement.Item2);
        
//        //< Frame Grid.Row = "0" Grid.Column = "0" Padding = "40" BackgroundColor = "Transparent" BorderColor = "Transparent" >
//        //  < Image Source = "Rubik's_cube.svg" />
//        //</ Frame >
//        //Image thumbnail = new();
//        //thumbnail.SetBinding(Image.SourceProperty, new Binding(nameof(Source), source: this));

//        Label title = new()
//        {
//            Text = Title
//        };
//        title.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
//    }
//}
