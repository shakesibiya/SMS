using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Models.PaypalSetUp;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Domain;

namespace SchoolManagementSystem.Controllers
{
    public class PayPalController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();
        private DbSchoolContext db = new DbSchoolContext();

        public static PaymentViewModel paymentModel { get; set; }
        public static APIContext paypalContext { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentWithCreditCard()
        {
            ViewBag.SubjectId = new SelectList(db.Disciplines, "Id", "Subject");
            return View();
        }

        [HttpPost]
        public ActionResult PaymentWithCreditCard(PaymentViewModel paymentViewModel)
        {
            ViewBag.SubjectId = new SelectList(db.Disciplines, "Id", "Subject", paymentViewModel.subjectId);

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            paymentViewModel.studentPin = currUser.Login;

            //create and item for which you are taking payment
            //if you need to add more items in the list
            //Then you will need to create multiple item objects or use some loop to instantiate object
            Item item = new Item();
            item.name = "Subject Fees";
            item.currency = "USD";
            item.price = paymentViewModel.price;
            item.quantity = "1";
            item.sku = "sku";
            
            //Now make a List of Item and add the above item to it
            //you can create as many items as you want and add to this list
            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;

            //Address for the payment
            Address billingAddress = new Address();
            billingAddress.city = paymentViewModel.city;
            billingAddress.country_code = paymentViewModel.countryCode.ToUpper();
            billingAddress.line1 = paymentViewModel.line1;
            billingAddress.postal_code = paymentViewModel.postalCode;
            billingAddress.state = paymentViewModel.state;

            //Now Create an object of credit card and add above details to it
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = paymentViewModel.cvv;
            crdtCard.expire_month = paymentViewModel.expireMonth;
            crdtCard.expire_year = paymentViewModel.expireYear;
            crdtCard.first_name = paymentViewModel.firstName;
            crdtCard.last_name = paymentViewModel.lastName;
            crdtCard.number = paymentViewModel.creditCardNo;
            crdtCard.type = paymentViewModel.creditCardType.ToLower();

            // Specify details of your payment amount.
            Details details = new Details();
            details.shipping = "0";
            details.subtotal = paymentViewModel.price;
            details.tax = Convert.ToString(Convert.ToDecimal(paymentViewModel.price) * Convert.ToDecimal(0.14));

            // Specify your total payment amount and assign the details object
            Amount amnt = new Amount();
            amnt.currency = "USD";
            // Total = shipping tax + subtotal.
            amnt.total = Convert.ToString(Convert.ToDecimal(details.subtotal) + Convert.ToDecimal(details.tax));
            amnt.details = details;

            paymentViewModel.total = amnt.total;

            int count = repository.Payments.Where(p => p.Student_PIN == currUser.Login).Count();

            // Now make a trasaction object and assign the Amount object
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "Transaction made directly with credit card.";
            tran.item_list = itemList;
            tran.invoice_number = paymentViewModel.studentPin + "APPA0" + count + 1;

            // Now, we have to make a list of trasaction and add the trasactions object
            // to this list. You can create one or more object as per your requirements

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // Now we need to specify the FundingInstrument of the Payer
            // for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of FundingIntrument

            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // Now create Payer object and assign the fundinginstrument list to the object
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            paymentViewModel.paymentType = payr.payment_method;
            
            // finally create the payment object and assign the payer object & transaction list to it
            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {
                //getting context from the paypal, basically we are sending the clientID and clientSecret key in this function 
                //to the get the context from the paypal API to make the payment for which we have created the object above.

                //Code for the configuration class is provided next

                // Basically, apiContext has a accesstoken which is sent by the paypal to authenticate the payment to facilitator account. An access token could be an alphanumeric string

                APIContext apiContext = Models.PaypalSetUp.Configuration.GetAPIContext();

                // Create is a Payment class function which actually sends the payment details to the paypal API for the payment. The function is passed with the ApiContext which we received above.

                Payment createdPayment = pymnt.Create(apiContext);

                //if the createdPayment.State is "approved" it means the payment was successfull else not

                if (createdPayment.state.ToLower() != "approved")
                {
                    return View("FailureView");
                }
                SavePayment(paymentViewModel);
            }
            catch (PayPal.PayPalException ex)
            {
                Logger.Log("Error: " + ex.Message);
                return View("FailureView");
            }
            
            return RedirectToAction("Index", "Payment");
        }

        [HttpGet]
        public ActionResult PaymentWithPaypal()
        {
            ViewBag.SubjectId = new SelectList(db.Disciplines, "Id", "Subject");
            return View();
        }

        [HttpPost]
        public ActionResult PaymentWithPaypal(PaymentViewModel paymentViewModel)
        {
            ViewBag.SubjectId = new SelectList(db.Disciplines, "Id", "Subject", paymentViewModel.subjectId);

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            paymentViewModel.studentPin = currUser.Login;

            //getting the apiContext as earlier
            APIContext apiContext = Models.PaypalSetUp.Configuration.GetAPIContext();
            paypalContext = apiContext;
            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist

                    //it is returned by the create function call of the payment class

                    // Creating a payment

                    // baseURL is the url on which paypal sendsback the data.

                    // So we have provided URL of this controller only

                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaypalPayment?";
                    //guid we are generating for storing the paymentID received in session

                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url

                    //on which payer is redirected for paypal acccount payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, paymentViewModel);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                    SavePayment(paymentViewModel);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error" + ex.Message);
                return View("FailureView");
            }

            return RedirectToAction("Index", "Payment");
        }

        public ActionResult PaypalPayment()
        {
            try
            {
                string payerId = Request.Params["PayerID"];

                if (!string.IsNullOrEmpty(payerId))
                {
                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(paypalContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                    SavePayment(paymentModel);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error" + ex.Message);
                return View("FailureView");
            }

            return RedirectToAction("Index", "Payment");
        }

        private Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, PaymentViewModel paymentViewModel)
        {

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Subject Fees",
                currency = "USD",
                price = paymentViewModel.price,
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            paymentViewModel.paymentType = payer.payment_method;
            
            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = Convert.ToString(Convert.ToDecimal(paymentViewModel.price) * Convert.ToDecimal(0.14)),
                shipping = "0",
                subtotal = paymentViewModel.price
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = Convert.ToString(Convert.ToDecimal(details.subtotal) + Convert.ToDecimal(details.tax)),
                details = details
            };

            paymentViewModel.total = amount.total;
            int count = repository.Payments.Where(p => p.Student_PIN == paymentViewModel.studentPin).Count();

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction made with paypal account.",
                invoice_number = paymentViewModel.studentPin + "APPA0" + count + 1,
                amount = amount,
                item_list = itemList
            });
            
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            
            paymentModel = paymentViewModel;

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        private ActionResult SavePayment(PaymentViewModel paymentViewModel)
        {
            var ct = new Domain.Entities.Payment();
            ct.Value = Convert.ToDecimal(paymentViewModel.total);
            ct.Type = paymentViewModel.paymentType;
            ct.Date = DateTime.Now;
            ct.Discipline_Id = paymentViewModel.subjectId;
            ct.Student_PIN = paymentViewModel.studentPin;
            ct.StudentID = repository.Students.FirstOrDefault(x => x.PIN == paymentViewModel.studentPin).StudentID;
            db.Payments.Add(ct);
            db.SaveChanges();

            return null;
        }
    }
}