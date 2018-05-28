using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SportStore.API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }
        [Required, StringLength(30)]
        public string Category { get; set; }
        public string PictureFile { get; set; }
        public string Picture { get; set; }
        public int Stocklevel { get; set; }
        public virtual List<StockIn> StockIns { get; set; }
    }
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        [Required, StringLength(50)]
        public string CustomerName { get; set; }
        [Required, StringLength(250)]
        public string ShippingAddress { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [Required, StringLength(50), EmailAddress]
        public string Email { get; set; }
        [StringLength(50)]
        public string TransactionId { get; set; }
        //pu
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderDetail
    {
        [Key,Column(Order=0)]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        //
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

    public class StockIn
    {
        public int StockInId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int quantity { get; set; }
        //Navigation
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
    public class TechShopDbContext : DbContext
    {
        public TechShopDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public System.Data.Entity.DbSet<SportStore.API.Models.StockIn> StockIns { get; set; }
    }
    public class DbInitializer : DropCreateDatabaseIfModelChanges<TechShopDbContext>
    {
        protected override void Seed(TechShopDbContext context)
        {
            Product p1 = (new Product { Name = "Head Phones", Price = 1200, Description = "HeadPhone KEF M5000", Category = "Accessories", PictureFile = "KEF M5000.jpg", Picture = "KEF M5000.jpg", Stocklevel = 10 });
            Product p2 = (new Product { Name = "Head Phones", Price = 1500, Description = "HeadPhone samsungWireless", Category = "Accessories", PictureFile = "samsungWireless.jpg", Picture = "samsungWireless.jpg", Stocklevel = 5 });
            Product p3 = (new Product { Name = "Head Phones", Price = 2000, Description = "HeadPhone upc", Category = "Accessories", PictureFile = "upc.jpg", Picture = "upc.jpg", Stocklevel = 10 });
            Product p4 = (new Product { Name = "Pen Drive", Price = 1200, Description = "adata32 Pen Drive", Category = "Accessories", PictureFile = "adata32.jpg", Picture = "adata32.jpg", Stocklevel = 11 });
            Product p5 = (new Product { Name = "Pen Drive", Price = 1100, Description = "Emteec64 Pen Drive", Category = "Accessories", PictureFile = "Emteec64.jpg", Picture = "Emteec64.jpg", Stocklevel = 15 });
            Product p6 = (new Product { Name = "Pen Drive", Price = 1500, Description = "hamausb2 Pen Drive", Category = "Accessories", PictureFile = "hamausb2.jpg", Picture = "hamausb2.jpg", Stocklevel = 5 });
            Product p7 = (new Product { Name = "Toshiba HDD", Price = 5200, Description = "Toshiba HDD", Category = "Accessories", PictureFile = "hdd2.jpg", Picture = "hdd2.jpg", Stocklevel = 10 });
            Product p8 = (new Product { Name = "Western Digital HDD", Price = 4200, Description = "Western Digital HDD", Category = "Accessories", PictureFile = "3.jpg", Picture = "3.jpg", Stocklevel = 4 });
            Product p9 = (new Product { Name = "Adata SD700 External HDD", Price = 5200, Description = "Adata SD700 External HDD", Category = "Accessories", PictureFile = "hdd4.jpg", Picture = "hdd4.jpg", Stocklevel = 7 });
            Product p10 = (new Product { Name = "Router", Price = 1500, Description = "dlink Router", Category = "Accessories", PictureFile = "dlink.jpg", Picture = "dlink.jpg", Stocklevel = 8 });
            Product p11 = (new Product { Name = "Router", Price = 1100, Description = "tplink archar Router", Category = "Accessories", PictureFile = "tplink archar.jpg", Picture = "tplink archar.jpg", Stocklevel = 9 });
            Product p12 = (new Product { Name = "Router", Price = 1300, Description = "tplink Router", Category = "Accessories", PictureFile = "tplink.jpg", Picture = "tplink.jpg", Stocklevel = 10 });

            Product p13 = (new Product { Name = "Hp Desktop", Price = 45000, Description = "hpCompaq6910p Desktop", Category = "Laptop & Desktop", PictureFile = "hpCompaq6910p.jpg", Picture = "hpCompaq6910p.jpg", Stocklevel = 11 });
            Product p14 = (new Product { Name = "Hp Desktop", Price = 80000, Description = "hpPromo5698x Desktop", Category = "Laptop & Desktop", PictureFile = "hpPromo5698x.jpg", Picture = "hpPromo5698x.jpg", Stocklevel = 15 });
            Product p15 = (new Product { Name = "Hp Desktop", Price = 60000, Description = "hpQuadCore Desktop", Category = "Laptop & Desktop", PictureFile = "hpQuadCore.jpg", Picture = "hpQuadCore.jpg", Stocklevel = 13 });
            Product p16 = (new Product { Name = "Desktop", Price = 80000, Description = "intelcorei7 Desktop", Category = "Laptop & Desktop", PictureFile = "intelcorei7.jpg", Picture = "intelcorei7.jpg", Stocklevel = 14 });
            Product p17 = (new Product { Name = "Apple Laptop", Price = 12000, Description = "macbook_retina Laptop", Category = "Laptop & Desktop", PictureFile = "macbook_retina.jpg", Picture = "macbook_retina.jpg", Stocklevel = 0 });
            Product p18 = (new Product { Name = "Apple Laptop", Price = 90000, Description = "macbooki5 Laptop", Category = "Laptop & Desktop", PictureFile = "macbooki5.jpg", Picture = "macbooki5.jpg", Stocklevel = 2 });
            Product p19 = (new Product { Name = "Dell Laptop", Price = 95000, Description = "dellInspiration5000 Laptop", Category = "Laptop & Desktop", PictureFile = "dellInspiration5000.jpg", Picture = "dellInspiration5000.jpg", Stocklevel = 3 });
            Product p20 = (new Product { Name = "Dell Laptop", Price = 46000, Description = "dellInspire5500 Laptop", Category = "Laptop & Desktop", PictureFile = "dellInspire5500.jpg", Picture = "dellInspire5500.jpg", Stocklevel = 5 });
            Product p21 = (new Product { Name = "Dell Laptop", Price = 60000, Description = "dellInspireUltra Laptop", Category = "Laptop & Desktop", PictureFile = "dellInspireUltra.jpg", Picture = "dellInspireUltra.jpg", Stocklevel = 4 });
            Product p22 = (new Product { Name = "HP Laptop", Price = 48000, Description = "hpPavillion Laptop", Category = "Laptop & Desktop", PictureFile = "hpPavillion.jpg", Picture = "hpPavillion.jpg", Stocklevel = 3 });
            Product p23 = (new Product { Name = "HP Laptop", Price = 55000, Description = "hpProbook450 Laptop", Category = "Laptop & Desktop", PictureFile = "hpProbook450.jpg", Picture = "hpProbook450.jpg", Stocklevel = 6 });
            Product p24 = (new Product { Name = "HP Laptop", Price = 65000, Description = "hpx2 Laptop", Category = "Laptop & Desktop", PictureFile = "hpx2.jpg", Picture = "hpx2.jpg", Stocklevel = 3 });

            Product p25 = (new Product { Name = "Sony", Price = 35000, Description = "SonyXperia_X Mobile", Category = "Mobile", PictureFile = "SonyXperia_X.jpg", Picture = "SonyXperia_X.jpg", Stocklevel = 5 });
            Product p26 = (new Product { Name = "Sony", Price = 45500, Description = "SonyXperia_Z1 Mobile", Category = "Mobile", PictureFile = "SonyXperia_Z1.jpg", Picture = "SonyXperia_Z1.jpg", Stocklevel = 3 });
            Product p27 = (new Product { Name = "Sony", Price = 25000, Description = "SonyXperia_Z3D6653 Mobile", Category = "Mobile", PictureFile = "SonyXperia_Z3D6653.jpg", Picture = "SonyXperia_Z3D6653.jpg", Stocklevel = 2 });
            Product p28 = (new Product { Name = "Apple IPhone", Price = 35000, Description = "Iphone6 Mobile", Category = "Mobile", PictureFile = "Iphone6.jpg", Picture = "Iphone6.jpg", Stocklevel = 3 });
            Product p29 = (new Product { Name = "Apple IPhone", Price = 45000, Description = "iPhone7_Plus Mobile", Category = "Mobile", PictureFile = "iPhone7_Plus.jpg", Picture = "iPhone7_Plus.jpg", Stocklevel = 2 });
            Product p30 = (new Product { Name = "Apple IPhone", Price = 101000, Description = "iPhoneX Mobile", Category = "Mobile", PictureFile = "iPhoneX.jpg", Picture = "iPhoneX.jpg", Stocklevel = 4 });
            Product p31 = (new Product { Name = "Samsung Mobile", Price = 14500, Description = "R8 Mobile", Category = "Mobile", PictureFile = "R8.jpg", Picture = "R8.jpg", Stocklevel = 6 });
            Product p32 = (new Product { Name = "Samsung Mobile", Price = 350500, Description = "samsung_s9_dual Mobile", Category = "Mobile", PictureFile = "samsung_s9_dual.jpg", Picture = "samsung_s9_dual.jpg", Stocklevel = 5 });
            Product p33 = (new Product { Name = "Samsung Mobile", Price = 14700, Description = "SamsungGalaxy_+ Mobile", Category = "Mobile", PictureFile = "SamsungGalaxy_+.jpg", Picture = "SamsungGalaxy_+.jpg", Stocklevel = 4 });
            Product p34 = (new Product { Name = "Nokia Mobile", Price = 15200, Description = "new-nokia-6 Mobile", Category = "Mobile", PictureFile = "new-nokia-6.jpg", Picture = "new-nokia-6.jpg", Stocklevel = 3 });
            Product p35 = (new Product { Name = "Nokia Mobile", Price = 13300, Description = "Nokia_lumia720 Mobile", Category = "Mobile", PictureFile = "Nokia_lumia720.jpg", Picture = "Nokia_lumia720.jpg", Stocklevel = 1 });
            Product p36 = (new Product { Name = "Nokia Mobile", Price = 16520, Description = "Nokia-Lumia-Windows-Phone-8-1-LTE Mobile", Category = "Mobile", PictureFile = "Nokia-Lumia-Windows-Phone-8-1-LTE.jpg", Picture = "Nokia-Lumia-Windows-Phone-8-1-LTE.jpg", Stocklevel = 1 });

            Product p37 = (new Product { Name = "Samsung Tablet", Price = 12000, Description = "s10 Tablet", Category = "Tablet", PictureFile = "s10.jpg", Picture = "s10.jpg", Stocklevel = 3 });
            Product p38 = (new Product { Name = "Samsung Tablet", Price = 17000, Description = "tablet3g Tablet", Category = "Tablet", PictureFile = "tablet3g.jpg", Picture = "tablet3g.jpg", Stocklevel = 1 });
            Product p39 = (new Product { Name = "Samsung Tablet", Price = 15000, Description = "tabs3 Tablet", Category = "Tablet", PictureFile = "tabs3.jpg", Picture = "tabs3.jpg", Stocklevel = 1 });
            Product p40 = (new Product { Name = "Apple IPad", Price = 45000, Description = "tab7 Tablet", Category = "Tablet", PictureFile = "tab7.jpg", Picture = "tab7.jpg", Stocklevel = 1 });
            Product p41 = (new Product { Name = "Apple IPad", Price = 35500, Description = "tabs2 Tablet", Category = "Tablet", PictureFile = "tabs2.jpg", Picture = "tabs2.jpg", Stocklevel = 1 });
            Product p42 = (new Product { Name = "Apple IPad", Price = 25000, Description = "tabs05 Tablet", Category = "Tablet", PictureFile = "tabs05.jpg", Picture = "tabs05.jpg", Stocklevel = 1 });
            Product p43 = (new Product { Name = "Lenovo tab47-android", Price = 12000, Description = "lenovo-tab-37-android-tablet-tb3-730x Tablet", Category = "Tablet", PictureFile = "lenovo-tab-37-android-tablet-tb3-730x.jpg", Picture = "lenovo-tab-37-android-tablet-tb3-730x.jpg", Stocklevel = 1 });
            Product p44 = (new Product { Name = "Lenovo tab48-android", Price = 12500, Description = "lenovo-tab--tablet-tb-8504x Tablet", Category = "Tablet", PictureFile = "lenovo-tab-48-android-tablet-tb-8504x.jpg", Picture = "lenovo-tab-48-android-tablet-tb-8504x.jpg", Stocklevel = 1 });
            Product p45 = (new Product { Name = "Lenovo tab", Price = 16500, Description = "windows Tablet", Category = "Tablet", PictureFile = "windows.jpg", Picture = "windows.jpg", Stocklevel = 1 });
            Product p46 = (new Product { Name = "Sony tabz4 ", Price = 22000, Description = "sony_z4 Tablet", Category = "Tablet", PictureFile = "sony_z4.jpg", Picture = "sony_z4.jpg", Stocklevel = 1 });
            Product p47 = (new Product { Name = "Sony tabz", Price = 21000, Description = "sonyz Tablet", Category = "Tablet", PictureFile = "sonyz.jpg", Picture = "sonyz.jpg", Stocklevel = 1 });
            Product p48 = (new Product { Name = "Sony tabz4x", Price = 15000, Description = "z4x Tablet", Category = "Tablet", PictureFile = "z4x.jpg", Picture = "z4x.jpg", Stocklevel = 1 });
            context.Products.AddRange(new Product[] { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48 });
            context.SaveChanges();

            StockIn si1 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 10, ProductId = 1 });
            StockIn si2 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 15, ProductId = 2 });
            StockIn si3 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 10, ProductId = 3 });
            StockIn si4 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 11, ProductId = 4 });
            StockIn si5 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 15, ProductId = 5 });
            StockIn si6 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 5, ProductId = 6 });
            StockIn si7 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 10, ProductId = 7 });
            StockIn si8 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 4, ProductId = 8 });
            StockIn si9 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 7, ProductId = 9 });
            StockIn si10 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 8, ProductId = 10 });
            StockIn si11 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 9, ProductId = 11 });
            StockIn si12 = (new StockIn { Date = DateTime.Parse("2018-01-16"), quantity = 10, ProductId = 12 });

            StockIn si13 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 11, ProductId = 13 });
            StockIn si14 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 15, ProductId = 14 });
            StockIn si15 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 13, ProductId = 15 });
            StockIn si16 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 14, ProductId = 16 });
            StockIn si17 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 0, ProductId = 17 });
            StockIn si18 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 2, ProductId = 18 });
            StockIn si19 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 3, ProductId = 19 });
            StockIn si20 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 5, ProductId = 20 });
            StockIn si21 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 4, ProductId = 21 });
            StockIn si22 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 3, ProductId = 22 });
            StockIn si23 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 6, ProductId = 23 });
            StockIn si24 = (new StockIn { Date = DateTime.Parse("2018-02-11"), quantity = 3, ProductId = 24 });

            StockIn si25 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 5, ProductId = 25 });
            StockIn si26 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 3, ProductId = 26 });
            StockIn si27 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 2, ProductId = 27 });
            StockIn si28 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 3, ProductId = 28 });
            StockIn si29 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 2, ProductId = 29 });
            StockIn si30 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 4, ProductId = 30 });
            StockIn si31 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 6, ProductId = 31 });
            StockIn si32 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 5, ProductId = 32 });
            StockIn si33 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 4, ProductId = 33 });
            StockIn si34 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 3, ProductId = 34 });
            StockIn si35 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 1, ProductId = 35 });
            StockIn si36 = (new StockIn { Date = DateTime.Parse("2018-03-19"), quantity = 1, ProductId = 36 });

            StockIn si37 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 3, ProductId = 37 });
            StockIn si38 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 38 });
            StockIn si39 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 39 });
            StockIn si40 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 40 });
            StockIn si41 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 41 });
            StockIn si42 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 42 });
            StockIn si43 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 43 });
            StockIn si44 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 44 });
            StockIn si45 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 45 });
            StockIn si46 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 46 });
            StockIn si47 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 47 });
            StockIn si48 = (new StockIn { Date = DateTime.Parse("2018-04-02"), quantity = 1, ProductId = 48 });
            context.StockIns.AddRange(new StockIn[] { si1, si2, si3, si4, si5, si6, si7, si8, si9, si10, si11, si12, si13, si14, si15, si16, si17, si18, si19, si20, si21, si22, si23, si24, si25, si26, si27, si28, si29, si30, si31, si32, si33, si34, si35, si36, si37, si38, si39, si40, si41, si42, si43, si44, si45, si46, si47, si48 });
            context.SaveChanges();

            //Order od = (new Order {OrderDate=DateTime.Parse("2018-04-10"),ShippedDate=DateTime.Parse("2018-04-12"),CustomerName="Al-Amin",ShippingAddress="H-122,R-10,S-07,Uttara,Dhaka",Phone="01727750408",Email="alamin@gmail.com",TransactionId="t1" });
            //Order od1 = (new Order { OrderDate = DateTime.Parse("2018-04-15"), ShippedDate = DateTime.Parse("2018-04-17"), CustomerName = "Faisal Ahmed", ShippingAddress = "H-89,R-08,S-12,Mirpur,Dhaka", Phone = "01828850408", Email = "faisal@gmail.com", TransactionId = "t2" });
            //Order od2 = (new Order { OrderDate = DateTime.Parse("2018-05-01"), ShippedDate = DateTime.Parse("2018-05-03"), CustomerName = "Rabiul Islam", ShippingAddress = "H-05,R-15,S-01,Gulshan,Dhaka", Phone = "01924350049", Email = "rabiul@gmail.com", TransactionId = "t3" });
            //Order od3 = (new Order { OrderDate = DateTime.Parse("2018-04-22"), ShippedDate = DateTime.Parse("2018-04-25"), CustomerName = "Rana Khan", ShippingAddress = "H-02,R-21,S-14,Dhanmondi,Dhaka", Phone = "01622791019", Email = "rana@gmail.com", TransactionId = "t4" });
            //Order od4 = (new Order { OrderDate = DateTime.Parse("2018-04-25"), ShippedDate = DateTime.Parse("2018-04-28"), CustomerName = "Rafiq Islam", ShippingAddress = "H-D15,R-01,S-06,Uttara,Dhaka", Phone = "01712456578", Email = "rafiq@gmail.com", TransactionId = "t5" });
            //context.Orders.AddRange(new Order[] { od, od1, od2, od3, od4 });
            //context.SaveChanges();

            //OrderDetail orderD1=(new OrderDetail{OrderId=1,ProductId=1,Quantity=1});
            //OrderDetail orderD2=(new OrderDetail{OrderId=2,ProductId=5,Quantity=1});
            //OrderDetail orderD3=(new OrderDetail{OrderId=3,ProductId=6,Quantity=1});
            //OrderDetail orderD4=(new OrderDetail{OrderId=4,ProductId=8,Quantity=1});
            //OrderDetail orderD5=(new OrderDetail{OrderId=5,ProductId=10,Quantity=1});
            //context.OrderDetails.AddRange(new OrderDetail[] {orderD1,orderD2,orderD3,orderD4,orderD5 });
            //context.SaveChanges();

            base.Seed(context);
        }
    }
}