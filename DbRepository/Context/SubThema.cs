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

    public partial class SubThema
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubThema()
        {
            this.Tasks = new HashSet<Task>();
        }
    
        public int SubthemaId { get; set; }
        public int ThemaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual Thema Thema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
