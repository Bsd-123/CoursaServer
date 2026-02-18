namespace Service.Dto
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPercentages { get; set; }
        public double Value { get; set; }
        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }
        public CourseDto? Course { get; set; }

    }
}
