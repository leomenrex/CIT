namespace CIT
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.logPanel = new System.Windows.Forms.Panel();
            this.configSWMenu = new System.Windows.Forms.ContextMenuStrip();
            this.logMenu = new System.Windows.Forms.ContextMenuStrip();
            this.logTextBoxMenu = new System.Windows.Forms.ContextMenuStrip();
            this.logMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.logMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
            this.logMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip();
            this.trayMenuItemConfigSW = new System.Windows.Forms.ToolStripMenuItem();
            this.trayMenuItemReload = new System.Windows.Forms.ToolStripMenuItem();
            this.trayMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.trayMenuSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolSet = new System.Windows.Forms.ToolStrip();
            this.toolSetButtonReload = new System.Windows.Forms.ToolStripButton();
            this.toolSetButtonSettings = new System.Windows.Forms.ToolStripButton();
            this.toolSetButtonConfigSW = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolSetButtonLogSave = new System.Windows.Forms.ToolStripSplitButton();
            this.toolSetButtonTheme = new System.Windows.Forms.ToolStripButton();
            this.toolMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMenuSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.statusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.logMenu.SuspendLayout();
            this.trayMenu.SuspendLayout();
            this.toolSet.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Location = new System.Drawing.Point(0, 88);
            this.logTextBox.MaxLength = 9000000;
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.logTextBox.Size = new System.Drawing.Size(800, 339);
            this.logTextBox.TabIndex = 3;
            this.logTextBox.TabStop = false;
            this.logTextBox.ContextMenuStrip = this.logTextBoxMenu;
            this.logTextBox.TextChanged += new System.EventHandler(this.logTextBox_Change);
            this.logTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.logTextBox_MouseDown);
            // 
            // logPanel
            // 
            this.logPanel.Name = "logPanel";
            this.logPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logPanel.Controls.Add(this.logTextBox);
            this.logPanel.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            // 
            // configSWMenu
            // 
            this.configSWMenu.Name = "configSWMenu";
            this.configSWMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // logMenu
            // 
            this.logMenu.Name = "logMenu";
            this.logMenu.Size = new System.Drawing.Size(125, 26);
            //
            // logTextBoxMenu
            //
            this.logTextBoxMenu.Name = "logTextBoxMenu";
            // 
            // logMenuItemSettings
            // 
            this.logMenuItemSettings.Name = "logMenuItemSettings";
            this.logMenuItemSettings.Size = new System.Drawing.Size(124, 22);
            this.logMenuItemSettings.Text = "日志设置";
            this.logMenuItemSettings.Click += new System.EventHandler(this.LogSettingsFormShow);
            // 
            // logMenuItemClear
            // 
            this.logMenuItemClear.Name = "logMenuItemClear";
            this.logMenuItemClear.Text = "清空日志";
            this.logMenuItemClear.Click += new System.EventHandler(this.logMenuItemClear_Click);
            // 
            // logMenuItemCopy
            // 
            this.logMenuItemCopy.Name = "logMenuItemCopy";
            this.logMenuItemCopy.Text = "复制";
            this.logMenuItemCopy.Click += new System.EventHandler(this.logMenuItemCopy_Click);
            // 
            // trayIcon
            // 
            this.trayIcon.Icon = global::CIT.Properties.Resources.icon16;
            this.trayIcon.Visible = true;
            this.trayIcon.ContextMenuStrip = this.trayMenu;
            this.trayIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseUp);
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayMenuItemConfigSW,
            this.trayMenuItemReload,
            this.trayMenuSeparator,
            this.trayMenuItemExit});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(125, 70);
            // 
            // trayMenuItemConfigSW
            // 
            this.trayMenuItemConfigSW.DropDown = this.configSWMenu;
            this.trayMenuItemConfigSW.Name = "trayMenuItemConfigSW";
            this.trayMenuItemConfigSW.Size = new System.Drawing.Size(124, 22);
            this.trayMenuItemConfigSW.Text = "配置切换";
            // 
            // trayMenuItemReload
            // 
            this.trayMenuItemReload.Name = "trayMenuItemReload";
            this.trayMenuItemReload.Size = new System.Drawing.Size(124, 22);
            this.trayMenuItemReload.Text = "重新载入";
            this.trayMenuItemReload.Click += new System.EventHandler(this.toolSetButtonReload_Click);
            // 
            // trayMenuItemExit
            // 
            this.trayMenuItemExit.Name = "trayMenuItemExit";
            this.trayMenuItemExit.Size = new System.Drawing.Size(124, 22);
            this.trayMenuItemExit.Text = "退出";
            this.trayMenuItemExit.Click += new System.EventHandler(this.window_Exit);
            // 
            // trayMenuSeparator
            // 
            this.trayMenuSeparator.Name = "trayMenuSeparator";
            // 
            // toolSet
            // 
            this.toolSet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSetButtonConfigSW,
            this.toolSetButtonSettings,
            this.toolSetButtonReload,
            toolMenuSeparator3,
            this.toolSetButtonLogSave,
            toolMenuSeparator2,
            this.toolSetButtonTheme,
            toolMenuSeparator1});
            this.toolSet.Location = new System.Drawing.Point(0, 0);
            this.toolSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolSet.Name = "toolSet";
            this.toolSet.Size = new System.Drawing.Size(800, 88);
            this.toolSet.AutoSize = true;
            this.toolSet.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolSet.TabIndex = 4;
            this.toolSet.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            // 
            // toolSetButtonReload
            // 
            this.toolSetButtonReload.Name = "toolSetButtonReload";
            this.toolSetButtonReload.Size = new System.Drawing.Size(68, 85);
            this.toolSetButtonReload.Margin = new System.Windows.Forms.Padding(2, 1, 4, 2);
            this.toolSetButtonReload.ToolTipText = "重载配置";
            this.toolSetButtonReload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSetButtonReload.Click += new System.EventHandler(this.toolSetButtonReload_Click);
            // 
            // toolSetButtonSettings
            // 
            this.toolSetButtonSettings.Name = "toolSetButtonSettings";
            this.toolSetButtonSettings.Size = new System.Drawing.Size(84, 85);
            this.toolSetButtonSettings.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolSetButtonSettings.ToolTipText = "修改配置明细";
            this.toolSetButtonSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSetButtonSettings.Click += new System.EventHandler(this.toolSetButtonSettings_Click);
            // 
            // toolSetButtonConfigSW
            // 
            this.toolSetButtonConfigSW.DropDown = this.configSWMenu;
            this.toolSetButtonConfigSW.DropDownDirection = System.Windows.Forms.ToolStripDropDownDirection.BelowRight;
            this.toolSetButtonConfigSW.Name = "toolSetButtonConfigSW";
            this.toolSetButtonConfigSW.Size = new System.Drawing.Size(77, 85);
            this.toolSetButtonConfigSW.Margin = new System.Windows.Forms.Padding(4, 1, 2, 2);
            this.toolSetButtonConfigSW.ToolTipText = "配置切换";
            this.toolSetButtonConfigSW.ShowDropDownArrow = false;
            this.toolSetButtonConfigSW.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSetButtonConfigSW.DropDownOpening += new System.EventHandler(this.configSWMenu_Refresh);
            // 
            // toolSetButtonLogSave
            // 
            this.toolSetButtonLogSave.DropDown = this.logMenu;
            this.toolSetButtonLogSave.DropDownButtonWidth = 15;
            this.toolSetButtonLogSave.Name = "toolSetButtonLogSave";
            this.toolSetButtonLogSave.Size = new System.Drawing.Size(80, 85);
            this.toolSetButtonLogSave.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
            this.toolSetButtonLogSave.ToolTipText = "导出日志";
            this.toolSetButtonLogSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSetButtonLogSave.ButtonClick += new System.EventHandler(this.toolSetButtonLogSave_Click);
            this.toolSetButtonLogSave.DropDownOpening += new System.EventHandler(this.logMenuDropDown_Opening);
            // 
            // toolSetButtonTheme
            // 
            this.toolSetButtonTheme.Name = "toolSetButtonTheme";
            this.toolSetButtonTheme.Size = new System.Drawing.Size(84, 85);
            this.toolSetButtonTheme.Margin = new System.Windows.Forms.Padding(4, 1, 4, 2);
            this.toolSetButtonTheme.ToolTipText = "切换主题";
            this.toolSetButtonTheme.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSetButtonTheme.Click += new System.EventHandler(this.toolSetButtonTheme_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel1,
            this.statusSeparator,
            this.statusLabel2});
            this.statusBar.Location = new System.Drawing.Point(0, 427);
            this.statusBar.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.Name = "statusBar";
            this.statusBar.TabIndex = 2;
            // 
            // statusLabel1
            // 
            this.statusLabel1.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(0, 21);
            this.statusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusSeparator
            // 
            this.statusSeparator.Name = "statusSeparator";
            // 
            // toolMenuSeparator1
            // 
            this.toolMenuSeparator1.Name = "toolMenuSeparator1";
            // 
            // toolMenuSeparator2
            // 
            this.toolMenuSeparator2.Name = "toolMenuSeparator2";
            // 
            // toolMenuSeparator3
            // 
            this.toolMenuSeparator3.Name = "toolMenuSeparator3";
            // 
            // statusLabel2
            // 
            this.statusLabel2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.statusLabel2.Name = "statusLabel2";
            this.statusLabel2.Size = new System.Drawing.Size(0, 21);
            this.statusLabel2.AutoSize = false;
            this.statusLabel2.Spring = true;
            this.statusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerBlink
            // 
            this.timerBlink.Interval = 500;
            this.timerBlink.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.Add(this.logPanel);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.toolSet);
            this.Icon = global::CIT.Properties.Resources.icon2;
            this.Name = "MainForm";
            this.Text = "CIT";
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.window_Exit);
            this.SizeChanged += new System.EventHandler(this.window_StateChanged);
            this.logMenu.ResumeLayout(false);
            this.trayMenu.ResumeLayout(false);
            this.toolSet.ResumeLayout(false);
            this.toolSet.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.Panel logPanel;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip configSWMenu;
        private System.Windows.Forms.ContextMenuStrip logMenu;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem trayMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem trayMenuItemReload;
        private System.Windows.Forms.ToolStripMenuItem trayMenuItemConfigSW;
        private System.Windows.Forms.ToolStripSeparator trayMenuSeparator;
        private System.Windows.Forms.ContextMenuStrip logTextBoxMenu;
        private System.Windows.Forms.ToolStripMenuItem logMenuItemClear;
        private System.Windows.Forms.ToolStripMenuItem logMenuItemCopy;
        private System.Windows.Forms.ToolStrip toolSet;
        private System.Windows.Forms.ToolStripButton toolSetButtonReload;
        private System.Windows.Forms.ToolStripButton toolSetButtonSettings;
        private System.Windows.Forms.ToolStripDropDownButton toolSetButtonConfigSW;
        private System.Windows.Forms.ToolStripSplitButton toolSetButtonLogSave;
        private System.Windows.Forms.ToolStripButton toolSetButtonTheme;
        private System.Windows.Forms.ToolStripMenuItem logMenuItemSettings;
        private System.Windows.Forms.ToolStripSeparator toolMenuSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolMenuSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolMenuSeparator3;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel1;
        private System.Windows.Forms.ToolStripSeparator statusSeparator;
        private System.Windows.Forms.Timer timerBlink;
    }
}

