using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Xml.Serialization;

namespace ConsoleApp2.Library
{
    public class Car
    {
        [XmlIgnore]
        private DateTime date;

        public virtual string DateString
        {
            get => date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            set => date = DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        [MinLength(1)]
        public virtual string BrandName { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public virtual int Price { get; set; }
    }
}
