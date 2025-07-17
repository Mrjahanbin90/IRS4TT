using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRS4TT.Domains
{
    public class CoverGroup
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ClassOfBusiness { get; set; }
        public string TypeOrCover { get; set; }
        public string MultiOrSingle { get; set; }

        [ValidateNever] // این پراپرتی ولید نشه
        public ICollection<Cover> Covers { get; set; }

    }

    public class Cover
    {
        [Key]
        public int Id { get; set; }
        public string TitleEn { get; set; }
        public string TitleFa { get; set; }
        public int GroupId { get; set; }

        [ValidateNever] // این پراپرتی ولید نشه
        [ForeignKey("GroupId")]
        public virtual CoverGroup Group { get; set; }
    }
}
