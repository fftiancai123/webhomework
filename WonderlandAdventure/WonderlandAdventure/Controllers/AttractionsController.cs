using System;
using System.Web.Mvc;
using WonderlandAdventure.Models;

namespace WonderlandAdventure.Controllers
{
    public class AttractionsController : Controller
    {
        public ActionResult Index()
        {
            var attractions = new[]
            {
                new Attraction {
                    Id = 1,
                    Name = "Dragon's Fury Coaster",
                    Zone = "Fantasy Kingdom Zone",
                    Description = "Our newest and most extreme roller coaster featuring a 200-foot vertical drop, 5 inversions, and speeds up to 80mph!",
                    MinHeight = 54,
                    Duration = "2:30",
                    ThrillLevel = 90,
                    ImageUrl = "/Content/images/a1.jpg",
                    Details = new[] { "Speed: 80mph", "Drop: 200ft", "5 inversions" }
                },
                new Attraction {
                    Id = 2,
                    Name = "Splash Mountain Adventure",
                    Zone = "Jungle River Zone",
                    Description = "Journey through mysterious caves and ancient ruins before plunging down a 50-foot waterfall.",
                    MinHeight = 42,
                    Duration = "5:00",
                    ThrillLevel = 70,
                    ImageUrl = "/Content/images/a2.jpg",
                    Details = new[] { "Water Ride", "Drop: 50ft", "Family Friendly" }
                },
                new Attraction {
                    Id = 3,
                    Name = "Fairy Tale Carousel",
                    Zone = "Storybook Village Zone",
                    Description = "A magical carousel featuring 60 hand-carved horses and chariots.",
                    MinHeight = 0,
                    Duration = "3:00",
                    ThrillLevel = 20,
                    ImageUrl = "/Content/images/a3.jpg",
                    Details = new[] { "All Ages", "60 Animals", "Live Music" }
                },
                new Attraction {
                    Id = 4,
                    Name = "Space Ranger Spin",
                    Zone = "Galaxy Frontier Zone",
                    Description = "Interactive dark ride where you control your spinning vehicle while zapping aliens with laser blasters.",
                    MinHeight = 36,
                    Duration = "4:30",
                    ThrillLevel = 50,
                    ImageUrl = "/Content/images/a4.jpg",
                    Details = new[] { "Interactive", "2-4 per vehicle", "Scoring System" }
                },
                new Attraction {
                    Id = 5,
                    Name = "Haunted Mansion",
                    Zone = "Mystery Manor Zone",
                    Description = "Classic dark ride through a spooky mansion filled with 999 happy haunts.",
                    MinHeight = 0,
                    Duration = "7:00",
                    ThrillLevel = 40,
                    ImageUrl = "/Content/images/a5.jpg",
                    Details = new[] { "Dark Ride", "Special Effects", "Family Friendly" }
                }
            };

            return View(attractions);
        }

        public ActionResult Schedule(int id)
        {
            var attraction = GetAttractionById(id);
            if (attraction == null)
            {
                return HttpNotFound();
            }

            // 生成戏剧表演的排班表（如果是大剧院）
            if (id == 6) // 假设大剧院的ID是6
            {
                var schedule = new TheaterSchedule
                {
                    AttractionName = "Wonder Theater",
                    Location = "Grand Theater Zone",
                    ShowTimes = GenerateTheaterSchedule(),
                    Capacity = 145,
                    RequiresTicket = true
                };
                return View("TheaterSchedule", schedule);
            }

            // 普通景点的等待时间
            var model = new AttractionSchedule
            {
                AttractionId = attraction.Id,
                AttractionName = attraction.Name,
                CurrentWaitTime = new Random().Next(5, 120), // 随机生成等待时间
                OperatingHours = "9:00 AM - 5:00 PM",
                FastPassAvailable = true
            };

            return View(model);
        }

        private string[] GenerateTheaterSchedule()
        {
            var showTimes = new System.Collections.Generic.List<string>();
            var currentTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0);
            var endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0);

            while (currentTime <= endTime)
            {
                showTimes.Add(currentTime.ToString("h:mm tt"));
                currentTime = currentTime.AddMinutes(45); // 30分钟表演 + 15分钟休息
            }

            return showTimes.ToArray();
        }

        private Attraction GetAttractionById(int id)
        {
            // 这里应该是从数据库获取，简化示例直接硬编码
            return new Attraction
            {
                Id = id,
                Name = id == 1 ? "Dragon's Fury Coaster" :
                       id == 2 ? "Splash Mountain Adventure" :
                       id == 3 ? "Fairy Tale Carousel" :
                       id == 4 ? "Space Ranger Spin" : "Haunted Mansion",
                Zone = "Various Zones",
                MinHeight = id == 1 ? 54 : id == 2 ? 42 : id == 4 ? 36 : 0
            };
        }
    }
}