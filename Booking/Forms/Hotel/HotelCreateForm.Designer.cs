namespace Booking.Forms.Hotel
{
    partial class HotelCreateForm
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
            label1 = new Label();
            txtName = new TextBox();
            label2 = new Label();
            txtAddress = new TextBox();
            label3 = new Label();
            txtDescription = new TextBox();
            btnCraete = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(66, 28);
            label1.TabIndex = 0;
            label1.Text = "Назва";
            // 
            // txtName
            // 
            txtName.Location = new Point(12, 40);
            txtName.Name = "txtName";
            txtName.Size = new Size(435, 34);
            txtName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.ForeColor = Color.Blue;
            label2.Location = new Point(12, 97);
            label2.Name = "label2";
            label2.Size = new Size(77, 28);
            label2.TabIndex = 0;
            label2.Text = "Адреса";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(12, 128);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(435, 34);
            txtAddress.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.ForeColor = Color.Blue;
            label3.Location = new Point(12, 183);
            label3.Name = "label3";
            label3.Size = new Size(60, 28);
            label3.TabIndex = 0;
            label3.Text = "Опис";
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(12, 214);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(435, 190);
            txtDescription.TabIndex = 1;
            // 
            // btnCraete
            // 
            btnCraete.Location = new Point(486, 34);
            btnCraete.Name = "btnCraete";
            btnCraete.Size = new Size(112, 46);
            btnCraete.TabIndex = 2;
            btnCraete.Text = "Створити";
            btnCraete.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(486, 88);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 46);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // HotelCreateForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 442);
            Controls.Add(btnCancel);
            Controls.Add(btnCraete);
            Controls.Add(txtDescription);
            Controls.Add(label3);
            Controls.Add(txtAddress);
            Controls.Add(label2);
            Controls.Add(txtName);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 4, 4, 4);
            Name = "HotelCreateForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "HotelCreateForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtName;
        private Label label2;
        private TextBox txtAddress;
        private Label label3;
        private TextBox txtDescription;
        private Button btnCraete;
        private Button btnCancel;
    }
}