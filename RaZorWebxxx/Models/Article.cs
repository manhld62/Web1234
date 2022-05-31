using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace RaZorWebxxx.Models
{
    public class Article
    {
        [Key]
        public int ID { set; get; }
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Maximum 30 characters")]
        [Required(ErrorMessage ="{0} phải nhập")]
        [Column(TypeName ="nvarchar")]
        [DisplayName("Tiêu Đề")]
        public string Title { set; get; }
        [DisplayName("Ngày tạo")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Created { set; get; }
        [Column(TypeName ="ntext")]
        [DisplayName("Nội Dung ")]
        public string Content { set; get; }

    }
}
