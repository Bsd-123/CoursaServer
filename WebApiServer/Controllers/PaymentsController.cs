using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Service.Dto;
using Service.Interfaces;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

//using Stripe.Checkout;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    // acct_1T4ouP2Mxms28khM
    private string endpointSecret = "";
    private readonly IService<EnrollmentDto> service1;
    private readonly IConfiguration config;
    public PaymentsController(IService<OwnerDto> serviceOwner, IService<EnrollmentDto> service, IConfiguration config)
    {   
        this.service1 = service;
        this.config = config;
        this.endpointSecret = config["Stripe:EndpointSecret"];
    }

    [HttpPost("buy")]
    
    public async Task<ActionResult> Create([FromBody] CourseDto course)
    {
        Repository.Entities.Coupon couponOwner = null;
        Repository.Entities.Coupon couponManager = null;
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
        var owner =  course.Owner;
        if (owner == null)
            return NotFound("Owner not found");
        string sellerStripeId = owner.PaymentNumber;
        //צריך לכתוב את הפעולה הזו
        //var finalPrice = CalculteFinalPrice(course.Id,coupon);
        var options = new SessionCreateOptions
        {
            Metadata = new Dictionary<string, string>
            {
            { "courseId", course.Id.ToString() },
            { "userId",  currentUserId.ToString()},
            { "couponOwnerId",couponOwner==null? "null": couponOwner.Id.ToString() },
            { "couponManagerId",couponManager==null? "null": couponManager.Id.ToString() }
            },
            LineItems = new List<SessionLineItemOptions> {
                new SessionLineItemOptions
            {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmount = (long)(course.Price * 100.0),
                Currency = "ils",  // מטבע
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = course.Name, // השם שיופיע ללקוח בדף התשלום
                },
            },
            Quantity = 1, // כמות
        },
     },
            Mode = "payment",
            PaymentIntentData = new SessionPaymentIntentDataOptions
            {
            ApplicationFeeAmount = course.Owner.PaymentNumber == sellerStripeId ? null:(long)((1 - ((owner.Percentage) / 100.0)) * (course.Price * 100.0)),
            
            TransferData = course.Owner.PaymentNumber == sellerStripeId? null:new SessionPaymentIntentDataTransferDataOptions
                {
                    Destination = sellerStripeId,

                },
            },

            SuccessUrl = ("http://localhost:5173/courseView/"+course.Id),
            CancelUrl = "http://localhost:5173/home"
        };
        try
        {
            var service = new SessionService();
            var session = service.Create(options);
            return Ok(new { url = session.Url });
        }
        catch (StripeException e)
        {
            // הדפסת השגיאה ללוגים של השרת
            Console.WriteLine("Stripe ENo API key provided. Set your API key using `var client = new Stripe.StripeClient('').You can generate API keys from the Stripe Dashboard. See https://stripe.com/docs/api/authentication for details or contact support at https://support.stripe.com/email if you have any questions.'rror: {e.Message}");

            // החזרת שגיאה ברורה ל-Frontend כדי שתוכל להציג אותה למשתמש
            return BadRequest(new { error = e.Message });
        }
        catch (Exception e)
        {
            // תפיסת שגיאות כלליות (לא של Stripe)
            Console.WriteLine($"General Error: {e.Message}");
            return StatusCode(500, "An unexpected error occurred.");
        }

    }
    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            // אימות החתימה של Stripe כדי לוודא שהבקשה אכן הגיעה מהם
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], endpointSecret);

            // בדיקה האם התשלום עבר בהצלחה
            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;
               //if ((await service1.GetAll()).Any(r => r.PaymentNumber == int.Parse(session.PaymentIntentId)))
               // {
               //     return Ok(); // כבר עשינו את זה, הכל טוב
               // }
                session.Metadata.TryGetValue("courseId", out string courseIdStr);
                session.Metadata.TryGetValue("userId", out string userIdStr);
                session.Metadata.TryGetValue("couponId", out string couponIdStr);
                int courseId = int.Parse(courseIdStr);
                int userId = int.Parse(userIdStr);
                EnrollmentDto enrollment = new EnrollmentDto
                {
                    Id = 0,
                    CourseId = courseId,
                    UserId = userId,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(3), // לדוגמה, תקופת גישה של חודש
                    Status = true,
                    FullPrice = (double)(session.AmountTotal / 100.0), // המרה לשקלים
                    CouponOwnerId = int.Parse(couponIdStr),
                    CouponManagerId = int.Parse(couponIdStr),

                    PaymentNumber = session.PaymentIntentId, // שמירת מזהה התשלום של Stripe
                    ReceptionNumber = -1 // ניתן לעדכן מאוחר יותר עם מספר קבלה אמיתי אם יש צו
                };
                 var en = await service1.AddItem(enrollment);
                return Ok(en.Id);
            }
            return Ok();
        }
        catch (StripeException)
        {
            return BadRequest();
        }
    }
}