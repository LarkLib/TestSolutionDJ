namespace TestMinimalAPIWebApplication
{
    class Todo
    {
        public int Id { get; set; }
        public string GatewayId { get; set; }
        public string FaceNo { get; set; }
        public string FaceName { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Ctime { get; set; }

        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
