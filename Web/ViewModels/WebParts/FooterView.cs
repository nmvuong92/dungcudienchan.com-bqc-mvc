using VD.Data.Entity;

namespace Web.ViewModels.WebParts
{
    public class FooterView
    {
        public Statistic Statistic { get; set; }

        public string copyright { get; set; }
        public string address { get; set; }
        public string tel { get; set; }
        public string mail { get; set; }
        public string design_by { get; set; }
        public string link_facebook { get; set; }
        public string link_linkedin { get; set; }
    }
}