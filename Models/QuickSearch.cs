using System.ComponentModel.DataAnnotations;
using System.Globalization;





namespace DimondDating

{

    public class QuickSearch

    {

        public int id { get; set; }

        public string familyname { get; set; }

        public string address1 { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zip { get; set; }

        public string homephone { get; set; }

    }

}