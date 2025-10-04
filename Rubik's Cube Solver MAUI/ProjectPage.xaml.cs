using CommunityToolkit.Maui.Layouts;

namespace Rubik_s_Cube_Solver_MAUI;

public partial class ProjectPage : ContentPage
{
    public ProjectPage()
    {
        InitializeComponent();
    }

    private async void CreateNewProject(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProjectPage());
    }

    private async void OpenProjectFile(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new TestPage());

        var fileResult = await FilePicker.Default.PickAsync();
        if (fileResult == null) return;

        byte[] fileBytes = await File.ReadAllBytesAsync(fileResult.FullPath);

        // Process the binary data
        ProcessBinaryData(fileBytes);
    }

    private void ProcessBinaryData(byte[] data)
    {

    }
}

// You can try clearing NuGet's HTTP cache (dotnet nuget locals http-cache --clear from the command line) and try again.