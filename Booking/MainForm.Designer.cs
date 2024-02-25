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
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(803, 464);
            Controls.Add(btnHotel);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 4, 4, 4);
            Name = "MainForm";
            Text = "Туристична агренція";
            ResumeLayout(false);
        }

        #endregion

        private Button btnHotel;
    }
}
