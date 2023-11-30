using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class RemoteOffice{
        public int Id { get; set; }
        public bool Out0 { get; set; }
        public bool Out1 { get; set; }
        public bool Out2 { get; set; }
        public bool Out3 { get; set; }
        public bool Out4 { get; set; }
        public bool Out5 { get; set; }
        public bool Out6 { get; set; }
        public bool Out7 { get; set; }
    }
}