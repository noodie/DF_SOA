using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF_Model
{
public  class PayModel
    {
        private string fdbs;
        private string djbh;
        private string suburl;
        private string appid;
        private string appsecret;
        private string sign;
        private string detail;  //详情
        private int total_amount;//支付金额
        private string auth_code;  //用户付款码
        private string pay_type;//支付类型 0 微信，1支付宝
        private int discountable_amount;//优惠金额
        private string discount_coupon;//优惠券
        private string body;//商品名
        private string attach;//附加数据 回调返回
        private string nonce_str;//随机数 32位
        private string out_trade_no;//商户订单号
        private string version;//嗨付版本号
        private string openid;//嗨付中的用户openid

        public string Fdbs
        {
            get
            {
                return fdbs;
            }

            set
            {
                fdbs = value;
            }
        }

        public string Djbh
        {
            get
            {
                return djbh;
            }

            set
            {
                djbh = value;
            }
        }

        public string Detail
        {
            get
            {
                return detail;
            }

            set
            {
                detail = value;
            }
        }

        public double Total_amount
        {
            get
            {
                return total_amount;
            }

            set
            {
                total_amount = (int)value;
            }
        }

        public string Auth_code
        {
            get
            {
                return auth_code;
            }

            set
            {
                auth_code = value;
            }
        }

        public string Pay_type
        {
            get
            {
                return pay_type;
            }

            set
            {
                pay_type = value;
            }
        }

        public double Discountable_amount
        {
            get
            {
                return discountable_amount;
            }

            set
            {
                discountable_amount = (int)value;
            }
        }

        public string Discount_coupon
        {
            get
            {
                return discount_coupon;
            }

            set
            {
                discount_coupon = value;
            }
        }

        public string Body
        {
            get
            {
                return body;
            }

            set
            {
                body = value;
            }
        }

        public string Attach
        {
            get
            {
                return attach;
            }

            set
            {
                attach = value;
            }
        }

        public string Nonce_str
        {
            get
            {
                return nonce_str;
            }

            set
            {
                nonce_str = value;
            }
        }

        public string Out_trade_no
        {
            get
            {
                return out_trade_no;
            }

            set
            {
                out_trade_no = value;
            }
        }

        public string Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
            }
        }

        public string Openid
        {
            get
            {
                return openid;
            }

            set
            {
                openid = value;
            }
        }

        public string Appid
        {
            get
            {
                return appid;
            }

            set
            {
                appid = value;
            }
        }

        public string Sign
        {
            get
            {
                return sign;
            }

            set
            {
                sign = value;
            }
        }

        public string Appsecret
        {
            get
            {
                return appsecret;
            }

            set
            {
                appsecret = value;
            }
        }

        public string Suburl
        {
            get
            {
                return suburl;
            }

            set
            {
                suburl = value;
            }
        }
    }
}
