using System.Collections.Generic;

namespace XFBindableStackLayout
{
    public sealed class MyColor
    {
        public string Name { get; }
        public string HexCode { get; }

        public MyColor(string name, string hexCode)
        {
            Name = name;
            HexCode = hexCode;
        }
    }

    public class MainViewModel
    {
        public IReadOnlyCollection<MyColor> MyColors { get; } = new List<MyColor>
        {
            new MyColor("Black", "#000000"),
            new MyColor("Hot Pink", "#ff69b4"),
            new MyColor("Red", "#ff0000"),
            new MyColor("Sort of Green", "#7cff00")
        };
    }
}
