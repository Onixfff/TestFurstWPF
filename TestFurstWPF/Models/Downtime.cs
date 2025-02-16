﻿namespace TestFurstWPF
{
    public class Downtime
    {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        public DateTime TimestampEnd { get; set; }

        public TimeOnly Difference { get; set; }

        public int? IdIdle { get; set; }

        public string? Comment { get; set; }

        public string? Recept { get; set; }

        public bool? IsUpdate { get; set; }

        public virtual Ididle? IdIdleNavigation { get; set; }

        public virtual Recepttime? ReceptNavigation { get; set; }
    }

}
