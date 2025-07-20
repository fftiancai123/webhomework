using System.Web.Mvc;
using WonderlandAdventure.Models;

namespace WonderlandAdventure.Controllers
{
    public class HotelsController : Controller
    {
        public ActionResult Index()
        {
            var hotels = new[]
            {
                new Hotel {
                    Id = 1,
                    Name = "Enchanted Castle Hotel",
                    Location = "0.2 miles from park entrance",
                    Description = "Live like royalty in our fairy-tale castle hotel featuring turret suites, a moat pool, and nightly knight shows.",
                    PriceRange = "$250 - $400/night",
                    ImageUrl = "/Content/images/hotel1.jpg",
                    Amenities = new[] { "Free WiFi", "Pool", "5 Restaurants", "Spa", "Kids Club" }
                },
                new Hotel {
                    Id = 2,
                    Name = "Jungle Explorer Lodge",
                    Location = "0.5 miles from park entrance",
                    Description = "Adventure awaits at our safari-themed lodge featuring exotic animal encounters, canopy walkways between buildings.",
                    PriceRange = "$180 - $300/night",
                    ImageUrl = "/Content/images/hotel2.jpg",
                    Amenities = new[] { "Free WiFi", "Water Park", "3 Restaurants", "Nature Trails", "Animal Viewing" }
                },
                new Hotel {
                    Id = 3,
                    Name = "Pirate's Cove Resort",
                    Location = "0.3 miles from park entrance",
                    Description = "Set sail for adventure at our pirate-themed waterfront resort featuring ship-shaped buildings.",
                    PriceRange = "$220 - $350/night",
                    ImageUrl = "/Content/images/hotel3.jpg",
                    Amenities = new[] { "Free WiFi", "Pirate Ship Pool", "4 Restaurants", "Private Beach", "Pirate Shows" }
                }
            };

            return View(hotels);
        }

        public ActionResult Book(int id)
        {
            var hotel = GetHotelById(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }

            var model = new BookingViewModel
            {
                HotelId = hotel.Id,
                HotelName = hotel.Name,
                RoomTypes = new[]
                {
                    new RoomType { Id = 1, Name = "Standard Room", BasePrice = 150 },
                    new RoomType { Id = 2, Name = "Deluxe Room", BasePrice = 220 },
                    new RoomType { Id = 3, Name = "Suite", BasePrice = 350 }
                }
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Book(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 这里应该处理预订逻辑，保存到数据库等
                return RedirectToAction("Confirmation", new { id = model.HotelId });
            }

            // 如果模型验证失败，重新加载房间类型
            model.RoomTypes = new[]
            {
                new RoomType { Id = 1, Name = "Standard Room", BasePrice = 150 },
                new RoomType { Id = 2, Name = "Deluxe Room", BasePrice = 220 },
                new RoomType { Id = 3, Name = "Suite", BasePrice = 350 }
            };

            return View(model);
        }

        public ActionResult Confirmation(int id)
        {
            var hotel = GetHotelById(id);
            return View(hotel);
        }

        private Hotel GetHotelById(int id)
        {
            // 这里应该是从数据库获取，简化示例直接硬编码
            return new Hotel
            {
                Id = id,
                Name = id == 1 ? "Enchanted Castle Hotel" :
                       id == 2 ? "Jungle Explorer Lodge" : "Pirate's Cove Resort",
                Location = "Near park entrance",
                PriceRange = id == 1 ? "$250 - $400/night" :
                            id == 2 ? "$180 - $300/night" : "$220 - $350/night"
            };
        }
    }
}