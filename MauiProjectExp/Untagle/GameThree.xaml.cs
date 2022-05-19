namespace MauiProjectExp;

public partial class GameThree : ContentPage
{
	public UntagleScenario Untagle { get; set; }

	public GameThree()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		if (int.TryParse(sizeEntry.Text, out int res))
		{
			if(res < 6)
				DisplayAlert("Error", "You must enter a number greater than or equal to 6", "Ok");
			else if(res > 25)
				DisplayAlert("Error", "You must enter a number less than or equal to 25", "Ok");
			else
				Initialize(res);
		}
		else
			DisplayAlert("Error", "Need to input number", "Ok");
	}

    private void Initialize(int count)
    {
		Untagle = new UntagleScenario((int)Content.Width, (int)Content.Height, count);

		paintFrame.IsEnabled = true;
		paintFrame.Drawable = Untagle;

		//foreach(EllipseData i in Untagle.Ellipses)
  //      {
		//	Button button = new()
		//	{
		//		Background = new SolidPaint(Colors.Transparent),
		//		WidthRequest = 25,
		//		HeightRequest = 25
		//	};

  //          button.Clicked += VerticesClicked;


  //      }

		GameLevel.Clear();
		GameLevel.HeightRequest = 0;
		GameLevel.IsEnabled = false;
	}

    private void VerticesClicked(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}