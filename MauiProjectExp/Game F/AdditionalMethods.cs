namespace MauiProjectExp.Game_F
{
    internal static class AdditionalMethods
    {
        public static bool OnBoard(Point point, int size)
        {
            if (point.X < 0 || point.X > size - 1) return false;
            if (point.Y < 0 || point.Y > size - 1) return false;

            return true;
        }

        public static IEnumerable<Point> YieldPoint(int size)
        {
            for(int x = 0; x < size; x++)
                for(int y = 0; y < size; y++)
                    yield return new Point(x, y);
        }
    }
}
