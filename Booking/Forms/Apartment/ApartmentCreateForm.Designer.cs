namespace Booking.Forms.Apartment
{
    partial class ApartmentCreateForm
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
            txtNumberOfRooms = new TextBox();
            label2 = new Label();
            txtNumber = new TextBox();
            label1 = new Label();
            textNumberOfBeds = new TextBox();
            label4 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            lvImages = new ListView();
            btnSelectImages = new Button();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(790, 79);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 46);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Скасувати";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnCraete
            // 
            btnCraete.Location = new Point(790, 25);
            btnCraete.Name = "btnCraete";
            btnCraete.Size = new Size(112, 46);
            btnCraete.TabIndex = 10;
            btnCraete.Text = "Створити";
            btnCraete.UseVisualStyleBackColor = true;
            // 
            // txtNumberOfRooms
            // 
            txtNumberOfRooms.Location = new Point(22, 124);
            txtNumberOfRooms.Name = "txtNumberOfRooms";
            txtNumberOfRooms.Size = new Size(197, 27);
            txtNumberOfRooms.TabIndex = 7;
            txtNumberOfRooms.Text = "1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.ForeColor = Color.Blue;
            label2.Location = new Point(22, 93);
            label2.Name = "label2";
            label2.Size = new Size(119, 20);
            label2.TabIndex = 4;
            label2.Text = "Кількість кімнат";
            // 
            // txtNumber
            // 
            txtNumber.Location = new Point(22, 48);
            txtNumber.Name = "txtNumber";
            txtNumber.Size = new Size(435, 27);
            txtNumber.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(22, 17);
            label1.Name = "label1";
            label1.Size = new Size(115, 20);
            label1.TabIndex = 5;
            label1.Text = "Номер кімнати";
            // 
            // textNumberOfBeds
            // 
            textNumberOfBeds.Location = new Point(260, 124);
            textNumberOfBeds.Name = "textNumberOfBeds";
            textNumberOfBeds.Size = new Size(197, 27);
            textNumberOfBeds.TabIndex = 12;
            textNumberOfBeds.Text = "1";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.ForeColor = Color.Blue;
            label4.Location = new Point(260, 93);
            label4.Name = "label4";
            label4.Size = new Size(113, 20);
            label4.TabIndex = 11;
            label4.Text = "Кількість ліжок";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.ForeColor = Color.Blue;
            label3.Location = new Point(502, 17);
            label3.Name = "label3";
            label3.Size = new Size(97, 20);
            label3.TabIndex = 4;
            label3.Text = "Ціна за добу";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(502, 48);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(251, 27);
            textBox1.TabIndex = 7;
            textBox1.Text = "400";
            // 
            // lvImages
            // 
            lvImages.Location = new Point(22, 177);
            lvImages.Name = "lvImages";
            lvImages.Size = new Size(885, 356);
            lvImages.TabIndex = 43;
            lvImages.UseCompatibleStateImageBehavior = false;
            lvImages.ItemDrag += lvImages_ItemDrag;
            lvImages.DragDrop += lvImages_DragDrop;
            lvImages.DragEnter += lvImages_DragEnter;
            lvImages.DragOver += lvImages_DragOver;
            lvImages.DragLeave += lvImages_DragLeave;
            // 
            // btnSelectImages
            // 
            btnSelectImages.Location = new Point(529, 114);
            btnSelectImages.Name = "btnSelectImages";
            btnSelectImages.Size = new Size(214, 47);
            btnSelectImages.TabIndex = 44;
            btnSelectImages.Text = "Додати фото";
            btnSelectImages.UseVisualStyleBackColor = true;
            btnSelectImages.Click += btnSelectImages_Click;
            // 
            // ApartmentCreateForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 562);
            Controls.Add(btnSelectImages);
            Controls.Add(lvImages);
            Controls.Add(textNumberOfBeds);
            Controls.Add(label4);
            Controls.Add(btnCancel);
            Controls.Add(btnCraete);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(txtNumberOfRooms);
            Controls.Add(label2);
            Controls.Add(txtNumber);
            Controls.Add(label1);
            Name = "ApartmentCreateForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Додати номер у готель";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnCraete;
        private TextBox txtNumberOfRooms;
        private Label label2;
        private TextBox txtNumber;
        private Label label1;
        private TextBox textNumberOfBeds;
        private Label label4;
        private Label label3;
        private TextBox textBox1;
        private ListView lvImages;
        private Button btnSelectImages;
    }
}