namespace MauiProjectExp.Game_F;

public partial class GameTwo : ContentPage
{
    private bool _winner;

    public GameFModel GameModel { get; private set; }
    public double CellSize { get; set; }
    public bool Winner
    {
        get => _winner;
        set
        {
            if (value)
            {
                _winner = value;

                Thread thread = new(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Dispatcher.Dispatch(() =>
                    {
                        DisplayAlert("You Win!", "You win in F game. Congratulation!", "Ok");
                        Navigation.PopToRootAsync(true);
                    });
                });

                thread.Start();
            }
        }
    }
    public int Steps { get; set; }
    public string Score { get; set; }

    public GameTwo()
	{
		InitializeComponent();
	}
    private void CreateGame(int size, int seed)
    {
        GameModel = new(size);

        GameModel.Start(seed);

        for (int i = 0; i < GameModel.Map.GetLength(0); i++)
            Place.AddRowDefinition(new RowDefinition() { Height = GridLength.Auto });
        for (int j = 0; j < GameModel.Map.GetLength(1); j++)
            Place.AddColumnDefinition(new ColumnDefinition() { Width = GridLength.Auto });

        if (Content.Height < Content.Width)
            CellSize = (Content.Height - 150) / size;
        else if (Content.Width < Content.Height)
            CellSize = (Content.Width -  150) / size;

        Score = $"Steps: 0";

        AddToGrid();
    }

    private void AddToGrid()
    {
        for (int i = 0; i < GameModel.Map.GetLength(0); i++)
        {
            for (int j = 0; j < GameModel.Map.GetLength(1); j++)
            {
                if (GameModel.Map.Get(new Point(i, j)) == 0)
                {
                    Place.Add(new Frame()
                    {
                        WidthRequest = CellSize,
                        HeightRequest = CellSize,
                        Background = new SolidPaint(Colors.White)
                    }, j, i);
                }
                else
                {
                    Button button = new()
                    {
                        WidthRequest = CellSize,
                        HeightRequest = CellSize,
                        Background = new SolidPaint(RectangleColor(GameModel.Map.Get(new Point(i, j)))),
                        Text = GameModel.Map.Get(new Point(i, j)) > 0 ? GameModel.Map.Get(new Point(i, j)).ToString() : string.Empty,
                        TextColor = Colors.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = CellSize < 60 ? 8 : CellSize / 5
                    };

                    button.Clicked += Button_Clicked;

                    Place.Add(button, j, i);
                }
            }
        }

        if (GameModel.Solved())
            Winner = true;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button btn = sender as Button;

        Point btnPoint = GameModel.GetDigitPoint(int.Parse(btn.Text));

        if(btnPoint == new Point(-1,-1))
            btnPoint = new Point(-1,-1);
        else
        {
            GameModel.PressAt(btnPoint);
            Steps = GameModel.Moves;
            Score = $"Steps: {Steps}";

            Place.Clear();
            AddToGrid();
        }

    }

    private Color RectangleColor(int num)
    {
        if (num == 0)
            return Colors.White;

        return num % 2 == 0 ? Colors.Wheat : Colors.LightBlue;
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Button but = sender as Button;
        Random random = new((int)DateTime.Now.Ticks);

        switch (but.Text)
        {
            case "4x4":
                CreateGame(4, random.Next(200));
                break;
            case "5x5":
                CreateGame(5, random.Next(200));
                break;
            case "6x6":
                CreateGame(6, random.Next(200));
                break;
        }

        GameLevel.Clear();
        GameLevel.WidthRequest = 0;
        GameLevel.HeightRequest = 0;
    }
}