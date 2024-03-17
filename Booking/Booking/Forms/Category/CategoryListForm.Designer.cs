namespace Booking.Forms.Category
{
    partial class CategoryListForm
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
            tvCategory = new TreeView();
            SuspendLayout();
            // 
            // tvCategory
            // 
            tvCategory.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            tvCategory.ItemHeight = 44;
            tvCategory.Location = new Point(21, 12);
            tvCategory.Name = "tvCategory";
            tvCategory.Size = new Size(447, 480);
            tvCategory.TabIndex = 0;
            tvCategory.NodeMouseClick += tvCategory_NodeMouseClick;
            // 
            // CategoryListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 516);
            Controls.Add(tvCategory);
            Name = "CategoryListForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Керування категоріями";
            Load += CategoryListForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private TreeView tvCategory;
    }
}