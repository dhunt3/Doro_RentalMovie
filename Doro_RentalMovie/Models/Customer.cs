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
    
    public partial class Customer
    {
        public Customer()
        {
            this.CheckOuts = new HashSet<CheckOut>();
        }
    
        public int CustomerID { get; set; }
        public string Last_Name { get; set; }
        public string First_Name { get; set; }
        public string Phone_Number { get; set; }
    
        public virtual ICollection<CheckOut> CheckOuts { get; set; }
    }
}
