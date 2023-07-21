namespace FastPizza.Service.Commons.Helper
{
    public class TimeHelper
    {
        public static DateTime GetDateTime()
        {
            var time = DateTime.UtcNow;
            //time = time.AddHours(TimeConstan.UTC);
            return time;
        }
    }
}
