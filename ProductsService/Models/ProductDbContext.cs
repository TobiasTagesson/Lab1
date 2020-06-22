﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Models
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(               
                new Product { Id = new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), Name = "Wilson Pro Staff 97", Description = "Racquet for the advanced player", Price = 199.00M, ImageUrl = "https://shop.wilson.com/media/catalog/product/cache/38/image/9df78eab33525d08d6e5fb8d27136e95/c/f/cf893c83c0ff231061c2beb3f5a68306228e5d5c_wrt73900u_pro_staff_97_bl_bl_side.jpg" },
                new Product { Id = new Guid("04259d6e-d326-4d40-8936-cd85e688bab4"), Name = "Dunlop Biomimetic 300", Description = "Racquet with great control", Price = 179.00M, ImageUrl = "https://cdni.onedayonly.co.za/catalog/product/1/_/1_7_12.jpg?auto=compress&bg=fff&fit=fill&h=200&w=200" },
                new Product { Id = new Guid("7dec6022-2f61-47cb-8bd0-8bd9e35680dd"), Name = "Yonex DR 98", Description = "Great power, great control", Price = 189.00M, ImageUrl = "https://www.mistertennis.com/images/1-media/yonex/racket/EZD98_Plus_Blue_B.jpg" },
                new Product { Id = new Guid("2559ce81-66f4-46f4-a924-8254fd188889"), Name = "Head Prestige MP", Description = "Classic control racquet", Price = 209.00M, ImageUrl = "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=HPRMPR-1.jpg&nw=43" },
                new Product { Id = new Guid("3a4a7aec-8119-4700-bd81-b22f79089ec1"), Name = "Wilson Blade 98", Description = "Modern feel", Price = 199.00M, ImageUrl = "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=981619-1.jpg&nw=43" },
                new Product { Id = new Guid("fe9fee5a-5792-49d4-a146-09e6961b1863"), Name = "Babolat Pure Aero", Description = "Spin friendly racquet", Price = 169.00M, ImageUrl = "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=BPAR-1.jpg&nw=43" }
            );
        }

    }
}
