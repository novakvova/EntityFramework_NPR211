using Booking.Forms.Apartment;
using Booking.Forms.Floor;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Booking.Forms.Hotel
{
    public partial class HotelMainForm : Form
    {
        public HotelMainForm()
        {
            InitializeComponent();
        }

        private void btnCreateHotel_Click(object sender, EventArgs e)
        {
            HotelCreateForm dlg = new HotelCreateForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                LoadList();
            }
        }

        private void HotelMainForm_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            dgvHotels.Rows.Clear();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var list = context.Hotels.ToList();
                foreach (var hotel in list)
                {
                    object[] row = { hotel.Id, hotel.Name, hotel.Address, hotel.Description };
                    dgvHotels.Rows.Add(row);
                }
            }
        }

        private void dgvHotels_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                int hotelId = (int)dgvHotels.Rows[e.RowIndex].Cells[0].Value;
                loadFloors(hotelId);
            }
        }

        private void loadFloors(int hotelId)
        {
            lvFloors.Items.Clear();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var floors = context.Floors.Where(x => x.HotelId == hotelId).ToList();
                foreach (var floor in floors)
                {
                    ListViewItem lvFloorsItem = new ListViewItem();
                    lvFloorsItem.Text = floor.Name;
                    lvFloorsItem.Tag = floor.Id;
                    lvFloors.Items.Add(lvFloorsItem);
                }
            }
        }

        private void btnAddFloor_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvHotels.CurrentCell.RowIndex;
            if (rowIndex >= 0)
            {
                int hotelId = (int)dgvHotels.Rows[rowIndex].Cells[0].Value;
                string hotelName = (string)dgvHotels.Rows[rowIndex].Cells[1].Value;
                FloorCreateForm dlg = new FloorCreateForm();
                dlg.HotelId = hotelId;
                dlg.HotelName = hotelName;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    loadFloors(hotelId);
                }
            }
        }

        private void lvFloors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get the item that was double-clicked
            ListViewItem item = lvFloors.SelectedItems.Count > 0 ? lvFloors.SelectedItems[0] : null;
            if (item != null)
            {
                ApartmentListForm dlg = new ApartmentListForm();
                dlg.FloorId = (int)item.Tag;
                dlg.ShowDialog();

            }
        }
    }
}
