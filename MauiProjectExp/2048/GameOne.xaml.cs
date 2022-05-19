namespace MauiProjectExp;

public partial class GameOne : ContentPage
{
    private bool _winner;
    private bool _gameOver;

    public int[,] Grid;
    public double CellSize;

    public bool Winner {
        get => _winner;
        set
        {
            if(value)
            {
                _winner = value;

                Thread thread = new(() => 
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                        Dispatcher.Dispatch(() =>
                        {
                            DisplayAlert("You Win!", "You win in 2048 game. Congratulation!", "Ok");
                            Navigation.PopToRootAsync(true);
                        });
                    });

                thread.Start();
            }
        }
    }
    public bool GameOver 
    { 
        get => _gameOver;
        set 
        {
            if (value)
            {
                _gameOver = value;

                Thread thread = new(() =>
                {
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Dispatcher.Dispatch(() =>
                    {
                        DisplayAlert("You Lose!", "You lose in 2048 game. Congratulation!", "Ok");
                        Navigation.PopToRootAsync(true);
                    });
                });

                thread.Start();
            }
        } 
    }

    public GameOne()
    {
        InitializeComponent();
    }

    private void CreateGame(int size)
    {
        Grid = new int[size, size];

        SpawnNumber();
        SpawnNumber();

        for (int i = 0; i < Grid.GetLength(0); i++)
            Place.AddRowDefinition(new RowDefinition() { Height = GridLength.Auto });
        for (int j = 0; j < Grid.GetLength(1); j++)
            Place.AddColumnDefinition(new ColumnDefinition() { Width = GridLength.Auto });

        if(Content.Height < Content.Width)
            CellSize = (Content.Height - (UpArrow.HeightRequest * 2) - 50) / size;
        else if(Content.Width < Content.Height)
            CellSize = (Content.Width - (UpArrow.HeightRequest * 2) - 50) / size;

        AddToGrid();
    }

    private void AddToGrid()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Grid rect = new()
                {
                    WidthRequest = CellSize,
                    HeightRequest = CellSize,
                };
                rect.Add(new Frame()
                {
                    Background = new SolidPaint(RectangleColor(Grid[i, j])),
                    Content = new Label() 
                    {
                        Text = Grid[i, j] > 0 ? Grid[i, j].ToString() : string.Empty,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        TextColor = Colors.Black,
                        FontAttributes = FontAttributes.Bold,
                        FontSize =  CellSize < 60 ? 8 : CellSize / 5
                    }
                });

                if (Grid[i, j] == 2048)
                    Winner = true;

                Place.Add(rect, j, i);
            }
        }
    }

    private static Color RectangleColor(int number) =>
        number switch
        {
            2 => Color.FromRgb(255, 248, 242),
            4 => Color.FromRgb(255, 231, 209),
            8 => Color.FromRgb(237, 197, 161),
            16 => Color.FromRgb(237, 156, 85),
            32 => Color.FromRgb(227, 126, 39),
            64 => Color.FromRgb(209, 98, 0),
            128 => Color.FromRgb(227, 48, 48),
            256 => Color.FromRgb(227, 16, 16),
            512 => Color.FromRgb(155, 245, 137),
            1024 => Color.FromRgb(94, 209, 71),
            2048 => Color.FromRgb(34, 201, 0),
            _ => Color.FromRgb(105, 105, 105),
        };

    private bool SpawnNumber()
    {
        Random _random = new((int)DateTime.Now.Ticks);

        List<EmptyCell> options = new();

        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                if (Grid[i, j] == 0)
                    options.Add(new() { X = i, Y = j });
            }
        }

        if (options.Count > 0)
        {
            EmptyCell cell = options[_random.Next(options.Count - 1)];
            double randNum = _random.NextDouble();

            Grid[cell.X, cell.Y] = randNum > .5 ? 2 : 4;
            return true;
        }
        else
        {
            GameOver = true;
            return false;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button but = sender as Button;

        switch (but.Text)
        {
            case "4x4":
                CreateGame(4);
                break;
            case "5x5":
                CreateGame(5);
                break;
            case "6x6":
                CreateGame(6);
                break;
        }

        GameLevel.Clear();
        UpArrow.IsVisible = true;
        DownArrow.IsVisible = true;
        PlaceHolder.IsVisible = true;
    }

    private static int[] Slide(int[] row)
    {
        int[] slide = new int[row.Length];
        int index = 0;

        for (int i = 0; i < slide.Length; i++)
        {
            if (row[i] > 0)
            {
                slide[index] = row[i];
                index++;
            }
        }

        return slide;
    }

    private void UpArrow_Clicked(object sender, EventArgs e)
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            int[] row = new int[Grid.GetLength(0)];

            for (int j = 0; j < Grid.GetLength(1); j++)
                row[j] = Grid[j, i];

            row = Slide(row);
            row = Combine(row);

            for (int j = 0; j < Grid.GetLength(1); j++)
                Grid[j, i] = row[j];
        }

        Place.Clear();

        SpawnNumber();

        AddToGrid();
    }

    private void LeftArrow_Clicked(object sender, EventArgs e)
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            int[] row = new int[Grid.GetLength(0)];

            for (int j = 0; j < Grid.GetLength(1); j++)
                row[j] = Grid[i, j];

            row = Slide(row);
            row = Combine(row);

            for (int j = 0; j < Grid.GetLength(1); j++)
                Grid[i, j] = row[j];
        }

        Place.Clear();

       SpawnNumber();


        AddToGrid();
    }

    private void RightArrow_Clicked(object sender, EventArgs e)
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            int[] row = new int[Grid.GetLength(0)];
            int index = 0;
            for (int j = Grid.GetLength(1) - 1; j >= 0; j--)
            {
                row[index] = Grid[i, j];
                index++;
            }

            row = Slide(row);
            row = Combine(row);
            index = 0;

            for (int j = Grid.GetLength(1) - 1; j >= 0; j--)
            {
                Grid[i, j] = row[index];
                index++;
            }
        }

        Place.Clear();

        SpawnNumber();

        AddToGrid();
    }

    private void DownArrow_Clicked(object sender, EventArgs e)
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            int[] row = new int[Grid.GetLength(0)];
            int index = 0;

            for (int j = Grid.GetLength(1) - 1; j >= 0; j--)
            {
                row[index] = Grid[j, i];
                index++;
            }

            row = Slide(row);
            row = Combine(row);
            index = 0;

            for (int j = Grid.GetLength(1) - 1; j >= 0; j--)
            {
                Grid[j, i] = row[index];
                index++;
            }
        }

        Place.Clear();

        SpawnNumber();

        AddToGrid();
    }

    private int[] Combine(int[] row)
    {
        //TODO: Think how to combine numbers
        //TODO: Add GameOver and WellDone
        int j = 0;

        for (int i = 1; i < row.Length; i++)
        {
            j = i - 1;

            if (row[i] == row[j] && row[i] != 0)
            {
                row[j] += row[i];
                row[i] = 0;
            }
        }

        row = Slide(row);

        return row;
    }
}