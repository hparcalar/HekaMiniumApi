using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class CalendarElement{
        public int Id { get; set; }
        public string CalendarId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string DragBgColor { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool? IsImportant { get; set; }
        public string Location { get; set; }
        public bool? IsReadOnly { get; set; }
        public bool? IsAllDay { get; set; }
        public bool? IsPrivate { get; set; }
        public string Body { get; set; }
        public string State { get; set; }
    }
}