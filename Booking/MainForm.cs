using Booking.Forms.Hotel;
using Domain.Entities;
using Helpers;
using Infrastructure.Data;

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

        private void mHead_File_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mHead_Options_Seed_Click(object sender, EventArgs e)
        {
            using (ApplicationDbContext applicationDbContext = new ApplicationDbContext())
            {
                if(applicationDbContext.Categories.Count()==0)
                {
                    CategoryEntity tepla_pidloga = new CategoryEntity();
                    tepla_pidloga.Name = "Тепла підлога";
                    tepla_pidloga.Priority = 1;
                    tepla_pidloga.Image = ImageWorker.ImageSaveUrl("https://comfortheat.kiev.ua/image/cache/catalog/comfortheat/products/slideshow_home/img_6521-375x375.jpg", "categories");
                    tepla_pidloga.Description = " Тепла підлога Comfort Heat - система електричного підігріву " +
                        "поверхні підлоги до комфортної температури.";
                    applicationDbContext.Categories.Add(tepla_pidloga);
                    applicationDbContext.SaveChanges();

                    CategoryEntity opalennya = new CategoryEntity();
                    opalennya.Name = "Опалення";
                    opalennya.Priority = 2;
                    opalennya.Description = "Повне електричне опалення через конструкцію підлоги - " +
                        "популярний вид обігріву будь-яких приміщень.";
                    applicationDbContext.Categories.Add(opalennya);
                    applicationDbContext.SaveChanges();

                    #region Опалення дочірні категорії

                    CategoryEntity konvektory = new CategoryEntity();
                    konvektory.ParentId = opalennya.Id;
                    konvektory.Image = ImageWorker.ImageSaveUrl("https://comfortheat.kiev.ua/image/cache/catalog/airelec/airelec-375x375.jpg","categories");
                    konvektory.Name = "Конвектори Airelec";
                    konvektory.Priority = 1;
                    konvektory.Description = "Електричні конвектори компанії Airelec (Франція) - " +
                        "швидке і ефективне рішення для основного або допоміжного опалення приміщень будь-якого типу.";
                    applicationDbContext.Categories.Add(konvektory);
                    applicationDbContext.SaveChanges();

                    CategoryEntity komertsiynoi_neruhomosti = new CategoryEntity();
                    komertsiynoi_neruhomosti.ParentId = opalennya.Id;
                    komertsiynoi_neruhomosti.Priority = 2;
                    komertsiynoi_neruhomosti.Name = "Опалення комерційної нерухомості";
                    komertsiynoi_neruhomosti.Description = "За допомогою нагрівальних кабелів і матів Comfort Heat " +
                        "можна ефективно реалізувати систему повного опалення " +
                        "будь-якої комерційної нерухомості (магазинів, офісів, салонів, кафе, ресторанів, заправок та інше).";
                    applicationDbContext.Categories.Add(komertsiynoi_neruhomosti);
                    applicationDbContext.SaveChanges();


                    CategoryEntity zhytlovykh_prymishchen = new CategoryEntity();
                    zhytlovykh_prymishchen.ParentId = opalennya.Id;
                    zhytlovykh_prymishchen.Priority = 3;
                    zhytlovykh_prymishchen.Image = ImageWorker.ImageSaveUrl("https://comfortheat.kiev.ua/image/cache/catalog/comfortheat/categories/otoplenie_375_375-375x375.jpg", "categories");
                    zhytlovykh_prymishchen.Name = "Опалення житлових приміщень";
                    zhytlovykh_prymishchen.Description = "Електричний кабельний обігрів приміщень - один з " +
                        "варіантів вирішення питання опалення Вашого будинку або квартири. ";
                    applicationDbContext.Categories.Add(zhytlovykh_prymishchen);
                    applicationDbContext.SaveChanges();

                    #endregion

                }
            }
        }
    }
}
