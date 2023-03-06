using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HekaMiniumApi.Context{
    public class ExpenseCard{
    public int Id { get; set; }
    public int ExpenseCode { get; set; }
    public string ExpenseName { get; set; }
    }
}