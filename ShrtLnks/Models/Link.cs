using System;
using System.ComponentModel.DataAnnotations;

namespace ShrtLnks.Models
{
    public class Link
    {
        public int LinkId { get; set; }
        public string OwnerId { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreateAt { get; set; }
    }
}
