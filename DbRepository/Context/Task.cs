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
    
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.Task_Algotithm = new HashSet<Task_Algotithm>();
            this.Task_MethodRef = new HashSet<Task_MethodRef>();
            this.Task_Parametrs = new HashSet<Task_Parametrs>();
        }
    
        public int TaskId { get; set; }
        public int SubthemaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsReady { get; set; }
        public byte[] Image { get; set; }
    
        public virtual SubThema SubThema { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task_Algotithm> Task_Algotithm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task_MethodRef> Task_MethodRef { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task_Parametrs> Task_Parametrs { get; set; }
    }
}
