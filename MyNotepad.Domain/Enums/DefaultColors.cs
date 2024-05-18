using MyNotepad.Domain.Aggregates;

namespace MyNotepad.Domain.Enums
{
    public enum DefaultColors
    {
        Blue,
        Green,
        Red,
        Yellow,
        Orange,
        Pink,
        Black,
        White
    }

    public static class DefaultColorsExtension
    {
        public static Color From(this DefaultColors color)
        {
            switch (color)
            {
                case DefaultColors.Blue:
                    return new Color("", "");
                case DefaultColors.Green:
                    return new Color("", "");
                case DefaultColors.Red:
                    return new Color("", "");
                case DefaultColors.Yellow:
                    return new Color("", "");
                case DefaultColors.Orange:
                    return new Color("", "");
                case DefaultColors.Pink:
                    return new Color("", "");
                case DefaultColors.Black:
                    return new Color("", "");
                case DefaultColors.White:
                    return new Color("", "");
                default:
                    return new Color("", "");
            }
        }
    }
}
