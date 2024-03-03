namespace Booking.Forms.Apartment
{
    partial class ApartmentListForm
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
            btnCraeteApartment = new Button();
            SuspendLayout();
            // 
            // btnCraeteApartment
            // 
            btnCraeteApartment.Location = new Point(33, 39);
            btnCraeteApartment.Name = "btnCraeteApartment";
            btnCraeteApartment.Size = new Size(112, 46);
            btnCraeteApartment.TabIndex = 11;
            btnCraeteApartment.Text = "Створити";
            btnCraeteApartment.UseVisualStyleBackColor = true;
            btnCraeteApartment.Click += btnCraeteApartment_Click;
            // 
            // ApartmentListForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1038, 538);
            Controls.Add(btnCraeteApartment);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 4, 4, 4);
            Name = "ApartmentListForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Номери на поверсі";
            ResumeLayout(false);
        }

        #endregion

        private Button btnCraeteApartment;
    }
}