namespace FDBMerge
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            txtFileA = new TextBox();
            txtFileB = new TextBox();
            btnBrowseA = new Button();
            btnBrowseB = new Button();
            btnLoad = new Button();
            gridFieldA = new DataGridView();
            gridFieldB = new DataGridView();
            lblFileA = new Label();
            lblFileB = new Label();
            lblFieldA = new Label();
            lblFieldB = new Label();
            lblCountA = new Label();
            lblCountB = new Label();
            gridPreview = new DataGridView();
            lblPreview = new Label();
            btnSave = new Button();
            grpMerge = new GroupBox();
            labelHintm = new Label();
            btnMerge = new Button();
            rbMerge = new RadioButton();
            rbReplace = new RadioButton();
            grpReplace = new GroupBox();
            labelHintr = new Label();
            lblKey = new Label();
            cmbKey = new ComboBox();
            btnReplace = new Button();
            btnSlctall = new Button();
            btnUnslcall = new Button();
            ((System.ComponentModel.ISupportInitialize)gridFieldA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridFieldB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridPreview).BeginInit();
            grpMerge.SuspendLayout();
            grpReplace.SuspendLayout();
            SuspendLayout();
            // 
            // txtFileA
            // 
            txtFileA.Location = new Point(68, 18);
            txtFileA.Name = "txtFileA";
            txtFileA.PlaceholderText = "Your client file (.fdb to be updated)";
            txtFileA.Size = new Size(593, 23);
            txtFileA.TabIndex = 0;
            // 
            // txtFileB
            // 
            txtFileB.Location = new Point(68, 49);
            txtFileB.Name = "txtFileB";
            txtFileB.PlaceholderText = "Import file (.fdb to merge or replace)";
            txtFileB.Size = new Size(593, 23);
            txtFileB.TabIndex = 3;
            // 
            // btnBrowseA
            // 
            btnBrowseA.Location = new Point(667, 18);
            btnBrowseA.Name = "btnBrowseA";
            btnBrowseA.Size = new Size(80, 25);
            btnBrowseA.TabIndex = 1;
            btnBrowseA.Text = "Browse...";
            // 
            // btnBrowseB
            // 
            btnBrowseB.Location = new Point(667, 47);
            btnBrowseB.Name = "btnBrowseB";
            btnBrowseB.Size = new Size(80, 25);
            btnBrowseB.TabIndex = 4;
            btnBrowseB.Text = "Browse...";
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(753, 18);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(161, 54);
            btnLoad.TabIndex = 6;
            btnLoad.Text = "Load";
            // 
            // gridFieldA
            // 
            gridFieldA.AllowUserToAddRows = false;
            gridFieldA.AllowUserToDeleteRows = false;
            gridFieldA.AllowUserToResizeRows = false;
            gridFieldA.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridFieldA.Location = new Point(481, 123);
            gridFieldA.Name = "gridFieldA";
            gridFieldA.RowHeadersVisible = false;
            gridFieldA.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridFieldA.Size = new Size(204, 337);
            gridFieldA.TabIndex = 7;
            // 
            // gridFieldB
            // 
            gridFieldB.AllowUserToAddRows = false;
            gridFieldB.AllowUserToDeleteRows = false;
            gridFieldB.AllowUserToResizeRows = false;
            gridFieldB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridFieldB.Location = new Point(711, 123);
            gridFieldB.Name = "gridFieldB";
            gridFieldB.RowHeadersVisible = false;
            gridFieldB.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridFieldB.Size = new Size(204, 337);
            gridFieldB.TabIndex = 8;
            // 
            // lblFileA
            // 
            lblFileA.AutoSize = true;
            lblFileA.Location = new Point(20, 22);
            lblFileA.Name = "lblFileA";
            lblFileA.Size = new Size(39, 15);
            lblFileA.TabIndex = 2;
            lblFileA.Text = "File A:";
            // 
            // lblFileB
            // 
            lblFileB.AutoSize = true;
            lblFileB.Location = new Point(20, 53);
            lblFileB.Name = "lblFileB";
            lblFileB.Size = new Size(38, 15);
            lblFileB.TabIndex = 5;
            lblFileB.Text = "File B:";
            // 
            // lblFieldA
            // 
            lblFieldA.AutoSize = true;
            lblFieldA.Location = new Point(481, 103);
            lblFieldA.Name = "lblFieldA";
            lblFieldA.Size = new Size(85, 15);
            lblFieldA.TabIndex = 9;
            lblFieldA.Text = "Fields in File A:";
            // 
            // lblFieldB
            // 
            lblFieldB.AutoSize = true;
            lblFieldB.Location = new Point(711, 103);
            lblFieldB.Name = "lblFieldB";
            lblFieldB.Size = new Size(84, 15);
            lblFieldB.TabIndex = 10;
            lblFieldB.Text = "Fields in File B:";
            // 
            // lblCountA
            // 
            lblCountA.AutoSize = true;
            lblCountA.Location = new Point(481, 463);
            lblCountA.Name = "lblCountA";
            lblCountA.Size = new Size(72, 15);
            lblCountA.TabIndex = 11;
            lblCountA.Text = "Total Item: 0";
            // 
            // lblCountB
            // 
            lblCountB.AutoSize = true;
            lblCountB.Location = new Point(711, 463);
            lblCountB.Name = "lblCountB";
            lblCountB.Size = new Size(72, 15);
            lblCountB.TabIndex = 12;
            lblCountB.Text = "Total Item: 0";
            // 
            // gridPreview
            // 
            gridPreview.AllowUserToAddRows = false;
            gridPreview.AllowUserToDeleteRows = false;
            gridPreview.AllowUserToResizeRows = false;
            gridPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPreview.Location = new Point(20, 529);
            gridPreview.Name = "gridPreview";
            gridPreview.RowHeadersVisible = false;
            gridPreview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridPreview.Size = new Size(890, 200);
            gridPreview.TabIndex = 16;
            // 
            // lblPreview
            // 
            lblPreview.AutoSize = true;
            lblPreview.Location = new Point(20, 511);
            lblPreview.Name = "lblPreview";
            lblPreview.Size = new Size(134, 15);
            lblPreview.TabIndex = 15;
            lblPreview.Text = "Preview Merge/Replace:";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(789, 739);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(121, 37);
            btnSave.TabIndex = 17;
            btnSave.Text = "Save FDB";
            btnSave.Click += btnSave_Click;
            // 
            // grpMerge
            // 
            grpMerge.Controls.Add(labelHintm);
            grpMerge.Controls.Add(btnMerge);
            grpMerge.Location = new Point(21, 112);
            grpMerge.Name = "grpMerge";
            grpMerge.Size = new Size(439, 155);
            grpMerge.TabIndex = 13;
            grpMerge.TabStop = false;
            grpMerge.Text = "Merge Option";
            // 
            // labelHintm
            // 
            labelHintm.AutoSize = true;
            labelHintm.Location = new Point(71, 62);
            labelHintm.Name = "labelHintm";
            labelHintm.Size = new Size(280, 75);
            labelHintm.TabIndex = 3;
            labelHintm.Text = resources.GetString("labelHintm.Text");
            // 
            // btnMerge
            // 
            btnMerge.Location = new Point(136, 22);
            btnMerge.Name = "btnMerge";
            btnMerge.Size = new Size(140, 27);
            btnMerge.TabIndex = 2;
            btnMerge.Text = "Merge";
            btnMerge.UseVisualStyleBackColor = true;
            // 
            // rbMerge
            // 
            rbMerge.Location = new Point(21, 86);
            rbMerge.Name = "rbMerge";
            rbMerge.Size = new Size(70, 20);
            rbMerge.TabIndex = 0;
            rbMerge.Text = "Merge";
            // 
            // rbReplace
            // 
            rbReplace.Location = new Point(21, 273);
            rbReplace.Name = "rbReplace";
            rbReplace.Size = new Size(70, 20);
            rbReplace.TabIndex = 1;
            rbReplace.Text = "Replace";
            // 
            // grpReplace
            // 
            grpReplace.Controls.Add(labelHintr);
            grpReplace.Controls.Add(lblKey);
            grpReplace.Controls.Add(cmbKey);
            grpReplace.Controls.Add(btnReplace);
            grpReplace.Location = new Point(21, 296);
            grpReplace.Name = "grpReplace";
            grpReplace.Size = new Size(439, 211);
            grpReplace.TabIndex = 14;
            grpReplace.TabStop = false;
            grpReplace.Text = "Replace Option";
            // 
            // labelHintr
            // 
            labelHintr.AutoSize = true;
            labelHintr.Location = new Point(35, 92);
            labelHintr.Name = "labelHintr";
            labelHintr.Size = new Size(346, 90);
            labelHintr.TabIndex = 3;
            labelHintr.Text = resources.GetString("labelHintr.Text");
            // 
            // lblKey
            // 
            lblKey.Location = new Point(48, 25);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(90, 20);
            lblKey.TabIndex = 0;
            lblKey.Text = "Matching Key:";
            // 
            // cmbKey
            // 
            cmbKey.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbKey.Location = new Point(136, 22);
            cmbKey.Name = "cmbKey";
            cmbKey.Size = new Size(140, 23);
            cmbKey.TabIndex = 1;
            // 
            // btnReplace
            // 
            btnReplace.Location = new Point(136, 50);
            btnReplace.Name = "btnReplace";
            btnReplace.Size = new Size(140, 25);
            btnReplace.TabIndex = 2;
            btnReplace.Text = "Replace";
            btnReplace.UseVisualStyleBackColor = true;
            btnReplace.Click += btnReplace_Click;
            // 
            // btnSlctall
            // 
            btnSlctall.Location = new Point(572, 484);
            btnSlctall.Name = "btnSlctall";
            btnSlctall.Size = new Size(91, 23);
            btnSlctall.TabIndex = 18;
            btnSlctall.Text = "Select All";
            btnSlctall.UseVisualStyleBackColor = true;
            btnSlctall.Click += btnSlctall_Click;
            // 
            // btnUnslcall
            // 
            btnUnslcall.Location = new Point(475, 484);
            btnUnslcall.Name = "btnUnslcall";
            btnUnslcall.Size = new Size(91, 23);
            btnUnslcall.TabIndex = 19;
            btnUnslcall.Text = "Unselect All";
            btnUnslcall.UseVisualStyleBackColor = true;
            btnUnslcall.Click += btnUnslcall_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 785);
            Controls.Add(btnUnslcall);
            Controls.Add(btnSlctall);
            Controls.Add(rbMerge);
            Controls.Add(txtFileA);
            Controls.Add(rbReplace);
            Controls.Add(btnBrowseA);
            Controls.Add(lblFileA);
            Controls.Add(txtFileB);
            Controls.Add(btnBrowseB);
            Controls.Add(lblFileB);
            Controls.Add(btnLoad);
            Controls.Add(gridFieldA);
            Controls.Add(gridFieldB);
            Controls.Add(lblFieldA);
            Controls.Add(lblFieldB);
            Controls.Add(lblCountA);
            Controls.Add(lblCountB);
            Controls.Add(grpMerge);
            Controls.Add(grpReplace);
            Controls.Add(lblPreview);
            Controls.Add(gridPreview);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FDB File Merge & Replace by DuaSelipar";
            ((System.ComponentModel.ISupportInitialize)gridFieldA).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridFieldB).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridPreview).EndInit();
            grpMerge.ResumeLayout(false);
            grpMerge.PerformLayout();
            grpReplace.ResumeLayout(false);
            grpReplace.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtFileA;
        private System.Windows.Forms.TextBox txtFileB;
        private System.Windows.Forms.Button btnBrowseA;
        private System.Windows.Forms.Button btnBrowseB;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView gridFieldA;
        private System.Windows.Forms.DataGridView gridFieldB;
        private System.Windows.Forms.Label lblFileA;
        private System.Windows.Forms.Label lblFileB;
        private System.Windows.Forms.Label lblFieldA;
        private System.Windows.Forms.Label lblFieldB;
        private System.Windows.Forms.Label lblCountA;
        private System.Windows.Forms.Label lblCountB;
        private System.Windows.Forms.GroupBox grpMerge;
        private System.Windows.Forms.RadioButton rbMerge;
        private System.Windows.Forms.RadioButton rbReplace;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.GroupBox grpReplace;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.ComboBox cmbKey;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.DataGridView gridPreview;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button btnSave;
        private Button btnSlctall;
        private Button btnUnslcall;
        private Label labelHintm;
        private Label labelHintr;
    }
}
