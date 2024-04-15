namespace CLIQ_UE.Helpers
{
    public static class FormatTimeForChat
    {
       public static string CalculateLastSeen(string personTime)
        {
            // Parse the person's time string into a DateTime object
            DateTime personDateTime = DateTime.ParseExact(personTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            // Get today's date without the time component
            DateTime today = DateTime.Today;

            // Get the time difference
            TimeSpan timeDifference = DateTime.Now - personDateTime;

            // Check if the date difference is zero
            if (personDateTime.Date == today)
            {
                // If the time difference is zero, return "online"
                if (timeDifference.TotalMinutes <= 0)
                {
                    return "Online";
                }
                // If the time difference is greater than zero, return the specified time without a date
                else
                {
                    return "Today " + personDateTime.ToString("hh:mm tt");
                }
            }
            // Check if the date difference is one (yesterday)
            else if (personDateTime.Date == today.AddDays(-1))
            {
                // Return "yesterday" with the time without the date
                return "Yesterday " + personDateTime.ToString("hh:mm tt");
            }
            // If the difference is greater than one, return the given date as it is
            else
            {
                return personDateTime.ToString("yyyy-MM-dd hh:mm tt");
            }
        }
        public static string CalculateLastSeenForUserList(string personTime)
        {
            // Parse the person's time string into a DateTime object
            DateTime personDateTime = DateTime.ParseExact(personTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);

            // Get today's date without the time component
            DateTime today = DateTime.Today;

            // Get the time difference
            TimeSpan timeDifference = DateTime.Now - personDateTime;

            // Check if the date difference is zero
            if (personDateTime.Date == today)
            {
                    return personDateTime.ToString("hh:mm");
                
            }
            // Check if the date difference is one (yesterday)
            else if (personDateTime.Date == today.AddDays(-1))
            {
                // Return "yesterday" with the time without the date
                return "Yesterday";
            }
            // If the difference is greater than one, return the given date as it is
            else
            {
                return personDateTime.ToString("yyyy-MM-dd");
            }
        }
    }
}
