using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CIT
{
    /// <summary>
    /// 公用的自定义颜色类。
    /// </summary>
    internal static class CustomColorScheme
    {
        public static Color FirstLevelForeColor { get; set; }
        public static Color FirstLevelBackColor { get; set; }
        public static Color SecondLevelForeColor { get; set; }
        public static Color SecondLevelBackColor { get; set; }
        public static Color TerminalForeColor { get; set; }
        public static Color TerminalBackColor { get; set; }
        public static Color ButtonBackColor { get; set; }
        public static Color ButtonPressedColor { get; set; }
        public static Color MenuSelectedLightColor { get; set; }
        public static Color MenuSelectedHeavyColor { get; set; }
        public static Color MenuCheckedRectColor { get; set; }
        public static Color MenuSelectedForeColor { get; set; }
        public static Color MenuBackColor { get; set; }
        public static Color BorderHeavyColor { get; set; }
        public static Color BorderLightColor { get; set; }

        /// <summary>
        /// 初始化设置默认值，在无法读取 Config.Theme 时兜底。
        /// </summary>
        static CustomColorScheme()
        {
            SetTheme(Themes.Light);
        }

        /// <summary>
        /// 公用的切换主题颜色方法。
        /// </summary>
        /// <param name="currentTheme">目标主题的Themes枚举</param>
        public static void SetTheme(Themes currentTheme)
        {
            switch (currentTheme)
            {
                case Themes.Dark:
                {
                    FirstLevelForeColor = Color.FromArgb(253, 246, 227);
                    FirstLevelBackColor = Color.FromArgb(50, 49, 48);
                    SecondLevelForeColor = Color.FromArgb(216, 216, 216);
                    SecondLevelBackColor = Color.FromArgb(54, 54, 54);
                    TerminalForeColor = Color.FromArgb(147, 161, 161);
                    TerminalBackColor = Color.FromArgb(38, 38, 38);
                    ButtonBackColor = Color.FromArgb(68, 68, 68);
                    ButtonPressedColor = Color.FromArgb(88, 110, 117);
                    MenuSelectedLightColor = Color.FromArgb(87, 85, 83);
                    MenuSelectedHeavyColor = Color.FromArgb(174, 174, 174);
                    MenuCheckedRectColor = Color.FromArgb(238, 232, 213);
                    MenuSelectedForeColor = Color.FromArgb(255, 255, 255);
                    MenuBackColor = Color.FromArgb(50, 49, 48);
                    BorderHeavyColor = Color.FromArgb(166, 176, 172);
                    BorderLightColor = Color.FromArgb(102, 102, 102);
                    break;
                }
                default:
                {
                    FirstLevelForeColor = Color.FromArgb(0, 43, 54);
                    FirstLevelBackColor = Color.FromArgb(255, 255, 255);
                    SecondLevelForeColor = Color.FromArgb(37, 36, 35);
                    SecondLevelBackColor = Color.FromArgb(255, 255, 255);
                    TerminalForeColor = Color.FromArgb(87, 110, 118);
                    TerminalBackColor = Color.FromArgb(253, 246, 228);
                    ButtonBackColor = Color.FromArgb(230, 230, 230);
                    ButtonPressedColor = Color.FromArgb(165, 185, 209);
                    MenuSelectedLightColor = Color.FromArgb(210, 208, 206);
                    MenuSelectedHeavyColor = Color.FromArgb(150, 150, 150);
                    MenuCheckedRectColor = Color.FromArgb(54, 92, 102);
                    MenuSelectedForeColor = Color.FromArgb(0, 0, 0);
                    MenuBackColor = Color.FromArgb(255, 255, 255);
                    BorderHeavyColor = Color.FromArgb(7, 54, 66);
                    BorderLightColor = Color.FromArgb(169, 178, 173);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 公用的窗体标题栏主题类。
    /// </summary>
    internal class WindowTitlebarTheme
    {
        private const int BUILT_INT_1809 = 17763;
        private const int BUILT_INT_20H1 = 18985;
        private static DwmWindowAttribute windowAttr;
        private static Themes currentTheme = Themes.Light;
        // 使用 Win32API 实现，
        // 其中属性和属性值均需要使用 int 传值，具体值在下方枚举内。
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute attribute, ref BoolValue attributeValue, int attributeSize);

        private enum BoolValue
        {
            IS_FALSE = 0,
            IS_TRUE = 1
        }

        private enum DwmWindowAttribute
        {
            // before 1809(17763)
            DWMWA_USE_IMMERSIVE_DARK_MODE_NOT_SUPPORT = -1,
            // from 1809(17763) to 20H1(18985)
            DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19,
            // after 20H1(18985)
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20
        }

        public WindowTitlebarTheme()
        {
            // 判断需要用到的 WindowAttribute 值，实例化时只执行一次即可。
            windowAttr = this.OSBuild2Attribute();
        }
        
        /// <summary>
        /// 公用的主题切换方法。
        /// </summary>
        /// <param name="form">需要处理标题栏的窗体</param>
        /// <param name="theme">可选，目标主题的Themes枚举</param>
        public static void SetTheme(Form form, Themes? theme = null)
        {
            // 若可空类型不为空，表示需要切换主题，给静态字段重新赋值。
            if (theme.HasValue)
            {
                currentTheme = (Themes)theme;
            }
            if (windowAttr == DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE_NOT_SUPPORT)
            {
                return;
            }
            BoolValue pvAttribute = Themes2BoolValue(currentTheme);
            DwmSetWindowAttribute(form.Handle, windowAttr, ref pvAttribute, sizeof(BoolValue));
            // 修改透明度可以触发窗体重绘。
            form.Opacity = 0.5;
            form.Opacity = 1;
        }

        /// <summary>
        /// 根据系统版本返回 DwmWindowAttribute 值。
        /// </summary>
        /// <returns>DwmWindowAttribute枚举值</returns>
        private DwmWindowAttribute OSBuild2Attribute()
        {
            var buildInt = Environment.OSVersion.Version.Build;
            if (buildInt >= BUILT_INT_20H1)
            {
                return DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE;
            }
            else if (buildInt >= BUILT_INT_1809)
            {
                return DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
            }
            else
            {
                return DwmWindowAttribute.DWMWA_USE_IMMERSIVE_DARK_MODE_NOT_SUPPORT;
            }
        }

        /// <summary>
        /// 将 Themes 值转换为 BoolValue 值。
        /// </summary>
        /// <param name="theme">目标主题的Themes枚举</param>
        /// <returns></returns>
        private static BoolValue Themes2BoolValue(Themes theme)
        {
            switch (theme)
            {
                case Themes.Dark:
                {
                    return BoolValue.IS_TRUE;
                }
                default:
                {
                    return BoolValue.IS_FALSE;
                }
            }
        }
    }
}
