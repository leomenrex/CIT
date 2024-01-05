namespace CIT
{
    partial class SettingsForm
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
            this.buttonExecute = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelExecuteTips = new System.Windows.Forms.Label();
            this.buttonAddRow = new System.Windows.Forms.Button();
            this.buttonDelRow = new System.Windows.Forms.Button();
            this.labelDataTips = new System.Windows.Forms.Label();
            this.labelCfgSubTitle = new System.Windows.Forms.Label();
            this.labelCfgTitle = new System.Windows.Forms.Label();
            this.labelExecuteTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
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
            // buttonExecute
            // 
            this.buttonExecute.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left));
            this.buttonExecute.Location = new System.Drawing.Point(28, 52);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 25);
            this.buttonExecute.TabIndex = 0;
            this.buttonExecute.Text = "选择文件";
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            this.buttonExecute.Enter += new System.EventHandler(this.RemoveFocus);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            this.buttonConfirm.Location = new System.Drawing.Point(431, 354);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 25);
            this.buttonConfirm.TabIndex = 10;
            this.buttonConfirm.Text = "保存";
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGrid.Location = new System.Drawing.Point(28, 176);
            this.dataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 25;
            this.dataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGrid.Size = new System.Drawing.Size(418, 142);
            this.dataGrid.AutoSize = false;
            this.dataGrid.TabIndex = 16;
            this.dataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGird_DoubleClick);
            this.dataGrid.SelectionChanged += new System.EventHandler(this.dataGird_SelectionChange);
            this.dataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid_KeysDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 70F;
            this.dataGridViewTextBoxColumn1.HeaderText = "自定义配置名";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 20;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn2.HeaderText = "命令参数（可选）";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 100;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.ToolTipText = "若参数里需要用到配置文件，请用%代替";
            this.dataGridViewTextBoxColumn2.Width = 95;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn3.HeaderText = "配置文件（可选）";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 95;
            // 
            // labelExecuteTips
            // 
            this.labelExecuteTips.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Left));
            this.labelExecuteTips.Margin = new System.Windows.Forms.Padding(9, 3, 3, 3);
            this.labelExecuteTips.AutoSize = true;
            this.labelExecuteTips.Location = new System.Drawing.Point(123, 56);
            this.labelExecuteTips.Name = "labelExecuteTips";
            this.labelExecuteTips.Size = new System.Drawing.Size(65, 12);
            this.labelExecuteTips.TabIndex = 13;
            this.labelExecuteTips.Text = "当前未选定";
            // 
            // buttonAddRow
            // 
            this.buttonAddRow.Location = new System.Drawing.Point(456, 180);
            this.buttonAddRow.Name = "buttonAddRow";
            this.buttonAddRow.Size = new System.Drawing.Size(46, 36);
            this.buttonAddRow.TabIndex = 14;
            this.buttonAddRow.Text = "增行";
            this.buttonAddRow.Click += new System.EventHandler(this.buttonAddRow_Click);
            this.buttonAddRow.Enter += new System.EventHandler(this.RemoveFocus);
            // 
            // buttonDelRow
            // 
            this.buttonDelRow.Location = new System.Drawing.Point(456, 250);
            this.buttonDelRow.Name = "buttonDelRow";
            this.buttonDelRow.Size = new System.Drawing.Size(46, 36);
            this.buttonDelRow.TabIndex = 15;
            this.buttonDelRow.Text = "删行";
            this.buttonDelRow.Click += new System.EventHandler(this.buttonDelRow_Click);
            this.buttonDelRow.Enter += new System.EventHandler(this.RemoveFocus);
            // 
            // labelDataTips
            // 
            this.labelDataTips.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.labelDataTips.AutoSize = true;
            this.labelDataTips.Location = new System.Drawing.Point(28, 322);
            this.labelDataTips.Name = "labelDataTips";
            this.labelDataTips.Size = new System.Drawing.Size(0, 12);
            this.labelDataTips.TabIndex = 0;
            // 
            // labelCfgSubTitle
            // 
            this.labelCfgSubTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCfgSubTitle.Location = new System.Drawing.Point(28, 134);
            this.labelCfgSubTitle.Name = "labelCfgSubTitle";
            this.labelCfgSubTitle.Size = new System.Drawing.Size(481, 34);
            this.labelCfgSubTitle.TabIndex = 17;
            this.labelCfgSubTitle.Text = "说明：单击选中，Delete键清空单元格，双击编辑单元格（命令参数里可以用 % 表示配置文件，如：-config %）";
            this.labelCfgSubTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelExecuteTitle
            // 
            this.labelExecuteTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelExecuteTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelExecuteTitle.Location = new System.Drawing.Point(14, 15);
            this.labelExecuteTitle.Name = "labelExecuteTitle";
            this.labelExecuteTitle.Size = new System.Drawing.Size(493, 25);
            this.labelExecuteTitle.TabIndex = 18;
            this.labelExecuteTitle.Text = " 执行程序文件";
            this.labelExecuteTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCfgTitle
            // 
            this.labelCfgTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCfgTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCfgTitle.Location = new System.Drawing.Point(14, 100);
            this.labelCfgTitle.Name = "labelCfgTitle";
            this.labelCfgTitle.Size = new System.Drawing.Size(493, 25);
            this.labelCfgTitle.TabIndex = 19;
            this.labelCfgTitle.Text = " 后接参数配置";
            this.labelCfgTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 1;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayout.Controls.Add(this.labelExecuteTitle, 0, 0);
            this.mainTableLayout.Controls.Add(this.subFlowLayout1, 0, 1);
            this.mainTableLayout.Controls.Add(this.labelCfgTitle, 0, 2);
            this.mainTableLayout.Controls.Add(this.subTableLayout1, 0, 3);
            this.mainTableLayout.Controls.Add(this.buttonConfirm, 0, 4);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Padding = new System.Windows.Forms.Padding(10, 20, 10, 15);
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 5;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.Size = new System.Drawing.Size(369, 205);
            this.mainTableLayout.AutoSize = true;
            this.mainTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            // 
            // subTableLayout1
            // 
            this.subTableLayout1.ColumnCount = 2;
            this.subTableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.subTableLayout1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.subTableLayout1.Controls.Add(this.labelCfgSubTitle, 0, 0);
            this.subTableLayout1.Controls.Add(this.dataGrid, 0, 1);
            this.subTableLayout1.Controls.Add(this.buttonAddRow, 1, 1);
            this.subTableLayout1.Controls.Add(this.buttonDelRow, 1, 2);
            this.subTableLayout1.Controls.Add(this.labelDataTips, 0, 3);
            this.subTableLayout1.SetColumnSpan(this.labelCfgSubTitle, 2);
            this.subTableLayout1.SetRowSpan(this.dataGrid, 2);
            this.subTableLayout1.SetColumnSpan(this.labelDataTips, 2);
            this.subTableLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subTableLayout1.Padding = new System.Windows.Forms.Padding(10, 2, 0, 10);
            this.subTableLayout1.Location = new System.Drawing.Point(0, 0);
            this.subTableLayout1.Name = "subTableLayout1";
            this.subTableLayout1.RowCount = 4;
            this.subTableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.subTableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.subTableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.subTableLayout1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.subTableLayout1.AutoSize = true;
            this.subFlowLayout1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            // 
            // subFlowLayout1
            // 
            this.subFlowLayout1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 10);
            this.subFlowLayout1.AutoSize = true;
            this.subFlowLayout1.Controls.Add(this.buttonExecute);
            this.subFlowLayout1.Controls.Add(this.labelExecuteTips);
            this.subFlowLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subFlowLayout1.Location = new System.Drawing.Point(3, 49);
            this.subFlowLayout1.Name = "subFlowLayout1";
            this.subFlowLayout1.Size = new System.Drawing.Size(363, 26);
            // 
            // SettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 400);
            this.MinimumSize = new System.Drawing.Size(537, 438);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.mainTableLayout);
            this.ShowInTaskbar = false;
            this.MaximizeBox = true;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "配置明细";
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = global::CIT.Properties.Resources.icon2;
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.subFlowLayout1.ResumeLayout(false);
            this.subFlowLayout1.PerformLayout();
            this.subTableLayout1.ResumeLayout(false);
            this.subTableLayout1.PerformLayout();
            this.subTableLayout2.ResumeLayout(false);
            this.subTableLayout2.PerformLayout();
            this.mainTableLayout.ResumeLayout(false);
            this.mainTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label labelExecuteTips;
        private System.Windows.Forms.Button buttonAddRow;
        private System.Windows.Forms.Button buttonDelRow;
        private System.Windows.Forms.Label labelDataTips;
        private System.Windows.Forms.Label labelCfgSubTitle;
        private System.Windows.Forms.Label labelExecuteTitle;
        private System.Windows.Forms.Label labelCfgTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.TableLayoutPanel subTableLayout1;
        private System.Windows.Forms.TableLayoutPanel subTableLayout2;
        private System.Windows.Forms.FlowLayoutPanel subFlowLayout1;
    }
}