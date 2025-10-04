/*using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    private async void CreateProject(object? sender, EventArgs e)
    {
        if (Parent is AbsoluteLayout body && body.Parent is ContentPage mainPage &&
            body.Children[1] is Window window1 && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel &&
            body.Children[2] is Window window2 && window2.Children[1] is Wrapper wrapper2 && wrapper2.Children[0] is Controls controls &&
            controls.Children[4] is Grid toolbar && toolbar.Children[0] is Editor fieldInput &&
            body.Children[3] is Window window3 && window3.Children[1] is Wrapper wrapper3 && wrapper3.Children[0] is Moveset moveset &&
            moveset.Children[0] is ScrollView scrollView && scrollView.Content is Label label && label.FormattedText.Spans is IList<Span> spans)
        {
            var result = await mainPage.ShowPopupAsync(new SaveConfirmationPopup(
                "Save Changes?",
                "Do you want to save your changes before closing?"
            ));

            switch (result)
            {
                case SaveConfirmationPopup.PopupResult.Save:
                    SaveProject(null, EventArgs.Empty);
                    break;
                case SaveConfirmationPopup.PopupResult.Cancel:
                case null:
                    return;
            }

            cubeModel.Reset();
            fieldInput.Text = "";
            spans[0].Text = "";
            spans[1].Text = ">>";
            spans[2].Text = "";

            path = "";
        }
    }
    
    private async void OpenProject(object? sender, EventArgs e)
    {
        if (Parent is AbsoluteLayout body && body.Parent is ContentPage mainPage)
        {
            var result = await mainPage.ShowPopupAsync(new SaveConfirmationPopup(
                "Save Changes?",
                "Do you want to save your changes before closing?"
            ));

            switch (result)
            {
                case SaveConfirmationPopup.PopupResult.Save:
                    SaveProject(null, EventArgs.Empty);
                    break;
                case SaveConfirmationPopup.PopupResult.Cancel:
                case null:
                    return;
            }

            *//*try
            {*//*
                var pickResult = await FilePicker.Default.PickAsync(new());

                Debug.WriteLine("picked");

                if (pickResult == null)
                {
                    return;
                }

                byte[] data;
                using (var stream = await pickResult.OpenReadAsync())
                {
                    data = new byte[stream.Length];
                    await stream.ReadAsync(data, 0, (int)stream.Length);
                }

                path = pickResult.FullPath;

                GetData(data);
            *//*}
            catch (Exception ex)
            {

            }*//*
        }
    }

    private async void SaveProject(object? sender, EventArgs e)
    {
        try
        {
            byte[] data = GetBytes();
            if (path.Length == 0)
            {
                using MemoryStream stream = new(data);

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
            else
            {
                await File.WriteAllBytesAsync(path, data);
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

    private string GetFailureReason(FileSaverResult result)
    {
        return result.Exception switch
        {
            UnauthorizedAccessException => "Access denied to target location",
            PathTooLongException => "File path exceeds system limits",
            DirectoryNotFoundException => "Target directory does not exist",
            IOException ioEx when (ioEx.HResult & 0xFFFF) == 0x27 => "Disk is full",
            IOException ioEx when (ioEx.HResult & 0xFFFF) == 0x20 => "File is locked or in use",
            NotSupportedException => "Invalid path format",
            _ => "Unknown filesystem rejection"
        };
    }

    private byte[] GetBytes()
    {
        // 3 bits for each cell => 24 bits = 3 bytes for each face => 18 bytes for the cube
        // count the moves in the field text input
        // count the moves in the solution
        if (Parent is AbsoluteLayout body &&
            body.Children[1] is Window window1 && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel &&
            body.Children[2] is Window window2 && window2.Children[1] is Wrapper wrapper2 && wrapper2.Children[0] is Controls controls &&
            controls.Children[4] is Grid toolbar && toolbar.Children[0] is Editor fieldInput &&
            body.Children[3] is Window window3 && window3.Children[1] is Wrapper wrapper3 && wrapper3.Children[0] is Moveset moveset &&
            moveset.Children[0] is ScrollView scrollView && scrollView.Content is Label label && label.FormattedText.Spans is IList<Span> spans)
        {
            string[] moves = [];
            if ((spans[0].Text + spans[1].Text + spans[2].Text).Length > 4) moves = (spans[0].Text + spans[1].Text + spans[2].Text)[4..].Split("  ");
            controls.ToolbarRun(null, EventArgs.Empty);
            string[] sequence = [];
            if (fieldInput.Text != null) sequence = fieldInput.Text.Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int length = 18 + sequence.Length*5/8 + 1 + moves.Length*5/8;
            if (sequence.Length * 5 % 8 != 0) length++;
            if (moves.Length * 5 % 8 != 0) length++;
            byte[] stream = new byte[length];
            int cursor = 0;

            *//*Debug.WriteLine(length);
            Debug.WriteLine(sequence.Length);
            Debug.WriteLine(moves.Length);*//*

            Dictionary<string, byte> colorToByte = new()
            {
                { "#ffffff", 0b000_00000 },  // W
                { "#ffa500", 0b001_00000 },  // O
                { "#00ff00", 0b010_00000 },  // G
                { "#ff0000", 0b011_00000 },  // R
                { "#0000ff", 0b100_00000 },  // B
                { "#ffff00", 0b101_00000 }   // Y
            };
            Dictionary<string, byte> moveToByte = new()
            {
                { "U",  0b00001000 },  
                { "D",  0b00010000 },
                { "F",  0b00011000 },
                { "B",  0b00100000 },
                { "L",  0b00101000 },
                { "R",  0b00110000 },
                { "U2", 0b00111000 },
                { "D2", 0b01000000 },
                { "F2", 0b01001000 },
                { "B2", 0b01010000 },
                { "L2", 0b01011000 },
                { "R2", 0b01100000 },
                { "U'", 0b01101000 },
                { "D'", 0b01110000 },
                { "F'", 0b01111000 },
                { "B'", 0b10000000 },
                { "L'", 0b10001000 },
                { "R'", 0b10010000 }
            };

            for (int i=2; i<=7; i++)
            {
                if (cubeModel.Children[i] is Grid face)
                {
                    for (int j=0; j<9; j++)
                    {
                        if (j == 4) continue;
                        if (face[j] is BoxView cell)
                        {
                            byte data = colorToByte[cell.BackgroundColor.ToHex().ToLower()];
                            stream[cursor / 8] |= (byte)(data >> (cursor % 8));
                            cursor += 3;
                            if (0 < cursor % 8 && cursor % 8 <= 2) stream[cursor / 8] |= (byte)(data << (3 - cursor % 8));
                        }
                    }
                }
            }

            for (int i = 0; i < length; i++)
            {
                string binary = Convert.ToString(stream[i], 2).PadLeft(8, '0');
                Debug.WriteLine(binary);
            }
            Debug.WriteLine("\n");

            for (int i=0; i<sequence.Length; i++)
            {
                byte data = moveToByte[sequence[i]];
                stream[cursor / 8] |= (byte)(data >> (cursor % 8));
                cursor += 5;
                if (0 < cursor % 8 && cursor % 8 <= 4) stream[cursor / 8] |= (byte)(data << (5 - cursor % 8));
            }
            for (; cursor % 8 == 0; cursor++) ;
            cursor += 8;

            for (int i=0; i<moves.Length; i++)
            {
                byte data = moveToByte[moves[i]];
                stream[cursor / 8] |= (byte)(data >> (cursor % 8));
                cursor += 5;
                if (0 < cursor % 8 && cursor % 8 <= 4) stream[cursor / 8] |= (byte)(data << (5 - cursor % 8));
            }

            for (int i = 0; i < length; i++)
            {
                string binary = Convert.ToString(stream[i], 2).PadLeft(8, '0');
                Debug.WriteLine(binary);
            }
            Debug.WriteLine("\n");

            path = "";

            return stream;
        }
        return [];
    }

    private void GetData(byte[] stream)
    {
        if (Parent is AbsoluteLayout body &&
            body.Children[1] is Window window1 && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel &&
            body.Children[2] is Window window2 && window2.Children[1] is Wrapper wrapper2 && wrapper2.Children[0] is Controls controls &&
            controls.Children[4] is Grid toolbar && toolbar.Children[0] is Editor fieldInput &&
            body.Children[3] is Window window3 && window3.Children[1] is Wrapper wrapper3 && wrapper3.Children[0] is Moveset moveset &&
            moveset.Children[0] is ScrollView scrollView && scrollView.Content is Label label && label.FormattedText.Spans is IList<Span> spans)
        {
            byte current;
            int cursor = 0, separator = 0;

            Dictionary<byte, string> byteToColor = new()
            {
                { 0b00000000, "#ffffff" },
                { 0b00100000, "#ffa500" },
                { 0b01000000, "#00ff00" },
                { 0b01100000, "#ff0000" },
                { 0b10000000, "#0000ff" },
                { 0b10100000, "#ffff00" }
            };
            Dictionary<byte, string> byteToMove = new()
            {
                { 0b00001000, "U"  },
                { 0b00010000, "D"  },
                { 0b00011000, "F"  },
                { 0b00100000, "B"  },
                { 0b00101000, "L"  },
                { 0b00110000, "R"  },
                { 0b00111000, "U2" },
                { 0b01000000, "D2" },
                { 0b01001000, "F2" },
                { 0b01010000, "B2" },
                { 0b01011000, "L2" },
                { 0b01100000, "R2" },
                { 0b01101000, "U'" },
                { 0b01110000, "D'" },
                { 0b01111000, "F'" },
                { 0b10000000, "B'" },
                { 0b10001000, "L'" },
                { 0b10010000, "R'" }
            };

            for (int i = 2; i <= 7; i++)
            {
                if (cubeModel.Children[i] is Grid face)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (j == 4) continue;
                        if (face[j] is BoxView cell)
                        {
                            current = (byte)((stream[cursor / 8] << (cursor % 8)) & 0b11100000);
                            cursor += 3;
                            if (0 < cursor % 8 && cursor % 8 <= 2) current  |= (byte)((stream[cursor / 8] >> (3 - cursor % 8)) & 0b11100000);
                            cell.BackgroundColor = Color.FromArgb(byteToColor[current]);
                        }
                    }
                }
            }
            //cubeModel.Configure(true);

            Debug.WriteLine(cursor);

            *//*for (int i = stream.Length-1; i>=0; i--)
            {
                if (stream[i] == 0)
                {
                    separator = i*8;
                }
            }

            Debug.WriteLine(separator);*/

            /*for (; cursor<separator;)
            {
                Debug.WriteLine(cursor);
                current = (byte)((stream[cursor / 8] << (cursor % 8)) & 0b11111000);
                cursor += 5;
                if (0 < cursor % 8 && cursor % 8 <= 4) current |= (byte)((stream[cursor / 8] >> (5 - cursor % 8)) & 0b11111000);
                fieldInput.Text += byteToMove[current] + " ";
            }

            for (; cursor % 8 != 0; cursor++) ;
            
            for (; cursor<separator;)
            {
                current = (byte)((stream[cursor / 8] << (cursor % 8)) & 0b11111000);
                cursor += 5;
                if (0 < cursor % 8 && cursor % 8 <= 4) current |= (byte)((stream[cursor / 8] >> (5 - cursor % 8)) & 0b11111000);
                spans[2].Text += byteToMove[current] + "  ";
            }*//*
        }
    }
}
*/