using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WonderlandAdventure.Controllers
{
    public class TicketsController : Controller
    {
        // GET: Tickets
        public ActionResult Index()
        {
            // 设置默认日期为明天
            ViewBag.DefaultDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessOrder(TicketOrder order)
        {
            if (!ModelState.IsValid)
            {
                // 如果有验证错误，返回表单并显示错误
                ViewBag.DefaultDate = order.VisitDate.ToString("yyyy-MM-dd");
                return View("Index", order);
            }

            // 处理订单逻辑
            order.TotalAmount = CalculateTotal(order.TicketType, order.Quantity);

            // 存储订单信息以便在支付页面显示
            TempData["Order"] = order;

            return RedirectToAction("Payment");
        }

        public ActionResult Payment()
        {
            var order = TempData["Order"] as TicketOrder;
            if (order == null)
            {
                return RedirectToAction("Index");
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompletePayment(PaymentModel payment)
        {
            if (!ModelState.IsValid)
            {
                return View("Payment", payment);
            }

            // 处理支付逻辑
            // 这里可以保存支付信息到数据库等

            return RedirectToAction("Confirmation");
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        private decimal CalculateTotal(string ticketType, int quantity)
        {
            decimal price;

            switch (ticketType)
            {
                case "standard":
                    price = 300m;
                    break;
                case "discount":
                    price = 150m;
                    break;
                case "show":
                    price = 60m;
                    break;
                default:
                    price = 0m;
                    break;
            }

            return price * quantity;
        }
    }

    public class TicketOrder
    {
        [Required(ErrorMessage = "Please select a ticket type")]
        public string TicketType { get; set; }

        [Required(ErrorMessage = "Please enter quantity")]
        [Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please select a visit date")]
        public DateTime VisitDate { get; set; }

        [Required(ErrorMessage = "Please select a payment method")]
        public string PaymentMethod { get; set; }

        public string SpecialRequests { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class PaymentModel
    {
        [Required(ErrorMessage = "Please select a payment option")]
        public string PaymentOption { get; set; }

        // 信用卡支付字段
        [CreditCard(ErrorMessage = "Invalid credit card number")]
        public string CardNumber { get; set; }

        public string ExpiryDate { get; set; }

        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV must be 3 or 4 digits")]
        public string Cvv { get; set; }

        public string CardName { get; set; }
    }
}