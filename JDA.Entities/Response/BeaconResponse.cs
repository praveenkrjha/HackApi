namespace JDA.Entities.Response
{
    public class BeaconResponse
    {
        public int BeaconId { get; set; }

        public string UUID { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }

        public string BeaconLocation { get; set; }
    }
}
