namespace MauiProjectExp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp() => MauiApp
            .CreateBuilder()
            //.UseSkiaSharp(true)
            .UseMauiApp<App>()
            .Build();
    }
}