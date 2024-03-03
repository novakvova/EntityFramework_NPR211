using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Booking.Forms.Apartment
{
    public partial class ApartmentListForm : Form
    {
        //Номер поверха по якому ми відображаємо кімнати
        public int FloorId { get; set; }
        public ApartmentListForm()
        {
            InitializeComponent();
        }

        private void btnCraeteApartment_Click(object sender, EventArgs e)
        {
            ApartmentCreateForm apartmentCreateForm = new ApartmentCreateForm();
            apartmentCreateForm.FloorId = FloorId;
            if (apartmentCreateForm.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
