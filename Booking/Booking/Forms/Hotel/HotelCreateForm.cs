using Domain.Entities;
using Infrastructure.Data;

namespace Booking.Forms.Hotel
{
    public partial class HotelCreateForm : Form
    {
        public HotelCreateForm()
        {
            InitializeComponent();
        }

        private void btnCraete_Click(object sender, EventArgs e)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                HotelEntity hotel = new HotelEntity();
                hotel.Name = txtName.Text;
                hotel.Description = txtDescription.Text;
                hotel.Address = txtAddress.Text;    
                context.Hotels.Add(hotel);
                context.SaveChanges();
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
