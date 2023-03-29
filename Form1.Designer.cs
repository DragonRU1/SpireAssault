namespace SpireAssault
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.Equipment = new System.Windows.Forms.DataGridView();
			this.loadGame = new System.Windows.Forms.Button();
			this.saveText = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Result = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.eLevel = new System.Windows.Forms.NumericUpDown();
			this.reset = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Equipment)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.eLevel)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(713, 415);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Calculate";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Equipment
			// 
			this.Equipment.AllowUserToAddRows = false;
			this.Equipment.AllowUserToDeleteRows = false;
			this.Equipment.AllowUserToResizeRows = false;
			this.Equipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Equipment.Location = new System.Drawing.Point(12, 12);
			this.Equipment.Name = "Equipment";
			this.Equipment.RowHeadersVisible = false;
			this.Equipment.RowTemplate.Height = 25;
			this.Equipment.Size = new System.Drawing.Size(329, 397);
			this.Equipment.TabIndex = 1;
			// 
			// loadGame
			// 
			this.loadGame.Location = new System.Drawing.Point(453, 27);
			this.loadGame.Name = "loadGame";
			this.loadGame.Size = new System.Drawing.Size(75, 23);
			this.loadGame.TabIndex = 3;
			this.loadGame.Text = "Load Game";
			this.loadGame.UseVisualStyleBackColor = true;
			this.loadGame.Click += new System.EventHandler(this.loadGame_Click);
			// 
			// saveText
			// 
			this.saveText.Location = new System.Drawing.Point(347, 27);
			this.saveText.Name = "saveText";
			this.saveText.Size = new System.Drawing.Size(100, 43);
			this.saveText.TabIndex = 4;
			this.saveText.Text = "";
			this.saveText.Enter += new System.EventHandler(this.saveText_Enter);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(347, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 15);
			this.label1.TabIndex = 5;
			this.label1.Text = "Import save:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(347, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 15);
			this.label2.TabIndex = 7;
			this.label2.Text = "Enemy level";
			// 
			// Result
			// 
			this.Result.Location = new System.Drawing.Point(598, 27);
			this.Result.Multiline = true;
			this.Result.Name = "Result";
			this.Result.Size = new System.Drawing.Size(190, 78);
			this.Result.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(598, 11);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 15);
			this.label3.TabIndex = 9;
			this.label3.Text = "Result";
			// 
			// eLevel
			// 
			this.eLevel.Location = new System.Drawing.Point(347, 91);
			this.eLevel.Name = "eLevel";
			this.eLevel.Size = new System.Drawing.Size(120, 23);
			this.eLevel.TabIndex = 10;
			this.eLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// reset
			// 
			this.reset.Location = new System.Drawing.Point(12, 415);
			this.reset.Name = "reset";
			this.reset.Size = new System.Drawing.Size(75, 23);
			this.reset.TabIndex = 11;
			this.reset.Text = "Reset";
			this.reset.UseVisualStyleBackColor = true;
			this.reset.Click += new System.EventHandler(this.reset_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.reset);
			this.Controls.Add(this.eLevel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.Result);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.saveText);
			this.Controls.Add(this.loadGame);
			this.Controls.Add(this.Equipment);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Spire assault simulator";
			((System.ComponentModel.ISupportInitialize)(this.Equipment)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.eLevel)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Button button1;
		private DataGridView Equipment;
		private Button loadGame;
		private RichTextBox saveText;
		private Label label1;
		private Label label2;
		private TextBox Result;
		private Label label3;
		private NumericUpDown eLevel;
		private Button reset;
	}
}