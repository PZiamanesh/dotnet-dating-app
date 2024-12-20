namespace API.Extensions
{
    public static class DateTimeExtentions
    {
        public static int CalculateAge(this DateOnly date)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - date.Year;

            if (date > today.AddDays(-age))
            {
                age--;
            }

            return age;
        }
    }
}
