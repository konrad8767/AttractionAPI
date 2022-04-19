using AttractionAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace AttractionAPI
{
    public class AttractionSeeder
    {
        private readonly AttractionDbContext _dbContext;
        public AttractionSeeder(AttractionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Attractions.Any())
                {
                    var attractions = GetAttractions();
                    _dbContext.Attractions.AddRange(attractions);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Attraction> GetAttractions()
        {
            var attractions = new List<Attraction>()
            {
                new Attraction()
            {
                Name = "Copernicus Science Centre",
                Rate = 4.6,
                TotalRates = 40782,
                Description = "Copernicus Science Centre (Centrum Nauki Kopernik) in Warsaw - a science center located at Wybrzeże Kościuszkowskie Street 20 in Warsaw, whose goal is to develop science, cooperation with scientists and teachers, and according to the mission of the institution also: inspire to observe, experience, ask questions and seek answers. The Centre has been open to the public since 5 November 2010.",
                Category = "Science Museum",
                Address = new Address()
                {
                    City = "Warsaw",
                    Street = "Wybrzeże Kościuszkowskie 20",
                    PostalCode = "00-390"
                },
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Content = "Nice place. Really liked it!",
                        User = new User()
                        {
                            Name = "hellokitty888"
                        },
                        UserName = "hellokitty888"
                    },
                    new Comment()
                    {
                        Content = "Very boring place, wouldn't recommend.",
                        User = new User()
                        {
                            Name = "kapitanBomba12"
                        },
                        UserName = "kapitanBomba12"
                    }
                }
            },

                new Attraction()
            {
                Name = "The National Museum in Warsaw",
                Rate = 4.6,
                TotalRates = 13331,
                Description = "The National Museum in Warsaw - an art museum in Warsaw, founded in 1862 as the Museum of Fine Arts in Warsaw, a national cultural institution; one of the largest museums in Poland and the largest in Warsaw. The National Museum in Warsaw houses collections of ancient art, Polish painting from the thirteenth century, as well as a gallery of foreign paintings, including several paintings from Adolf Hitler's private collection, donated to the museum by the American authorities in post-war Germany, numismatic and handicraft collections. Since 1916, the museum has been a municipal institution. The modernistic building was erected between 1927 and 1938 and designed by Tadeusz Tolwinski and Antoni Dygat; since 1933 the east wing has been occupied by the Museum of the Polish Army in Warsaw. As a result of the German plunder of Polish cultural property during World War II, the National Museum in Warsaw lost a huge part of its holdings, including 99% of numismatic items, 100% of clocks, 80% of goldsmiths' and jewellers' wares, 63% of textiles, 60% of furniture, 70% of bindings, books and royal superscripts. In 1945 the museum was nationalized.",
                Category = "Museum",
                AddressId = 2,
                Address = new Address()
                {
                    City = "Warsaw",
                    Street = "al. Jerozolimskie 3",
                    PostalCode = "00-495"
                },
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Content = "❤️ So big, so many artworks, everything is really interesting and beautifull",
                        User = new User()
                        {
                            Name = "dinoguy18"
                        },
                        UserName = "dinoguy18"
                    },
                    new Comment()
                    {
                        Content = "Excellent medieval works and unique Nubian wall paintings.",
                        User = new User()
                        {
                            Name = "aManHasFallenIntoTheRiverInLegoCity"
                        },
                        UserName = "aManHasFallenIntoTheRiverInLegoCity"
                    },
                    new Comment()
                    {
                        Content = "On Tuesday is for free, awesome place and awesome art.",
                        User = new User()
                        {
                            Name = "marvelIsBoring2001"
                        },
                        UserName = "marvelIsBoring2001"
                    }
                }
            }

            };

            return attractions;
        }
    }
}
