using FnaScreenSaver.Services;

namespace FnaScreenSaver;

internal static class Program
{
    [STAThread]
    public static void Main()
    {
        using var screenSaver = new ScreenSaver();
        screenSaver.Run();
    }
}
