using Booking.Forms.Hotel;

namespace Booking
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnHotel_Click(object sender, EventArgs e)
        {
            HotelMainForm dlg = new HotelMainForm();
            dlg.ShowDialog();
        }
    }
}
