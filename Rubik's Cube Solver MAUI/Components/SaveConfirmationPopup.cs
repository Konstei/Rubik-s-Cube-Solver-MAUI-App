using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;

public class SaveConfirmationPopup : Popup
{
    public enum PopupResult { Save, Discard, Cancel }
    private readonly Color _hoverColor = Color.FromArgb("#B9B4C7");
    private readonly Color _textColor = Color.FromArgb("#FAF0E6");
    private readonly Color _accentColor = Color.FromArgb("#5C5470");

    public SaveConfirmationPopup(string title, string message)
    {
        Content = new Frame
        {
            BackgroundColor = Colors.White,
            CornerRadius = 8,
            HasShadow = true,
            Padding = 20,
            WidthRequest = 320,
            Content = new VerticalStackLayout
            {
                Spacing = 15,
                Children =
                {
                    new Label { Text = title, FontSize = 18, FontAttributes = FontAttributes.Bold },
                    new Label { Text = message, FontSize = 14 },
                    new HorizontalStackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Spacing = 10,
                        Children =
                        {
                            CreateHoverButton("Cancel", PopupResult.Cancel),
                            CreateHoverButton("Don't Save", PopupResult.Discard),
                            new Button
                            {
                                Text = "Save",
                                BackgroundColor = _accentColor,
                                TextColor = Colors.White,
                                CornerRadius = 4,
                                Command = new Command(() => Close(PopupResult.Save))
                            }
                        }
                    }
                }
            }
        };
    }

    private View CreateHoverButton(string text, PopupResult result)
    {
        var button = new Button
        {
            Text = text,
            BackgroundColor = Colors.Transparent,
            TextColor = _accentColor,
            CornerRadius = 4
        };

        var hoverRecognizer = new PointerGestureRecognizer();
        hoverRecognizer.PointerEntered += (s, e) => { button.BackgroundColor = _hoverColor; button.TextColor = _textColor; };
        hoverRecognizer.PointerExited += (s, e) => { button.BackgroundColor = Colors.Transparent; button.TextColor = _accentColor; };
        button.GestureRecognizers.Add(hoverRecognizer);

        button.Command = new Command(() => Close(result));
        return button;
    }
}