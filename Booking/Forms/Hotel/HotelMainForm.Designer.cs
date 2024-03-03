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
            dgvHotels = new DataGridView();
            ColId = new DataGridViewTextBoxColumn();
            ColName = new DataGridViewTextBoxColumn();
            ColAddress = new DataGridViewTextBoxColumn();
            ColDescription = new DataGridViewTextBoxColumn();
            lvFloors = new ListView();
            btnAddFloor = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHotels).BeginInit();
            SuspendLayout();
            // 
            // btnCreateHotel
            // 
            btnCreateHotel.Location = new Point(12, 99);
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
            // dgvHotels
            // 
            dgvHotels.AllowUserToAddRows = false;
            dgvHotels.AllowUserToDeleteRows = false;
            dgvHotels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHotels.Columns.AddRange(new DataGridViewColumn[] { ColId, ColName, ColAddress, ColDescription });
            dgvHotels.Location = new Point(12, 165);
            dgvHotels.Name = "dgvHotels";
            dgvHotels.ReadOnly = true;
            dgvHotels.RowHeadersWidth = 51;
            dgvHotels.Size = new Size(855, 305);
            dgvHotels.TabIndex = 2;
            dgvHotels.CellEnter += dgvHotels_CellEnter;
            // 
            // ColId
            // 
            ColId.HeaderText = "Id";
            ColId.MinimumWidth = 6;
            ColId.Name = "ColId";
            ColId.ReadOnly = true;
            ColId.Width = 75;
            // 
            // ColName
            // 
            ColName.HeaderText = "Назва";
            ColName.MinimumWidth = 6;
            ColName.Name = "ColName";
            ColName.ReadOnly = true;
            ColName.Width = 200;
            // 
            // ColAddress
            // 
            ColAddress.HeaderText = "Адрес";
            ColAddress.MinimumWidth = 6;
            ColAddress.Name = "ColAddress";
            ColAddress.ReadOnly = true;
            ColAddress.Width = 150;
            // 
            // ColDescription
            // 
            ColDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColDescription.HeaderText = "Опис";
            ColDescription.MinimumWidth = 6;
            ColDescription.Name = "ColDescription";
            ColDescription.ReadOnly = true;
            // 
            // lvFloors
            // 
            lvFloors.Location = new Point(905, 165);
            lvFloors.Name = "lvFloors";
            lvFloors.Size = new Size(317, 303);
            lvFloors.TabIndex = 3;
            lvFloors.UseCompatibleStateImageBehavior = false;
            // 
            // btnAddFloor
            // 
            btnAddFloor.Location = new Point(1061, 102);
            btnAddFloor.Name = "btnAddFloor";
            btnAddFloor.Size = new Size(161, 46);
            btnAddFloor.TabIndex = 4;
            btnAddFloor.Text = "Додати повер";
            btnAddFloor.UseVisualStyleBackColor = true;
            // 
            // HotelMainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 529);
            Controls.Add(btnAddFloor);
            Controls.Add(lvFloors);
            Controls.Add(dgvHotels);
            Controls.Add(label1);
            Controls.Add(btnCreateHotel);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4);
            Name = "HotelMainForm";
            Text = "HotelMainForm";
            Load += HotelMainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHotels).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCreateHotel;
        private Label label1;
        private DataGridView dgvHotels;
        private DataGridViewTextBoxColumn ColId;
        private DataGridViewTextBoxColumn ColName;
        private DataGridViewTextBoxColumn ColAddress;
        private DataGridViewTextBoxColumn ColDescription;
        private ListView lvFloors;
        private Button btnAddFloor;
    }
}