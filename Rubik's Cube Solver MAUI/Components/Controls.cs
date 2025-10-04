using Rubik_s_Cube_Solver_MAUI.Core;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Rubik_s_Cube_Solver_MAUI.Components;

public class Controls : HorizontalStackLayout
{
    private static double buttonSize;
    private double width, height;
    private string currentColor = "";

    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (Parent is Wrapper wrapper)
        {
            if (wrapper.gte136)
            {
                buttonSize = 45 * wrapper.windowHeight / 711;
                width = 15 * buttonSize;
                height = 3 * buttonSize;
            }
            else
            {
                buttonSize = 0;
                width = 0;
                height = 0;
            }
        }

        WidthRequest = width;
        HeightRequest = height;

        AddPaletteMenu();

        Children.Add(new BoxView()
        {
            WidthRequest = buttonSize,
            HeightRequest = 3 * buttonSize,
            BackgroundColor = Colors.Transparent
        });

        AddMoveMenu();
        
        Children.Add(new BoxView()
        {
            WidthRequest = buttonSize,
            HeightRequest = 3 * buttonSize,
            BackgroundColor = Colors.Transparent
        });

        AddToolbarMenu();
    }

    private void AddPaletteMenu()
    {
        Grid cells = new()
        {
            WidthRequest = 3 * buttonSize,
            HeightRequest = 3 * buttonSize,
            RowDefinitions = { new RowDefinition(), new RowDefinition(), new RowDefinition() },
            ColumnDefinitions = { new ColumnDefinition(), new ColumnDefinition(), new ColumnDefinition() },
            BackgroundColor = Colors.Gray
        };

        string[] colors = ["#ffffff", "#ffff00", "#ffa500", "#ff0000", "#00ff00", "#0000ff"];
        for (int col = 0; col < 3; col++)
        {
            for (int row = 0; row < 2; row++)
            {
                BoxView colorButton = new()
                {
                    WidthRequest = buttonSize - 2,
                    HeightRequest = buttonSize - 2,
                    CornerRadius = 2,
                    BackgroundColor = Color.FromArgb(colors[2 * col + row])
                };
                cells.Add(colorButton, col, row);

                PointerGestureRecognizer pgr = new();
                pgr.PointerPressed += ColorOnPointerPressed;
                pgr.PointerReleased += ColorOnPointerReleased;
                colorButton.GestureRecognizers.Add(pgr);
            }
        }

        Children.Add(cells);
	}

    private void ColorOnPointerPressed(object? sender, PointerEventArgs e)
    {
        if (sender != null && sender is BoxView btn)
        {
            currentColor = btn.BackgroundColor.ToHex().ToLower();
        }
    }

    private void ColorOnPointerReleased(object? sender, PointerEventArgs e)
    {
        if (sender != null && sender is BoxView btn &&
            currentColor == btn.BackgroundColor.ToHex().ToLower() &&
            btn.Parent is Grid cells
        )
        {
            cells.BackgroundColor = btn.BackgroundColor;

            if (Parent is Wrapper wrapper2 && wrapper2.Parent is Window window2 && window2.Parent is AbsoluteLayout body &&
                body.Children[1] is Window window1 && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel)
            {
                cubeModel.SelectedColor = btn.BackgroundColor;
            }
        }
    }

    private void AddMoveMenu()
    {
        Grid cells = new()
        {
            WidthRequest = 4 * buttonSize,
            HeightRequest = 3 * buttonSize,
            RowDefinitions = { new RowDefinition(), new RowDefinition(), new RowDefinition() },
            ColumnDefinitions = { new ColumnDefinition(), new ColumnDefinition(), new ColumnDefinition(), new ColumnDefinition() }
        };

        string[] moves = [ "U", "D", "U'", "D'", "F", "B", "F'", "B'", "L", "R", "L'", "R'" ];
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                Button moveButton = new()
                {
                    Text = moves[4 * row + col],
                    WidthRequest = buttonSize - 2,
                    HeightRequest = buttonSize - 2,
                    CornerRadius = 2,
                    BackgroundColor = Colors.DarkSlateBlue
                };

                moveButton.Clicked += MoveOnClicked;

                cells.Add(moveButton, col, row);
            }
        }
        Children.Add(cells);
    }

    private void MoveOnClicked(object? sender, EventArgs e)
    {
        if (sender != null && sender is Button btn && Parent is Wrapper wrapper2 && wrapper2.Parent is Window window2 &&
            window2.Parent is AbsoluteLayout body && body.Children[1] is Window window1 &&
            window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel)
        {
            cubeModel.RotateFace(btn.Text);
        }
    }

    private void AddToolbarMenu()
    {
        Grid toolbar = new()
        {
            WidthRequest = 6 * buttonSize,
            HeightRequest = 3 * buttonSize,
            RowDefinitions = { new RowDefinition(), new RowDefinition(), new RowDefinition() },
            ColumnDefinitions = { new ColumnDefinition(), new ColumnDefinition(), new ColumnDefinition(), new ColumnDefinition() }
        };

        Editor input = new()
        {
            AutoSize = EditorAutoSizeOption.TextChanges,
            WidthRequest = 6 * buttonSize - 2,
            HeightRequest = 2 * buttonSize - 2,
            BackgroundColor = Colors.LightGray
        };
        Grid.SetRow(input, 0);
        Grid.SetColumn(input, 0);
        Grid.SetRowSpan(input, 2);
        Grid.SetColumnSpan(input, 4);
        toolbar.Add(input);

        string[] names = [ "Run", "Reset", "Scramble", "Solve" ];
        EventHandler[] commands = [ ToolbarRun, ToolbarReset, ToolbarScramble, ToolbarSolve ];
        for (int i=0; i<4; i++)
        {
            Button command = new()
            {
                Text = names[i],
                WidthRequest = 3 * buttonSize / 2 - 2,
                HeightRequest = buttonSize - 2,
                CornerRadius = 2,
                FontSize = 13,
                Padding = new Thickness(0, 0, 0, 0),
                BackgroundColor = Colors.Gray
            };
            command.Clicked += commands[i];
            toolbar.Add(command, i, 2);
        }

        Children.Add(toolbar);
    }

    public void ToolbarRun(object? sender, EventArgs e)
    {
        if (Children[4] is Grid toolbar && toolbar.Children[0] is Editor input && input.Text != null &&
            Parent is Wrapper wrapper2 && wrapper2.Parent is Window window2 && window2.Parent is AbsoluteLayout body &&
            body.Children[1] is Window window1 && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel)
        {
            string moves = Regex.Replace(input.Text, @"\s+", "").ToUpper();
            input.Text = "";
            int i;
            for (i=0; i < moves.Length-1; i++)
            {
                string move = moves[i..(i+2)];
                if ("U2 D2 F2 B2 L2 R2 U' D' F' B' L' R'".Contains(move))
                {
                    cubeModel.RotateFace(move);
                    input.Text += move + " ";
                    i++;
                }
                else if ("UDFBLR".Contains(move[0]))
                {
                    cubeModel.RotateFace($"{move[0]}");
                    input.Text += move[0] + " ";
                }
            }
            if (i < moves.Length && "UDFBLR".Contains(moves[moves.Length-1]))
            {
                cubeModel.RotateFace($"{moves[moves.Length-1]}");
                input.Text += moves[moves.Length - 1] + " ";
            }
        }
    }

    private void ToolbarReset(object? sender, EventArgs e)
    {
        if (Parent is Wrapper wrapper2 && wrapper2.Parent is Window window2 &&
            window2.Parent is AbsoluteLayout body && body.Children[1] is Window window1
            && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel)
        {
            cubeModel.Reset();
        }
    }

    private void ToolbarScramble(object? sender, EventArgs e)
    {
        if (Parent is Wrapper wrapper2 && wrapper2.Parent is Window window2 &&
            window2.Parent is AbsoluteLayout body && body.Children[1] is Window window1
            && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel)
        {
            cubeModel.Scramble();
        }
    }

    private async void ToolbarSolve(object? sender, EventArgs e)
    {
        if (Parent is Wrapper wrapper2 && wrapper2.Parent is Window window2 && window2.Parent is AbsoluteLayout body &&
            body.Children[1] is Window window1 && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel &&
            body.Children[3] is Window window3 && window3.Children[1] is Wrapper wrapper3 && wrapper3.Children[0] is Moveset moveset)
        {
            string moves = await cubeModel.Solve();
            if (moves.Length > 0) moveset.DisplayMoves(moves);
        }
    }
}