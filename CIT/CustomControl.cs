using System.Drawing;
using System.Windows.Forms;

namespace CIT
{
    /// <summary>
    /// 重新实现的 ToolStripProfessionalRenderer 类。
    /// <para>注意：使用了自定义颜色类</para>
    /// </summary>
    internal class CustomRenderer : ToolStripProfessionalRenderer
    {
        private Themes theme;

        /// <summary>
        /// 构造函数将 ProfessionalColorTable 传回父类。
        /// </summary>
        public CustomRenderer(CustomColorTable customColorTable) : base(customColorTable)
        {
            // 使用自定义 renderer 默认开启圆角，这里关闭。
            this.RoundedEdges = false;
        }

        /// <summary>
        /// 公用的切换主题方法。
        /// </summary>
        /// <param name="theme">目标主题的Themes枚举</param>
        public void SetTheme(Themes t)
        {
            this.theme = t;
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item is ToolStripStatusLabel)
            {
                // 为状态栏文本末尾增加省略号。
                TextRenderer.DrawText(
                    e.Graphics,
                    e.Text,
                    e.TextFont,
                    e.TextRectangle,
                    e.TextColor,
                    e.TextFormat | TextFormatFlags.EndEllipsis
                );
            }
            else if (e.Item is ToolStripMenuItem)
            {
                var item = (ToolStripMenuItem)e.Item;
                // 重绘菜单项行高和文字行高。
                // 默认 item.height=textrect.height+2
                // 默认 textrect.height=18
                e.Item.AutoSize = false;
                //e.Item.Height = e.TextRectangle.Height + 10;
                e.Item.Height = (int)System.Math.Ceiling(e.TextRectangle.Height * 1.55);
                if (e.Item.Selected)
                {
                    TextRenderer.DrawText(
                        e.Graphics,
                        e.Text,
                        e.TextFont,
                        new Rectangle(
                            (int)(e.Item.Height * 1.1),
                            e.TextRectangle.Y,
                            e.TextRectangle.Width,
                        //e.TextRectangle.Height + 8
                            (int)System.Math.Ceiling(e.TextRectangle.Height * 1.45)
                        ),
                        CustomColorScheme.MenuSelectedForeColor,
                        e.TextFormat
                    );
                }
                else
                {
                    TextRenderer.DrawText(
                        e.Graphics,
                        e.Text,
                        e.TextFont,
                        new Rectangle(
                            (int)(e.Item.Height * 1.1),
                            e.TextRectangle.Y,
                            e.TextRectangle.Width,
                        //e.TextRectangle.Height + 8
                            (int)System.Math.Ceiling(e.TextRectangle.Height * 1.45)
                        ),
                        e.TextColor,
                        e.TextFormat
                    );
                }
            }
            else
            {
                // 其余控件无需重绘
                base.OnRenderItemText(e);
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            // 因为有图标和无图标菜单项宽度不一，
            // 这里重新调整宽度为整个菜单的宽度。
            e.Item.Width = e.ToolStrip.Width;
            // 重绘菜单选择项的背景。
            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(
                    new SolidBrush(CustomColorScheme.MenuSelectedLightColor),
                    new Rectangle(
                        2,
                        0,
                        e.Item.Width - 4,
                        e.Item.Height
                    )
                );
            }
            else
            {
                base.OnRenderItemBackground(e);
            }
        }

        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            // 重绘被勾选菜单项的已选方块。
            Image img = this.theme == Themes.Dark ? Properties.Resources.tick_dark : Properties.Resources.tick_light;
            float boxLength = e.Item.Height * 0.75F;
            float imgLength = e.Item.Height * 0.55F;
            var boxRectF = new RectangleF(
                (e.Item.Height - boxLength) / 2,
                (e.Item.Height - boxLength) / 2,
                boxLength,
                boxLength
            );
            var imgRectF = new RectangleF(
                (boxLength - imgLength) / 2 + boxRectF.X,
                (boxLength - imgLength) / 2 + boxRectF.Y,
                imgLength,
                imgLength
            );
            e.Graphics.FillRectangle(
                new SolidBrush(CustomColorScheme.MenuSelectedLightColor),
                /* new Rectangle(
                    6,
                    3,
                    e.ImageRectangle.Width + 6,
                    e.ImageRectangle.Height + 6
                ) */
                boxRectF
            );
            e.Graphics.DrawImage(
                img,
                imgRectF
            );
            // 无兜底因为没有其它控件用到勾选项目。
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            // 重绘菜单项左侧图标显示位置。
            if (e.Item is ToolStripMenuItem && e.Image != null)
            {
                float imgLength = e.Item.Height * 0.65F;
                e.Graphics.DrawImage(
                    e.Image,
                    new RectangleF(
                    //e.ImageRectangle.X + 4,
                    //e.ImageRectangle.Y - 1,
                        (e.Item.Height - imgLength) / 2 + 4,
                        (e.Item.Height - imgLength) / 2,
                    // 默认 ImageRectangle.size=16*16
                        imgLength,
                        imgLength
                    )
                );
            }
            else
            {
                // 其余没有图标的无需重绘。
                base.OnRenderItemImage(e);
            }
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            // 为有下级的菜单项的小三角绑定自定义颜色类。
            if (e.Item.Selected)
            {
                e.ArrowColor = CustomColorScheme.MenuSelectedForeColor;
            }
            else
            {
                e.ArrowColor = CustomColorScheme.SecondLevelForeColor;
            }
            base.OnRenderArrow(e);
        }
    }

    /// <summary>
    /// 重新实现的 ProfessionalColorTable 类。
    /// <para>注意：引用了自定义颜色类</para>
    /// </summary>
    internal class CustomColorTable : ProfessionalColorTable
    {
        // 菜单背景色
        /* public override Color ToolStripDropDownBackground { get { return CustomColorScheme.MenuBackColor; } } */
        // 右键菜单项悬停颜色
        public override Color MenuItemSelected { get { return CustomColorScheme.MenuSelectedLightColor; } }
        public override Color MenuItemSelectedGradientBegin { get { return CustomColorScheme.MenuSelectedLightColor; } }
        public override Color MenuItemSelectedGradientEnd { get { return CustomColorScheme.MenuSelectedLightColor; } }
        // 工具条按钮(有下级菜单)按下颜色
        public override Color MenuItemPressedGradientBegin { get { return CustomColorScheme.MenuSelectedLightColor; } }
        public override Color MenuItemPressedGradientEnd { get { return CustomColorScheme.MenuSelectedLightColor; } }
        public override Color MenuItemPressedGradientMiddle { get { return CustomColorScheme.MenuSelectedLightColor; } }
        // 右键菜单项左侧留白颜色
        public override Color ImageMarginGradientBegin { get { return CustomColorScheme.MenuBackColor; } }
        public override Color ImageMarginGradientEnd { get { return CustomColorScheme.MenuBackColor; } }
        public override Color ImageMarginGradientMiddle { get { return CustomColorScheme.MenuBackColor; } }
        // 被勾选菜单项留白位置颜色
        /* public override Color CheckBackground { get { return CustomColorScheme.MenuSelectedHeavyColor; } } */
        // 被勾选菜单项留白位置悬停颜色
        /* public override Color CheckSelectedBackground { get { return CustomColorScheme.MenuSelectedHeavyColor; } } */
        // 工具条按钮悬停颜色
        public override Color ButtonSelectedGradientBegin { get { return CustomColorScheme.MenuSelectedLightColor; } }
        public override Color ButtonSelectedGradientEnd { get { return CustomColorScheme.MenuSelectedLightColor; } }
        // 工具条按钮按下颜色
        public override Color ButtonPressedGradientBegin { get { return CustomColorScheme.MenuSelectedHeavyColor; } }
        public override Color ButtonPressedGradientMiddle { get { return CustomColorScheme.MenuSelectedHeavyColor; } }
        public override Color ButtonPressedGradientEnd { get { return CustomColorScheme.MenuSelectedHeavyColor; } }
        // 被勾选菜单项留白位置外框、工具条按钮边框颜色
        public override Color ButtonSelectedBorder { get { return CustomColorScheme.FirstLevelBackColor; } }
        // 右键菜单整体外框颜色
        public override Color MenuBorder { get { return CustomColorScheme.BorderLightColor; } }
        // 工具条底部边框颜色
        public override Color ToolStripBorder { get { return CustomColorScheme.BorderHeavyColor; } }
        // 右键菜单悬停项外框颜色
        public override Color MenuItemBorder { get { return CustomColorScheme.MenuSelectedLightColor; } }
        // 分隔符其中一种颜色
        public override Color SeparatorLight { get { return Color.Transparent; } }
        // 分隔符另一种颜色
        public override Color SeparatorDark { get { return CustomColorScheme.MenuSelectedHeavyColor; } }

    }

    // 下方为重写的自定义控件，主要目的是绑定自定义颜色类。已废弃，改为在每个窗体 BindCustomColor()。
    //internal class CustomContextMenuStrip : ContextMenuStrip
    //{
    //    public CustomContextMenuStrip()
    //    {
    //        this.Renderer = new CustomRenderer(new CustomColorTable());
    //        this.ForeColor = CustomColorScheme.FirstLevelForeColor;
    //        this.BackColor = CustomColorScheme.MenuBackColor;
    //    }
    //}
    //internal class CustomToolStrip : ToolStrip
    //{
    //    public CustomToolStrip()
    //    {
    //        this.Renderer = new CustomRenderer(new CustomColorTable());
    //        this.ForeColor = CustomColorScheme.FirstLevelForeColor;
    //        this.BackColor = CustomColorScheme.FirstLevelBackColor;
    //    }
    //}
    //internal class CustomStatusStrip : StatusStrip
    //{
    //    public static Color StatusBarDefaultForeColor { get { return CustomColorScheme.FirstLevelForeColor; } }
    //    public CustomStatusStrip()
    //    {
    //        this.Renderer = new CustomRenderer(new CustomColorTable());
    //        this.ForeColor = StatusBarDefaultForeColor;
    //        this.BackColor = CustomColorScheme.FirstLevelBackColor;
    //    }
    //}
    //internal class CustomTextBox : TextBox
    //{
    //    public CustomTextBox()
    //    {
    //        this.ForeColor = CustomColorScheme.SecondLevelForeColor;
    //        this.BackColor = CustomColorScheme.SecondLevelBackColor;
    //    }
    //}
    //internal class CustomTitleLabel : Label
    //{
    //    public CustomTitleLabel()
    //    {
    //        this.ForeColor = CustomColorScheme.MenuSelectedForeColor;
    //        this.BackColor = CustomColorScheme.BorderLightColor;

    //    }
    //}
    //internal class CustomNormalLabel : Label
    //{
    //    public CustomNormalLabel()
    //    {
    //        this.ForeColor = CustomColorScheme.SecondLevelForeColor;
    //        this.BackColor = Color.Transparent;

    //    }
    //}
    //internal class CustomButton : Button
    //{
    //    public CustomButton()
    //    {
    //        this.UseVisualStyleBackColor = false;
    //        this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
    //        this.FlatAppearance.BorderColor = CustomColorScheme.BorderHeavyColor;
    //        this.FlatAppearance.MouseDownBackColor = CustomColorScheme.ButtonPressedColor;
    //        this.FlatAppearance.MouseOverBackColor = CustomColorScheme.MenuSelectedLightColor;
    //        this.BackColor = CustomColorScheme.ButtonBackColor;
    //        this.ForeColor = CustomColorScheme.FirstLevelForeColor;
    //    }
    //}
    //internal class CustomDataGridView : DataGridView
    //{
    //    public CustomDataGridView()
    //    {
    //        DataGridViewCellStyle style = new DataGridViewCellStyle();
    //        style.ForeColor = CustomColorScheme.SecondLevelForeColor;
    //        style.BackColor = CustomColorScheme.SecondLevelBackColor;
    //        style.SelectionBackColor = CustomColorScheme.MenuSelectedLightColor;
    //        style.SelectionForeColor = CustomColorScheme.MenuSelectedForeColor;
    //        this.DefaultCellStyle = style;
    //        this.ColumnHeadersDefaultCellStyle = style;
    //        this.RowHeadersDefaultCellStyle = style;
    //        this.TopLeftHeaderCell.Style = style;
    //        this.EnableHeadersVisualStyles = false;
    //        this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
    //        this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
    //        this.BackgroundColor = CustomColorScheme.SecondLevelBackColor;
    //        this.GridColor = CustomColorScheme.BorderLightColor;
    //    }
    //}
}