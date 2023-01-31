using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class ItemDtoFilter
    {
        public string CodeContains { get; set; }
        public string NameContains { get; set; }
        public string ColorName { get; set; }
    }
}
