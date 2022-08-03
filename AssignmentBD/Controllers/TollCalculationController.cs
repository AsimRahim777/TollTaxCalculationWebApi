using AssignmentBD.Common;
using AssignmentBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AssignmentBD.Controllers
{
    [AllowAnonymous]
    public class TollCalculationController : ApiController
    {
        private IDictionary<int, string> EntryPoints = new Dictionary<int, string>();
        private List<ViewTollModel> Tolls = new List<ViewTollModel>();
        CommonUtitlity CommonUtitlity = new CommonUtitlity();
        readonly private int BaseRate = 20;
        readonly private float DistanceCharges = 0.2f; // 0.2 rupees per KM
        readonly private float WeekendCharges = 1.5f; // 1.5x on weekends (Sat/Sun)
        public TollCalculationController()
        {
            EntryPoints = CommonUtitlity.LoadEntryPoints();
        }

        [HttpPost]
        public IHttpActionResult PostEntry(TollModel tollModel)
        {
            EntryResponse response = new EntryResponse();

            try
            {      
                
            if (ModelState.IsValid)
            {
                Guid newId = Guid.NewGuid();
                //Here have to make database operation - for the time being i just add entry into a list but you cand do here code for database as well
                Tolls.Add(new ViewTollModel
                {
                    GuidId = newId,
                    NumberPlate = tollModel.NumberPlate,
                    DateTime = tollModel.DateTime,
                    InterChange = tollModel.InterChange
                });


                if (Tolls.Count > 0) //Its mean that Entry successfully added into db
                {
                    //Making Response upon successfull entry
                    response.HttpStatus = HttpStatusCode.OK;
                    response.Message = "Record save successfully";
                    response.ResponseData = Tolls.SingleOrDefault();
                }
                else
                {
                    //Making Response For facing any issue while saving record into database
                    response.HttpStatus = HttpStatusCode.BadRequest;
                    response.Message = "Getting some error while saving data";
                    response.ResponseData = null;
                }

            }
            else
            {
                response.HttpStatus = HttpStatusCode.BadRequest;
                response.Message = "Please provide required data";
            }
            }
            catch (Exception ex)
            {
                response.HttpStatus = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return Json(response);
        }

        [HttpGet]
        public IHttpActionResult GetExitTax(TollModel tollModel)
        {
            //tollModel.DateTime =new DateTime(2022,8,14);
            ExitResponse response = new ExitResponse();
            ViewExitResponse viewExit = new ViewExitResponse();
            string CurrentDay = string.Empty;

            try
            {
          
            if (ModelState.IsValid)
            {
                tollModel.InterChange= tollModel.InterChange.ToUpper();
                CurrentDay = tollModel.DateTime.Date.DayOfWeek.ToString();
                int InterChangeRate = EntryPoints.Where(x => x.Value.Equals(tollModel.InterChange)).FirstOrDefault().Key;
                double DistanceKMCharges = 0;

                if (InterChangeRate >= 0)
                {
                    DistanceKMCharges = InterChangeRate * DistanceCharges; //i.e. 29KM * 0.2
                    //if its national day
                    if (CurrentDay.ToUpper().Equals("SUNDAY") || CurrentDay.ToUpper().Equals("SATURDAY") || CurrentDay.ToUpper().Equals("FRIDAY"))
                    {
                        if (!CurrentDay.ToUpper().Equals("FRIDAY"))
                        {
                            DistanceKMCharges *= WeekendCharges; //i.e. 29KM * 0.2 * 1.5 for weekend - if Friday then no weekend charges will be applied, remaining is the same process
                        }

                            if (CommonUtitlity.IsNationalHolidayDiscount(tollModel.DateTime.Date.ToString("MMMM"), tollModel.DateTime.Date.Day))
                        {
                            viewExit.DistanceCost = DistanceKMCharges; 
                            viewExit.BaseRate = BaseRate;

                            viewExit.SubTotal = BaseRate + viewExit.DistanceCost; // 20 * DistanceCost
                            viewExit.Discount = (0.5) * viewExit.SubTotal;  //calculate 50% discount on national day

                            viewExit.TotalCharges = viewExit.SubTotal - viewExit.Discount; //Total charges - 50% discount
                        }
                        else //if its weekend and no national day
                        {
                            viewExit.DistanceCost = DistanceKMCharges; // 0.2 * 1.5 
                            viewExit.BaseRate = BaseRate;

                            viewExit.SubTotal = BaseRate + viewExit.DistanceCost; // 20 * DistanceCost
                            viewExit.Discount = 0;

                            viewExit.TotalCharges = viewExit.SubTotal; //Total charges
                        }



                    }
                    else if (CurrentDay.ToUpper().Equals("MONDAY") || CurrentDay.ToUpper().Equals("WEDNESDAY"))
                    {                        

                        if (CommonUtitlity.IsNationalHolidayDiscount(tollModel.DateTime.Date.ToString("MMMM"), tollModel.DateTime.Date.Day))
                        {
                            viewExit.DistanceCost = DistanceKMCharges;
                            viewExit.BaseRate = BaseRate;

                            viewExit.SubTotal = BaseRate + viewExit.DistanceCost; // 20 * DistanceCost                           
                            if (CommonUtitlity.EvenOrOdd(tollModel.NumberPlate).Equals("even"))
                            {
                                viewExit.Discount = (0.6) * viewExit.SubTotal;  //calculate 50% discount on national day + 10% for even number
                            }
                            else
                            {
                                viewExit.Discount = (0.5) * viewExit.SubTotal; //calculate 50% discount on national day
                            }

                            viewExit.TotalCharges = viewExit.SubTotal - viewExit.Discount; //Total charges - 50% discount
                        }
                        else //if its weekend and not national day
                        {
                            viewExit.DistanceCost = DistanceKMCharges;
                            viewExit.BaseRate = BaseRate;

                            viewExit.SubTotal = BaseRate + viewExit.DistanceCost; // 20 * DistanceCost
                            if (CommonUtitlity.EvenOrOdd(tollModel.NumberPlate).Equals("even"))
                            {
                                viewExit.Discount = (0.1) * viewExit.SubTotal;  //10% for even number
                            }
                            else
                            {
                                viewExit.Discount = 0;
                            }

                            viewExit.TotalCharges = viewExit.SubTotal- viewExit.Discount; //Total charges
                        }

                    }
                    else if (CurrentDay.ToUpper().Equals("TUESDAY") || CurrentDay.ToUpper().Equals("THURSDAY"))
                    {
                        if (CommonUtitlity.IsNationalHolidayDiscount(tollModel.DateTime.Date.ToString("MMMM"), tollModel.DateTime.Date.Day))
                        {
                            viewExit.DistanceCost = DistanceKMCharges;
                            viewExit.BaseRate = BaseRate;

                            viewExit.SubTotal = BaseRate + viewExit.DistanceCost; // 20 * DistanceCost                           
                            if (CommonUtitlity.EvenOrOdd(tollModel.NumberPlate).Equals("odd"))
                            {
                                viewExit.Discount = (0.6) * viewExit.SubTotal;  //calculate 50% discount on national day + 10% for odd number
                            }
                            else
                            {
                                viewExit.Discount = (0.5) * viewExit.SubTotal; //calculate 50% discount on national day
                            }

                            viewExit.TotalCharges = viewExit.SubTotal - viewExit.Discount; //Total charges - 50% discount
                        }
                        else //if its weekend and not national day
                        {
                            viewExit.DistanceCost = DistanceKMCharges;
                            viewExit.BaseRate = BaseRate;

                            viewExit.SubTotal = BaseRate + viewExit.DistanceCost; // 20 * DistanceCost
                            if (CommonUtitlity.EvenOrOdd(tollModel.NumberPlate).Equals("odd"))
                            {
                                viewExit.Discount = (0.1) * viewExit.SubTotal;  //10% for odd number
                            }
                            else
                            {
                                viewExit.Discount = 0;
                            }

                            viewExit.TotalCharges = viewExit.SubTotal- viewExit.Discount; //Total charges
                        }

                    }

                    //Just round up the values
                    viewExit = new ViewExitResponse()
                    {
                        BaseRate = viewExit.BaseRate,
                        DistanceCost = Math.Round(viewExit.DistanceCost, 2),
                        Discount = Math.Round(viewExit.Discount, 2),
                        SubTotal = Math.Round(viewExit.SubTotal, 2),
                        TotalCharges = Math.Round(viewExit.TotalCharges, 2)
                    };
                    //successfull response
                    response.HttpStatus = HttpStatusCode.OK;
                    response.Message = "Tax Calculated";                   
                    response.ResponseData = viewExit;
                }
                else
                {
                    response.HttpStatus = HttpStatusCode.NoContent;
                    response.Message = "Please provide required data";
                }
            }
            else
            {
                response.HttpStatus = HttpStatusCode.BadRequest;
                response.Message = "Please provide required data";
            }
            }
            catch (Exception ex)
            {
                response.HttpStatus = HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }
            return Json(response);
        }


    }
}
