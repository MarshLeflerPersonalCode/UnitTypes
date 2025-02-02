﻿namespace Library.UnitTypeForm
{
	partial class unitTypeForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(unitTypeForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lstBoxCategories = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtBoxFilterCategoriesByString = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainerUnitType = new System.Windows.Forms.SplitContainer();
			this.panel3 = new System.Windows.Forms.Panel();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtBoxFilterUnitTypesByString = new System.Windows.Forms.TextBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.cmbBoxFilterUnittypes = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtBoxFilterPropertiesByString = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.filterTimer = new System.Windows.Forms.Timer(this.components);
			this.logicTimer = new System.Windows.Forms.Timer(this.components);
			this.treeViewUnitTypes = new System.Windows.Forms.TreeView();
			this.filteredPropertyGrid = new CustomControls.FilteredPropertyGrid();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.btnShowAndHideUnitTypeProperties = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.unitTypePanelProperties = new System.Windows.Forms.Panel();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerUnitType)).BeginInit();
			this.splitContainerUnitType.Panel1.SuspendLayout();
			this.splitContainerUnitType.Panel2.SuspendLayout();
			this.splitContainerUnitType.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.panel2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.unitTypePanelProperties.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(893, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.quitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lstBoxCategories);
			this.splitContainer1.Panel1.Controls.Add(this.panel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainerUnitType);
			this.splitContainer1.Size = new System.Drawing.Size(893, 529);
			this.splitContainer1.SplitterDistance = 206;
			this.splitContainer1.SplitterWidth = 8;
			this.splitContainer1.TabIndex = 1;
			// 
			// lstBoxCategories
			// 
			this.lstBoxCategories.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstBoxCategories.FormattingEnabled = true;
			this.lstBoxCategories.Location = new System.Drawing.Point(0, 78);
			this.lstBoxCategories.Name = "lstBoxCategories";
			this.lstBoxCategories.Size = new System.Drawing.Size(204, 449);
			this.lstBoxCategories.Sorted = true;
			this.lstBoxCategories.TabIndex = 1;
			this.lstBoxCategories.SelectedIndexChanged += new System.EventHandler(this.lstBoxCategories_SelectedIndexChanged);
			this.lstBoxCategories.DoubleClick += new System.EventHandler(this.lstBoxCategories_DoubleClick);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(5);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(5);
			this.panel1.Size = new System.Drawing.Size(204, 78);
			this.panel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtBoxFilterCategoriesByString);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(5, 26);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(194, 47);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Filter";
			// 
			// txtBoxFilterCategoriesByString
			// 
			this.txtBoxFilterCategoriesByString.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtBoxFilterCategoriesByString.Location = new System.Drawing.Point(3, 16);
			this.txtBoxFilterCategoriesByString.Name = "txtBoxFilterCategoriesByString";
			this.txtBoxFilterCategoriesByString.Size = new System.Drawing.Size(188, 20);
			this.txtBoxFilterCategoriesByString.TabIndex = 0;
			this.txtBoxFilterCategoriesByString.TextChanged += new System.EventHandler(this.txtBoxFilterCategoriesByString_TextChanged);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(5, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(194, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "CATEGORIES";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// splitContainerUnitType
			// 
			this.splitContainerUnitType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainerUnitType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerUnitType.Location = new System.Drawing.Point(0, 0);
			this.splitContainerUnitType.Name = "splitContainerUnitType";
			// 
			// splitContainerUnitType.Panel1
			// 
			this.splitContainerUnitType.Panel1.Controls.Add(this.treeViewUnitTypes);
			this.splitContainerUnitType.Panel1.Controls.Add(this.panel3);
			// 
			// splitContainerUnitType.Panel2
			// 
			this.splitContainerUnitType.Panel2.Controls.Add(this.unitTypePanelProperties);
			this.splitContainerUnitType.Panel2.Controls.Add(this.btnShowAndHideUnitTypeProperties);
			this.splitContainerUnitType.Size = new System.Drawing.Size(679, 529);
			this.splitContainerUnitType.SplitterDistance = 410;
			this.splitContainerUnitType.SplitterWidth = 8;
			this.splitContainerUnitType.TabIndex = 2;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
			this.panel3.Controls.Add(this.splitContainer3);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Margin = new System.Windows.Forms.Padding(5);
			this.panel3.Name = "panel3";
			this.panel3.Padding = new System.Windows.Forms.Padding(5);
			this.panel3.Size = new System.Drawing.Size(408, 78);
			this.panel3.TabIndex = 1;
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(5, 26);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.groupBox3);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.groupBox4);
			this.splitContainer3.Size = new System.Drawing.Size(398, 47);
			this.splitContainer3.SplitterDistance = 186;
			this.splitContainer3.SplitterWidth = 8;
			this.splitContainer3.TabIndex = 2;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtBoxFilterUnitTypesByString);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(186, 47);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Filter";
			// 
			// txtBoxFilterUnitTypesByString
			// 
			this.txtBoxFilterUnitTypesByString.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtBoxFilterUnitTypesByString.Location = new System.Drawing.Point(3, 16);
			this.txtBoxFilterUnitTypesByString.Name = "txtBoxFilterUnitTypesByString";
			this.txtBoxFilterUnitTypesByString.Size = new System.Drawing.Size(180, 20);
			this.txtBoxFilterUnitTypesByString.TabIndex = 0;
			this.txtBoxFilterUnitTypesByString.TextChanged += new System.EventHandler(this.txtBoxFilterUnitTypesByString_TextChanged);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.cmbBoxFilterUnittypes);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox4.Location = new System.Drawing.Point(0, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(204, 47);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Filter By Unit Type";
			// 
			// cmbBoxFilterUnittypes
			// 
			this.cmbBoxFilterUnittypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cmbBoxFilterUnittypes.FormattingEnabled = true;
			this.cmbBoxFilterUnittypes.Location = new System.Drawing.Point(3, 16);
			this.cmbBoxFilterUnittypes.Name = "cmbBoxFilterUnittypes";
			this.cmbBoxFilterUnittypes.Size = new System.Drawing.Size(198, 21);
			this.cmbBoxFilterUnittypes.TabIndex = 0;
			this.cmbBoxFilterUnittypes.SelectedIndexChanged += new System.EventHandler(this.cmbBoxFilterUnittypes_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(5, 5);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(398, 21);
			this.label3.TabIndex = 1;
			this.label3.Text = "UNIT TYPES";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.panel2.Controls.Add(this.groupBox2);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(5);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(5);
			this.panel2.Size = new System.Drawing.Size(239, 78);
			this.panel2.TabIndex = 1;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.txtBoxFilterPropertiesByString);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(5, 26);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(229, 47);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filter";
			// 
			// txtBoxFilterPropertiesByString
			// 
			this.txtBoxFilterPropertiesByString.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtBoxFilterPropertiesByString.Location = new System.Drawing.Point(3, 16);
			this.txtBoxFilterPropertiesByString.Name = "txtBoxFilterPropertiesByString";
			this.txtBoxFilterPropertiesByString.Size = new System.Drawing.Size(223, 20);
			this.txtBoxFilterPropertiesByString.TabIndex = 0;
			this.txtBoxFilterPropertiesByString.TextChanged += new System.EventHandler(this.txtBoxFilterPropertiesByString_TextChanged);
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(5, 5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(229, 21);
			this.label2.TabIndex = 1;
			this.label2.Text = "PROPERTIES";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// filterTimer
			// 
			this.filterTimer.Interval = 333;
			this.filterTimer.Tick += new System.EventHandler(this.filterTimer_Tick);
			// 
			// logicTimer
			// 
			this.logicTimer.Enabled = true;
			this.logicTimer.Tick += new System.EventHandler(this.logicTimer_Tick);
			// 
			// treeViewUnitTypes
			// 
			this.treeViewUnitTypes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewUnitTypes.FullRowSelect = true;
			this.treeViewUnitTypes.HideSelection = false;
			this.treeViewUnitTypes.Location = new System.Drawing.Point(0, 78);
			this.treeViewUnitTypes.Name = "treeViewUnitTypes";
			this.treeViewUnitTypes.Size = new System.Drawing.Size(408, 449);
			this.treeViewUnitTypes.TabIndex = 2;
			this.treeViewUnitTypes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewUnitTypes_AfterSelect);
			this.treeViewUnitTypes.DoubleClick += new System.EventHandler(this.treeViewUnitTypes_DoubleClick);
			// 
			// filteredPropertyGrid
			// 
			this.filteredPropertyGrid.BrowsableProperties = null;
			this.filteredPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.filteredPropertyGrid.FilterIsCaseSensitive = false;
			this.filteredPropertyGrid.FilterString = "";
			this.filteredPropertyGrid.HiddenAttributes = null;
			this.filteredPropertyGrid.HiddenProperties = null;
			this.filteredPropertyGrid.Location = new System.Drawing.Point(0, 78);
			this.filteredPropertyGrid.Name = "filteredPropertyGrid";
			this.filteredPropertyGrid.Size = new System.Drawing.Size(239, 449);
			this.filteredPropertyGrid.TabIndex = 2;
			this.filteredPropertyGrid.ToolbarVisible = false;
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.loadToolStripMenuItem.Text = "&Load";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.quitToolStripMenuItem.Text = "&Quit";
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// configToolStripMenuItem
			// 
			this.configToolStripMenuItem.Name = "configToolStripMenuItem";
			this.configToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.configToolStripMenuItem.Text = "&Config";
			this.configToolStripMenuItem.Click += new System.EventHandler(this.configToolStripMenuItem_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "Unit Type|*.json";
			this.saveFileDialog.Title = "Save Unit Type File";
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "openFileDialog1";
			this.openFileDialog.Filter = "Unit Type|*.json";
			this.openFileDialog.Title = "Load Unit Type File";
			// 
			// btnShowAndHideUnitTypeProperties
			// 
			this.btnShowAndHideUnitTypeProperties.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnShowAndHideUnitTypeProperties.Location = new System.Drawing.Point(0, 0);
			this.btnShowAndHideUnitTypeProperties.Name = "btnShowAndHideUnitTypeProperties";
			this.btnShowAndHideUnitTypeProperties.Size = new System.Drawing.Size(20, 527);
			this.btnShowAndHideUnitTypeProperties.TabIndex = 3;
			this.btnShowAndHideUnitTypeProperties.Text = ">";
			this.btnShowAndHideUnitTypeProperties.UseVisualStyleBackColor = true;
			this.btnShowAndHideUnitTypeProperties.Click += new System.EventHandler(this.btnShowAndHideUnitTypeProperties_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 553);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(893, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// unitTypePanelProperties
			// 
			this.unitTypePanelProperties.Controls.Add(this.filteredPropertyGrid);
			this.unitTypePanelProperties.Controls.Add(this.panel2);
			this.unitTypePanelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.unitTypePanelProperties.Location = new System.Drawing.Point(20, 0);
			this.unitTypePanelProperties.Name = "unitTypePanelProperties";
			this.unitTypePanelProperties.Size = new System.Drawing.Size(239, 527);
			this.unitTypePanelProperties.TabIndex = 4;
			// 
			// unitTypeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(893, 575);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "unitTypeForm";
			this.Text = "Unit Type Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.unitTypeForm_FormClosing);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.splitContainerUnitType.Panel1.ResumeLayout(false);
			this.splitContainerUnitType.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerUnitType)).EndInit();
			this.splitContainerUnitType.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.unitTypePanelProperties.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainerUnitType;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtBoxFilterCategoriesByString;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox txtBoxFilterPropertiesByString;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtBoxFilterUnitTypesByString;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ComboBox cmbBoxFilterUnittypes;
		private System.Windows.Forms.ListBox lstBoxCategories;
		private System.Windows.Forms.Timer filterTimer;
		private System.Windows.Forms.Timer logicTimer;
		private System.Windows.Forms.TreeView treeViewUnitTypes;
		private CustomControls.FilteredPropertyGrid filteredPropertyGrid;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Button btnShowAndHideUnitTypeProperties;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Panel unitTypePanelProperties;
	}
}

