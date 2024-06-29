using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppCoreTargilME.Common.QueryData
{
    public class NQuery
    {
        [Required]
        [Range(0, 20, ErrorMessage = "Can only be between 0 .. 20")]
        public int N { get; set; }
    }
}
