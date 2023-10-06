namespace ConnectionResiliency.Models
{
    public static class Outputs
    {
        public static string Success = "İşlem Başarılı";
        public static string FailtureRetryLimited = "Hata, vt bağlanılamadı"; //rabbitmq calıscak
        public static string Failture = "Hata";
    }
}
