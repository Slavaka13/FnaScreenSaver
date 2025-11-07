namespace FnaScreenSaver.Models;

/// <summary>
/// Снежинка.
/// </summary>
internal struct Snowflake
{
    /// <summary>
    /// Размер.
    /// </summary>
    public double Size { get; set; }

    /// <summary>
    /// Скорость.
    /// </summary>
    public double Speed { get; set; }

    /// <summary>
    /// Координата левого края.
    /// </summary>
    public double Left { get; set; }

    /// <summary>
    /// Координата верхнего края.
    /// </summary>
    public double Top { get; set; }
}
