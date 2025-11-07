using FnaScreenSaver.Services;

namespace FnaScreenSaver;

/// <summary>
/// Точка входа в приложение — запускает заставку FNA ScreenSaver.
/// </summary>
internal static class Program
{
    /// <summary>
    /// Основной метод приложения. Создаёт и запускает экземпляр заставки.
    /// </summary>
    [STAThread]
    public static void Main()
    {
        using var screenSaver = new ScreenSaver();
        screenSaver.Run();
    }
}
