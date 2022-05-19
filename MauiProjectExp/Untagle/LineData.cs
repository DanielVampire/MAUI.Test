namespace MauiProjectExp.Untagle
{
    public class LineData
    {
        public EllipseData StartEllipse { get; set; }
        public EllipseData EndEllipse { get; set; }
        public bool IsIntersect { get; set; }

        public bool LineIntersect(LineData line)
        {
            double v1 = (line.EndEllipse.Rectangle.X - line.StartEllipse.Rectangle.X) * (StartEllipse.Rectangle.Y - line.StartEllipse.Rectangle.Y) 
                - (line.EndEllipse.Rectangle.Y - line.StartEllipse.Rectangle.Y) * (StartEllipse.Rectangle.X - line.StartEllipse.Rectangle.X);

            double v2 = (line.EndEllipse.Rectangle.X - line.StartEllipse.Rectangle.X) * (EndEllipse.Rectangle.Y - line.StartEllipse.Rectangle.Y) 
                - (line.EndEllipse.Rectangle.Y - line.StartEllipse.Rectangle.Y) * (EndEllipse.Rectangle.X - line.StartEllipse.Rectangle.X);

            double v3 = (EndEllipse.Rectangle.X - StartEllipse.Rectangle.X) * (line.StartEllipse.Rectangle.Y - StartEllipse.Rectangle.Y) 
                - (EndEllipse.Rectangle.Y - StartEllipse.Rectangle.Y) * (line.StartEllipse.Rectangle.X - StartEllipse.Rectangle.X);

            double v4 = (EndEllipse.Rectangle.X - StartEllipse.Rectangle.X) * (line.EndEllipse.Rectangle.Y - StartEllipse.Rectangle.Y) 
                - (EndEllipse.Rectangle.Y - StartEllipse.Rectangle.Y) * (line.EndEllipse.Rectangle.X - StartEllipse.Rectangle.X);

            IsIntersect = (v1 * v2 < 0) && (v3 * v4 < 0);
            
            if(!line.IsIntersect && IsIntersect)
                line.IsIntersect = true;

            return IsIntersect;
        }
    }
}
