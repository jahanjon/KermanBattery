using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.Farmework.Core
{
    public  class ResultInfo
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ResultInfo(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public static ResultInfo OperationSuccess => new ResultInfo(200, "عملیات با موفقیت انجام شد");
        public static ResultInfo LoginSuccess => new ResultInfo(201, "ورود موفقیت‌آمیز بود");
        public static ResultInfo ConfirmationCodeGenerated => new ResultInfo(202, "کد تأیید تولید شد");
        public static ResultInfo PhoneNumberFetched => new ResultInfo(203, "شماره تلفن با موفقیت واکشی شد");
        public static ResultInfo PasswordChanged => new ResultInfo(204, "تغییر رمز عبور با موفقیت انجام شد");
        public static ResultInfo LogoutSuccess => new ResultInfo(205, "خروج با موفقیت انجام شد");
        public static ResultInfo SuccessUpdate => new ResultInfo(204, "ویرایش با موفقیت انجام شد");
        public static ResultInfo BadRequest => new ResultInfo(400, "لطفا ورودی خود را بررسی کنید.");
        public static ResultInfo Unauthorized => new ResultInfo(401, "شما مجاز به انجام این عمل نیستید.");
        public static ResultInfo NotFound => new ResultInfo(404, "منبع درخواستی یافت نشد.");
        public static ResultInfo InternalServerError => new ResultInfo(500, "خطایی در سرور رخ داده است. لطفاً بعداً دوباره امتحان کنید.");
        public static ResultInfo IncorrectEmailOrPassword => new ResultInfo(402, "ایمیل یا رمز عبور اشتباه است");
        public static ResultInfo UserInactive => new ResultInfo(408, "کاربر غیرفعال است. لطفاً با پشتیبانی تماس بگیرید.");
        public static ResultInfo OperationFailed => new ResultInfo(409, "عملیات با مشکل مواجه شده است. لطفاً دوباره امتحان کنید");
        public static ResultInfo UserPhoneNumberNotFound => new ResultInfo(410, "کاربری با این شماره تلفن یافت نشد");
        public static ResultInfo InvalidOrExpiredConfirmationCode => new ResultInfo(411, "کد تأیید نامعتبر یا منقضی شده است");
        public static ResultInfo SellerNotFound => new ResultInfo(412, "نماینده وجود ندارد,لطفا با پشتیبانی تماس بگیرید");
        public static ResultInfo FailedUpdate => new ResultInfo(413, "ویرایش انجام نشد لطفا مجدد تلاش کنید");


    }

}
