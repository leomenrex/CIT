using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CIT
{
    /// <summary>
    /// 日志设置窗体。
    /// </summary>
    internal partial class LogSettingsForm : Form
    {
        private Config config;

        /// <summary>
        /// 日志设置窗体初始化。
        /// </summary>
        /// <param name="c">接收实例化的 Config</param>
        public LogSettingsForm(Config c)
        {
            this.config = c;
            InitializeComponent();
            this.ReScaleSize();
            this.BindCustomColor();
            WindowTitlebarTheme.SetTheme(this);
            this.checkBoxAutoSave.Checked = this.config.IsAutoSave;
            this.checkBoxKeepData.Checked = this.config.IsKeepData;
            // comboBoxMaxLine 显示的值比实际小 1000 倍。
            foreach (int i in this.comboBoxMaxLine.Items)
            {
                if (i == this.config.LogMaxLine / 1000)
                {
                    this.comboBoxMaxLine.SelectedItem = i;
                    break;
                }
            }
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
            this.buttonConfirm.Size = this.GetSize(this.buttonConfirm.Text, this.buttonConfirm.Font, normalBtnMinSize, 0.8F, 0.75F);
            this.labelAutoSaveTitle.Height = this.GetSize(this.labelAutoSaveTitle.Text, this.labelAutoSaveTitle.Font, textRectHeightRatio: 0.625F).Height;
            this.labelKeepDataTitle.Height = this.GetSize(this.labelKeepDataTitle.Text, this.labelKeepDataTitle.Font, textRectHeightRatio: 0.625F).Height;
            // 此窗体使用了 autosize 所以这里不手动调整窗体大小。
            // 另外 checkbox 的 Size 在 Paint 事件回调函数中实现。
        }

        /// <summary>
        /// 将组件颜色绑定 CustomColorScheme 的自定义颜色。
        /// </summary>
        private void BindCustomColor()
        {
            this.BackColor = CustomColorScheme.SecondLevelBackColor;
            this.ForeColor = CustomColorScheme.SecondLevelForeColor;
            var customButton = new List<Control> { this.buttonConfirm };
            var customCheckBox = new List<Control> { this.checkBoxAutoSave, checkBoxKeepData };
            var customComboBox = new List<Control> { this.comboBoxMaxLine };
            var customTitleLabel = new List<Control> { this.labelAutoSaveTitle, this.labelKeepDataTitle };
            foreach (Button i in customButton)
            {
                i.UseVisualStyleBackColor = false;
                i.FlatStyle = FlatStyle.Flat;
                i.FlatAppearance.BorderColor = CustomColorScheme.BorderHeavyColor;
                i.FlatAppearance.MouseDownBackColor = CustomColorScheme.ButtonPressedColor;
                i.FlatAppearance.MouseOverBackColor = CustomColorScheme.MenuSelectedLightColor;
                i.BackColor = CustomColorScheme.ButtonBackColor;
                i.ForeColor = CustomColorScheme.FirstLevelForeColor;
                i.TabStop = false;
                i.AutoSize = false;
            }
            foreach (CheckBox i in customCheckBox)
            {
                i.UseVisualStyleBackColor = false;
                i.Appearance = System.Windows.Forms.Appearance.Button;
                i.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                i.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                i.FlatAppearance.BorderSize = 0;
                // 这里显示透明色搭配重绘事件。
                i.FlatAppearance.MouseDownBackColor = Color.Transparent;
                i.FlatAppearance.MouseOverBackColor = Color.Transparent;
                i.FlatAppearance.CheckedBackColor = Color.Transparent;
                i.AutoSize = true;
            }
            foreach (ComboBox i in customComboBox)
            {
                i.ForeColor = CustomColorScheme.SecondLevelForeColor;
                i.BackColor = CustomColorScheme.SecondLevelBackColor;
            }
            foreach (Label i in customTitleLabel)
            {
                i.ForeColor = CustomColorScheme.FirstLevelBackColor;
                i.BackColor = CustomColorScheme.MenuCheckedRectColor;
            }
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
        /// 根据文本和比例重新计算所需控件大小。
        /// </summary>
        /// <param name="text">目标文本</param>
        /// <param name="textFont">目标文本所用字体</param>
        /// <param name="textRectWidthRatio">可选，文本矩形的宽度占控件比例</param>
        /// <param name="textRectHeightRatio">可选，文本矩形的高度占控件比例</param>
        /// <returns>所需控件的尺寸大小Size</returns>
        private Size GetSize(string text, Font textFont, float textRectWidthRatio = 0F, float textRectHeightRatio = 0F)
        {
            Graphics g = CreateGraphics();
            int width = 0;
            int height = 0;
            SizeF textRect = g.MeasureString(text, textFont);
            if (textRectWidthRatio >= 0F)
            {
                width = (int)(textRect.Width / textRectWidthRatio);
            }
            if (textRectHeightRatio >= 0F)
            {
                height = (int)(textRect.Height / textRectHeightRatio);
            }
            return new Size(width, height);
        }

        private void checkBoxAutoSave_Click(object sender, EventArgs e)
        {
            if (this.checkBoxAutoSave.Checked)
            {
                // 和 checkBoxKeepData 互为冲突选项，所以自动避开。
                this.checkBoxKeepData.Checked = false;
            }
            this.RemoveFocus(sender, e);
        }
        private void checkBoxKeepData_Click(object sender, EventArgs e)
        {
            if (this.checkBoxKeepData.Checked)
            {
                // 和 checkBoxAutoSave 互为冲突选项，所以自动避开。
                this.checkBoxAutoSave.Checked = false;
            }
            this.RemoveFocus(sender, e);
        }

        /// <summary>
        /// 点击确定按钮时操作，实现写回 Config 变量并关闭窗口。
        /// </summary>
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            this.config.IsAutoSave = this.checkBoxAutoSave.Checked;
            this.config.IsKeepData = this.checkBoxKeepData.Checked;
            this.config.LogMaxLine = Convert.ToInt32(this.comboBoxMaxLine.SelectedItem) * 1000;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 重新绘制 checkbox 方框部分，搭配 BindCustomColor 设置 checkbox 样式使用。
        /// <para>这里绘制鼠标不在上方的状态。</para>
        /// </summary>
        private void checkBox_PaintNormal(object sender, PaintEventArgs e)
        {
            // 因为没有重写 CheckBox 类，所以这里用 paint 事件折中替代。
            Image img = this.config.Theme == Themes.Dark ? Properties.Resources.tick_dark : Properties.Resources.tick_light;
            CheckBox item = (CheckBox)sender;
            // 设置方框边长。
            int boxLength = (int)(e.ClipRectangle.Height * 0.55);
            // 方框左上角坐标。
            var boxPoint = new Point(e.ClipRectangle.X, (e.ClipRectangle.Height - boxLength) / 2 + e.ClipRectangle.Y);
            // 方框所在矩形。
            var boxRect = new Rectangle(boxPoint, new Size(boxLength, boxLength));
            if (item.Checked)
            {
                boxPoint.Offset(new Point(1, 1));
                // 根据方框矩形内折 1px 显示勾选图片。
                Rectangle picRect = new Rectangle(boxPoint, new Size(boxLength - 1, boxLength - 1));
                e.Graphics.DrawImage(img, picRect);
            }
            // 绘制方框。
            e.Graphics.DrawRectangle(new Pen(CustomColorScheme.BorderHeavyColor), boxRect);
            // 因为使用了强制失焦，所以这里不实现选中时整个按钮状态。
            /* if (item.Focused)
            {
                Rectangle outline_rect = e.ClipRectangle;
                outline_rect.Inflate(-1, -1);
                using (Pen pen = new Pen(CustomColorScheme.SecondLevelForeColor))
                { 
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    e.Graphics.DrawRectangle(pen, outline_rect);
                }
            } */
        }

        /// <summary>
        /// 重新绘制 checkbox 方框部分，搭配 BindCustomColor 设置 checkbox 样式使用。
        /// <para>这里绘制鼠标悬停上方的状态。</para>
        /// </summary>
        private void checkBox_PaintEnter(object sender, PaintEventArgs e)
        {
            Image img = this.config.Theme == Themes.Dark ? Properties.Resources.tick_dark : Properties.Resources.tick_light;
            CheckBox item = (CheckBox)sender;
            int boxLength = (int)(e.ClipRectangle.Height * 0.55);
            var boxPoint = new Point(e.ClipRectangle.X, (e.ClipRectangle.Height - boxLength) / 2 + e.ClipRectangle.Y);
            var boxRect = new Rectangle(boxPoint, new Size(boxLength, boxLength));
            boxPoint.Offset(new Point(1, 1));
            var picRect = new Rectangle(boxPoint, new Size(boxLength - 1, boxLength - 1));
            // 这里区别于 checkBox_PaintNormal
            e.Graphics.FillRectangle(new SolidBrush(CustomColorScheme.MenuSelectedLightColor), picRect);
            if (item.Checked)
            {
                e.Graphics.DrawImage(img, picRect);
            }
            e.Graphics.DrawRectangle(new Pen(CustomColorScheme.BorderHeavyColor), boxRect);
        }

        /// <summary>
        /// 鼠标进入 checkbox 时的操作。
        /// </summary>
        private void checkBox_MouseEnter(object sender, EventArgs e)
        {
            CheckBox item = (CheckBox)sender;
            item.Paint -= this.checkBox_PaintNormal;
            item.Paint += new PaintEventHandler(this.checkBox_PaintEnter);

        }

        /// <summary>
        /// 鼠标离开 checkbox 时的操作。
        /// </summary>
        private void checkBox_MouseLeave(object sender, EventArgs e)
        {
            CheckBox item = (CheckBox)sender;
            item.Paint -= this.checkBox_PaintEnter;
            item.Paint += new PaintEventHandler(this.checkBox_PaintNormal);
        }

        private void RemoveFocus(object sender, EventArgs e)
        {
            this.label1.Focus();
        }
    }
}
