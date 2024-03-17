using DAL.Constants;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace DAL.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed()
        {
            using (EFAppContext dataContext = new EFAppContext())
            {
                SeedCategories(dataContext);
                SeedProducts(dataContext);
                SeedImages(dataContext);
                SeedRoles(dataContext);
                SeedUsers(dataContext);
                SeedOrderStatuses(dataContext);
            }
        }
        private static void SeedOrderStatuses(EFAppContext dataContext)
        {
            if (!dataContext.OrderStatuses.Any())
            {
                foreach (var order in OrderStatuses.All)
                {
                    dataContext.OrderStatuses.Add(new OrderStatusEntity()
                    {
                        DateCreated = DateTime.Now,
                        Name = order
                    });
                }
                dataContext.SaveChanges();
            }
        }
        private static void SeedRoles(EFAppContext dataContext)
        {
            if (!dataContext.Roles.Any())
            {
                foreach (var role in Roles.All)
                {
                    dataContext.Roles.Add(new RoleEntity()
                    {
                        DateCreated = DateTime.Now,
                        Name = role
                    });
                }
                dataContext.SaveChanges();
            }
        }

        private static void SeedUsers(EFAppContext dataContext)
        {
            if (!dataContext.Users.Any())
            {
                var user = new UserEntity()
                {
                    FirstName = "Микола",
                    SecondName = "Коробочка",
                    Phone = "098 34 34 222",
                    Email = "admin@gmail.com",
                    Password = Hash("123456"),
                    DateCreated = DateTime.Now
                };
                dataContext.Users.Add(user);
                dataContext.SaveChanges();
                var role = dataContext.Roles.SingleOrDefault(x => x.Name == Roles.Admin);
                if (role != null)
                {
                    var userRoles = new UserRoleEntity
                    {
                        RoleId = role.Id,
                        UserId = user.Id
                    };
                    dataContext.UserRoles.Add(userRoles);
                    dataContext.SaveChanges();
                }
            }
        }

        private static string Hash(string password)
        {
            MD5 mD5 = MD5.Create();

            byte[] b = Encoding.UTF8.GetBytes(password);
            byte[] hash = mD5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();
            foreach (var a in hash)
                sb.Append(a.ToString("X2"));

            return Convert.ToString(sb);
        }
        private static void SeedCategories(EFAppContext dataContext)
        {
            if (!dataContext.Categories.Any())
            {
                var noutbuki = new CategoryEntity
                {
                    Name = "Ноутбуки",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/noutbuki.png"
                };

                var kompyutery = new CategoryEntity
                {
                    Name = "Комп'ютери",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/kompyutery.png"
                };

                var monitory = new CategoryEntity
                {
                    Name = "Монітори",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/monitory.png"
                };
                
                var mobilnye_telefony = new CategoryEntity
                {
                    Name = "Мобільні телефони",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/smartfony_tv_i_elektronika/mobilnye_telefony.jpg"
                };
                
                var televizory = new CategoryEntity
                {
                    Name = "Телевізори",
                    DateCreated = DateTime.Now,
                    Image = @"https://video.rozetka.com.ua/img_superportal/smartfony_tv_i_elektronika/televizory.jpg"
                };

                dataContext.Categories.Add(noutbuki);
                dataContext.Categories.Add(kompyutery);
                dataContext.Categories.Add(monitory);
                dataContext.Categories.Add(mobilnye_telefony);
                dataContext.Categories.Add(televizory);
                dataContext.SaveChanges();
            }
        }
        private static void SeedProducts(EFAppContext dataContext)
        {
            if (!dataContext.Products.Any())
            {
                ICategoryRepository categoryRepository = new CategoryRepository(dataContext);
                var categories = categoryRepository.GetAll();

                #region Ноутбуки
                var notebook = categories.Where(c => c.Name == "Ноутбуки").First().Id;

                var acerAspire = new ProductEntity
                {
                    Name = "Acer Aspire 7 A715-42G-R8BL (NH.QDLEU.008) Charcoal Black",
                    Description = "Екран 15.6\" IPS (1920x1080) Full HD 144 Гц, матовий / AMD Ryzen 5 5500U (2.1 - 4.0 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050 Ti, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.15 кг / чорний",
                    Price = 36999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var macBookAir = new ProductEntity
                {
                    Name = "Apple MacBook Air 13\" M1 256GB 2020 (MGN93) Silver",
                    Description = "Екран 13.3\" Retina (2560x1600) WQXGA, глянсовий / Apple M1 / RAM 8 ГБ / SSD 256 ГБ / Apple M1 Graphics / Wi-Fi / Bluetooth / macOS Big Sur / 1.29 кг / сріблястий",
                    Price = 42999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var lenovoIdeaPad = new ProductEntity
                {
                    Name = "Lenovo IdeaPad Gaming 3 15IHU6 (82K101FJRA) Shadow Black",
                    Description = "Екран 15.6\" IPS (1920x1080) Full HD, матовий / Intel Core i5-11320H (2.5 - 4.5 ГГц) / RAM 8 ГБ / SSD 256 ГБ / nVidia GeForce GTX 1650, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.25 кг / чорний",
                    Price = 29999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var asusTufGaming = new ProductEntity
                {
                    Name = "ASUS TUF Gaming F15 (90NR0704-M00CW0) Graphite Black",
                    Description = "Екран 15.6\" IPS (1920x1080) Full HD 144 Гц, матовий / Intel Core i5-11400H (2.7 - 4.5 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050 Ti, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.3 кг / чорний",
                    Price = 40999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var asusRogStrix = new ProductEntity
                {
                    Name = "Asus ROG Strix G15 (90NR0502-M003L0) Eclipse Gray",
                    Description = "Екран 15.6\" IPS (1920x1080) Full HD 144 Гц, матовий / AMD Ryzen 7 4800H (2.9 - 4.2 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3050, 4 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / без ОС / 2.1 кг / сірий",
                    Price = 39999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var lenovoLegion5 = new ProductEntity
                {
                    Name = "Lenovo Legion 5 15IAH7H (82RB00QKRA) Storm Grey",
                    Description = "Экран 15.6\" IPS (2560x1440) WQHD 165 Гц, матовый / Intel Core i7-12700H (3.5 - 4.7 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce RTX 3070, 8 ГБ / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 2.4 кг / серый",
                    Price = 75999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var asusZenBookFlip15 = new ProductEntity
                {
                    Name = "ASUS ZenBook Flip 15 (90NB0VJ2-M00110) Light Grey",
                    Description = "Екран 15.6\" IPS (1920x1080) Full HD, Multi-touch, глянсовий / AMD Ryzen 7 5700U (1.8 — 4.3 ГГц) / RAM 16 ГБ / SSD 512 ГБ / nVidia GeForce MX450, 2 ГБ / без ОД / Wi-Fi / Bluetooth / веб-камера / без ОС / 1.9 кг / сірий / стилус",
                    Price = 34999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var lenovoYoga7 = new ProductEntity
                {
                    Name = "Lenovo Yoga 7 (82N7009QRA) Slate Grey",
                    Description = "Екран 14\" IPS (1920x1080) Full HD, Multi-Touch, глянсовий / AMD Ryzen 7 5800U (1.9 - 4.4 ГГц) / RAM 16 ГБ / SSD 512 ГБ / AMD Radeon Graphics / без ОД / Wi-Fi / Bluetooth / веб-камера / Windows 11 Home 64bit / 1.45 кг / сірий",
                    Price = 47777,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                var acerAspire3 = new ProductEntity
                {
                    Name = "Acer Aspire 3 (NX.A6LEU.01A) Pure Silver",
                    Description = "Екран 15.6\" (1920x1080) Full HD, матовий / Intel Celeron N4500 (1.1 - 2.8 ГГц) / RAM 4 ГБ / SSD 128 ГБ / Intel UHD Graphics / без ОД / LAN / Wi-Fi / Bluetooth / веб-камера / без ОС / 1.7 кг / срібний",
                    Price = 12999,
                    CategoryId = notebook,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(acerAspire);
                dataContext.Products.Add(macBookAir);
                dataContext.Products.Add(lenovoIdeaPad);
                dataContext.Products.Add(asusTufGaming);
                dataContext.Products.Add(asusRogStrix);
                dataContext.Products.Add(lenovoLegion5);
                dataContext.Products.Add(asusZenBookFlip15);
                dataContext.Products.Add(lenovoYoga7);
                dataContext.Products.Add(acerAspire3);
                #endregion
                #region Комп'ютери
                var computers = categories.Where(c => c.Name == "Комп'ютери").First().Id;

                var artlineGaming = new ProductEntity
                {
                    Name = "Artline Gaming X51 v07 (X51v07)",
                    Description = "Intel Core i5-10400F (2.9 — 4.3 ГГц) / RAM 16 ГБ / HDD 1 ТБ + SSD 240 ГБ / nVidia GeForce GTX 1660, 6 ГБ / без ОД / LAN / без ОС",
                    Price = 28999,
                    CategoryId = computers,
                    DateCreated = DateTime.Now
                };

                var QUBE = new ProductEntity
                {
                    Name = "QUBE Ігровий QB R i5 10400F RTX 3060 12 GB 1621",
                    Description = "Intel Core i5-10400F (2.9 — 4.3 ГГц) / RAM 16 ГБ / HDD 1 ТБ + SSD 240 ГБ / nVidia GeForce RTX 3060, 12 ГБ / без ОД / LAN / без ОС",
                    Price = 35799,
                    CategoryId = computers,
                    DateCreated = DateTime.Now
                };

                var artlineGamingX88 = new ProductEntity
                {
                    Name = "Artline Gaming X88 v09 (X88v09)",
                    Description = "AMD Ryzen 9 5900X (3.7 — 4.8 ГГц) / RAM 32 ГБ / SSD 1 ТБ / nVidia GeForce RTX 3080, 10 ГБ / без ОД / LAN / без ОС",
                    Price = 75999,
                    CategoryId = computers,
                    DateCreated = DateTime.Now
                };

                var COBRAAdvanced = new ProductEntity
                {
                    Name = "COBRA Advanced (I14F.16.H1S2.165.12983)",
                    Description = "Intel Core i5-10400F (2.9 - 4.3 ГГц) / RAM 16 ГБ / HDD 1 ТБ + SSD 240 ГБ / GeForce GTX 1650, 4 ГБ / без ОД / LAN / Linux",
                    Price = 22799,
                    CategoryId = computers,
                    DateCreated = DateTime.Now
                };

                var artlineGaming1 = new ProductEntity
                {
                    Name = "Artline Gaming X35v41",
                    Description = "Intel Core i5-12400F (2.5 — 4.4 ГГц) / RAM 16 ГБ / HDD 1 ТБ + SSD 480 ГБ / nVidia GeForce RTX 3050, 8 ГБ / без ОД / LAN / без ОС",
                    Price = 24799,
                    CategoryId = computers,
                    DateCreated = DateTime.Now
                };

                var ARTLINEOverlord = new ProductEntity
                {
                    Name = "ARTLINE Overlord CG10 (CG10v04UA)",
                    Description = "Intel Core i5-10400F (2.9 - 4.3 ГГц) / RAM 16 ГБ / HDD 1 ТБ + SSD 480 ГБ / nVidia GeForce RTX 3060, 12 ГБ / без ОД / LAN / без ОС",
                    Price = 37499,
                    CategoryId = computers,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(artlineGaming);
                dataContext.Products.Add(QUBE);
                dataContext.Products.Add(artlineGamingX88);
                dataContext.Products.Add(COBRAAdvanced);
                dataContext.Products.Add(artlineGaming1);
                dataContext.Products.Add(ARTLINEOverlord);
                #endregion
                #region Монітори
                var monitor = categories.Where(c => c.Name == "Монітори").First().Id;

                var samsungOdyssey = new ProductEntity
                {
                    Name = "Samsung Odyssey G7 S28AG702 (LS28AG702NIXCI) 4K HDR400 / IPS 8-Bit / 144Гц / sRGB 99% / G-SYNC Compatible",
                    Description = "Діагональ дисплея\r\n28\"\r\nМаксимальна роздільна здатність дисплея\r\n3840x2160 (4K UltraHD)\r\nЧас реакції матриці\r\n1 мс\r\nЯскравість дисплея\r\n400 кд/м2\r\nТип матриці\r\nIPS\r\nКонтрастність дисплея\r\n1000:1 (Typ)\r\nОсобливості\r\nFlicker-Free\r\nUSB-концентратор\r\nБезрамковий (Сinema screen)\r\nПоворотний екран (Pivot)\r\nРегулювання за висотою",
                    Price = 21999,
                    CategoryId = monitor,
                    DateCreated = DateTime.Now
                };

                var asusTUF = new ProductEntity
                {
                    Name = "Asus TUF Gaming VG259QR (90LM0530-B03370) 8-Bit / 165Hz / Adaptive-Sync / G-Sync Compatible",
                    Description = "Діагональ дисплея\r\n24.5\"\r\nМаксимальна роздільна здатність дисплея\r\n1920x1080 (FullHD)\r\nЧас реакції матриці\r\n1 мс MPRT\r\nЯскравість дисплея\r\n300 кд/м²\r\nТип матриці\r\nIPS\r\nКонтрастність дисплея\r\n1000:1\r\nОсобливості\r\nFlicker-Free\r\nБезрамковий (Сinema screen)\r\nПоворотний екран (Pivot)\r\nРегулювання за висотою",
                    Price = 9399,
                    CategoryId = monitor,
                    DateCreated = DateTime.Now
                };

                var samsungCurved = new ProductEntity
                {
                    Name = "Samsung Curved C24F396F (LC24F396FHIXCI)",
                    Description = "Діагональ дисплея\r\n23.5\"\r\nМаксимальна роздільна здатність дисплея\r\n1920x1080 (FullHD)\r\nЧас реакції матриці\r\n4 мс\r\nЯскравість дисплея\r\n250 кд/м²\r\nТип матриці\r\nVA\r\nКонтрастність дисплея\r\n3000:1",
                    Price = 5199,
                    CategoryId = monitor,
                    DateCreated = DateTime.Now
                };

                var dell = new ProductEntity
                {
                    Name = "Dell S2721QS",
                    Description = "Діагональ дисплея\r\n27\"\r\nМаксимальна роздільна здатність дисплея\r\n3840x2160 (4K UltraHD)\r\nЧас реакції матриці\r\n4 мс\r\nЯскравість дисплея\r\n350 кд/м²\r\nТип матриці\r\nIPS\r\nКонтрастність дисплея\r\n1300:1",
                    Price = 13599,
                    CategoryId = monitor,
                    DateCreated = DateTime.Now
                };

                var asusProArtDisplay = new ProductEntity
                {
                    Name = "Asus ProArt Display PA278CV",
                    Description = "Діагональ дисплея\r\n27\"\r\nМаксимальна роздільна здатність дисплея\r\n2560x1440 (2K QHD)\r\nЧас реакції матриці\r\n5 мс\r\nЯскравість дисплея\r\n350 кд/м²\r\nТип матриці\r\nIPS\r\nКонтрастність дисплея\r\n1000:1",
                    Price = 16499,
                    CategoryId = monitor,
                    DateCreated = DateTime.Now
                };

                var philips = new ProductEntity
                {
                    Name = "Philips - IPS 8-Bit 165Гц/ sRGB 123,9% / nVidia G-Sync Compatible / AMD FreeSync Premium",
                    Description = "Діагональ дисплея\r\n23.8\"\r\nМаксимальна роздільна здатність дисплея\r\n1920x1080 (FullHD)\r\nЧас реакції матриці\r\n1 мс (MPRT), 4 мс (серый к серому)\r\nЯскравість дисплея\r\n250 кд/м²\r\nТип матриці\r\nIPS\r\nКонтрастність дисплея\r\n1100:1",
                    Price = 7799,
                    CategoryId = monitor,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(samsungOdyssey);
                dataContext.Products.Add(asusTUF);
                dataContext.Products.Add(samsungCurved);
                dataContext.Products.Add(dell);
                dataContext.Products.Add(asusProArtDisplay);
                dataContext.Products.Add(philips);
                #endregion
                #region Мобільні телефони
                var phone = categories.Where(c => c.Name == "Мобільні телефони").First().Id;

                var appleiPhone13 = new ProductEntity
                {
                    Name = "Apple iPhone 13 128GB Pink (MLPH3HU/A)",
                    Description = "Екран (6.1\", OLED (Super Retina XDR), 2532x1170) / Apple A15 Bionic / подвійна основна камера: 12 Мп + 12 Мп, фронтальна камера: 12 Мп / 128 ГБ вбудованої пам'яті / 3G / LTE / 5G / GPS / Nano-SIM, eSIM / iOS 15",
                    Price = 35999,
                    CategoryId = phone,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(appleiPhone13);
                #endregion
                #region Телевізори
                var tv = categories.Where(c => c.Name == "Телевізори").First().Id;

                var samsung = new ProductEntity
                {
                    Name = "Samsung UE50AU7100UXUA",
                    Description = "Діагональ екрана\r\n50\"\r\nРоздільна здатність\r\n3840x2160\r\nПлатформа\r\nTizen\r\nДіапазони цифрового тюнера\r\nDVB-C\r\nDVB-S2\r\nDVB-T2",
                    Price = 19799,
                    CategoryId = tv,
                    DateCreated = DateTime.Now
                };

                dataContext.Products.Add(samsung);
                #endregion

                dataContext.SaveChanges();
            }
        }
        private static void SeedImages(EFAppContext dataContext)
        {
            if (!dataContext.ProductImages.Any())
            {
                IProductRepository productRepository = new ProductRepository(dataContext);
                var products = productRepository.GetAll();

                #region Ноутбуки
                #region Acer Aspire 7 A715-42G-R8BL (NH.QDLEU.008) Charcoal Black
                var AcerAspire = products.Where(x => x.Name == "Acer Aspire 7 A715-42G-R8BL (NH.QDLEU.008) Charcoal Black").First().Id;

                var AcerAspirephoto1 = new ProductImageEntity
                {
                   Name = @"https://content1.rozetka.com.ua/goods/images/big/254116608.jpg",
                   Priority = 1,
                   ProductId = AcerAspire,
                   DateCreated = DateTime.Now
                };

                var AcerAspirephoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/290848838.jpg",
                    Priority = 2,
                    ProductId = AcerAspire,
                    DateCreated = DateTime.Now
                };

                var AcerAspirephoto3 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/254116609.jpg",
                    Priority = 3,
                    ProductId = AcerAspire,
                    DateCreated = DateTime.Now
                };

                var AcerAspirephoto4 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/254116610.jpg",
                    Priority = 4,
                    ProductId = AcerAspire,
                    DateCreated = DateTime.Now
                };

                var AcerAspirephoto5 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/254116611.jpg",
                    Priority = 5,
                    ProductId = AcerAspire,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AcerAspirephoto1);
                dataContext.ProductImages.Add(AcerAspirephoto2);
                dataContext.ProductImages.Add(AcerAspirephoto3);
                dataContext.ProductImages.Add(AcerAspirephoto4);
                dataContext.ProductImages.Add(AcerAspirephoto5);
                #endregion
                #region Apple MacBook Air 13\" M1 256GB 2020 (MGN93) Silver
                var MacBookAir = products.Where(x => x.Name == "Apple MacBook Air 13\" M1 256GB 2020 (MGN93) Silver").First().Id;

                var MacBookAirphoto1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/215770075.jpg",
                    Priority = 1,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                var MacBookAirphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/215770142.jpg",
                    Priority = 2,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                var MacBookAirphoto3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/215770223.jpg",
                    Priority = 3,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                var MacBookAirphoto4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/215770290.jpg",
                    Priority = 4,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                var MacBookAirphoto5 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/215770341.jpg",
                    Priority = 5,
                    ProductId = MacBookAir,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(MacBookAirphoto1);
                dataContext.ProductImages.Add(MacBookAirphoto2);
                dataContext.ProductImages.Add(MacBookAirphoto3);
                dataContext.ProductImages.Add(MacBookAirphoto4);
                dataContext.ProductImages.Add(MacBookAirphoto5);
                #endregion
                #region Lenovo IdeaPad Gaming 3 15IHU6 (82K101FJRA) Shadow Black
                var LenovoIdeaPad = products.Where(x => x.Name == "Lenovo IdeaPad Gaming 3 15IHU6 (82K101FJRA) Shadow Black").First().Id;

                var LenovoIdeaPadphoto1 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/280598520.jpg",
                    Priority = 1,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                var LenovoIdeaPadphoto2 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/284974732.jpg",
                    Priority = 2,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                var LenovoIdeaPadphoto3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/280598574.jpg",
                    Priority = 3,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                var LenovoIdeaPadphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/280598621.jpg",
                    Priority = 4,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                var LenovoIdeaPadphoto5 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/280598671.jpg",
                    Priority = 5,
                    ProductId = LenovoIdeaPad,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(LenovoIdeaPadphoto1);
                dataContext.ProductImages.Add(LenovoIdeaPadphoto2);
                dataContext.ProductImages.Add(LenovoIdeaPadphoto3);
                dataContext.ProductImages.Add(LenovoIdeaPadphoto4);
                dataContext.ProductImages.Add(LenovoIdeaPadphoto5);
                #endregion
                #region ASUS TUF Gaming F15 (90NR0704-M00CW0) Graphite Black
                var AsusTufGaming = products.Where(x => x.Name == "ASUS TUF Gaming F15 (90NR0704-M00CW0) Graphite Black").First().Id;

                var AsusTufGamingphoto1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/283529272.jpg",
                    Priority = 1,
                    ProductId = AsusTufGaming,
                    DateCreated = DateTime.Now
                };

                var AsusTufGamingphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/283529275.jpg",
                    Priority = 2,
                    ProductId = AsusTufGaming,
                    DateCreated = DateTime.Now
                };

                var AsusTufGamingphoto3 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/283529281.jpg",
                    Priority = 3,
                    ProductId = AsusTufGaming,
                    DateCreated = DateTime.Now
                };

                var AsusTufGamingphoto4 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/283529282.jpg",
                    Priority = 4,
                    ProductId = AsusTufGaming,
                    DateCreated = DateTime.Now
                };

                var AsusTufGamingphoto5 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/283529284.jpg",
                    Priority = 5,
                    ProductId = AsusTufGaming,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AsusTufGamingphoto1);
                dataContext.ProductImages.Add(AsusTufGamingphoto2);
                dataContext.ProductImages.Add(AsusTufGamingphoto3);
                dataContext.ProductImages.Add(AsusTufGamingphoto4);
                dataContext.ProductImages.Add(AsusTufGamingphoto5);
                #endregion
                #region Asus ROG Strix G15 (90NR0502-M003L0) Eclipse Gray
                var AsusRogStrix = products.Where(x => x.Name == "Asus ROG Strix G15 (90NR0502-M003L0) Eclipse Gray").First().Id;

                var AsusRogStrixphoto1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/283528165.jpg",
                    Priority = 1,
                    ProductId = AsusRogStrix,
                    DateCreated = DateTime.Now
                };

                var AsusRogStrixphoto2 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/283528166.jpg",
                    Priority = 2,
                    ProductId = AsusRogStrix,
                    DateCreated = DateTime.Now
                };

                var AsusRogStrixphoto3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/283528167.jpg",
                    Priority = 3,
                    ProductId = AsusRogStrix,
                    DateCreated = DateTime.Now
                };

                var AsusRogStrixphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/283528168.jpg",
                    Priority = 4,
                    ProductId = AsusRogStrix,
                    DateCreated = DateTime.Now
                };

                var AsusRogStrixphoto5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/283528171.jpg",
                    Priority = 5,
                    ProductId = AsusRogStrix,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AsusRogStrixphoto1);
                dataContext.ProductImages.Add(AsusRogStrixphoto2);
                dataContext.ProductImages.Add(AsusRogStrixphoto3);
                dataContext.ProductImages.Add(AsusRogStrixphoto4);
                dataContext.ProductImages.Add(AsusRogStrixphoto5);
                #endregion
                #region Lenovo Legion 5 15IAH7H (82RB00QKRA) Storm Grey
                var LenovoLegion5 = products.Where(x => x.Name == "Lenovo Legion 5 15IAH7H (82RB00QKRA) Storm Grey").First().Id;

                var LenovoLegion5photo1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/308669792.jpg",
                    Priority = 1,
                    ProductId = LenovoLegion5,
                    DateCreated = DateTime.Now
                };

                var LenovoLegion5photo2 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/308669793.jpg",
                    Priority = 2,
                    ProductId = LenovoLegion5,
                    DateCreated = DateTime.Now
                };

                var LenovoLegion5photo3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/308669794.jpg",
                    Priority = 3,
                    ProductId = LenovoLegion5,
                    DateCreated = DateTime.Now
                };

                var LenovoLegion5photo4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/308669797.jpg",
                    Priority = 4,
                    ProductId = LenovoLegion5,
                    DateCreated = DateTime.Now
                };

                var LenovoLegion5photo5 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/308669795.jpg",
                    Priority = 5,
                    ProductId = LenovoLegion5,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(LenovoLegion5photo1);
                dataContext.ProductImages.Add(LenovoLegion5photo2);
                dataContext.ProductImages.Add(LenovoLegion5photo3);
                dataContext.ProductImages.Add(LenovoLegion5photo4);
                dataContext.ProductImages.Add(LenovoLegion5photo5);
                #endregion
                #region ASUS ZenBook Flip 15 (90NB0VJ2-M00110) Light Grey
                var AsusZenBookFlip15 = products.Where(x => x.Name == "ASUS ZenBook Flip 15 (90NB0VJ2-M00110) Light Grey").First().Id;

                var AsusZenBookFlip15photo1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/300283555.jpg",
                    Priority = 1,
                    ProductId = AsusZenBookFlip15,
                    DateCreated = DateTime.Now
                };

                var AsusZenBookFlip15photo2 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/300283557.jpg",
                    Priority = 2,
                    ProductId = AsusZenBookFlip15,
                    DateCreated = DateTime.Now
                };

                var AsusZenBookFlip15photo3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/300283558.jpg",
                    Priority = 3,
                    ProductId = AsusZenBookFlip15,
                    DateCreated = DateTime.Now
                };

                var AsusZenBookFlip15photo4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/300283561.jpg",
                    Priority = 4,
                    ProductId = AsusZenBookFlip15,
                    DateCreated = DateTime.Now
                };

                var AsusZenBookFlip15photo5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/300283565.jpg",
                    Priority = 5,
                    ProductId = AsusZenBookFlip15,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AsusZenBookFlip15photo1);
                dataContext.ProductImages.Add(AsusZenBookFlip15photo2);
                dataContext.ProductImages.Add(AsusZenBookFlip15photo3);
                dataContext.ProductImages.Add(AsusZenBookFlip15photo4);
                dataContext.ProductImages.Add(AsusZenBookFlip15photo5);
                #endregion
                #region Lenovo Yoga 7 (82N7009QRA) Slate Grey
                var LenovoYoga7 = products.Where(x => x.Name == "Lenovo Yoga 7 (82N7009QRA) Slate Grey").First().Id;

                var LenovoYoga7photo1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/254116608.jpg",
                    Priority = 1,
                    ProductId = LenovoYoga7,
                    DateCreated = DateTime.Now
                };

                var LenovoYoga7photo2 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/247773737.jpg",
                    Priority = 2,
                    ProductId = LenovoYoga7,
                    DateCreated = DateTime.Now
                };

                var LenovoYoga7photo3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/247773740.jpg",
                    Priority = 3,
                    ProductId = LenovoYoga7,
                    DateCreated = DateTime.Now
                };

                var LenovoYoga7photo4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/247773744.jpg",
                    Priority = 4,
                    ProductId = LenovoYoga7,
                    DateCreated = DateTime.Now
                };

                var LenovoYoga7photo5 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/247773745.jpg",
                    Priority = 5,
                    ProductId = LenovoYoga7,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(LenovoYoga7photo1);
                dataContext.ProductImages.Add(LenovoYoga7photo2);
                dataContext.ProductImages.Add(LenovoYoga7photo3);
                dataContext.ProductImages.Add(LenovoYoga7photo4);
                dataContext.ProductImages.Add(LenovoYoga7photo5);
                #endregion
                #region Acer Aspire 3 (NX.A6LEU.01A) Pure Silver
                var AcerAspire3 = products.Where(x => x.Name == "Acer Aspire 3 (NX.A6LEU.01A) Pure Silver").First().Id;

                var AcerAspire3photo1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/163390217.jpg",
                    Priority = 1,
                    ProductId = AcerAspire3,
                    DateCreated = DateTime.Now
                };

                var AcerAspire3photo2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/163390218.jpg",
                    Priority = 2,
                    ProductId = AcerAspire3,
                    DateCreated = DateTime.Now
                };

                var AcerAspire3photo3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/163390219.jpg",
                    Priority = 3,
                    ProductId = AcerAspire3,
                    DateCreated = DateTime.Now
                };

                var AcerAspire3photo4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/163390220.jpg",
                    Priority = 4,
                    ProductId = AcerAspire3,
                    DateCreated = DateTime.Now
                };

                var AcerAspire3photo5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/163390221.jpg",
                    Priority = 5,
                    ProductId = AcerAspire3,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AcerAspire3photo1);
                dataContext.ProductImages.Add(AcerAspire3photo2);
                dataContext.ProductImages.Add(AcerAspire3photo3);
                dataContext.ProductImages.Add(AcerAspire3photo4);
                dataContext.ProductImages.Add(AcerAspire3photo5);
                #endregion
                #endregion
                #region Комп'ютери
                #region Artline Gaming X51 v07 (X51v07)
                var ArtlineGaming = products.Where(x => x.Name == "Artline Gaming X51 v07 (X51v07)").First().Id;

                var ArtlineGamingphoto1 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/244859893.jpg",
                    Priority = 1,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingphoto2 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/244859895.jpg",
                    Priority = 2,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingphoto3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/244859897.jpg",
                    Priority = 3,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/244859902.jpg",
                    Priority = 4,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingphoto5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/244859905.jpg",
                    Priority = 5,
                    ProductId = ArtlineGaming,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(ArtlineGamingphoto1);
                dataContext.ProductImages.Add(ArtlineGamingphoto2);
                dataContext.ProductImages.Add(ArtlineGamingphoto3);
                dataContext.ProductImages.Add(ArtlineGamingphoto4);
                dataContext.ProductImages.Add(ArtlineGamingphoto5);
                #endregion
                #region QUBE Ігровий QB R i5 10400F RTX 3060 12 GB 1621
                var QUBE = products.Where(x => x.Name == "QUBE Ігровий QB R i5 10400F RTX 3060 12 GB 1621").First().Id;

                var QUBEphoto1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/255402319.jpg",
                    Priority = 1,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                var QUBEphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/255402320.jpg",
                    Priority = 2,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                var QUBEphoto3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/255402321.jpg",
                    Priority = 3,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                var QUBEphoto4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/255402322.jpg",
                    Priority = 4,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                var QUBEphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/255402328.jpg",
                    Priority = 5,
                    ProductId = QUBE,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(QUBEphoto1);
                dataContext.ProductImages.Add(QUBEphoto2);
                dataContext.ProductImages.Add(QUBEphoto3);
                dataContext.ProductImages.Add(QUBEphoto4);
                dataContext.ProductImages.Add(QUBEphoto5);
                #endregion
                #region Artline Gaming X88 v09 (X88v09)
                var ArtlineGamingX88 = products.Where(x => x.Name == "Artline Gaming X88 v09 (X88v09)").First().Id;

                var ArtlineGamingX88photo1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/308537718.jpg",
                    Priority = 1,
                    ProductId = ArtlineGamingX88,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingX88photo2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/308537725.jpg",
                    Priority = 2,
                    ProductId = ArtlineGamingX88,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingX88photo3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/308537724.jpg",
                    Priority = 3,
                    ProductId = ArtlineGamingX88,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingX88photo4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/308537733.jpg",
                    Priority = 4,
                    ProductId = ArtlineGamingX88,
                    DateCreated = DateTime.Now
                };

                var ArtlineGamingX88photo5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/308537734.jpg",
                    Priority = 5,
                    ProductId = ArtlineGamingX88,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(ArtlineGamingX88photo1);
                dataContext.ProductImages.Add(ArtlineGamingX88photo2);
                dataContext.ProductImages.Add(ArtlineGamingX88photo3);
                dataContext.ProductImages.Add(ArtlineGamingX88photo4);
                dataContext.ProductImages.Add(ArtlineGamingX88photo5);
                #endregion
                #region COBRA Advanced (I14F.16.H1S2.165.12983)
                var COBRAAdvanced = products.Where(x => x.Name == "COBRA Advanced (I14F.16.H1S2.165.12983)").First().Id;

                var COBRAAdvancedphoto1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/286272849.jpg",
                    Priority = 1,
                    ProductId = COBRAAdvanced,
                    DateCreated = DateTime.Now
                };

                var COBRAAdvancedphoto2 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/286272851.jpg",
                    Priority = 2,
                    ProductId = COBRAAdvanced,
                    DateCreated = DateTime.Now
                };

                var COBRAAdvancedphoto3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/286272852.jpg",
                    Priority = 3,
                    ProductId = COBRAAdvanced,
                    DateCreated = DateTime.Now
                };

                var COBRAAdvancedphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/286272853.jpg",
                    Priority = 4,
                    ProductId = COBRAAdvanced,
                    DateCreated = DateTime.Now
                };

                var COBRAAdvancedphoto5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/286272855.jpg",
                    Priority = 5,
                    ProductId = COBRAAdvanced,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(COBRAAdvancedphoto1);
                dataContext.ProductImages.Add(COBRAAdvancedphoto2);
                dataContext.ProductImages.Add(COBRAAdvancedphoto3);
                dataContext.ProductImages.Add(COBRAAdvancedphoto4);
                dataContext.ProductImages.Add(COBRAAdvancedphoto5);
                #endregion
                #region Artline Gaming X35v41
                var ArtlineGaming1 = products.Where(x => x.Name == "Artline Gaming X35v41").First().Id;

                var ArtlineGaming1photo1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/253652590.jpg",
                    Priority = 1,
                    ProductId = ArtlineGaming1,
                    DateCreated = DateTime.Now
                };

                var ArtlineGaming1photo2 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/253652591.jpg",
                    Priority = 2,
                    ProductId = ArtlineGaming1,
                    DateCreated = DateTime.Now
                };

                var ArtlineGaming1photo3 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/253652592.jpg",
                    Priority = 3,
                    ProductId = ArtlineGaming1,
                    DateCreated = DateTime.Now
                };

                var ArtlineGaming1photo4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/253652595.jpg",
                    Priority = 4,
                    ProductId = ArtlineGaming1,
                    DateCreated = DateTime.Now
                };

                var ArtlineGaming1photo5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/253652596.jpg",
                    Priority = 5,
                    ProductId = ArtlineGaming1,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(ArtlineGaming1photo1);
                dataContext.ProductImages.Add(ArtlineGaming1photo2);
                dataContext.ProductImages.Add(ArtlineGaming1photo3);
                dataContext.ProductImages.Add(ArtlineGaming1photo4);
                dataContext.ProductImages.Add(ArtlineGaming1photo5);
                #endregion
                #region ARTLINE Overlord CG10 (CG10v04UA)
                var ARTLINEOverlord = products.Where(x => x.Name == "ARTLINE Overlord CG10 (CG10v04UA)").First().Id;

                var ARTLINEOverlordphoto1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/276447013.jpg",
                    Priority = 1,
                    ProductId = ARTLINEOverlord,
                    DateCreated = DateTime.Now
                };

                var ARTLINEOverlordphoto2 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/276447017.jpg",
                    Priority = 2,
                    ProductId = ARTLINEOverlord,
                    DateCreated = DateTime.Now
                };

                var ARTLINEOverlordphoto3 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/276447020.jpg",
                    Priority = 3,
                    ProductId = ARTLINEOverlord,
                    DateCreated = DateTime.Now
                };

                var ARTLINEOverlordphoto4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/276447027.jpg",
                    Priority = 4,
                    ProductId = ARTLINEOverlord,
                    DateCreated = DateTime.Now
                };

                var ARTLINEOverlordphoto5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/276447030.jpg",
                    Priority = 5,
                    ProductId = ARTLINEOverlord,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(ARTLINEOverlordphoto1);
                dataContext.ProductImages.Add(ARTLINEOverlordphoto2);
                dataContext.ProductImages.Add(ARTLINEOverlordphoto3);
                dataContext.ProductImages.Add(ARTLINEOverlordphoto4);
                dataContext.ProductImages.Add(ARTLINEOverlordphoto5);
                #endregion
                #endregion
                #region Монітори
                #region Samsung Odyssey G7 S28AG702 (LS28AG702NIXCI) 4K HDR400 / IPS 8-Bit / 144Гц / sRGB 99% / G-SYNC Compatible
                var SamsungOdyssey = products.Where(x => x.Name == "Samsung Odyssey G7 S28AG702 (LS28AG702NIXCI) 4K HDR400 / IPS 8-Bit / 144Гц / sRGB 99% / G-SYNC Compatible").First().Id;

                var SamsungOdysseyphoto1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/250044076.jpg",
                    Priority = 1,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                var SamsungOdysseyphoto2 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/250044127.jpg",
                    Priority = 2,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                var SamsungOdysseyphoto3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/250044109.jpg",
                    Priority = 3,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                var SamsungOdysseyphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/250044106.jpg",
                    Priority = 4,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                var SamsungOdysseyphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/250044088.jpg",
                    Priority = 5,
                    ProductId = SamsungOdyssey,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(SamsungOdysseyphoto1);
                dataContext.ProductImages.Add(SamsungOdysseyphoto2);
                dataContext.ProductImages.Add(SamsungOdysseyphoto3);
                dataContext.ProductImages.Add(SamsungOdysseyphoto4);
                dataContext.ProductImages.Add(SamsungOdysseyphoto5);
                #endregion
                #region Asus TUF Gaming VG259QR (90LM0530-B03370) 8-Bit / 165Hz / Adaptive-Sync / G-Sync Compatible
                var AsusTUF = products.Where(x => x.Name == "Asus TUF Gaming VG259QR (90LM0530-B03370) 8-Bit / 165Hz / Adaptive-Sync / G-Sync Compatible").First().Id;

                var AsusTUFphoto1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/168721089.jpg",
                    Priority = 1,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                var AsusTUFphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/168721090.jpg",
                    Priority = 2,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                var AsusTUFphoto3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/168721091.jpg",
                    Priority = 3,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                var AsusTUFphoto4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/168721092.jpg",
                    Priority = 4,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                var AsusTUFphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/168721093.jpg",
                    Priority = 5,
                    ProductId = AsusTUF,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AsusTUFphoto1);
                dataContext.ProductImages.Add(AsusTUFphoto2);
                dataContext.ProductImages.Add(AsusTUFphoto3);
                dataContext.ProductImages.Add(AsusTUFphoto4);
                dataContext.ProductImages.Add(AsusTUFphoto5);
                #endregion
                #region Samsung Curved C24F396F (LC24F396FHIXCI)
                var SamsungCurved = products.Where(x => x.Name == "Samsung Curved C24F396F (LC24F396FHIXCI)").First().Id;

                var SamsungCurvedphoto1 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/106654984.jpg",
                    Priority = 1,
                    ProductId = SamsungCurved,
                    DateCreated = DateTime.Now
                };

                var SamsungCurvedphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/106655028.jpg",
                    Priority = 2,
                    ProductId = SamsungCurved,
                    DateCreated = DateTime.Now
                };

                var SamsungCurvedphoto3 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/106655088.jpg",
                    Priority = 3,
                    ProductId = SamsungCurved,
                    DateCreated = DateTime.Now
                };

                var SamsungCurvedphoto4 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/106655100.jpg",
                    Priority = 4,
                    ProductId = SamsungCurved,
                    DateCreated = DateTime.Now
                };

                var SamsungCurvedphoto5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/106655044.jpg",
                    Priority = 5,
                    ProductId = SamsungCurved,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(SamsungCurvedphoto1);
                dataContext.ProductImages.Add(SamsungCurvedphoto2);
                dataContext.ProductImages.Add(SamsungCurvedphoto3);
                dataContext.ProductImages.Add(SamsungCurvedphoto4);
                dataContext.ProductImages.Add(SamsungCurvedphoto5);
                #endregion
                #region Dell S2721QS
                var Dell = products.Where(x => x.Name == "Dell S2721QS").First().Id;

                var Dellphoto1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/177849855.jpg",
                    Priority = 1,
                    ProductId = Dell,
                    DateCreated = DateTime.Now
                };

                var Dellphoto2 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/177849862.jpg",
                    Priority = 2,
                    ProductId = Dell,
                    DateCreated = DateTime.Now
                };

                var Dellphoto3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/177849860.jpg",
                    Priority = 3,
                    ProductId = Dell,
                    DateCreated = DateTime.Now
                };

                var Dellphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/177849857.jpg",
                    Priority = 4,
                    ProductId = Dell,
                    DateCreated = DateTime.Now
                };

                var Dellphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/177849863.jpg",
                    Priority = 5,
                    ProductId = Dell,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(Dellphoto1);
                dataContext.ProductImages.Add(Dellphoto2);
                dataContext.ProductImages.Add(Dellphoto3);
                dataContext.ProductImages.Add(Dellphoto4);
                dataContext.ProductImages.Add(Dellphoto5);
                #endregion
                #region Asus ProArt Display PA278CV
                var AsusProArtDisplay = products.Where(x => x.Name == "Asus ProArt Display PA278CV").First().Id;

                var AsusProArtDisplayphoto1 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/176729088.jpg",
                    Priority = 1,
                    ProductId = AsusProArtDisplay,
                    DateCreated = DateTime.Now
                };

                var AsusProArtDisplayphoto2 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/176729087.jpg",
                    Priority = 2,
                    ProductId = AsusProArtDisplay,
                    DateCreated = DateTime.Now
                };

                var AsusProArtDisplayphoto3 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/176729090.jpg",
                    Priority = 3,
                    ProductId = AsusProArtDisplay,
                    DateCreated = DateTime.Now
                };

                var AsusProArtDisplayphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/176729089.jpg",
                    Priority = 4,
                    ProductId = AsusProArtDisplay,
                    DateCreated = DateTime.Now
                };

                var AsusProArtDisplayphoto5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/176729084.jpg",
                    Priority = 5,
                    ProductId = AsusProArtDisplay,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AsusProArtDisplayphoto1);
                dataContext.ProductImages.Add(AsusProArtDisplayphoto2);
                dataContext.ProductImages.Add(AsusProArtDisplayphoto3);
                dataContext.ProductImages.Add(AsusProArtDisplayphoto4);
                dataContext.ProductImages.Add(AsusProArtDisplayphoto5);
                #endregion
                #region Philips - IPS 8-Bit 165Гц/ sRGB 123,9% / nVidia G-Sync Compatible / AMD FreeSync Premium
                var Philips = products.Where(x => x.Name == "Philips - IPS 8-Bit 165Гц/ sRGB 123,9% / nVidia G-Sync Compatible / AMD FreeSync Premium").First().Id;

                var Philipsphoto1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/302356540.jpg",
                    Priority = 1,
                    ProductId = Philips,
                    DateCreated = DateTime.Now
                };

                var Philipsphoto2 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/301346716.jpg",
                    Priority = 2,
                    ProductId = Philips,
                    DateCreated = DateTime.Now
                };

                var Philipsphoto3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/301346715.jpg",
                    Priority = 3,
                    ProductId = Philips,
                    DateCreated = DateTime.Now
                };

                var Philipsphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/301346711.jpg",
                    Priority = 4,
                    ProductId = Philips,
                    DateCreated = DateTime.Now
                };

                var Philipsphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/301346712.jpg",
                    Priority = 5,
                    ProductId = Philips,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(Philipsphoto1);
                dataContext.ProductImages.Add(Philipsphoto2);
                dataContext.ProductImages.Add(Philipsphoto3);
                dataContext.ProductImages.Add(Philipsphoto4);
                dataContext.ProductImages.Add(Philipsphoto5);
                #endregion
                #endregion
                #region Мобільні телефони
                #region Apple iPhone 13 128GB Pink (MLPH3HU/A)
                var AppleiPhone13 = products.Where(x => x.Name == "Apple iPhone 13 128GB Pink (MLPH3HU/A)").First().Id;

                var AppleiPhone13photo1 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/221214151.jpg",
                    Priority = 1,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                var AppleiPhone13photo2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/221023395.jpg",
                    Priority = 2,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                var AppleiPhone13photo3 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/221214152.jpg",
                    Priority = 3,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                var AppleiPhone13photo4 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/221214153.jpg",
                    Priority = 4,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                var AppleiPhone13photo5 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/221214154.jpg",
                    Priority = 5,
                    ProductId = AppleiPhone13,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(AppleiPhone13photo1);
                dataContext.ProductImages.Add(AppleiPhone13photo2);
                dataContext.ProductImages.Add(AppleiPhone13photo3);
                dataContext.ProductImages.Add(AppleiPhone13photo4);
                dataContext.ProductImages.Add(AppleiPhone13photo5);
                #endregion
                #endregion
                #region Телевізори
                #region Samsung UE50AU7100UXUA
                var Samsung = products.Where(x => x.Name == "Samsung UE50AU7100UXUA").First().Id;

                var Samsungphoto1 = new ProductImageEntity
                {
                    Name = @"https://content.rozetka.com.ua/goods/images/big/303985202.jpg",
                    Priority = 1,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                var Samsungphoto2 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/303985203.jpg",
                    Priority = 2,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                var Samsungphoto3 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/303985204.jpg",
                    Priority = 3,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                var Samsungphoto4 = new ProductImageEntity
                {
                    Name = @"https://content1.rozetka.com.ua/goods/images/big/303985205.jpg",
                    Priority = 4,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                var Samsungphoto5 = new ProductImageEntity
                {
                    Name = @"https://content2.rozetka.com.ua/goods/images/big/303985206.jpg",
                    Priority = 5,
                    ProductId = Samsung,
                    DateCreated = DateTime.Now
                };

                dataContext.ProductImages.Add(Samsungphoto1);
                dataContext.ProductImages.Add(Samsungphoto2);
                dataContext.ProductImages.Add(Samsungphoto3);
                dataContext.ProductImages.Add(Samsungphoto4);
                dataContext.ProductImages.Add(Samsungphoto5);
                #endregion
                #endregion

                dataContext.SaveChanges();
            }
        }
    }
}
