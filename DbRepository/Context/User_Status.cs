//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DbRepository.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Status
    {
        public int Id_User { get; set; }
        public string Permission { get; set; }
        public string Subgroup { get; set; }
        public string Description { get; set; }
    
        public virtual User User { get; set; }
    }
}