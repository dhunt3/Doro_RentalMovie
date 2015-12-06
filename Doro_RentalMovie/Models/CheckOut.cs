//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Doro_RentalMovie.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class CheckOut
    {
        public CheckOut()
        {
            this.Price = 2.99m;
        }

        public int CheckOutID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public int CustomerID { get; set; }
        public int MovieID { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> Checkout_Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> Return_Date
        {
            get
            {
                return Checkout_Date.Value.AddDays(3);
            }
            set { }
        }

        public Nullable<int> Days_Late
        {
            get
            {
                DateTime today = DateTime.Now.Date;
                TimeSpan amountOfDays = today.Subtract(Return_Date.Value);
                return Convert.ToInt32(amountOfDays.TotalDays);
            }

            set { }

        }
        public Nullable<decimal> Late_Fees
        {
            get
            {
                return Days_Late * Price;
            }
            set { }
        }

        public virtual Customer Customer { get; set; }
        public virtual Movie Movie { get; set; }

        /*public IList<Movies> MovieList{get;set;}
        public IList<SelectListItem> PriceListSelectListItem
        {
            get
            {
                   var list = (from item in MovieList
                            select new SelectListItem()
                            {
                                Text = item.customerID.ToString(CultureInfo.InvariantCulture),
                                Value = item.selectedCustomer.ToString(CultureInfo.InvariantCulture)
                            }).ToList();
                return list;
            }
            set{}
        } */

    }
}


