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
    }
}
