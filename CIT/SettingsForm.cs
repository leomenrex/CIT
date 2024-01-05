using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CIT
{
    /// <summary>
    /// 配置明细设置窗体。
    /// </summary>
    internal partial class SettingsForm : Form
    {
        // dic 缓存字典既用于检测重复，也用于后续写入，避免多次读取 datagridview。
        private Dictionary<string, string[]> dic = new Dictionary<string, string[]>();
        private Config config;

        /// <summary>
        /// 明细配置窗体初始化。
        /// </summary>
        /// <param name="c">接收实例化的 Config</param>
        public SettingsForm(Config c)
        {
            this.config = c;
            InitializeComponent();
            this.ReScaleSize();
            this.BindCustomColor();
            WindowTitlebarTheme.SetTheme(this);
            this.Var2UI();
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
            this.buttonExecute.Size = this.GetSize(this.buttonExecute.Text, this.buttonExecute.Font, normalBtnMinSize, 0.8F, 0.75F);
            this.buttonConfirm.Size = this.GetSize(this.buttonConfirm.Text, this.buttonConfirm.Font, normalBtnMinSize, 0.8F, 0.75F);
            this.buttonAddRow.Size = this.GetSize(this.buttonAddRow.Text, this.buttonAddRow.Font, 0.625F, 0.55F);
            this.buttonDelRow.Size = this.GetSize(this.buttonDelRow.Text, this.buttonDelRow.Font, 0.625F, 0.55F);
            this.labelExecuteTitle.Height = this.GetSize(this.labelExecuteTitle.Text, this.labelExecuteTitle.Font, textRectHeightRatio: 0.625F).Height;
            this.labelCfgTitle.Height = this.GetSize(this.labelCfgTitle.Text, this.labelCfgTitle.Font, textRectHeightRatio: 0.625F).Height;
            this.labelCfgSubTitle.Height = this.GetSize("\n\r", this.labelCfgSubTitle.Font, textRectHeightRatio: 0.8F).Height;
            this.MinimumSize = new SizeF(this.MinimumSize.Width * ratioX, this.MinimumSize.Height * ratioY).ToSize();
            this.ClientSize = new SizeF(this.ClientSize.Width * ratioX, this.ClientSize.Height * ratioY).ToSize();
        }

        /// <summary>
        /// 将组件颜色绑定 CustomColorScheme 的自定义颜色。
        /// </summary>
        private void BindCustomColor()
        {
            this.BackColor = CustomColorScheme.SecondLevelBackColor;
            this.ForeColor = CustomColorScheme.SecondLevelForeColor;
            var customButton = new List<Control> { this.buttonExecute, this.buttonConfirm, this.buttonAddRow, this.buttonDelRow };
            var customTitleLabel = new List<Control> { this.labelExecuteTitle, this.labelCfgTitle };
            var customNormalLabel = new List<Control> { this.labelCfgSubTitle, labelDataTips, labelExecuteTips };
            var customDataGirdView = new List<Control> { this.dataGrid };
            foreach (Button i in customButton)
            {
                i.UseVisualStyleBackColor = false;
                i.FlatStyle = FlatStyle.Flat;
                i.FlatAppearance.BorderColor = CustomColorScheme.BorderHeavyColor;
                i.FlatAppearance.MouseDownBackColor = CustomColorScheme.ButtonPressedColor;
                i.FlatAppearance.MouseOverBackColor = CustomColorScheme.MenuSelectedLightColor;
                i.BackColor = CustomColorScheme.ButtonBackColor;
                i.ForeColor = CustomColorScheme.FirstLevelForeColor;
            }
            foreach (Label i in customTitleLabel)
            {
                i.ForeColor = CustomColorScheme.FirstLevelBackColor;
                i.BackColor = CustomColorScheme.MenuCheckedRectColor;
            }
            foreach (DataGridView i in customDataGirdView)
            {
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.ForeColor = CustomColorScheme.SecondLevelForeColor;
                style.BackColor = CustomColorScheme.SecondLevelBackColor;
                style.SelectionBackColor = CustomColorScheme.MenuSelectedLightColor;
                style.SelectionForeColor = CustomColorScheme.MenuSelectedForeColor;
                i.RowsDefaultCellStyle = style;
                i.ColumnHeadersDefaultCellStyle = style;
                i.RowHeadersDefaultCellStyle = style;
                i.TopLeftHeaderCell.Style = style;
                i.EnableHeadersVisualStyles = false;
                i.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                i.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
                i.BackgroundColor = CustomColorScheme.SecondLevelBackColor;
                i.GridColor = CustomColorScheme.BorderLightColor;
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
            SizeF textRect = g.MeasureString(text, textFont);
            int width = textRect.ToSize().Width;
            int height = textRect.ToSize().Height;
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

        /// <summary>
        /// 将 Config 变量写到界面。
        /// </summary>
        private void Var2UI()
        {
            this.labelExecuteTips.Text = this.PathConvert(this.config.ProgramName, true);
            string[] r = new string[3];
            foreach (string name in this.config.MainDic.Keys)
            {
                r[0] = name;
                r[1] = this.config.MainDic[name][0];
                r[2] = this.PathConvert(this.config.MainDic[name][1], true);
                this.dataGrid.Rows.Add(r);
            }
            this.DGVKeepOneLine();
        }

        /// <summary>
        /// 将检查后的缓存字典 dic 数据和界面数据写回 Config 变量。
        /// </summary>
        private void UI2Var()
        {
            this.config.MainDic.Clear();
            foreach (string name in this.dic.Keys)
            {
                this.config.MainDic.Add(name, this.dic[name]);
            }
            this.config.ProgramName = this.PathConvert(this.labelExecuteTips.Text, false);
            this.config.CurNickName = this.DGVCellConvert(this.dataGrid.Rows[0].Cells[0]);
        }

        #region reusable part

        /// <summary>
        /// 用于处理 datagridview 单元格数据为字符串。
        /// </summary>
        /// <param name="cell">代表需要处理的单元格</param>
        /// <returns>单元格值字符串</returns>
        private string DGVCellConvert(DataGridViewCell cell)
        {
            if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
            {
                return "";
            }
            else
            {
                return cell.Value.ToString();
            }
        }

        /// <summary>
        /// 对路径缩写、还原处理，便于界面显示。
        /// </summary>
        /// <param name="path">需要处理的路径字符串</param>
        /// <param name="shorter">决定是缩写还是还原</param>
        /// <returns>路径字符串</returns>
        private string PathConvert(string path, bool isShorter)
        {
            if (isShorter)
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    return "(当前未选择)";
                }
                else
                {
                    if (path.Contains(MainForm.BaseDir))
                    {
                        return path.Replace(MainForm.BaseDir, "..\\");
                    }
                    else
                    {
                        return "(路径超出范围)";
                    }
                }
            }
            else
            {
                if (path == "(当前未选择)" | path == "(路径不存在)")
                {
                    return "";
                }
                else
                {
                    return path.Replace("..\\", MainForm.BaseDir);
                }
            }
        }

        /// <summary>
        /// 文件选择窗体。
        /// </summary>
        /// <param name="filter">文件过滤字符串，决定了可以选择的文件类型</param>
        /// <param name="path">默认显示的路径字符串，留空则取显示当前CIT目录</param>
        /// <returns>已选文件的绝对路径</returns>
        private string FilePickerDialog(string filter, string path)
        {
            string file = null;
            if (path == "")
            {
                path = MainForm.BaseDir;
            }
            while (!this.FilePickerPathCheck(file))
            {
                using (var filePicker = new OpenFileDialog())
                {
                    filePicker.InitialDirectory = path;
                    filePicker.Filter = filter;
                    filePicker.Title = "仅支持选择相同目录下的文件";

                    if (filePicker.ShowDialog() == DialogResult.OK)
                    {
                        file = filePicker.FileName;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return file;
        }

        /// <summary>
        /// 用于限制文件选择窗体只能选择 BaseDir 路径。
        /// </summary>
        /// <param name="fullpath">需要检测的路径字符串</param>
        /// <returns>若符合路径为true，否则为false</returns>
        private bool FilePickerPathCheck(string fullpath)
        {
            if (fullpath == null)
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(fullpath))
            {
                this.MsgboxShow("错误", "仅支持选择位于 CIT 相同目录的文件！");
                return false;
            }
            else if (fullpath.ToLower() == Application.ExecutablePath.ToLower())
            {
                this.MsgboxShow("错误", "选错了吧？再选一遍。。");
                return false;
            }
            else
            {
                return fullpath.Contains(MainForm.BaseDir);
            }
        }
        
        /// <summary>
        /// 当 datagridview 行数少于 1 时自动加一行。
        /// </summary>
        private void DGVKeepOneLine()
        {
            if (this.dataGrid.Rows.Count < 1)
            {
                this.dataGrid.Rows.Add();
            }
        }

        /// <summary>
        /// 提示窗体初始化。
        /// </summary>
        /// <param name="title">窗体标题字符串</param>
        /// <param name="text">窗体正文字符串</param>
        private void MsgboxShow(string title, string text)
        {
            using (MsgForm msgbox = new MsgForm(title, text))
            {
                msgbox.ShowDialog(this);
            }
        }

        /// <summary>
        /// 强制移动焦点到不可见位置，用于使按钮强制失焦。
        /// </summary>
        private void RemoveFocus(object sender, EventArgs e)
        {
            this.labelExecuteTitle.Focus();
        }
        #endregion reusable part

        private void buttonAddRow_Click(object sender, EventArgs e)
        {
            this.dataGrid.Rows.Add();
        }

        private void buttonDelRow_Click(object sender, EventArgs e)
        {
            if (this.dataGrid.SelectedCells.Count > 0)
            {
                this.dataGrid.Rows.RemoveAt(this.dataGrid.SelectedCells[0].RowIndex);
            }
            this.DGVKeepOneLine();
        }

        /// <summary>
        /// datagridview 按键操作，实现了 Delete 键清空单元格。
        /// </summary>
        private void dataGrid_KeysDown(object sender, KeyEventArgs e)
        {
            if (!this.dataGrid.IsCurrentCellInEditMode && e.KeyCode == Keys.Delete)
            {
                this.dataGrid.SelectedCells[0].Value = "";
            }
        }

        /// <summary>
        /// datagirdview 双击单元格操作。
        /// </summary>
        private void dataGird_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            int c = e.ColumnIndex;
            if (r == -1 || c == -1)
            {
                // 标题行列跳过。
                return;
            }
            if (c == 2)
            {
                // 第三列（配置文件列）双击选择文件。
                string path = this.PathConvert(this.DGVCellConvert(this.dataGrid.Rows[r].Cells[c]), false);
                path = File.Exists(path) ? Path.GetDirectoryName(path) : "";
                path = this.FilePickerDialog("所有文件|*", path);
                if (path != null)
                {
                    this.dataGrid.Rows[r].Cells[2].Value = this.PathConvert(path, true);
                }
            }
            else
            {
                // 其余单元格双击才可以编辑。
                this.dataGrid.BeginEdit(true);
            }
        }

        /// <summary>
        /// 切换选择单元格时操作，用于检测重复配置信息，显示在 datatips。
        /// </summary>
        private void dataGird_SelectionChange(object sender, EventArgs e)
        {
            var duplicateDic = new Dictionary<string, int>();
            string duplicateStr = "";
            foreach (DataGridViewRow r in this.dataGrid.Rows)
            {
                string v = this.DGVCellConvert(r.Cells[1]) + this.DGVCellConvert(r.Cells[2]);
                if (duplicateDic.ContainsKey(v))
                {
                    if (duplicateStr == "")
                    {
                        duplicateStr = (duplicateDic[v] + 1).ToString();
                    }
                    duplicateStr = duplicateStr + "," + (r.Index + 1);
                }
                else
                {
                    duplicateDic.Add(v, r.Index);
                }
            }
            if (duplicateStr.Length > 1)
            {
                this.labelDataTips.Text = "提示：" + duplicateStr + "行的配置可能存在重复。";
            }
            else
            {
                this.labelDataTips.Text = "";
            }
            duplicateDic = null;
            duplicateStr = null;
        }

        /// <summary>
        /// 检查界面变量是否合法。
        /// </summary>
        /// <returns>通过检查为true，否则为false</returns>
        private bool UIVarCheck()
        {
            // dic 缓存字典既用于检测重复，也用于后续写入，避免多次读取 datagridview。
            // 执行程序文件则多次读取。
            this.dic.Clear();
            foreach (DataGridViewRow r in this.dataGrid.Rows)
            {
                string nickname = this.DGVCellConvert(r.Cells[0]);
                string argument = this.DGVCellConvert(r.Cells[1]);
                string configfile = this.PathConvert(this.DGVCellConvert(r.Cells[2]), false);
                // 这里仅检查别名，之后交由通用类检查其余合法性。
                if (this.dic.ContainsKey(nickname))
                {
                    this.MsgboxShow("配置未保存", "当前设置存在以下问题：\r\n检查到部分自定义别名存在重复\r\n\r\n请重新填写");
                    this.dic.Clear();
                    return false;
                }
                if (nickname == "")
                {
                    this.MsgboxShow("配置未保存", "当前设置存在以下问题：\r\n检查到部分自定义别名为空\r\n\r\n请重新填写");
                    this.dic.Clear();
                    return false;
                }
                this.dic.Add(r.Cells[0].Value.ToString(), new string[2] { argument, configfile });
            }
            // 调用通用类。
            var configHandler = new ConfigHandler(this.config);
            string issue = configHandler.VarCheck(this.dic, this.PathConvert(this.labelExecuteTips.Text, false));
            configHandler = null;
            if (issue != "")
            {
                this.MsgboxShow("配置未保存", "当前设置存在以下问题：\r\n" + issue + "\r\n请重新填写");
                this.dic.Clear();
                return false;
            }
            if (this.dic.Count > 1)
            {
                this.MsgboxShow("配置已保存", "默认取第一个配置执行，可在主界面自行切换");
            }
            return true;
        }

        /// <summary>
        /// 点击选择执行程序文件按钮。
        /// </summary>
        private void buttonExecute_Click(object sender, EventArgs e)
        {
            string programname = this.FilePickerDialog("可执行文件|*.exe", "");
            if (programname != null)
            {
                this.labelExecuteTips.Text = this.PathConvert(programname, true);
            }
        }

        /// <summary>
        /// 点击确定按钮。
        /// </summary>
        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (this.UIVarCheck())
            {
                this.DialogResult = DialogResult.OK;
                // 完成检查，正式写入 Config 变量。
                this.UI2Var();
                this.Close();
            }
        }
    }
}
