using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Views;
using Rubik_s_Cube_Solver_MAUI.Core;
using System.Diagnostics;
using System.Text;

namespace Rubik_s_Cube_Solver_MAUI.Components;

public class Menu : HorizontalStackLayout
{
	private double height = 25;
    private string path = "";

    public double hHeight
    {
        get { return height ; }
        set { height = value; }
    }

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

            try
            {
                var pickResult = await FilePicker.Default.PickAsync(new());

                if (pickResult == null)
                {
                    return;
                }

                cubeModel.Reset();

                string data = await File.ReadAllTextAsync(pickResult.FullPath);

                Debug.WriteLine(data);

                for (int f=0; f<6; f++)
                {
                    if (cubeModel.Children[f+2] is Grid face)
                    {
                        ((BoxView)face.Children[0]).BackgroundColor = Color.FromArgb(data[(49 * f + 7 * (f + 0))..(49 * f + 7 * (f + 1))]);
                        ((BoxView)face.Children[1]).BackgroundColor = Color.FromArgb(data[(49 * f + 7 * (f + 1))..(49 * f + 7 * (f + 2))]);
                        ((BoxView)face.Children[2]).BackgroundColor = Color.FromArgb(data[(49 * f + 7 * (f + 2))..(49 * f + 7 * (f + 3))]);
                        ((BoxView)face.Children[3]).BackgroundColor = Color.FromArgb(data[(49 * f + 7 * (f + 3))..(49 * f + 7 * (f + 4))]);
                        ((BoxView)face.Children[5]).BackgroundColor = Color.FromArgb(data[(49 * f + 7 * (f + 4))..(49 * f + 7 * (f + 5))]);
                        ((BoxView)face.Children[6]).BackgroundColor = Color.FromArgb(data[(49 * f + 7 * (f + 5))..(49 * f + 7 * (f + 6))]);
                        ((BoxView)face.Children[7]).BackgroundColor = Color.FromArgb(data[(49 * f + 7 * (f + 6))..(49 * f + 7 * (f + 7))]);
                        ((BoxView)face.Children[8]).BackgroundColor = Color.FromArgb(data[(49 * f + 7 * (f + 7))..(49 * f + 7 * (f + 8))]);
                    }
                }
                cubeModel.Configure(true);

                int newline = data.IndexOf("\n", 336);
                spans[2].Text = "  " + data[336..newline];

                fieldInput.Text = data[(newline + 1)..];

                path = pickResult.FullPath;
            }
            catch (Exception ex)
            {
                await mainPage.DisplayAlert("Error", $"Failed to open file: {ex.Message}", "OK");
            }
        }
    }

    public async void SaveProject(object? sender, EventArgs e)
    {
        if (Parent is AbsoluteLayout body && body.Parent is ContentPage mainPage &&
            body.Children[1] is Window window1 && window1.Children[1] is Wrapper wrapper1 && wrapper1.Children[0] is CubeModelFlat cubeModel &&
            body.Children[2] is Window window2 && window2.Children[1] is Wrapper wrapper2 && wrapper2.Children[0] is Controls controls &&
            controls.Children[4] is Grid toolbar && toolbar.Children[0] is Editor fieldInput &&
            body.Children[3] is Window window3 && window3.Children[1] is Wrapper wrapper3 && wrapper3.Children[0] is Moveset moveset &&
            moveset.Children[0] is ScrollView scrollView && scrollView.Content is Label label && label.FormattedText.Spans is IList<Span> spans)
        {
            try
            {
                string data = "";
                for (int i = 2; i <= 7; i++)
                {
                    if (cubeModel.Children[i] is Grid face)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (j == 4) continue;
                            if (face[j] is BoxView cell)
                            {
                                data += cell.BackgroundColor.ToHex();
                            }
                        }
                    }
                }
                if ((spans[0].Text + spans[1].Text + spans[2].Text).Length > 4)
                {
                    data += (spans[0].Text + spans[1].Text + spans[2].Text)[4..] + "\n";
                }
                data += fieldInput.Text;

                if (path.Length == 0)
                {
                    using MemoryStream stream = new(Encoding.Default.GetBytes(data));

                    FileSaverResult fileSaverResult = await FileSaver.Default.SaveAsync(
                        $"RubiksCube_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.rks",
                        stream,
                        new CancellationToken());

                    if (fileSaverResult.IsSuccessful)
                    {
                        path = fileSaverResult.FilePath;
                    }
                    else if (fileSaverResult.Exception != null)
                    {
                        string failureReason = GetFailureReason(fileSaverResult);
                        if (failureReason != "Unknown filesystem rejection") await mainPage.DisplayAlert("Warning", "File not saved (reason: " + GetFailureReason(fileSaverResult) + ")", "OK");
                    }
                }
                else
                {
                    await File.WriteAllTextAsync(path, data);
                }
            }
            catch (Exception ex)
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
}




// DO YOU SEE THIS SHIT TOMIOKA SAN??!
// WHAT THE FUCK IS THIS LAG