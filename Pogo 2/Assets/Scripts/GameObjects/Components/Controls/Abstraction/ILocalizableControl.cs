using Assets.Scripts.GameObjects.Components.Abstraction;

namespace Assets.Scripts.GameObjects.Components.Controls.Abstraction
{
    public interface ILocalizableControl : IRectTransformAble
    {
        string DisplayText { get; set; }
    }
}
