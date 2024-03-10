namespace Booking
{
    partial class MainForm
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
            btnHotel = new Button();
            mHead = new MenuStrip();
            mHead_File = new ToolStripMenuItem();
            mHead_File_Exit = new ToolStripMenuItem();
            mHead_Operation = new ToolStripMenuItem();
            mHead_Opertaion_Category = new ToolStripMenuItem();
            mHead_Options = new ToolStripMenuItem();
            mHead_Options_Seed = new ToolStripMenuItem();
            mHead.SuspendLayout();
            SuspendLayout();
            // 
            // btnHotel
            // 
            btnHotel.Location = new Point(26, 53);
            btnHotel.Name = "btnHotel";
            btnHotel.Size = new Size(172, 52);
            btnHotel.TabIndex = 0;
            btnHotel.Text = "Готелі";
            btnHotel.UseVisualStyleBackColor = true;
            btnHotel.Click += btnHotel_Click;
            // 
            // mHead
            // 
            mHead.ImageScalingSize = new Size(20, 20);
            mHead.Items.AddRange(new ToolStripItem[] { mHead_File, mHead_Operation, mHead_Options });
            mHead.Location = new Point(0, 0);
            mHead.Name = "mHead";
            mHead.Size = new Size(1058, 28);
            mHead.TabIndex = 1;
            mHead.Text = "menuStrip1";
            // 
            // mHead_File
            // 
            mHead_File.DropDownItems.AddRange(new ToolStripItem[] { mHead_File_Exit });
            mHead_File.Name = "mHead_File";
            mHead_File.Size = new Size(59, 24);
            mHead_File.Text = "Файл";
            // 
            // mHead_File_Exit
            // 
            mHead_File_Exit.Name = "mHead_File_Exit";
            mHead_File_Exit.ShortcutKeys = Keys.Alt | Keys.X;
            mHead_File_Exit.Size = new Size(176, 26);
            mHead_File_Exit.Text = "Вихід";
            mHead_File_Exit.Click += mHead_File_Exit_Click;
            // 
            // mHead_Operation
            // 
            mHead_Operation.DropDownItems.AddRange(new ToolStripItem[] { mHead_Opertaion_Category });
            mHead_Operation.Name = "mHead_Operation";
            mHead_Operation.Size = new Size(85, 24);
            mHead_Operation.Text = "Операції";
            // 
            // mHead_Opertaion_Category
            // 
            mHead_Opertaion_Category.Name = "mHead_Opertaion_Category";
            mHead_Opertaion_Category.Size = new Size(224, 26);
            mHead_Opertaion_Category.Text = "Категорії";
            mHead_Opertaion_Category.Click += mHead_Opertaion_Category_Click;
            // 
            // mHead_Options
            // 
            mHead_Options.DropDownItems.AddRange(new ToolStripItem[] { mHead_Options_Seed });
            mHead_Options.Name = "mHead_Options";
            mHead_Options.Size = new Size(125, 24);
            mHead_Options.Text = "Налаштування";
            // 
            // mHead_Options_Seed
            // 
            mHead_Options_Seed.Name = "mHead_Options_Seed";
            mHead_Options_Seed.Size = new Size(189, 26);
            mHead_Options_Seed.Text = "Заповнить БД";
            mHead_Options_Seed.Click += mHead_Options_Seed_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 533);
            Controls.Add(btnHotel);
            Controls.Add(mHead);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            MainMenuStrip = mHead;
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "Туристична агренція";
            mHead.ResumeLayout(false);
            mHead.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnHotel;
        private MenuStrip mHead;
        private ToolStripMenuItem mHead_File;
        private ToolStripMenuItem mHead_File_Exit;
        private ToolStripMenuItem mHead_Options;
        private ToolStripMenuItem mHead_Options_Seed;
        private ToolStripMenuItem mHead_Operation;
        private ToolStripMenuItem mHead_Opertaion_Category;
    }
}
