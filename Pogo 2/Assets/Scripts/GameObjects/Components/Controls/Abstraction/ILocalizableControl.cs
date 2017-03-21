using GameObjects.Components.Abstraction;

namespace GameObjects.Components.Controls.Abstraction
{
    public interface ILocalizableControl : IRectTransformAble
    {
        string DisplayText { get; set; }
    }
}
