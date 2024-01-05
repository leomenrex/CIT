using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace CIT
{
    public partial class MainForm : Form
    {
        public static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        private Config config;
        private Process p;
        private SynchronizationContext uiContext;
        // 用于记录 trayIcon 状态，在和透明图标之间切换。
        private bool iconState;
        // 用于记录 statusLabel 的前景色，在和透明色之间切换。
        private Color statusColorCache;
        private CustomRenderer customRenderer;
        private WindowTitlebarTheme windowTitlebarTheme;

        public MainForm()
        {
            InitializeComponent();
            this.ReScaleSize();
            this.customRenderer = new CustomRenderer(new CustomColorTable());
            this.windowTitlebarTheme = new WindowTitlebarTheme();
            // 需要在子窗体显示前获取上下文。
            this.uiContext = SynchronizationContext.Current;
            // 实例化 Config 获取默认值，后续读取 ini 文件获取合法数据后再写回 Config 变量。
            this.config = new Config();
            var configHandler = new ConfigHandler(this.config);
            if (!configHandler.IniExist)
            {
                // 无论 ini 文件是否合法都要先显示主窗体，
                // 此时不合法也有 Config 默认值兜底被公共类取值。
                this.RefreshTheme();
                this.Show();
                if (!this.SettingsFormShow())
                {
                    this.Close();
                }
            }
            else
            {
                string issue = configHandler.FormInitializeCheck();
                this.RefreshTheme();
                this.Show();
                if (issue.Contains("需要重新填写设置"))
                {
                    this.MsgboxShow("错误", issue);
                    if (!this.SettingsFormShow(configHandler))
                    {
                        this.Close();
                    }
                }
                else if (issue.Contains("正常启动"))
                {
                    this.MsgboxShow("提示", issue);
                    configHandler.Var2Ini();
                }
            }
            configHandler = null;
            this.ProcStart();
        }

        #region 自定义图标、颜色
        /// <summary>
        /// 重新根据 DPI 调整窗体或部分不受文本大小影响的控件的 Size。
        /// <para>需要确保开启dpi感知</para>
        /// </summary>
        private void ReScaleSize()
        {
            Graphics g = CreateGraphics();
            float ratioX = g.DpiX / 96;
            float ratioY = g.DpiY / 96;
            this.MinimumSize = new SizeF(this.MinimumSize.Width * ratioX, this.MinimumSize.Height * ratioY).ToSize();
            this.ClientSize = new SizeF(this.ClientSize.Width * ratioX, this.ClientSize.Height * ratioY).ToSize();
            this.toolSet.ImageScalingSize = new SizeF(this.toolSet.ImageScalingSize.Width * ratioX, this.toolSet.ImageScalingSize.Height * ratioY).ToSize();
            this.toolSetButtonLogSave.DropDownButtonWidth = (int)(this.toolSetButtonLogSave.DropDownButtonWidth * ratioX);
        }

        /// <summary>
        /// 将组件颜色重新绑定 CustomColorScheme 的自定义颜色。
        /// <para>测试不会自动更新当前窗体控件颜色。</para>
        /// </summary>
        private void BindCustomColor()
        {
            var customContextMenuStrip = new List<Control> { this.logMenu, this.trayMenu, this.configSWMenu, this.logTextBoxMenu };
            var customToolStrip = new List<Control> { this.toolSet };
            var customStatusStrip = new List<Control> { this.statusBar };
            var customTextBox = new List<Control> { this.logTextBox, this.logPanel };
            foreach (ContextMenuStrip i in customContextMenuStrip)
            {
                // 使用自定义 Renender，部分颜色在其中绑定。
                i.Renderer = customRenderer;
                i.ForeColor = CustomColorScheme.SecondLevelForeColor;
                i.BackColor = CustomColorScheme.MenuBackColor;
            }
            foreach (ToolStrip i in customToolStrip)
            {
                i.Renderer = customRenderer;
                i.ForeColor = CustomColorScheme.FirstLevelForeColor;
                i.BackColor = CustomColorScheme.FirstLevelBackColor;
            }
            foreach (StatusStrip i in customStatusStrip)
            {
                i.Renderer = customRenderer;
                i.ForeColor = CustomColorScheme.SecondLevelForeColor;
                i.BackColor = CustomColorScheme.SecondLevelBackColor;
            }
            this.statusColorCache = CustomColorScheme.SecondLevelForeColor;
            // 报错时 label1 明确定义颜色后不继承 StatusStrip 颜色，所以统一重新赋值。
            this.statusLabel1.ForeColor = this.statusColorCache;
            foreach (Control i in customTextBox)
            {
                i.ForeColor = CustomColorScheme.TerminalForeColor;
                i.BackColor = CustomColorScheme.TerminalBackColor;
            }
        }

        /// <summary>
        /// 根据 Config.Theme 重新绑定图标。
        /// </summary>
        private void SetCustomIcon()
        {
            if (this.config.Theme == Themes.Dark)
            {
                this.logMenuItemSettings.Image = global::CIT.Properties.Resources.settings_dark_26;
                this.logMenuItemCopy.Image = global::CIT.Properties.Resources.copy_dark_26;
                this.trayMenuItemReload.Image = global::CIT.Properties.Resources.reload_dark_26;
                this.toolSetButtonReload.Image = global::CIT.Properties.Resources.reload_dark_40;
                this.toolSetButtonSettings.Image = global::CIT.Properties.Resources.settings_dark_40;
                this.toolSetButtonConfigSW.Image = global::CIT.Properties.Resources.configswitch_dark_40;
                this.toolSetButtonLogSave.Image = global::CIT.Properties.Resources.logoutput_dark_40;
                this.toolSetButtonTheme.Image = global::CIT.Properties.Resources.themes_dark_40;
            }
            else
            {
                this.logMenuItemSettings.Image = global::CIT.Properties.Resources.settings_light_26;
                this.logMenuItemCopy.Image = global::CIT.Properties.Resources.copy_light_26;
                this.trayMenuItemReload.Image = global::CIT.Properties.Resources.reload_light_26;
                this.toolSetButtonReload.Image = global::CIT.Properties.Resources.reload_light_40;
                this.toolSetButtonSettings.Image = global::CIT.Properties.Resources.settings_light_40;
                this.toolSetButtonConfigSW.Image = global::CIT.Properties.Resources.configswitch_light_40;
                this.toolSetButtonLogSave.Image = global::CIT.Properties.Resources.logoutput_light_40;
                this.toolSetButtonTheme.Image = global::CIT.Properties.Resources.themes_light_40;
            }
        }

        /// <summary>
        /// 更新 CustomColorScheme 主题颜色，Renderer、Titlebar 主题，并更新此窗体颜色、图标。
        /// </summary>
        private void RefreshTheme()
        {
            CustomColorScheme.SetTheme(this.config.Theme);
            this.customRenderer.SetTheme(this.config.Theme);
            this.BindCustomColor();
            WindowTitlebarTheme.SetTheme(this, this.config.Theme);
            this.SetCustomIcon();
        }
        #endregion

        #region 子窗体
        /// <summary>
        /// 设置窗体初始化，更新ini文件。
        /// </summary>
        /// <returns>窗体点击确定返回true，其余操作返回false</returns>
        private bool SettingsFormShow(ConfigHandler configHandler = null)
        {
            this.SetTrayClickable(false);
            using (var setsForm = new SettingsForm(this.config))
            {
                if (setsForm.ShowDialog(this) == DialogResult.Cancel)
                {
                    this.SetTrayClickable(true);
                    return false;
                }
            }
            if (configHandler == null)
            {
                configHandler = new ConfigHandler(this.config);
            }
            configHandler.Var2Ini();
            this.SetTrayClickable(true);
            return true;
        }

        /// <summary>
        /// 日志设置窗体初始化，更新ini文件。
        /// </summary>
        private void LogSettingsFormShow(object sender, EventArgs e)
        {
            this.SetTrayClickable(false);
            using (var lsetsForm = new LogSettingsForm(this.config))
            {
                if (lsetsForm.ShowDialog(this) == DialogResult.OK)
                {
                    var configHandler = new ConfigHandler(this.config);
                    configHandler.Var2Ini();
                }
            }
            this.SetTrayClickable(true);
        }

        /// <summary>
        /// 提示窗体初始化。
        /// </summary>
        /// <param name="title">窗体标题字符串</param>
        /// <param name="text">窗体正文字符串</param>
        private void MsgboxShow(string title, string text)
        {
            this.SetTrayClickable(false);
            using (var msgbox = new MsgForm(title, text))
            {
                msgbox.ShowDialog(this);
            }
            this.SetTrayClickable(true);
        }
        #endregion

        #region 操作执行程序进程
        /// <summary>
        /// 启动执行程序进程。
        /// </summary>
        private void ProcStart()
        {
            string argument = this.config.MainDic[this.config.CurNickName][0];
            if (argument.Length > 0 && argument.Contains("%"))
            {
                argument = argument.Replace(
                    "%",
                    "\"" + this.config.MainDic[this.config.CurNickName][1] + "\""
                );
            }

            this.p = new Process();
            GC.Collect();
            this.p.StartInfo.FileName = "\"" + this.config.ProgramName + "\"";
            this.p.StartInfo.Arguments = argument;
            this.p.StartInfo.UseShellExecute = false;
            this.p.StartInfo.RedirectStandardError = true;
            this.p.StartInfo.RedirectStandardOutput = true;
            this.p.StartInfo.CreateNoWindow = true;
            this.p.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            this.p.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            // 输出事件绑定。
            this.p.OutputDataReceived += new DataReceivedEventHandler(this.LogStdReceiver);
            this.p.ErrorDataReceived += new DataReceivedEventHandler(this.LogErrReceiver);
            this.p.EnableRaisingEvents = true;
            this.p.Exited += new System.EventHandler(this.LogExitReceiver);
            // 启动进程。
            this.p.Start();
            this.p.BeginOutputReadLine();
            this.p.BeginErrorReadLine();
            // 将处理好的 argument 传给后续更新 UI。
            this.ProcState2UIState(argument);
        }

        /// <summary>
        /// 退出执行程序的进程。
        /// </summary>
        private void ProcExit()
        {
            // 需要停止旧进程的输出，这里解绑事件。
            /* p.CancelErrorRead(); */
            /* p.CancelOutputRead(); */
            this.p.OutputDataReceived -= this.LogStdReceiver;
            this.p.ErrorDataReceived -= this.LogErrReceiver;
            this.p.Exited -= this.LogExitReceiver;
            if (!this.p.HasExited)
            {
                this.p.Kill();
            }
        }

        /// <summary>
        /// 重载执行程序的进程。
        /// </summary>
        private void ProcReload()
        {
            this.ProcExit();
            if (this.config.IsAutoSave)
            {
                this.LogSave();
            }
            if (!this.config.IsKeepData)
            {
                this.logTextBox.Clear();
            }
            this.ProcStart();
            this.logTextBox.AppendText("程序已重载..." + "\r\n");
        }
        #endregion

        #region 跨进程日志处理
        /// <summary>
        /// 在执行程序进程接收普通输出信号，Post 到 UI 上下文。
        /// </summary>
        private void LogStdReceiver(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                string s = e.Data.ToString();
                this.uiContext.Post(this.Log2UI, s + "\r\n");
            }
        }
        /// <summary>
        /// 在执行程序进程接收错误输出信号，Post 到 UI 上下文。
        /// </summary>
        private void LogErrReceiver(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                string s = e.Data.ToString();
                this.uiContext.Post(this.Log2UI, "[error_output] " + s + "\r\n");
            }
        }
        /// <summary>
        /// 位于 UI 进程处理 UI 上下文收到的信息，写进 LogTextBox。
        /// </summary>
        /// <param name="str">需要显示到LogTextBox的信息</param>
        private void Log2UI(object str)
        {
            this.logTextBox.AppendText(str.ToString());
            if (str.ToString() == "后台进程已退出......\r\n")
            {
                this.ProcState2UIState();
            }
        }
        /// <summary>
        /// 在执行程序进程接收退出信号，Post 特定字符到 UI 上下文。
        /// </summary>
        private void LogExitReceiver(object sender, EventArgs e)
        {
            this.uiContext.Post(this.Log2UI, "后台进程已退出......" + "\r\n");
        }
        #endregion

        /// <summary>
        /// 根据执行程序状态更新状态栏、标题、气球文本和托盘图标。
        /// </summary>
        /// <param name="argument">执行程序的实际后接参数，可留空</param>
        private void ProcState2UIState(string argument = "")
        {
            string programname = Path.GetFileName(this.config.ProgramName);
            if (!this.p.HasExited)
            {
                if (this.timerBlink.Enabled)
                {
                    this.timerBlink.Stop();
                    this.statusLabel1.ForeColor = this.statusColorCache;
                    this.trayIcon.Icon = Properties.Resources.icon16;
                }
                this.statusLabel2.Text =
                    "当前配置：("
                    + this.config.CurNickName
                    + ")  "
                    + (this.config.ProgramName + " " + argument).Replace(BaseDir, "..\\");
                this.Text = programname + " - CIT";
                this.statusLabel1.Text = "正在运行";
                this.trayIcon.BalloonTipTitle = programname + "已重载";
                this.trayIcon.BalloonTipText = "当前配置：" + this.config.CurNickName;
                this.trayIcon.BalloonTipIcon = ToolTipIcon.Info;
                this.trayIcon.Text = this.Text;
                if (!this.Visible)
                {
                    this.trayIcon.ShowBalloonTip(4000);
                }
            }
            else
            {
                this.statusLabel1.Text = "意外停止";
                this.trayIcon.BalloonTipTitle = programname + this.statusLabel1.Text;
                this.trayIcon.BalloonTipText = "当前配置：" + this.config.CurNickName;
                this.trayIcon.BalloonTipIcon = ToolTipIcon.Error;
                if (!this.timerBlink.Enabled)
                {
                    this.timerBlink.Start();
                }
                this.trayIcon.ShowBalloonTip(4000);
            }
        }

        /// <summary>
        /// 计时器启动后操作。自动判断窗体状态。
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            // 根据激活状态闪烁托盘图标。
            if (this != Form.ActiveForm)
            {
                if (iconState)
                {
                    this.trayIcon.Icon = Properties.Resources.icon1;
                    iconState = !iconState;
                }
                else
                {
                    this.trayIcon.Icon = Properties.Resources.icon16;
                    iconState = !iconState;
                }
            }
            else
            {
                this.trayIcon.Icon = Properties.Resources.icon16;
            }
            // 根据窗体隐藏与否闪烁状态栏。
            if (this.WindowState != FormWindowState.Minimized || this.Visible)
            {
                // 字体前景色可以使用透明色（直接不渲染），不支持alpha通道读取。
                if (this.statusLabel1.ForeColor.A != 0)
                {
                    this.statusLabel1.ForeColor = Color.Transparent;
                }
                else
                {
                    this.statusLabel1.ForeColor = this.statusColorCache;
                }
            }
        }

        /// <summary>
        /// 设置托盘区图标可点击性。
        /// </summary>
        /// <param name="clickable">设置为可点击为true，否则为false</param>
        private void SetTrayClickable(bool clickable)
        {
            if (clickable)
            {
                this.trayIcon.MouseUp += new MouseEventHandler(this.trayIcon_MouseUp);
                this.trayIcon.ContextMenuStrip = trayMenu;
            }
            else
            {
                this.trayIcon.MouseUp -= this.trayIcon_MouseUp;
                this.trayIcon.ContextMenuStrip = null;
            }
        }

        /// <summary>
        /// 点击托盘图标时的操作。自动判断鼠标左右键。
        /// </summary>
        /// <param name="e">注意为 MouseEventArgs</param>
        private void trayIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visible)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // 因为已绑定 ContextMenuStrip 所以这里只需要刷新菜单即可。
                this.configSWMenu_Refresh(sender, e);
            }
        }

        /// <summary>
        /// 关闭窗体时的操作。
        /// </summary>
        private void window_Exit(object sender, EventArgs e)
        {
            this.trayIcon.Visible = false;
            if (this.p != null)
            {
                this.ProcExit();
            }
            // 使用 System.Environment.Exit(0) 偶尔会有延迟。
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// 窗口状态变化，判断是否滚动文本和隐藏窗体、气球。
        /// </summary>
        private void window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.logTextBox.SelectionStart = this.logTextBox.TextLength;
                this.logTextBox.ScrollToCaret();
            }
            else
            {
                this.Hide();
            }
        }

        /// <summary>
        /// 刷新配置切换菜单。
        /// </summary>
        private void configSWMenu_Refresh(object sender, EventArgs e)
        {
            this.configSWMenu.SuspendLayout();
            this.configSWMenu.Items.Clear();
            foreach (string nickname in this.config.MainDic.Keys)
            {
                this.configSWMenu.Items.Add(nickname, null, this.configSWItem_Click);
            }
            foreach (ToolStripMenuItem item in this.configSWMenu.Items)
            {
                if (item.Text == this.config.CurNickName)
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }
            this.configSWMenu.ResumeLayout(false);
            this.configSWMenu.PerformLayout();
        }

        /// <summary>
        /// 点击配置切换菜单的子项，更新ini文件，重载进程。
        /// </summary>
        private void configSWItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            if (item.Text == this.config.CurNickName)
            {
                return;
            }
            else
            {
                this.config.CurNickName = item.Text;
                var configHandler = new ConfigHandler(this.config);
                configHandler.Var2Ini();
                this.ProcReload();
            }
        }

        private void toolSetButtonSettings_Click(object sender, EventArgs e)
        {
            if (this.SettingsFormShow())
            {
                this.ProcReload();
            }
        }

        private void toolSetButtonReload_Click(object sender, EventArgs e)
        {
            this.ProcReload();
        }

        /// <summary>
        /// 点击导出日志按钮操作。
        /// </summary>
        private void toolSetButtonLogSave_Click(object sender, EventArgs e)
        {
            using (var logSaveDialog = new SaveFileDialog())
            {
                logSaveDialog.Filter = "文本日志|*.log|文本文件|*.txt|所有文件|*.*";
                logSaveDialog.InitialDirectory = BaseDir;
                logSaveDialog.Title = "导出日志";
                logSaveDialog.FileName =
                    Path.GetFileName(this.config.ProgramName)
                    + "_"
                    + DateTime.Now.ToString("MMdd-HHmmssff")
                    + ".log";
                if (logSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    this.LogSave(logSaveDialog.FileName);
                }
            }
        }

        /// <summary>
        /// 用于日志写入到文件。
        /// </summary>
        /// <param name="filename">需要写入的文件名，留空则使用默认生成文件名</param>
        private void LogSave(string filename = "")
        {
            if (filename == "")
            {
                filename =
                    Path.GetFileName(this.config.ProgramName)
                    + "_"
                    + DateTime.Now.ToString("MMdd-HHmmssff")
                    + ".log";
                filename = BaseDir + filename;
            }
            using (var streamWriter = new StreamWriter(filename, false, Encoding.UTF8))
            {
                streamWriter.WriteLine(this.logTextBox.Text);
            }
        }

        /// <summary>
        /// 日志区域变化时，清理超出保留行数的文本，自动滚动。
        /// </summary>
        private void logTextBox_Change(object sender, EventArgs e)
        {
            if (this.logTextBox.Lines.Length > this.config.LogMaxLine)
            {
                int fst = this.logTextBox.GetFirstCharIndexFromLine(0);
                int lst = this.logTextBox.GetFirstCharIndexFromLine(1);
                this.logTextBox.Select(fst, lst);
                this.logTextBox.SelectedText = "";
                this.logTextBox.SelectionStart = this.logTextBox.TextLength;
                this.logTextBox.ScrollToCaret();
            }
            this.logTextBox.ClearUndo();
        }

        private void logMenuItemClear_Click(object sender, EventArgs e)
        {
            this.logTextBox.Text = "";
            this.logTextBox.ScrollToCaret();
        }

        private void logMenuItemCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.logTextBox.SelectedText);
        }

        /// <summary>
        /// 日志区域鼠标右键按下时的操作。自动判断鼠标左右键。
        /// </summary>
        /// <param name="e">注意为 MouseEventArgs</param>
        private void logTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            // 与 logMenu 共用 logMenuItemClear 子项。
            if (e.Button == MouseButtons.Right)
            {
                this.logTextBoxMenu.SuspendLayout();
                this.logTextBoxMenu.Items.Clear();
                if (this.logTextBox.SelectedText != "")
                {
                    this.logTextBoxMenu.Items.AddRange(new ToolStripMenuItem[] {
                        this.logMenuItemCopy,
                        this.logMenuItemClear
                    });
                }
                else
                {
                    this.logTextBoxMenu.Items.Add(
                        this.logMenuItemClear
                    );
                }
                this.logTextBoxMenu.ResumeLayout(false);
                this.logTextBoxMenu.PerformLayout();
            }
        }

        private void logMenuDropDown_Opening(object sender, EventArgs e)
        {
            // toolstripmenuitem 同一时间只能为一个 contextmenu 的子对象，
            // 所以与 logTextBox 右键菜单竞争，每次打开都要重新添加子项。
            this.logMenu.SuspendLayout();
            this.logMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.logMenuItemSettings,
                this.logMenuItemClear
            });
            this.logMenu.ResumeLayout(false);
            this.logMenu.PerformLayout();
        }

        private void toolSetButtonTheme_Click(object sender, EventArgs e)
        {
            // 因为是双主题所以只做了是否切换。
            this.config.Theme = this.config.Theme == Themes.Dark ? Themes.Light : Themes.Dark;
            this.RefreshTheme();
            ConfigHandler configHandler = new ConfigHandler(this.config);
            configHandler.Var2Ini();
        }

    }
}
