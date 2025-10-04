/*using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using System.Text;

namespace Rubik_s_Cube_Solver_MAUI.Components;

public class Menu : HorizontalStackLayout
{
	int height = 25;
    string path = "";

    protected override void OnParentSet()
    {
        base.OnParentSet();

        string[] text = [ "New", "Open", "Save" ];
        EventHandler[] commands = [ CreateProject, OpenProject, SaveProject ];
		for (int i=0; i<3; i++)
		{
            Button btn = new()
            {
                Text = text[i],
                FontSize = 12,
                Padding = new Thickness(0),
                CornerRadius = 0,
                TextColor = Colors.Black,
                BackgroundColor = Colors.White,
                HeightRequest = height,
                MinimumHeightRequest = height
            };

			PointerGestureRecognizer pgr = new();
            pgr.PointerEntered += (s, e) => { if (s is Button button) button.BackgroundColor = Colors.WhiteSmoke; };
			pgr.PointerExited += (s, e) => { if (s is Button button) button.BackgroundColor = Colors.White; };
			btn.GestureRecognizers.Add(pgr);

            btn.Clicked += commands[i];

			Children.Add(btn);
        }

		BackgroundColor = Colors.White;
		HeightRequest = height;
	}

    private void CreateProject(object? sender, EventArgs e)
    {

    }
    
    private async void OpenProject(object? sender, EventArgs e)
    {
        if (Parent is AbsoluteLayout body && body.Parent is ContentPage mainPage)
        {
            var result = await mainPage.ShowPopupAsync(new WindowsStyledPopup(
                "Save Changes?",
                "Do you want to save your changes before closing?"
            ));

            switch (result)
            {
                case WindowsStyledPopup.PopupResult.Save:
                    SaveProject(null, EventArgs.Empty);
                    break;
                case WindowsStyledPopup.PopupResult.Cancel:
                case null:
                    return;
            }
        }

        *//*var fileResult = await FilePicker.Default.PickAsync();
        if (fileResult == null) return;
        byte[] fileBytes = await File.ReadAllBytesAsync(fileResult.FullPath);*//*
    }

    private async void SaveProject(object? sender, EventArgs e)
    {
        if (path.Length == 0)
        {
            try
            {
                var fileContent = "This is the file content";
                using var stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(fileContent));

                FileSaverResult fileSaverResult = await FileSaver.Default.SaveAsync(
                    $"RubiksCube_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.rks",
                    stream,
                    new CancellationToken());

                if (fileSaverResult.IsSuccessful)
                {
                    path = fileSaverResult.FilePath;
                }
                else if (fileSaverResult.Exception != null && Parent is AbsoluteLayout body && body.Parent is ContentPage mainPage)
                {
                    string failureReason = GetFailureReason(fileSaverResult);
                    if (failureReason != "Unknown filesystem rejection") await mainPage.DisplayAlert("Warning", "File not saved (reason: " + GetFailureReason(fileSaverResult) + ")", "OK");
                }
            }
            catch (Exception ex)
            {
                if (Parent is AbsoluteLayout body && body.Parent is ContentPage mainPage)
                {
                    await mainPage.DisplayAlert("Error", $"Failed to save file: {ex.Message}", "OK");
                }
            }
        }
        else
        {
            try
            {
                //await File.WriteAllBytes
            }
            catch (Exception ex)
            {

            }
        }
    }

    private string GetFailureReason(FileSaverResult result)
    {
        return result.Exception switch
        {
            UnauthorizedAccessException => "Access denied to target location",
            PathTooLongException => "File path exceeds system limits",
            DirectoryNotFoundException => "Target directory does not exist",
            IOException => "File is locked or in use",
            NotSupportedException => "Invalid path format",
            _ => "Unknown filesystem rejection"
        };
    }

    private byte[] GetBytes()
    {
        // 3 bits for each cell => 24 bits = 3 bytes for each face => 18 bytes for the cube
        // count the moves in the field text input
        // count the moves in the solution
        if (Parent is AbsoluteLayout body && body.Children[1] is Window window1 && window1.Children[0] is Wrapper wrapper1 &&
            wrapper1.Children[0] is CubeModelFlat cubeModel && body.Children[2] is Window window2 && window2.Children[0] is Wrapper wrapper2 &&
            wrapper2.Children[0] is Controls controls && controls.Children[4] is Grid toolbar && toolbar.Children[0] is Editor fieldInput &&
            body.Children[3] is Wrapper wrapper3 && wrapper3.Children[0] is Moveset moveset && moveset.Children[0] is ScrollView scrollView &&
            scrollView.Content is Label label && label.FormattedText.Spans is IList<Span> spans)
        {
            string[] moves = (spans[0].Text + spans[1].Text + spans[3])[4..].Split("  ");
            byte[] utf16Bytes = Encoding.Unicode.GetBytes(fieldInput.Text);

            int length = 18 + utf16Bytes.Length + moves.Length;
            byte[] stream = new byte[length];
            int cursor = 0;
            
            for (int i=2; i<=7; i++)
            {
                if (cubeModel.Children[i] is Grid face)
                {
                    for (int j=0; j<9; j++)
                    {
                        if (j == 4) continue;
                        if (face[j] is BoxView cell)
                        {
                            switch (cell.BackgroundColor.ToHex())
                            {
                                case "#ffffff":
                                    cursor += 3;
                                    break;
                                case "#ffa500":
                                    cursor += 2;
                                    stream[cursor/8] = (byte)(stream[cursor/8] | (1 << (7 - cursor % 8)));
                                    cursor++;
                                    break;
                                case "#00ff00":
                                    cursor++;
                                    stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                                    cursor += 2;
                                    break;
                                case "#ff0000":
                                    cursor++;
                                    stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                                    cursor++;
                                    stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                                    cursor++;
                                    break;
                                case "#0000ff":
                                    stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                                    cursor += 3;
                                    break;
                                case "#ffff00":
                                    stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                                    cursor += 2;
                                    stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                                    cursor++;
                                    break;
                            }
                        }
                    }
                }
            }

            Array.Copy(utf16Bytes, 0, stream, cursor / 8, utf16Bytes.Length);
            cursor += utf16Bytes.Length * 8;
            stream[cursor / 8] = 1;

            for (int i=0; i<moves.Length; i++)
            {
                switch (moves[i])
                {
                    case "U":
                        cursor += 4;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        break;
                    case "D":
                        cursor += 3;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 2;
                        break;
                    case "F":
                        cursor += 3;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        break;
                    case "B":
                        cursor += 2;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 3;
                        break;
                    case "L":
                        cursor += 2;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 2;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        break;
                    case "R":
                        cursor += 2;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 2;
                        break;
                    case "U2":
                        cursor += 2;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        break;
                    case "D2":
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 4;
                        break;
                    case "F2":
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 3;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        break;
                    case "B2":
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 2;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 2;
                        break;
                    case "L2":
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 2;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        break;
                    case "R2":
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 3;
                        break;
                    case "U'":
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 2;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        break;
                    case "D'":
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 2;
                        break;
                    case "F'":
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        break;
                    case "B'":
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 5;
                        break;
                    case "L'":
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 4;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor++;
                        break;
                    case "R'":
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 3;
                        stream[cursor / 8] = (byte)(stream[cursor / 8] | (1 << (7 - cursor % 8)));
                        cursor += 2;
                        break;
                }
            }

            return stream;
        }
        return [];
    }
}
*/