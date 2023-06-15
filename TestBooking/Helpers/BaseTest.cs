using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBooking.Helpers;

public class BaseTest
{
    protected DbContextHelper _dbHelper = new();
}
