namespace EFCore.BlazorApp1.Data.Quotes
{
    public class QuotesModel
    {
        public List<Quote> quotes { get; set; }
    }

    public class Quote  
    {
        public int id { get; set; }
        public string quote { get; set; }
        public string author { get; set; }
    }

}
