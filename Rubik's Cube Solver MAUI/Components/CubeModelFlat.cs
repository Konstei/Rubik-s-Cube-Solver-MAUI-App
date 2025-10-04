using Rubik_s_Cube_Solver_MAUI.Core;
using Rubik_s_Cube_Solver_MAUI.Core.Solver;
using System.Diagnostics;

namespace Rubik_s_Cube_Solver_MAUI.Components;

public class CubeModelFlat : AbsoluteLayout
{
    private double width = 452, height = 606, startX = 40, startY = 40, faceLength = 150;

    private Color _selectedColor = Colors.Gray;
    public Color SelectedColor
    {
        get { return _selectedColor; }
        set { _selectedColor = value; }
    }

    int currentFace = -1, currentCell = -1;

    private Cube cube = new();


    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (Parent is Wrapper wrapper)
        {
            if (wrapper.gte136)
            {
                width = 452 * wrapper.windowHeight / 711;
                height = 606 * wrapper.windowHeight / 711;
                startX = 40 * wrapper.windowHeight / 711;
                startY = 40 * wrapper.windowHeight / 711;
                faceLength = 150 * wrapper.windowHeight / 711;
            }
            else
            {
                width = 0;
                height = 0;
                startX = 0;
                startY = 0;
                faceLength = 0;
            }
        }

        WidthRequest = width;
        HeightRequest = height;

        AbsoluteLayout.SetLayoutBounds(this, new(
            startX,
            startY,
            width,
            height
        ));

        BoxView borderWide = new()
        {
            WidthRequest = 4*faceLength+5,
            HeightRequest = faceLength+2,
            BackgroundColor = Colors.Black
        };
        BoxView borderTall = new()
        {
            WidthRequest = faceLength+2,
            HeightRequest = 3*faceLength+4,
            BackgroundColor = Colors.Black
        };

        AbsoluteLayout.SetLayoutBounds(borderWide, new(-1,             faceLength,  4*faceLength+5,  faceLength+2  ));
        AbsoluteLayout.SetLayoutBounds(borderTall, new(faceLength,  -1,             faceLength+2,    3*faceLength+4));

        Children.Add(borderWide);
        Children.Add(borderTall);

        string[][][] colors =
        [
            [
                [ "#ffffff", "#ffffff", "#ffffff" ],
                [ "#ffffff", "#ffffff", "#ffffff" ],
                [ "#ffffff", "#ffffff", "#ffffff" ]
            ],
            [
                [ "#ffa500", "#ffa500", "#ffa500" ],
                [ "#ffa500", "#ffa500", "#ffa500" ],
                [ "#ffa500", "#ffa500", "#ffa500" ]
            ],
            [
                [ "#00ff00", "#00ff00", "#00ff00" ],
                [ "#00ff00", "#00ff00", "#00ff00" ],
                [ "#00ff00", "#00ff00", "#00ff00" ]
            ],
            [
                [ "#ff0000", "#ff0000", "#ff0000" ],
                [ "#ff0000", "#ff0000", "#ff0000" ],
                [ "#ff0000", "#ff0000", "#ff0000" ]
            ],
            [
                [ "#0000ff", "#0000ff", "#0000ff" ],
                [ "#0000ff", "#0000ff", "#0000ff" ],
                [ "#0000ff", "#0000ff", "#0000ff" ]
            ],
            [
                [ "#ffff00", "#ffff00", "#ffff00" ],
                [ "#ffff00", "#ffff00", "#ffff00" ],
                [ "#ffff00", "#ffff00", "#ffff00" ]
            ],
        ];

        DrawFace(faceLength+1,   0,              faceLength, faceLength, colors[0]);
        DrawFace(0,              faceLength+1,   faceLength, faceLength, colors[1]);
        DrawFace(faceLength+1,   faceLength+1,   faceLength, faceLength, colors[2]);
        DrawFace(2*faceLength+2, faceLength+1,   faceLength, faceLength, colors[3]);
        DrawFace(3*faceLength+3, faceLength+1,   faceLength, faceLength, colors[4]);
        DrawFace(faceLength+1,   2*faceLength+2, faceLength, faceLength, colors[5]);
	}

    private void DrawFace(double x, double y, double width, double height, string[][] colors)
    {
        Grid face = new()
        {
            RowDefinitions = { new RowDefinition(), new RowDefinition(), new RowDefinition() },
            ColumnDefinitions = { new ColumnDefinition(), new ColumnDefinition(), new ColumnDefinition() },
            RowSpacing = 1,
            ColumnSpacing = 1
        };

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var boxView = new BoxView { BackgroundColor = Color.FromArgb(colors[row][col]) };
                face.Add(boxView, col, row);

                if (row != 1 || col != 1)
                {
                    PointerGestureRecognizer pgr = new();
                    pgr.PointerPressed += PaintOnPointerPressed;
                    pgr.PointerReleased += PaintOnPointerReleased;
                    boxView.GestureRecognizers.Add(pgr);
                }
            }
        }

        AbsoluteLayout.SetLayoutBounds(face, new(x, y, width, height));
        Children.Add(face);
    }

    private void PaintOnPointerPressed(object? sender, PointerEventArgs e)
    {
        if (sender != null && sender is BoxView cell && cell.Parent is Grid face && face.Parent is AbsoluteLayout cube)
        {
            currentCell = face.Children.IndexOf(cell);
            currentFace = cube.Children.IndexOf(face);
        }
    }

    private void PaintOnPointerReleased(object? sender, PointerEventArgs e)
    {
        if (sender != null && sender is BoxView cell && cell.Parent is Grid face && face.Parent is AbsoluteLayout cube &&
            currentCell == face.Children.IndexOf(cell) && currentFace == cube.Children.IndexOf(face))
        {
            cell.BackgroundColor = SelectedColor;
        }
    }

    public async Task<string> Solve()
    {
        Cube test = new();
        bool ok = false;
        for (int i=0; i<6; i++)
        {
            for (int j=0; j<8; j++)
            {
                if (cube.Faces[i][j] != test.Faces[i][j])
                {
                    ok = true;
                    break;
                }
            }
            if (ok) break;
        }
        if (!ok) return "";
        List<Cube.Color> scrambledMoves = new(cube.Moves);
        cube.Moves.Clear();

        if (Parent is Wrapper wrapper && wrapper.Parent is Window window && window.Parent is AbsoluteLayout body && body.Parent is ContentPage mainPage)
        {
            (int, Cube.Color, Cube.Color, Cube.Color, int) values = cube.Validate();
            switch (values.Item1)
            {
                case 1:
                    await mainPage.DisplayAlert("Unsolvable state", "Each color must appear exactly 9 times", "OK");
                    break;

                case 2:
                    await mainPage.DisplayAlert("Unsolvable state", $"Edge cannot exist ( {values.Item2}, {values.Item3} )", "OK");
                    break;

                case 3:
                    await mainPage.DisplayAlert(
                        "Unsolvable state",
                        $"An edge cannot exist more than once, ( {values.Item2}, {values.Item3} ) appears {values.Item5} times",
                        "OK");
                    break;

                case 4:
                    await mainPage.DisplayAlert(
                        "Unsolvable state",
                        $"Corner cannot exist: ( {values.Item2}, {values.Item3}, {values.Item4} )",
                        "OK");
                    break;

                case 5:
                    await mainPage.DisplayAlert(
                        "Unsolvable state",
                        "Corner twist detected: One or more corners are misaligned, making the cube unsolvable.\n" +
                        "To verify: For each corner, count the twists needed to align its colors with their respective faces.\n" +
                        "If the total twists (modulo 3) are not zero, the cube cannot be solved.\n" +
                        "Please check and adjust your cube, then try again.",
                        "OK");
                    break;

                case 6:
                    await mainPage.DisplayAlert(
                        "Unsolvable state",
                        "Permutation parity error detected: The cube's piece permutation is inconsistent, making it unsolvable.\n" +
                        "To verify: Count the number of swaps needed to restore a valid permutation of all pieces.\n" +
                        "If the total number of swaps is odd, the cube cannot be solved in a legal state.\n" +
                        "Please check for incorrect sticker placements or reassembly errors, then try again.",
                        "OK");
                    break;

                default:
                    break;
            }
            if (values.Item1 != 0)
            {
                return "";
            }

            WhiteCross.Solve(cube);
            FirstLayer.Solve(cube);
            SecondLayer.Solve(cube);
            if (!LastLayer.Solve(cube))
            {
                await mainPage.DisplayAlert(
                    "Unsolvable state",
                    "Edge permutation error detected: The cube's edge orientations are inconsistent, making it unsolvable.\n" +
                    "To verify: Count the number of edge swaps needed to restore a valid permutation.\n" +
                    "If the total is odd, the cube cannot be solved in a legal state.\n" +
                    "Please check for incorrect sticker placements or reassembly errors, then try again.",
                    "OK");
                return "";
            }

            Configure(true);
        }

        List<Cube.Move> solvedMoves = cube.ColorsToMoves();
        cube.Moves.Clear();

        string stringMoves = string.Join(" ", solvedMoves)
            .Replace("U U", "U2")
            .Replace("U2 U2", "")
            .Replace("  ", " ")
            .Replace("U2 U", "U'")

            .Replace("D D", "D2")
            .Replace("D2 D2", "")
            .Replace("  ", " ")
            .Replace("D2 D", "D'")

            .Replace("F F", "F2")
            .Replace("F2 F2", "")
            .Replace("  ", " ")
            .Replace("F2 F", "F'")

            .Replace("B B", "B2")
            .Replace("B2 B2", "")
            .Replace("  ", " ")
            .Replace("B2 B", "B'")

            .Replace("L L", "L2")
            .Replace("L2 L2", "")
            .Replace("  ", " ")
            .Replace("L2 L", "L'")

            .Replace("R R", "R2")
            .Replace("R2 R2", "")
            .Replace("  ", " ")
            .Replace("R2 R", "R'")

            .Replace(" ", "  ");


        return stringMoves;
    }

    public void Reset()
    {
        cube.Reset();
        string[] colors = ["#ffffff", "#ffa500", "#00ff00", "#ff0000", "#0000ff", "#ffff00"];
        for (int f = 2; f < 8; f++)
        {
            if (Children[f] is Grid face)
            {
                for (int c = 0; c < 9 && face.Children[c] is BoxView cell; c++)
                {
                    cell.BackgroundColor = Color.FromArgb(colors[f - 2]);
                }
            }
        }
    }

    public void Scramble()
    {
        string[] moves = [ "U", "D", "F", "B", "L", "R", "U2", "D2", "F2", "B2", "L2", "R2", "U'", "D'", "F'", "B'", "L'", "R'" ];
        Random rand = new Random(Guid.NewGuid().GetHashCode());
        int numberOfMoves = rand.Next(15, 26);
        for (int i=0; i<numberOfMoves; i++)
        {
            RotateFace(moves[rand.Next(18)]);
        }
    }

    public void RotateFace(string move)
    {
        Cube.Color color = Cube.Color.NONE;
        int times = 1;
        switch (move)
        {
            case "U":
                color = Cube.Color.WHITE;
                break;
            case "L":
                color = Cube.Color.ORANGE;
                break;
            case "F":
                color = Cube.Color.GREEN;
                break;
            case "R":
                color = Cube.Color.RED;
                break;
            case "B":
                color = Cube.Color.BLUE;
                break;
            case "D":
                color = Cube.Color.YELLOW;
                break;
            case "U2":
                color = Cube.Color.WHITE;
                times = 2;
                break;
            case "L2":
                color = Cube.Color.ORANGE;
                times = 2;
                break;
            case "F2":
                color = Cube.Color.GREEN;
                times = 2;
                break;
            case "R2":
                color = Cube.Color.RED;
                times = 2;
                break;
            case "B2":
                color = Cube.Color.BLUE;
                times = 2;
                break;
            case "D2":
                color = Cube.Color.YELLOW;
                times = 2;
                break;
            case "U'":
                color = Cube.Color.WHITE;
                times = 3;
                break;
            case "L'":
                color = Cube.Color.ORANGE;
                times = 3;
                break;
            case "F'":
                color = Cube.Color.GREEN;
                times = 3;
                break;
            case "R'":
                color = Cube.Color.RED;
                times = 3;
                break;
            case "B'":
                color = Cube.Color.BLUE;
                times = 3;
                break;
            case "D'":
                color = Cube.Color.YELLOW;
                times = 3;
                break;
        }
        for (int i=0; i<times; i++) cube.RotateFace(color);
        cube.Configure();
        Configure();
    }

    public void Configure(bool reverse = false)
    {
        if (reverse)
        {
            for (int f = 0; f < 6; f++)
            {
                cube.Faces[f][0] = ColorObjectToEnum(((BoxView)((Grid)Children[f + 2]).Children[0]).BackgroundColor);
                cube.Faces[f][1] = ColorObjectToEnum(((BoxView)((Grid)Children[f + 2]).Children[1]).BackgroundColor);
                cube.Faces[f][2] = ColorObjectToEnum(((BoxView)((Grid)Children[f + 2]).Children[2]).BackgroundColor);
                cube.Faces[f][3] = ColorObjectToEnum(((BoxView)((Grid)Children[f + 2]).Children[3]).BackgroundColor);
                cube.Faces[f][4] = ColorObjectToEnum(((BoxView)((Grid)Children[f + 2]).Children[5]).BackgroundColor);
                cube.Faces[f][5] = ColorObjectToEnum(((BoxView)((Grid)Children[f + 2]).Children[6]).BackgroundColor);
                cube.Faces[f][6] = ColorObjectToEnum(((BoxView)((Grid)Children[f + 2]).Children[7]).BackgroundColor);
                cube.Faces[f][7] = ColorObjectToEnum(((BoxView)((Grid)Children[f + 2]).Children[8]).BackgroundColor);
            }
            return;
        }
        for (int f = 0; f < 6; f++)
        {
            ((BoxView)((Grid)Children[f + 2]).Children[0]).BackgroundColor = ColorEnumToObject(cube.Faces[f][0]);
            ((BoxView)((Grid)Children[f + 2]).Children[1]).BackgroundColor = ColorEnumToObject(cube.Faces[f][1]);
            ((BoxView)((Grid)Children[f + 2]).Children[2]).BackgroundColor = ColorEnumToObject(cube.Faces[f][2]);
            ((BoxView)((Grid)Children[f + 2]).Children[3]).BackgroundColor = ColorEnumToObject(cube.Faces[f][3]);
            ((BoxView)((Grid)Children[f + 2]).Children[5]).BackgroundColor = ColorEnumToObject(cube.Faces[f][4]);
            ((BoxView)((Grid)Children[f + 2]).Children[6]).BackgroundColor = ColorEnumToObject(cube.Faces[f][5]);
            ((BoxView)((Grid)Children[f + 2]).Children[7]).BackgroundColor = ColorEnumToObject(cube.Faces[f][6]);
            ((BoxView)((Grid)Children[f + 2]).Children[8]).BackgroundColor = ColorEnumToObject(cube.Faces[f][7]);
        }
    }

    private Cube.Color ColorObjectToEnum(Color color)
    {
        switch (color.ToHex().ToLower())
        {
            case "#ffffff": return Cube.Color.WHITE;
            case "#ffa500": return Cube.Color.ORANGE;
            case "#00ff00": return Cube.Color.GREEN;
            case "#ff0000": return Cube.Color.RED;
            case "#0000ff": return Cube.Color.BLUE;
            case "#ffff00": return Cube.Color.YELLOW;
        }
        return Cube.Color.NONE;
    }

    private Color ColorEnumToObject(Cube.Color color)
    {
        switch (color)
        {
            case Cube.Color.WHITE: return Color.FromArgb("#ffffff");
            case Cube.Color.ORANGE: return Color.FromArgb("#ffa500");
            case Cube.Color.GREEN: return Color.FromArgb("#00ff00");
            case Cube.Color.RED: return Color.FromArgb("#ff0000");
            case Cube.Color.BLUE: return Color.FromArgb("#0000ff");
            case Cube.Color.YELLOW: return Color.FromArgb("#ffff00");
        }
        return Colors.Transparent;
    }

    public void Test()
    {
        if (Children[2] is Grid face && face.Children[0] is BoxView cell)
        {
            cell.BackgroundColor = Colors.Aqua;
        }
    }
}
