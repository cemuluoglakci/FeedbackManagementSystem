namespace CoreFMS.Entities
{
    public partial class System
    {
        public int Id { get; set; }
        public string SystemVariable { get; set; } = string.Empty;
        public int Value { get; set; }
    }
}
