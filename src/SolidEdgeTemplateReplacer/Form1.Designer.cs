namespace SolidEdgeTemplateReplacer
{
    partial class frmTemplateReplacer
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.seVersionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.fileAlreadyOpenChkBox = new System.Windows.Forms.CheckBox();
            this.targetFileTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.templateFileTextBox = new System.Windows.Forms.TextBox();
            this.targetFileBtn = new System.Windows.Forms.Button();
            this.templateFileBtn = new System.Windows.Forms.Button();
            this.leaveTempOpenChkBox = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.clearLogChkbox = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seVersionLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 408);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(424, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // seVersionLabel
            // 
            this.seVersionLabel.Name = "seVersionLabel";
            this.seVersionLabel.Size = new System.Drawing.Size(66, 17);
            this.seVersionLabel.Text = "SE Version: ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(424, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(347, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Go";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fileAlreadyOpenChkBox
            // 
            this.fileAlreadyOpenChkBox.AutoSize = true;
            this.fileAlreadyOpenChkBox.Location = new System.Drawing.Point(106, 88);
            this.fileAlreadyOpenChkBox.Name = "fileAlreadyOpenChkBox";
            this.fileAlreadyOpenChkBox.Size = new System.Drawing.Size(144, 17);
            this.fileAlreadyOpenChkBox.TabIndex = 3;
            this.fileAlreadyOpenChkBox.Text = "Pick from Open SE Docs";
            this.fileAlreadyOpenChkBox.UseVisualStyleBackColor = true;
            this.fileAlreadyOpenChkBox.CheckedChanged += new System.EventHandler(this.fileAlreadyOpenChkBox_CheckedChanged);
            // 
            // targetFileTextbox
            // 
            this.targetFileTextbox.Location = new System.Drawing.Point(106, 62);
            this.targetFileTextbox.Name = "targetFileTextbox";
            this.targetFileTextbox.Size = new System.Drawing.Size(213, 20);
            this.targetFileTextbox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Target File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Template File:";
            // 
            // templateFileTextBox
            // 
            this.templateFileTextBox.Location = new System.Drawing.Point(106, 135);
            this.templateFileTextBox.Name = "templateFileTextBox";
            this.templateFileTextBox.Size = new System.Drawing.Size(213, 20);
            this.templateFileTextBox.TabIndex = 6;
            // 
            // targetFileBtn
            // 
            this.targetFileBtn.Location = new System.Drawing.Point(325, 60);
            this.targetFileBtn.Name = "targetFileBtn";
            this.targetFileBtn.Size = new System.Drawing.Size(52, 23);
            this.targetFileBtn.TabIndex = 8;
            this.targetFileBtn.Text = "Browse";
            this.targetFileBtn.UseVisualStyleBackColor = true;
            this.targetFileBtn.Click += new System.EventHandler(this.targetFileBtn_Click);
            // 
            // templateFileBtn
            // 
            this.templateFileBtn.Location = new System.Drawing.Point(325, 133);
            this.templateFileBtn.Name = "templateFileBtn";
            this.templateFileBtn.Size = new System.Drawing.Size(52, 23);
            this.templateFileBtn.TabIndex = 9;
            this.templateFileBtn.Text = "Browse";
            this.templateFileBtn.UseVisualStyleBackColor = true;
            this.templateFileBtn.Click += new System.EventHandler(this.templateFileBtn_Click);
            // 
            // leaveTempOpenChkBox
            // 
            this.leaveTempOpenChkBox.AutoSize = true;
            this.leaveTempOpenChkBox.Location = new System.Drawing.Point(106, 184);
            this.leaveTempOpenChkBox.Name = "leaveTempOpenChkBox";
            this.leaveTempOpenChkBox.Size = new System.Drawing.Size(157, 17);
            this.leaveTempOpenChkBox.TabIndex = 10;
            this.leaveTempOpenChkBox.Text = "Leave Template Open After";
            this.leaveTempOpenChkBox.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(106, 161);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(144, 17);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.Text = "Pick from Open SE Docs";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 301);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 104);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result Log";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(394, 85);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // clearLogChkbox
            // 
            this.clearLogChkbox.AutoSize = true;
            this.clearLogChkbox.Location = new System.Drawing.Point(15, 278);
            this.clearLogChkbox.Name = "clearLogChkbox";
            this.clearLogChkbox.Size = new System.Drawing.Size(109, 17);
            this.clearLogChkbox.TabIndex = 13;
            this.clearLogChkbox.Text = "Clear Log on Run";
            this.clearLogChkbox.UseVisualStyleBackColor = true;
            // 
            // frmTemplateReplacer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 430);
            this.Controls.Add(this.clearLogChkbox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.leaveTempOpenChkBox);
            this.Controls.Add(this.templateFileBtn);
            this.Controls.Add(this.targetFileBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.templateFileTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.targetFileTextbox);
            this.Controls.Add(this.fileAlreadyOpenChkBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmTemplateReplacer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Solid Edge Template Replacer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripStatusLabel seVersionLabel;
        private System.Windows.Forms.CheckBox fileAlreadyOpenChkBox;
        private System.Windows.Forms.TextBox targetFileTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox templateFileTextBox;
        private System.Windows.Forms.Button targetFileBtn;
        private System.Windows.Forms.Button templateFileBtn;
        private System.Windows.Forms.CheckBox leaveTempOpenChkBox;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox clearLogChkbox;
    }
}

