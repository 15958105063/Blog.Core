using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Model
{
    public class Products
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0}是必填项")]
        [MinLength(2, ErrorMessage = "{0}的最小长度是{1}")]
        [MaxLength(10, ErrorMessage = "{0}的长度不可以超过{1}")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "{0}的长度应该不小于{2}, 不大于{1}")]
        [Display(Name = "产品名称")]//引用System.ComponentModel.DataAnnotations
        public string Name { get; set; }


        [Display(Name = "价格")]
        [Range(0, double.MaxValue, ErrorMessage = "{0}的值必须大于{1}")]
        public float? Price { get; set; }

    }
}