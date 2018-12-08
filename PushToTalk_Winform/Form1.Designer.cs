namespace PushToTalk_Winform
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.changeHotkeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.autoOptimizeMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.hotKey_label = new System.Windows.Forms.Label();
            this.changeHotkey_btn = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // changeHotkeyToolStripMenuItem
            // 
            this.changeHotkeyToolStripMenuItem.Enabled = false;
            this.changeHotkeyToolStripMenuItem.Name = "changeHotkeyToolStripMenuItem";
            this.changeHotkeyToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.changeHotkeyToolStripMenuItem.Text = "Change Hotkey";
            this.changeHotkeyToolStripMenuItem.Click += new System.EventHandler(this.changeHotkeyToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoOptimizeMemoryToolStripMenuItem,
            this.changeHotkeyToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(200, 92);
            // 
            // autoOptimizeMemoryToolStripMenuItem
            // 
            this.autoOptimizeMemoryToolStripMenuItem.Name = "autoOptimizeMemoryToolStripMenuItem";
            this.autoOptimizeMemoryToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.autoOptimizeMemoryToolStripMenuItem.Text = "Auto Optimize Memory";
            this.autoOptimizeMemoryToolStripMenuItem.CheckedChanged += new System.EventHandler(this.autoOptimizeMemoryToolStripMenuItem_CheckedChanged);
            this.autoOptimizeMemoryToolStripMenuItem.Click += new System.EventHandler(this.autoOptimizeMemoryToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Text = "PushToTalk";
            this.notifyIcon1.Visible = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "The hotkey right now is :";
            // 
            // hotKey_label
            // 
            this.hotKey_label.AutoSize = true;
            this.hotKey_label.Location = new System.Drawing.Point(262, 114);
            this.hotKey_label.Name = "hotKey_label";
            this.hotKey_label.Size = new System.Drawing.Size(39, 13);
            this.hotKey_label.TabIndex = 2;
            this.hotKey_label.Text = "Loding";
            // 
            // changeHotkey_btn
            // 
            this.changeHotkey_btn.Location = new System.Drawing.Point(136, 140);
            this.changeHotkey_btn.Name = "changeHotkey_btn";
            this.changeHotkey_btn.Size = new System.Drawing.Size(165, 23);
            this.changeHotkey_btn.TabIndex = 3;
            this.changeHotkey_btn.Text = "Change Hotkey";
            this.changeHotkey_btn.UseVisualStyleBackColor = true;
            this.changeHotkey_btn.Click += new System.EventHandler(this.changeHotkey_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 261);
            this.Controls.Add(this.changeHotkey_btn);
            this.Controls.Add(this.hotKey_label);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem changeHotkeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label hotKey_label;
        private System.Windows.Forms.Button changeHotkey_btn;
        private System.Windows.Forms.ToolStripMenuItem autoOptimizeMemoryToolStripMenuItem;
    }
}

