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
    
    public partial class Movie
    {
        public Movie()
        {
            this.CheckOuts = new HashSet<CheckOut>();
            this.ReturnMovies = new HashSet<ReturnMovie>();
        }
    
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
    
        public virtual ICollection<CheckOut> CheckOuts { get; set; }
        public virtual ICollection<ReturnMovie> ReturnMovies { get; set; }
    }
}