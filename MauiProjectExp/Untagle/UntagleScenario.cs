namespace MauiProjectExp.Untagle
{
	public class UntagleScenario : AbstractScenario
	{
        private int _vertices;

        public List<EllipseData> Ellipses { get; set; }
		public List<LineData> Lines { get; set; }
        public bool Win { get; set; }

		public UntagleScenario(int width, int height, int vertices) : base(width, height)
		{
			Ellipses = new();
			Lines = new();
			_vertices = vertices;
        }

		public override void Draw(ICanvas canvas)
		{
			if (Ellipses.Count > 0)
			{
                DrawGraph(canvas);
			}
			else
            {
                int[,] matrix = CreatePlanarGraph();

                while (!GraphCreation.IsPlanar(matrix, _vertices))
					matrix = CreatePlanarGraph();

                PreparationGraph(matrix);
                DrawGraph(canvas);

                return;
			}
        }

        private int[,] CreatePlanarGraph()
        {
            int[,] matrix = GraphCreation.CreateMatrix(_vertices);
            GraphCreation.RandomAll(matrix, _vertices);

            return matrix;
        }

        private void DrawGraph(ICanvas canvas)
        {
            canvas.Antialias = true;
            canvas.StrokeSize = 2;

            bool intersect = false;

            foreach (LineData forDraw in Lines.ToArray())
            {
                foreach(LineData forCheck in Lines.ToArray())
                {
                    if (forDraw == forCheck)
                        continue;

                    if (!forDraw.IsIntersect)
                    {
                        canvas.StrokeColor = forDraw.LineIntersect(forCheck)
                            ? Colors.Red
                            : Colors.Green;

                        if (!intersect)
                            intersect = forDraw.IsIntersect;
                    }
                    else
                    {
                        canvas.StrokeColor = Colors.Red;
                        break;
                    }
                }

                canvas.DrawLine(forDraw.StartEllipse.Rectangle.Center, forDraw.EndEllipse.Rectangle.Center);
            }

            if (!intersect)
                Win = true;

            canvas.FillColor = Colors.Blue;

            foreach (EllipseData i in Ellipses.ToArray())
                canvas.FillEllipse(i.Rectangle);

            canvas.SaveState();
        }

        private void PreparationGraph(int[,] matrix)
        {
            foreach (Vector2 vector in CreateCircle(new Rectangle(0, 0, Width / 2 - (Width * .1), Height / 2 - (Height * .1)), _vertices, new Point(Width / 2, Height / 2)))
            {
                Rectangle rect = new(vector.X, vector.Y, 25, 25);

                Ellipses.Add(new EllipseData() { Rectangle = rect });
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                        Lines.Add(new LineData() { StartEllipse = Ellipses[i], EndEllipse = Ellipses[j] });
                }
            }
        }

        private static List<Vector2> CreateCircle(Rectangle rect, int sides, Point center)
		{
			List<Vector2> vectors = new();

			const double max = 2.0 * Math.PI;
			double step = max / sides;

			for (double theta = 0.1; theta < max; theta += step)
				vectors.Add(new Vector2((float)center.X + (float)(rect.Width * Math.Cos(theta)), (float)center.Y + (float)(rect.Height * Math.Sin(theta))));

			return vectors;
		}
	}
}
