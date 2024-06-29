using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAppCoreTargilME.Common.BaseModel
{
    public record PaginationModel
    {
        [Range(1, long.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public long? PageNumber { get; set; } = 1;

        [Required(ErrorMessage = "PageSize is required.")]
        [Range(1, 100, ErrorMessage = "Please enter a value bigger than {1}")]
        public int? PageSize { get; set; }
    }

    public record PaginationModel<T> : PaginationModel
    {
        public long? TotalPages { get; set; }
        public long? TotalRecords { get; set; }
        public T? DataPage { get; set; }
    }
    //    public class InsertModel
    //    {
    //        [Display(...)]
    //        public virtual string ID { get; set; }

    //    ...Other properties
    //}

    //    public class UpdateModel : InsertModel
    //    {
    //        [Required]
    //        public override string ID
    //        {
    //            get { return base.ID; }
    //            set { base.ID = value; }
    //        }
    //    }

}
