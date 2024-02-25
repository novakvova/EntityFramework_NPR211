namespace Booking.Forms.Hotel
{
    partial class HotelMainForm
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
            btnCreateHotel = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnCreateHotel
            // 
            btnCreateHotel.Location = new Point(28, 100);
            btnCreateHotel.Name = "btnCreateHotel";
            btnCreateHotel.Size = new Size(133, 49);
            btnCreateHotel.TabIndex = 0;
            btnCreateHotel.Text = "Додати";
            btnCreateHotel.UseVisualStyleBackColor = true;
            btnCreateHotel.Click += btnCreateHotel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(321, 28);
            label1.Name = "label1";
            label1.Size = new Size(235, 41);
            label1.TabIndex = 1;
            label1.Text = "Список готелів";
            // 
            // HotelMainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(879, 482);
            Controls.Add(label1);
            Controls.Add(btnCreateHotel);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 4, 4, 4);
            Name = "HotelMainForm";
            Text = "HotelMainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCreateHotel;
        private Label label1;
    }
}