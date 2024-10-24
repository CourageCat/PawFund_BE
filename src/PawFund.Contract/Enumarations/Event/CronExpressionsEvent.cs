namespace PawFund.Contract.Enumarations.Event
{
    public static class CronExpressionsEvent
    {
        public const string EveryMinute = "* * * * *";  // Mỗi phút
        public const string Hourly = "0 * * * *"; // Mỗi giờ
        public const string DailyAtMidnight = "0 0 * * *"; // Mỗi ngày vào nửa đêm
    }
}
