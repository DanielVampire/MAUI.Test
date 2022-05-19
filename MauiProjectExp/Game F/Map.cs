namespace MauiProjectExp.Game_F
{
    public class Map
    {
        private int _size;
        private int[,] _map;

        public Map(int size)
        {
            _size = size;
            _map = new int[size, size];
        }

        public int GetLength(int dimension) => _map.GetLength(dimension);
        public void Set(Point point, int value)
        {
            if (AdditionalMethods.OnBoard(point, _size))
                _map[(int)Math.Abs(point.X), (int)Math.Abs(point.Y)] = value;
        }

        public Point Get(int num)
        {
            for (int x = 0; x < _map.GetLength(0); x++)
            {
                for (int y = 0; y < _map.GetLength(1); y++)
                {
                    if (num == _map[x, y])
                        return new Point(x, y);
                }
            }
            return new Point(-1, -1);
        }

        public int Get(Point point)
        {
            if (AdditionalMethods.OnBoard(point, _size))
                return _map[(int)Math.Abs(point.X), (int)Math.Abs(point.Y)];

            return 0;
        }

        public void Copy(Point from, Point to) => Set(to, Get(from));
    }
}
