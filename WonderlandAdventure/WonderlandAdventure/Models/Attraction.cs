namespace WonderlandAdventure.Models
{
    public class Attraction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Description { get; set; }
        public int MinHeight { get; set; }
        public string Duration { get; set; }
        public int ThrillLevel { get; set; }
        public string ImageUrl { get; set; }
        public string[] Details { get; set; }
    }

    public class AttractionSchedule
    {
        public int AttractionId { get; set; }
        public string AttractionName { get; set; }
        public int CurrentWaitTime { get; set; }
        public string OperatingHours { get; set; }
        public bool FastPassAvailable { get; set; }
    }

    public class TheaterSchedule
    {
        public string AttractionName { get; set; }
        public string Location { get; set; }
        public string[] ShowTimes { get; set; }
        public int Capacity { get; set; }
        public bool RequiresTicket { get; set; }
    }
}