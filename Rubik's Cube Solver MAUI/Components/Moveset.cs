using System.Diagnostics;
using System.Resources;
using System.Text.RegularExpressions;

namespace Rubik_s_Cube_Solver_MAUI.Components;

public class Moveset : VerticalStackLayout
{
    private double width, height;

    protected override void OnParentSet()
	{
        base.OnParentSet();

        if (Parent is Wrapper wrapper)
        {
            if (wrapper.gte136)
            {
                width = wrapper.windowWidth;
                height = wrapper.windowHeight;
            }
            else
            {
                width = 0;
                height = 0;
            }
        }

        WidthRequest = width;
        HeightRequest = height;
        Padding = new Thickness(10);

        ScrollView scrollView = new()
        {
            Content = new Label
            {
                FormattedText = new FormattedString
                {
                    Spans = {
                        new Span { Text = "" },
                        new Span { Text = ">>", BackgroundColor = Color.FromArgb("#7D7580") },
                        new Span { Text = "" },
                    }
                },
                TextColor = Colors.Black,
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = 18,
                HeightRequest = height - 120 * height / 711,
            }
        };
        Children.Add(scrollView);

        HorizontalStackLayout buttons = new()
        {
            HeightRequest = 80
        };

        string[] icons = [ "beginning.png", "previous.png", "next.png", "end.png" ];
        EventHandler[] commands = [ BeginningOnClicked, PreviousOnClicked, NextOnClicked, EndOnClicked ];
        for (int i=0; i<4; i++)
        {
            ImageButton imgBtn = new()
            {
                WidthRequest = 157 * width / 673,
                HeightRequest = 70 * height / 711,
                Margin = new Thickness(3),
                Padding = new Thickness(10),
                CornerRadius = 5,
                Source = icons[i],
            };
            imgBtn.Clicked += commands[i];
            buttons.Add(imgBtn);
        }
        Children.Add(buttons);
    }

    private void BeginningOnClicked(object? sender, EventArgs e)
    {
        if (sender != null && sender is ImageButton imgBtn && imgBtn.Parent is HorizontalStackLayout hsl && hsl.Parent is Moveset moveset &&
            moveset.Children[0] is ScrollView scrollView && scrollView.Content is Label label && label.FormattedText.Spans is IList<Span> spans)
        {
            while (spans[0].Text.Length > 0)
            {
                PreviousOnClicked(sender, e);
            }
        }
    }

    private void EndOnClicked(object? sender, EventArgs e)
    {
        if (sender != null && sender is ImageButton imgBtn && imgBtn.Parent is HorizontalStackLayout hsl && hsl.Parent is Moveset moveset &&
            moveset.Children[0] is ScrollView scrollView && scrollView.Content is Label label && label.FormattedText.Spans is IList<Span> spans)
        {
            while (spans[2].Text.Length > 0)
            {
                NextOnClicked(sender, e);
            }
        }
    }

    private void PreviousOnClicked(object? sender, EventArgs e)
    {
        if (sender != null && sender is ImageButton imgBtn && imgBtn.Parent is HorizontalStackLayout hsl && hsl.Parent is Moveset moveset &&
            moveset.Children[0] is ScrollView scrollView && scrollView.Content is Label label && label.FormattedText.Spans is IList<Span> spans &&
            moveset.Parent is Wrapper wrapper3 && wrapper3.Parent is Window window3 && window3.Parent is AbsoluteLayout body &&
            body.Children[1] is Window window1 && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel)
        {
            string m = spans[1].Text;
            switch (spans[1].Text)
            {
                case "U": m = "U'"; break;
                case "D": m = "D'"; break;
                case "F": m = "F'"; break;
                case "B": m = "B'"; break;
                case "L": m = "L'"; break;
                case "R": m = "R'"; break;
                case "U'": m = "U"; break;
                case "D'": m = "D"; break;
                case "F'": m = "F"; break;
                case "B'": m = "B"; break;
                case "L'": m = "L"; break;
                case "R'": m = "R"; break;
            }
            cubeModel.RotateFace(m);
            if (spans[0].Text.Length > 4) {
                int s0end = spans[0].Text.LastIndexOf("  ", spans[0].Text.Length - 3) + 2;
                int s1end = spans[0].Text.Length-2;
                spans[2].Text = "  " + spans[1].Text + spans[2].Text;
                spans[1].Text = spans[0].Text[s0end..s1end];
                spans[0].Text = spans[0].Text[..s0end];
            }
            else if (spans[0].Text.Length == 4)
            {
                spans[2].Text = "  " + spans[1].Text + spans[2].Text;
                spans[1].Text = ">>";
                spans[0].Text = "";
            }
        }
    }

    private void NextOnClicked(object? sender, EventArgs e)
    {
        if (sender != null && sender is ImageButton imgBtn && imgBtn.Parent is HorizontalStackLayout hsl && hsl.Parent is Moveset moveset &&
            moveset.Children[0] is ScrollView scrollView && scrollView.Content is Label label && label.FormattedText.Spans is IList<Span> spans &&
            moveset.Parent is Wrapper wrapper3 && wrapper3.Parent is Window window3 && window3.Parent is AbsoluteLayout body &&
            body.Children[1] is Window window1 && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel)
        {
            if (spans[2].Text.Length > 0)
            {
                int s1start = Math.Min(2, spans[2].Text.Length);
                int s2start = spans[2].Text.IndexOf("  ", 2);
                if (s2start < 0) s2start = spans[2].Text.Length;
                spans[0].Text = spans[0].Text + spans[1].Text + "  ";
                spans[1].Text = spans[2].Text[s1start..s2start];
                spans[2].Text = spans[2].Text[s2start..];
                cubeModel.RotateFace(spans[1].Text);
            }
        }
    }

    public void DisplayMoves(string moves)
    {
        if (Children[0] is ScrollView scrollView && scrollView.Content is Label label && label.FormattedText is FormattedString ft &&
            (ft.Spans[0].Text + ft.Spans[1].Text + ft.Spans[2].Text)[2..] != moves)
        {
            ft.Spans[0].Text = "";
            ft.Spans[1].Text = ">>";
            ft.Spans[2].Text = "  " + moves;
        }
    }
}