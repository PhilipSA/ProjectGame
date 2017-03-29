using System;
using Object = UnityEngine.Object;

namespace Menus
{
    public static class MenuHelper
    {
        public static Menu GetCurrentMenu()
        {
            return (Menu)Object.FindObjectOfType(typeof(Menu));
        }
    }
}
