using System;
using System.Drawing;
using System.Windows.Forms;

namespace CIT
{
    /// <summary>
    /// 公用的通知窗体，可单独调用。
    /// </summary>
    internal partial class MsgForm : Form
    {
        private string msgText;

        /// <summary>
        /// 初始化通知窗体。
        /// </summary>
        /// <param name="title">作为窗体标题的文本</param>
        /// <param name="text">作为窗体正文的文本</param>
        /// <param name="resetTheme">是否为单独调用该窗体</param>
        public MsgForm(string title, string text, bool resetTheme = false)
        {
            InitializeComponent();
            this.ReScaleSize();
            this.Text = title;
            this.msgText = text;
            this.label1.Text = this.msgText;
            if (resetTheme)
            {
                this.ResetColorScheme();
                // 单独调用时需要重设窗体位置、任务栏显示。
                this.StartPosition = FormStartPosition.CenterScreen;
                this.ShowInTaskbar = true;
            }
            else
            {
                WindowTitlebarTheme.SetTheme(this);
            }
            this.BindCustomColor();
            this.SetLocation();
        }

        /// <summary>
        /// 重新根据 DPI 调整窗体或部分不受文本大小影响的控件的 Size。
        /// <para>需要确保开启dpi感知</para>
        /// </summary>
        private void ReScaleSize()
        {
            Graphics g = CreateGraphics();
            float ratioX = g.DpiX / 96;
            float ratioY = g.DpiY / 96;
            Size normalBtnMinSize = new SizeF(75F * ratioX, 25F * ratioY).ToSize();
            this.button1.Size = this.GetSize(this.button1.Text, this.button1.Font, normalBtnMinSize, 0.8F, 0.75F);
            this.ClientSize = new SizeF(this.ClientSize.Width * ratioX, this.ClientSize.Height * ratioY).ToSize();
        }

        /// <summary>
        /// 将组件颜色绑定 CustomColorScheme 的自定义颜色。
        /// </summary>
        private void BindCustomColor()
        {
            this.ForeColor = CustomColorScheme.SecondLevelForeColor;
            this.BackColor = CustomColorScheme.SecondLevelBackColor;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.FlatAppearance.BorderColor = CustomColorScheme.BorderHeavyColor;
            this.button1.FlatAppearance.MouseDownBackColor = CustomColorScheme.ButtonPressedColor;
            this.button1.FlatAppearance.MouseOverBackColor = CustomColorScheme.MenuSelectedLightColor;
            this.button1.BackColor = CustomColorScheme.ButtonBackColor;
            this.button1.ForeColor = CustomColorScheme.FirstLevelForeColor;
        }

        /// <summary>
        /// 根据文本和比例重新计算所需控件大小。
        /// </summary>
        /// <param name="text">目标文本</param>
        /// <param name="textFont">目标文本所用字体</param>
        /// <param name="minSize">控件最小尺寸</param>
        /// <param name="textRectWidthRatio">文本矩形的宽度占控件比例</param>
        /// <param name="textRectHeightRatio">文本矩形的高度占控件比例</param>
        /// <returns>所需控件的尺寸大小Size</returns>
        private Size GetSize(string text, Font textFont, Size minSize, float textRectWidthRatio, float textRectHeightRatio)
        {
            Graphics g = CreateGraphics();
            int width = minSize.Width;
            int height = minSize.Height;
            SizeF textRect = g.MeasureString(text, textFont);
            if ((minSize.Width * textRectWidthRatio) < textRect.Width)
            {
                width = (int)(textRect.Width / textRectWidthRatio);
            }
            if ((minSize.Height * textRectHeightRatio) < textRect.Height)
            {
                height = (int)(textRect.Height / textRectHeightRatio);
            }
            return new Size(width, height);
        }

        /// <summary>
        /// 设置正文和按钮的位置。
        /// </summary>
        private void SetLocation()
        {
            // 正文顶格时（即默认窗体大小可容纳的最大正文范围）正文的左上角坐标。
            int czWidth = this.ClientSize.Width;
            int czHeight = this.ClientSize.Height;
            int label1_X = (int)(czWidth * 0.096F);
            int label1_Y = (int)(czHeight * 0.24F);
            Graphics g = CreateGraphics();
            // 正文顶格时文本宽高。
            int labelMaxWidth = czWidth - (label1_X * 2);
            int labelMaxHeight = (int)(g.MeasureString("测量", this.Font).Height * 2);
            Size textSize = g.MeasureString(this.msgText, this.Font).ToSize();
            // 无使用 MeasureText 因为存在误差。
            if (textSize.Width <= labelMaxWidth)
            {
                // 正文未顶格时居中放置的X坐标。
                label1_X += (labelMaxWidth - textSize.Width) / 2;
            }
            else
            {
                // 正文顶格后窗体宽度扩展。
                czWidth += textSize.Width - labelMaxWidth;
            }
            if (this.msgText.Contains("\n"))
            {
                // 正文超过一行后窗体高度扩展。
                czHeight += (textSize.Height - labelMaxHeight);
            }
            else
            {
                // 正文未顶格时居中放置的Y坐标。
                label1_Y += (labelMaxHeight - textSize.Height) / 2;
            }
            this.label1.Location = new Point(label1_X, label1_Y);
            // 确定窗体宽高后计算按钮坐标。
            this.button1.Location = new Point(
                czWidth - 16 - this.button1.Width,
                czHeight - 18 - this.button1.Height
                );
            this.ClientSize = new Size(czWidth, czHeight);
        }

        /// <summary>
        /// 单独调用窗体时获取主题颜色。
        /// </summary>
        private void ResetColorScheme()
        {
            var iniFile = new RWIni();
            if (iniFile.IniExist & iniFile.KeySecExists("theme", "main"))
            {
                if (Enum.IsDefined(typeof(Themes), iniFile.Read("theme", "main")))
                {
                    var theme = (Themes)Enum.Parse(typeof(Themes), iniFile.Read("theme", "main"));
                    CustomColorScheme.SetTheme(theme);
                    WindowTitlebarTheme.SetTheme(this, theme);
                }
            }
            iniFile = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
