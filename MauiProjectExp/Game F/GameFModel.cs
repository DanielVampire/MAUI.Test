namespace MauiProjectExp.Game_F
{
    public class GameFModel
    {
        private int _size;
        public Map Map;
        private Point _space;

        public int Moves { get; private set; }

        public GameFModel(int size)
        {
            _size = size;
            Map = new(size);
        }

        public void Start(int seed = 0)
        {
            int digit = 0;

            foreach(Point point in AdditionalMethods.YieldPoint(_size))
                Map.Set(point, ++digit);

            _space = new Point(_size - 1, _size - 1);
            Map.Set(_space, 0);

            if (seed > 0)
                Shuffle(seed);

            Moves = 0;
        }

        private void Shuffle(int seed)
        {
            Random rand = new(seed);

            for(int i = 0; i < seed; i++)
                PressAt(new Point(rand.Next(_size), rand.Next(_size)));
        }

        public int PressAt(Point point)
        {
            if (_space.X == point.X && _space.Y == point.Y) 
                return 0;

            if (point.X != _space.X && point.Y != _space.Y)
                return 0;

            int steps = (int)Math.Abs(point.X - _space.X) + (int)Math.Abs(point.Y - _space.Y);

            while(point.X != _space.X)
                Shift(new Point(Math.Sign(point.X - _space.X), 0));

            while (point.Y != _space.Y)
                Shift( new Point(0, Math.Sign(point.Y - _space.Y)));

            Moves += steps;

            return steps;
        }

        private void Shift(Point point)
        {
            Point next = new(_space.X + point.X, _space.Y + point.Y);

            Map.Copy(next, _space);

            _space = next;
            Map.Set(_space, 0);
        }

        public Point GetDigitPoint(int num)
        {
            if (num == 0)
                return new Point(-1,-1);

            return Map.Get(num);
        }
        public int GetDigitAt(Point point)
        {
            if (_space.X == point.X && _space.Y == point.Y)
                return 0;

            return Map.Get(point);
        }
        public bool Solved()
        {
            if (_space != new Point(_size-1,_size-1))
                return false;

            int digit = 0;

            foreach (Point point in AdditionalMethods.YieldPoint(_size))
                if (Map.Get(point) != ++digit)
                    return _space.X == point.X && _space.Y == point.Y;

            return true;
        }
    }
}
