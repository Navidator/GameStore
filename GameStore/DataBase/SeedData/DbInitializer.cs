﻿using GameStore.Models;
using System;
using System.Linq;

namespace GameStore.DataBase.SeedData
{
    public static class DbInitializer
    {
        public static void Initialize(GameStoreContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Games.Count() > 0)
            {
                return;   // DB has been seeded
            }

            var games = new GameModel[]
            {
                new GameModel{Name="Witcher 1", Description="Witcher 1 game", GameDeveloper="CD PROJEKT RED", Publisher="CD PROJEKT RED", ReleaseDate=DateTime.Parse("2008-09-16"), Price=10},
                new GameModel{Name="Witcher 2", Description="Witcher 2 game", GameDeveloper="CD PROJEKT RED", Publisher="CD PROJEKT RED", ReleaseDate=DateTime.Parse("2011-05-17"), Price=15},
                new GameModel{Name="Witcher 3", Description="Witcher 3 game", GameDeveloper="CD PROJEKT RED", Publisher="CD PROJEKT RED", ReleaseDate=DateTime.Parse("2015-05-18"), Price=20}
            };
            foreach (GameModel g in games)
            {
                context.Games.Add(g);
            }
            context.SaveChanges();



            var currency = new CurrencyModel[]
            {
                new CurrencyModel{CurrencyName="GEL"},
                new CurrencyModel{CurrencyName="USD"}
            };
            foreach (CurrencyModel c in currency)
            {
                context.Currency.Add(c);
            }
            context.SaveChanges();




            var users = new UserModel[]
{
            new UserModel{UserName="DavitJ1",FirstName="Daviti",LastName="Janjalia", Email = "D@gmail.com",Country="Georgia"},
            new UserModel{UserName="DavitA1",FirstName="Davti",LastName="Abesalashvili", Email = "Davit@gmail.com",Country="Georgia"},
            new UserModel{UserName="LevaniS1",FirstName="Levani",LastName="Shengelia" , Email = "Levani@gmail.com",Country="Georgia"},
            new UserModel{UserName="Gelsona5",FirstName="Gela",LastName="Samsonadze", Email = "gelasamsona@gmail.com",Country="Georgia"},
            new UserModel{UserName="Pavlovichi",FirstName="Petre",LastName="Xachapuridze",Email="xachapura@yahoo.com",Country="Georgia"},
            new UserModel{UserName="Zouraba22",FirstName="Salome",LastName="Zurabishvili",Email="prezidenta@gmail.com",Country="Georgia"},
            new UserModel{UserName="DWill",FirstName="Luffy",LastName="Monkey D",Email="Luffytaro@OnePiece.com",Country="Goa Kingdom"}
};
            foreach (UserModel c in users)
            {
                context.Users.Add(c);
            }
            context.SaveChanges();
        }
    }
}
