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

namespace Booking.Forms.Floor
{
    public partial class FloorCreateForm : Form
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }

        public FloorCreateForm()
        {
            InitializeComponent();
        }

        private void btnCraete_Click(object sender, EventArgs e)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                FloorEntity floor = new FloorEntity();
                floor.HotelId = HotelId;
                floor.Name = txtName.Text;
                context.Floors.Add(floor);
                context.SaveChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FloorCreateForm_Load(object sender, EventArgs e)
        {
            lbHotelName.Text = HotelName;
        }
    }
}
