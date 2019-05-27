using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LogEsay.Models
{
    public class InstanceLogger
    {
        [Key]
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public DateTime CreatedDateInst { get; set; }
        public DateTime DisposedDateInst { get; set; }
        public string InstanceName { get; set; }
        public string AddressMac { get; set; }
        public string LocalIP { get; set; }
        public string PublicIP { get; set; }
        public string Stopwatch { get; set; }

    }
}