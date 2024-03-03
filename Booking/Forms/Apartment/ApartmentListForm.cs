using Domain.Entities;
using Helpers;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

            lvImages.LargeImageList = new ImageList();
            lvImages.LargeImageList.ImageSize = new Size(180, 130);
            // lvImages.LargeImageList.
            lvImages.MultiSelect = false;
            lvImages.ListViewItemSorter = new ListViewIndexComparer();
            lvImages.InsertionMark.Color = Color.Green;
            lvImages.AllowDrop = true;
        }

        private void LoadListApartments()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var list = context.Apartments
                    .Include(x => x.ApartmentImages)
                    .ToList();
                foreach (var apartment in list)
                {
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), "images", "apartments");
                    var first = apartment.ApartmentImages.FirstOrDefault();
                    if (first != null)
                    {
                        var imgName = first.Name;
                        var imagePath = Path.Combine(dir, "600_" + imgName);
                        string key = Guid.NewGuid().ToString();
                        ListViewItem item = new ListViewItem();
                        item.Tag = imagePath;
                        item.Text = apartment.Number;
                        item.ImageKey = key;
                        lvImages.LargeImageList.Images.Add(key,
                            Image.FromStream(ImageWorker.GetFileStream(imagePath)));
                        lvImages.Items.Add(item);
                    }
                }

            }
        }

        private void btnCraeteApartment_Click(object sender, EventArgs e)
        {
            ApartmentCreateForm apartmentCreateForm = new ApartmentCreateForm();
            apartmentCreateForm.FloorId = FloorId;
            if (apartmentCreateForm.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void ApartmentListForm_Load(object sender, EventArgs e)
        {
            LoadListApartments();
        }
    }
}
