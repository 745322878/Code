using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Guid Id { get; set; }
        public int ErrorTimes { get; set; }
    }
}
