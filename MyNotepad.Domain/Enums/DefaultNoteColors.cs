using MyNotepad.Domain.Aggregates;

namespace MyNotepad.Domain.Enums
{
    public enum DefaultNoteColors
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
        public static Color From(this DefaultNoteColors color)
        {
            switch (color)
            {
                case DefaultNoteColors.Blue:
                    return new Color("", "");
                case DefaultNoteColors.Green:
                    return new Color("", "");
                case DefaultNoteColors.Red:
                    return new Color("", "");
                case DefaultNoteColors.Yellow:
                    return new Color("", "");
                case DefaultNoteColors.Orange:
                    return new Color("", "");
                case DefaultNoteColors.Pink:
                    return new Color("", "");
                case DefaultNoteColors.Black:
                    return new Color("", "");
                case DefaultNoteColors.White:
                    return new Color("", "");
                default:
                    return new Color("", "");
            }
        }
    }
}
