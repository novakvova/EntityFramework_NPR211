namespace Booking.Forms.Floor
{
    partial class FloorCreateForm
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
            btnCancel = new Button();
            btnCraete = new Button();
            txtName = new TextBox();
            label1 = new Label();
            lbHotelName = new Label();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(486, 98);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 46);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnCraete
            // 
            btnCraete.Location = new Point(486, 44);
            btnCraete.Name = "btnCraete";
            btnCraete.Size = new Size(112, 46);
            btnCraete.TabIndex = 10;
            btnCraete.Text = "Створити";
            btnCraete.UseVisualStyleBackColor = true;
            btnCraete.Click += btnCraete_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(12, 50);
            txtName.Name = "txtName";
            txtName.Size = new Size(435, 34);
            txtName.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(147, 28);
            label1.TabIndex = 5;
            label1.Text = "Назва поверху";
            // 
            // lbHotelName
            // 
            lbHotelName.AutoSize = true;
            lbHotelName.BackColor = SystemColors.Control;
            lbHotelName.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lbHotelName.ForeColor = Color.Blue;
            lbHotelName.Location = new Point(12, 107);
            lbHotelName.Name = "lbHotelName";
            lbHotelName.Size = new Size(140, 28);
            lbHotelName.TabIndex = 11;
            lbHotelName.Text = "Назва готеля";
            // 
            // FloorCreateForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 199);
            Controls.Add(lbHotelName);
            Controls.Add(btnCancel);
            Controls.Add(btnCraete);
            Controls.Add(txtName);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FloorCreateForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FloorCreateForm";
            Load += FloorCreateForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnCraete;
        private TextBox txtName;
        private Label label1;
        private Label lbHotelName;
    }
}