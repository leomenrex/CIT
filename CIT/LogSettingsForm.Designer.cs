namespace CIT
{
    partial class LogSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxKeepData = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoSave = new System.Windows.Forms.CheckBox();
            this.comboBoxMaxLine = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.labelKeepDataTitle = new System.Windows.Forms.Label();
            this.labelAutoSaveTitle = new System.Windows.Forms.Label();
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.mainTableLayout.SuspendLayout();
            this.subFlowLayout1 = new System.Windows.Forms.FlowLayoutPanel();
            this.subFlowLayout1.SuspendLayout();
            this.subTableLayout1 = new System.Windows.Forms.TableLayoutPanel();
            this.subTableLayout1.SuspendLayout();
            this.subTableLayout2 = new System.Windows.Forms.TableLayoutPanel();
            this.subTableLayout2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxKeepData
            // 
            this.checkBoxKeepData.Location = new System.Drawing.Point(26, 80);
            this.checkBoxKeepData.Name = "checkBoxKeepData";
            this.checkBoxKeepData.Size = new System.Drawing.Size(178, 25);
            this.checkBoxKeepData.TabIndex = 6;
            this.checkBoxKeepData.TabStop = false;
            this.checkBoxKeepData.Text = "     重载(非重启)时保留日志？";
            this.checkBoxKeepData.Click += new System.EventHandler(this.checkBoxKeepData_Click);
            this.checkBoxKeepData.Paint += new System.Windows.Forms.PaintEventHandler(this.checkBox_PaintNormal);
            this.checkBoxKeepData.MouseEnter += new System.EventHandler(this.checkBox_MouseEnter);
            this.checkBoxKeepData.MouseLeave += new System.EventHandler(this.checkBox_MouseLeave);
            // 
            // checkBoxAutoSave
            // 
            this.checkBoxAutoSave.Location = new System.Drawing.Point(26, 157);
            this.checkBoxAutoSave.Name = "checkBoxAutoSave";
            this.checkBoxAutoSave.Size = new System.Drawing.Size(237, 25);
            this.checkBoxAutoSave.TabIndex = 5;
            this.checkBoxAutoSave.TabStop = false;
            this.checkBoxAutoSave.Text = "     重载时日志自动写入文件？(当前目录)";
            this.checkBoxAutoSave.Click += new System.EventHandler(this.checkBoxAutoSave_Click);
            this.checkBoxAutoSave.Paint += new System.Windows.Forms.PaintEventHandler(this.checkBox_PaintNormal);
            this.checkBoxAutoSave.MouseEnter += new System.EventHandler(this.checkBox_MouseEnter);
            this.checkBoxAutoSave.MouseLeave += new System.EventHandler(this.checkBox_MouseLeave);
            // 
            // comboBoxMaxLine
            // 
            this.comboBoxMaxLine.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left));
            this.comboBoxMaxLine.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.comboBoxMaxLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaxLine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBoxMaxLine.Items.AddRange(new object[] {
            1,
            3,
            5,
            10,
            30,
            50,
            90});
            this.comboBoxMaxLine.Location = new System.Drawing.Point(110, 50);
            this.comboBoxMaxLine.Name = "comboBoxMaxLine";
            this.comboBoxMaxLine.Size = new System.Drawing.Size(55, 20);
            this.comboBoxMaxLine.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Margin = new System.Windows.Forms.Padding(0, 4, 3, 2);
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "日志最大保留";
            // 
            // label2
            // 
            this.label2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 2);
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "千行";
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.buttonConfirm.Location = new System.Drawing.Point(191, 204);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 25);
            this.buttonConfirm.MinimumSize = new System.Drawing.Size(75, 25);
            this.buttonConfirm.TabIndex = 0;
            this.buttonConfirm.Text = "保存";
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // labelKeepDataTitle
            // 
            this.labelKeepDataTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKeepDataTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelKeepDataTitle.Location = new System.Drawing.Point(14, 15);
            this.labelKeepDataTitle.Name = "labelKeepDataTitle";
            this.labelKeepDataTitle.Size = new System.Drawing.Size(252, 25);
            this.labelKeepDataTitle.TabIndex = 7;
            this.labelKeepDataTitle.Text = " 日志保留";
            this.labelKeepDataTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelAutoSaveTitle
            // 
            this.labelAutoSaveTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAutoSaveTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelAutoSaveTitle.Location = new System.Drawing.Point(14, 122);
            this.labelAutoSaveTitle.Name = "labelAutoSaveTitle";
            this.labelAutoSaveTitle.Size = new System.Drawing.Size(252, 25);
            this.labelAutoSaveTitle.TabIndex = 8;
            this.labelAutoSaveTitle.Text = " 自动导出";
            this.labelAutoSaveTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayout.Controls.Add(this.labelKeepDataTitle, 0, 0);
            this.mainTableLayout.Controls.Add(this.subTableLayout1, 0, 1);
            this.mainTableLayout.Controls.Add(this.labelAutoSaveTitle, 0, 2);
            this.mainTableLayout.Controls.Add(this.subTableLayout2, 0, 3);
            this.mainTableLayout.Controls.Add(this.buttonConfirm, 0, 4);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Padding = new System.Windows.Forms.Padding(10, 20, 10, 15);
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 5;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.Size = new System.Drawing.Size(369, 205);
            this.mainTableLayout.AutoSize = true;
            this.mainTableLayout.TabIndex = 9;
            // 
            // subFlowLayout1
            // 
            this.subFlowLayout1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.subFlowLayout1.AutoSize = true;
            this.subFlowLayout1.Controls.Add(this.label1);
            this.subFlowLayout1.Controls.Add(this.comboBoxMaxLine);
            this.subFlowLayout1.Controls.Add(this.label2);
            this.subFlowLayout1.Location = new System.Drawing.Point(3, 49);
            this.subFlowLayout1.Name = "subFlowLayout1";
            this.subFlowLayout1.Size = new System.Drawing.Size(363, 26);
            this.subFlowLayout1.TabIndex = 10;
            // 
            // subTableLayout1
            // 
            this.subTableLayout1.ColumnCount = 1;
            this.subTableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.subTableLayout1.Controls.Add(this.subFlowLayout1);
            this.subTableLayout1.Controls.Add(this.checkBoxKeepData, 0, 1);
            this.subTableLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subTableLayout1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 10);
            this.subTableLayout1.Name = "subTableLayout1";
            this.subTableLayout1.RowCount = 2;
            this.subTableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.subTableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.subTableLayout1.AutoSize = true;
            // 
            // subTableLayout2
            // 
            this.subTableLayout2.ColumnCount = 1;
            this.subTableLayout2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.subTableLayout2.Controls.Add(this.checkBoxAutoSave, 0, 1);
            this.subTableLayout2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subTableLayout2.Padding = new System.Windows.Forms.Padding(10, 2, 0, 10);
            this.subTableLayout2.Name = "subTableLayout2";
            this.subTableLayout2.RowCount = 1;
            this.subTableLayout2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.subTableLayout2.AutoSize = true;
            // 
            // LogSettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(280, 250);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.mainTableLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ShowInTaskbar = false;
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = global::CIT.Properties.Resources.icon2;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogSettingsForm";
            this.Text = "日志设置";
            this.subFlowLayout1.ResumeLayout(false);
            this.subFlowLayout1.PerformLayout();
            this.subTableLayout1.ResumeLayout(false);
            this.subTableLayout1.PerformLayout();
            this.subTableLayout2.ResumeLayout(false);
            this.subTableLayout2.PerformLayout();
            this.mainTableLayout.ResumeLayout(false);
            this.mainTableLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxKeepData;
        private System.Windows.Forms.CheckBox checkBoxAutoSave;
        private System.Windows.Forms.ComboBox comboBoxMaxLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Label labelKeepDataTitle;
        private System.Windows.Forms.Label labelAutoSaveTitle;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.TableLayoutPanel subTableLayout1;
        private System.Windows.Forms.TableLayoutPanel subTableLayout2;
        private System.Windows.Forms.FlowLayoutPanel subFlowLayout1;
    }
}