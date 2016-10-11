using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SchoolManagementSystem.Domain.Entities;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class PayrollViewModel
    {
        public Payroll payroll { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}